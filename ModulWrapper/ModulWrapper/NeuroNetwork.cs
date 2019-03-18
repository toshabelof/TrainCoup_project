using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Data.OleDb;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using OpenCvSharp;


namespace ModulWrapper
{
    class NeuroNetwork
    {
        // Frame class
        public class CFrame
        {
            public Mat Frame = new Mat(); // Frame in OpenCV format
            public int frameNum; // Frame number
        }

        //public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseNeuro.mdb;";   // БД
        //private OleDbConnection myConnection;

        private Thread yoloThread;
        static bool analyzeStarted = false;
        public CFrame cframe = new CFrame();
        private Form1 window;
        public bool PLAY_FLAG = true;
        private VideoCapture cap = new VideoCapture();
        private int w = Utilities.YOLO_DETECTOR_WIDTH, h = Utilities.YOLO_DETECTOR_HEIGHT;
        public int CoupCount = 0;
        public int masTrackDcoup = -999;
        private int frameCnt = 0;
        private YoloWrapper yoloWrapper;
        private static CLAHE clhLight = Cv2.CreateCLAHE();

        private static double checkTime = 0;

        private static int frameTime;
        public List<string> listPhotos = new List<string>();


        public NeuroNetwork(Form1 f1, YoloWrapper yolo, List<string> listPhotos)
        {
            this.yoloWrapper = yolo;
            this.window = f1;
            this.listPhotos = listPhotos;
            this.trPhoto = 0;

            Init();
        }

        private void Init()
        {

            //myConnection = new OleDbConnection(connectString);
            //myConnection.Open();

            GC.Collect();
        }

        /*void CopyFile(string sourcefn, string destinfn)//ф-ция копирования файла
        {
            FileInfo fn = new FileInfo(sourcefn);
            fn.CopyTo(destinfn, true);
        }*/

        //public void dataBaseLog(string[] toAdd) //База данных// КОД ДЛЯ ЛОГИРОВАНИЯ В БД
        //{

        //    string query = "INSERT INTO GAMEOVER (Frame, X1, Y1, X2, Y2, _TimeRow) VALUES ('" + toAdd[0] + "','" + toAdd[1] + "','" + toAdd[2] + "','" + toAdd[3] + "','" + toAdd[4] + "','" + toAdd[4] + "')";

        //    // создаем объект OleDbCommand для выполнения запроса к БД MS Access
        //    OleDbCommand command = new OleDbCommand(query, myConnection);

        //    // выполняем запрос к MS Access
        //    command.ExecuteNonQuery();

        //}

        public void Log(string[] toAdd)
        {
            string x = "";
            string logFile = @"LOGS\" + Path.GetFileName(window.tBox_path.Text) + ".txt";
            using (StreamWriter sw = File.AppendText(logFile))
            {
                for (int i = 0; i < 4; i++)
                {
                    x += toAdd[i].Replace(',', '.') + ',';
                }
                sw.WriteLine(x + toAdd[5]);
                sw.Close();
            }
        }

        /// <summary>
        /// Analyze video frame
        /// </summary>
        private void AnalyzeVideo()
        {
            var newImage = new Mat();
            var newFrame = new CFrame();

            newFrame = cframe;
            newImage = newFrame.Frame;
            newImage = newImage.CvtColor(ColorConversionCodes.BGR2GRAY);
            Utilities.debugmessage("Clahe: " + Cv2.Mean(newImage)[0]);
            clhLight.SetClipLimit(2);
            clhLight.Apply(newImage, newImage);
            var st = new Stopwatch();

            Utilities.debugmessage("Clahe: " + Cv2.Mean(newImage)[0]);
            st.Start();
            List<YoloItem> items = yoloWrapper.Detect(newImage.Resize(new OpenCvSharp.Size(w, h)).ToBytes()).ToList();

            var coeffW = ((float)newImage.Width / w);
            var coeffH = ((float)newImage.Height / h);


            foreach (var itm in items)
            {

                if (itm.Confidence < 0.66) { break; }
                if (itm.Type == "dcoup")
                {

                    // Logging, Tracking

                    TimeSpan curTime = TimeSpan.FromMilliseconds(newFrame.frameNum * frameTime);
                    
                    string[] _toAdd = {
                        newFrame.frameNum.ToString(),
                        (itm.X * coeffW).ToString(),
                        (itm.Y * coeffH).ToString(),
                        ((itm.X * coeffW) + (itm.Width * coeffH)).ToString(),
                        ((itm.Y * coeffH) + (itm.Height * coeffW)).ToString(),
                        curTime.ToString(@"hh\:mm\:ss")
                    };

                    Log(_toAdd);

                   
                    //if (myConnection.State == System.Data.ConnectionState.Open)
                    //{
                    //    try
                    //    {
                    //        dataBaseLog(_toAdd);
                    //    }
                    //    catch { }
                    //}

                    ListViewItem item1 = new ListViewItem(newFrame.frameNum.ToString(), 0);
                    item1.SubItems.Add((itm.X * coeffW).ToString());
                    item1.SubItems.Add((itm.Y * coeffH).ToString());
                    item1.SubItems.Add((itm.Width * coeffW + (itm.X * coeffW)).ToString());
                    item1.SubItems.Add((itm.Height * coeffH + (itm.Y * coeffH)).ToString());
                    item1.SubItems.Add(curTime.ToString(@"hh\:mm\:ss"));

                    window.listView1.BeginInvoke(new Action(() =>
                    {
                        window.listView1.Items.AddRange(new ListViewItem[] { item1 });
                    }));

                    // Tracking algorithm
                    // Speed limit ~80 KM/h (if lenght between coups is 12-15 meters)
                    if (((newFrame.frameNum * frameTime) > (checkTime + 650)) && Math.Abs((float) masTrackDcoup * coeffW - (float) itm.X * coeffW) < 30)
                    {
                        checkTime = (newFrame.frameNum * frameTime);
                        masTrackDcoup = itm.X;
                    }
                    else if (((newFrame.frameNum * frameTime) > (checkTime + 650)))
                    {
                        CoupCount++;
                        checkTime = (newFrame.frameNum * frameTime);
                        masTrackDcoup = itm.X;
                    }
                    else if(CoupCount == 0) {
                        CoupCount++;
                        checkTime = (newFrame.frameNum * frameTime);
                        masTrackDcoup = itm.X;
                    }
                    else {
                        checkTime = (newFrame.frameNum * frameTime);
                        masTrackDcoup = itm.X;
                    }
                }
            }
            st.Stop();

            window.toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";
            window.toolStripCounter.Text = "Count: " + CoupCount;

            // Drawing new frame in picBox
            window.picBox.BeginInvoke(new Action(() =>
            {
                window.picBox.ImageIpl = newImage.Resize(new OpenCvSharp.Size(window.picBox.Width, window.picBox.Height));
                window.picBox.setRect(items);
            }));
            if (cframe.frameNum + 1 > frameCnt)
            {
                PLAY_FLAG = false;
                window.Invoke(new Action(() =>
                {
                    window.pauseButton.Enabled = false;
                    window.btn_Detect.Enabled = true;
                }));

            }

            analyzeStarted = false;
        }

        public void StartAnalyzingVideo(int frameN, int cc, int coupCoord)
        {

            Utilities.debugmessage("Analyze Video Thread STARTED!");
            CoupCount = cc;
            masTrackDcoup = -999;

            masTrackDcoup = coupCoord;

            window.Invoke(new Action(() =>
            {
                window.Text = "Train Coup - Processing...";
            }));

            cap = VideoCapture.FromFile(window.tBox_path.Text);


            cap.Set(CaptureProperty.PosFrames, frameN);
            cframe.frameNum = frameN;
            cap.Read(cframe.Frame);
            cframe.frameNum++;



            DateTime timeDelta;
            frameTime = (int)(1000 / cap.Fps);
            TimeSpan maxTime = TimeSpan.FromMilliseconds(cap.FrameCount * frameTime);
            frameCnt = cap.FrameCount;
            do
            {
                timeDelta = DateTime.Now;
                if (!analyzeStarted)
                {
                    analyzeStarted = true;
                    yoloThread = new Thread(AnalyzeVideo);
                    yoloThread.IsBackground = true;
                    yoloThread.Start();
                }
                window.picBoxSmall.BeginInvoke(new Action(() =>
                {
                    // Realtime drawing
                    window.picBoxSmall.ImageIpl = cframe.Frame.Resize(new OpenCvSharp.Size(window.picBoxSmall.Width, window.picBoxSmall.Height));
                }));
                // Sleep thread for new frame + collect grabage
                if ((DateTime.Now - timeDelta).Milliseconds < frameTime)
                {
                    Thread.Sleep(frameTime - (DateTime.Now - timeDelta).Milliseconds);
                    GC.Collect();

                    window.frameCnt.Invoke(new Action(() =>
                    {
                        TimeSpan curTime = TimeSpan.FromMilliseconds(cframe.frameNum * frameTime);

                        window.frameCnt.Text = "Frames: " + cframe.frameNum + "/" + cap.FrameCount + "\nTime: " + curTime.ToString(@"hh\:mm\:ss") + " / " + maxTime.ToString(@"hh\:mm\:ss");
                    }));

                    if (cframe.frameNum + 1 < frameCnt)
                    {
                        cap.Read(cframe.Frame);
                        cframe.frameNum++;
                    }
                }

            } while (!cframe.Frame.Empty() && PLAY_FLAG);

            // Close Thread
            try
            {

                masTrackDcoup = -999;
                cap.Dispose();
                window.Invoke(new Action(() =>
                {
                    if (window.isPaused)
                        window.Text = "Train Coup Detector - Paused";
                    else
                        window.Text = "Train Coup Detector - Ready";
                }));


            }
            catch { }
            Utilities.debugmessage("Analyze Video Thread FINISHED!");
            GC.Collect();
        }

        public int trPhoto = 0;
        public string nameFile;
        private bool analyzePhotoStarted = false;

        public void StartAnalyzingPhotos(TreatmentPhotos treatmentPhotos)
        {
            var photoCount = listPhotos.Count;
            Utilities.debugmessage("Analyze Photo Thread STARTED!");

            do
            {
                if (!analyzePhotoStarted)
                {
                    analyzePhotoStarted = true;
                    var path = listPhotos[trPhoto];
                    nameFile = path;

                    cap = VideoCapture.FromFile(path);
                    Mat photoFrm = new Mat();
                    cap.Read(photoFrm);

                    AnalyzePhoto(photoFrm);

                    trPhoto++;
                    GC.Collect();

                    treatmentPhotos.Invoke(new Action(() =>
                    {
                        treatmentPhotos.lblStatus.Text = "Treatment photo...";
                        treatmentPhotos.lblCount.Text = "Photo: " + trPhoto + "/" + photoCount;
                    }));
                }
            } while (trPhoto < photoCount);
            Utilities.debugmessage("Analyze Photo Thread FINISHED!");
        }

        private void AnalyzePhoto(Mat photo)
        {

            var newImage = photo;

            newImage = newImage.CvtColor(ColorConversionCodes.BGR2GRAY);


            clhLight.Apply(newImage, newImage);


            List<YoloItem> items = yoloWrapper.Detect(newImage.Resize(new OpenCvSharp.Size(w, h)).ToBytes()).ToList();

            var coeffW = ((float)newImage.Width / w);
            var coeffH = ((float)newImage.Height / h);

            foreach (var itm in items)
            {
                if (itm.Confidence < 0.66) { break; }

                float x = ((itm.X * coeffW) + ((itm.Width * coeffW) / 2.0f));
                float y = ((itm.Y * coeffH) + ((itm.Height * coeffH) / 2.0f));

                float[] center = new float[] { x, y };
                float width = itm.Width * coeffW;
                float height = itm.Height * coeffH;



                InJson inJson = new InJson(nameFile, center, width, height, window.tBoxTreatmentPath.Text);
                inJson.CreateJsonFile();
            }

            analyzePhotoStarted = false;
        }
    }
}
