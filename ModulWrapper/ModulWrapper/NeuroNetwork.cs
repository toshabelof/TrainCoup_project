using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.OleDb;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using OpenCvSharp;
using ADOX;

namespace ModulWrapper
{
    class NeuroNetwork
    {
        // Класс для хранения текущего кадра
        public class CFrame
        {
            public Mat Frame = new Mat(); // Само изображение в формате OpenCV
            public int frameNum; // Номер кадра
        }

        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DatabaseNeuro.mdb;";   // БД
        private OleDbConnection myConnection;

        private Thread yoloThread;
        static bool analyzeStarted = false;
        public CFrame cframe = new CFrame();
        private Form1 window;
        public bool PLAY_FLAG = true;
        private VideoCapture cap = new VideoCapture();
        private int w = Utilities.YOLO_DETECTOR_WIDTH, h = Utilities.YOLO_DETECTOR_HEIGHT;
        public int CoupCount = 0;
        int[] masTrackDcoup = { };
        private int frameCnt = 0;
        private YoloWrapper yoloWrapper;
        private static CLAHE clh = Cv2.CreateCLAHE(1.5, new OpenCvSharp.Size(7,7));
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
            // Deprecated but still here
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();

            GC.Collect();
        }

        /*void CopyFile(string sourcefn, string destinfn)//ф-ция копирования файла
        {
            FileInfo fn = new FileInfo(sourcefn);
            fn.CopyTo(destinfn, true);
        }*/

        public void dataBaseLog(string[] toAdd) //База данных// КОД ДЛЯ ЛОГИРОВАНИЯ В БД
        {
            string x = "";
            
            string query = "INSERT INTO EASY (Frame, X1, Y1, X2, Y2, _TimeRow) VALUES ('" + toAdd[0] + "','" + toAdd[1] + "','" + toAdd[2] + "','" + toAdd[3] + "','" + toAdd[4] + "','" + toAdd[4] + "')";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // выполняем запрос к MS Access
            command.ExecuteNonQuery();
            
        }

        public void Log(string[] toAdd)
        {
            string x = "";
            string logFile = @"LOGS\" + Path.GetFileName(window.tBox_path.Text) + ".txt";
            using (StreamWriter sw = File.AppendText(logFile))
            {
                for (int i = 0; i < 4; i++)
                {
                    x += toAdd[i].Replace(',','.') + ',';
                }
                sw.WriteLine(x + toAdd[5]);
                sw.Close();
            }
        }

        // Функция анализа
        private void AnalyzeVideo()
        { 
            var newImage = new Mat();
            var newFrame = new CFrame();

            newFrame = cframe;
            newImage = newFrame.Frame;
            newImage = newImage.CvtColor(ColorConversionCodes.BGR2GRAY);
            clh.Apply(newImage, newImage);
            var st = new Stopwatch();
            st.Start();
            List<YoloItem> items = yoloWrapper.Detect(newImage.Resize(new OpenCvSharp.Size(w, h)).ToBytes()).ToList();
            
            var coeffW = ((float)newImage.Width / w);
            var coeffH = ((float)newImage.Height / h);

            //foreach (var l in items)
            //{
            //    if (masTrackDcoup.Length == 0)
            //    {
            //        if (l.Type == "dcoup")
            //        {
            //            masTrackDcoup = new int[1] { l.X };
            //            CoupCount++;
            //            //masTrackDcoup.Append(l.Y);
            //        }
            //    }
            //    else
            //    {
            //        if (vector == false) //определяли направление? Нет - определяем
            //        {
            //            if (masTrackDcoup[0] < l.X)
            //            {
            //                vectorInRight = true;
            //                vector = true;
            //            }
            //            else
            //            {
            //                vectorInRight = false;
            //                vector = true;
            //            }


            //        }

            //        //если определили, то проверяем координаты
            //        //если новая координата изменилась не больше чем на 30 пикседей, то перезаписываем массив и чекаем дальше
            //        if (vectorInRight == true && (l.X - masTrackDcoup[0]) > (0) || vectorInRight == false && (masTrackDcoup[0] - l.X) > (0))
            //        {
            //            masTrackDcoup[0] = l.X;
            //            //masTrackDcoup.Append(l.Y); 
            //        }
            //        else if (vectorInRight == true && (l.X - masTrackDcoup[0]) < (-100) || vectorInRight == false && (masTrackDcoup[0] - l.X) < (-100))
            //        {
            //            masTrackDcoup[0] = l.X;
            //            CoupCount++; //иначе +1 в счётчик
            //        }

            //        //if (l.Type == "ocoup") //встретили одинарную сцепку вконце
            //        //{
            //        //    CoupCount++;
            //        //}
            //    }

            //}

            foreach (var itm in items)
            {
          
                if (itm.Confidence < 0.66) { break; } // Защищаемся от ложных срабатываний на 95%
                if (itm.Type == "dcoup")
                {

                    // Logging, Tracking

                    TimeSpan curTime = TimeSpan.FromMilliseconds(newFrame.frameNum * frameTime);
                    string[] _toAdd = {
                        newFrame.frameNum.ToString(),
                        (itm.X * coeffW).ToString(),
                        (itm.Y * coeffH).ToString(),
                        ((itm.Width * coeffW) + (itm.Y * coeffH)).ToString(),
                        ((itm.Height * coeffH) + (itm.X * coeffW)).ToString(),
                        curTime.ToString(@"hh\:mm\:ss")
                    };

                    Log(_toAdd);

                    if (myConnection.State == System.Data.ConnectionState.Open)
                    {
                        try
                        {
                            dataBaseLog(_toAdd);
                        }
                        catch { }
                    }

                    ListViewItem item1 = new ListViewItem(newFrame.frameNum.ToString(), 0);
                    item1.SubItems.Add((itm.X * coeffW).ToString());
                    item1.SubItems.Add((itm.Y * coeffH).ToString());
                    item1.SubItems.Add((itm.Width * coeffW + (itm.Y * coeffH)).ToString());
                    item1.SubItems.Add((itm.Height * coeffH + (itm.X * coeffW)).ToString());
                    item1.SubItems.Add(curTime.ToString(@"hh\:mm\:ss"));
                   
                    window.listView1.BeginInvoke(new Action(() =>
                    {
                        window.listView1.Items.AddRange(new ListViewItem[] { item1 } ); 
                    }));

                    if (masTrackDcoup.Length != 0)
                    {
                        if (Math.Abs(masTrackDcoup[0] - itm.X) < 100)
                        {
                            masTrackDcoup[0] = itm.X;
                            
                        }
                        else
                        {
                            masTrackDcoup[0] = itm.X;
                            CoupCount++;
                        }
                    }
                    else
                    {
                        masTrackDcoup = new int[1] { itm.X };
                        CoupCount++;
                    }
                   
                }
            }
            st.Stop();

            window.toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";
            window.toolStripCounter.Text = "Count: " + CoupCount;

            window.picBox.BeginInvoke(new Action(() =>
            {
                window.picBox.ImageIpl = newImage.Resize(new OpenCvSharp.Size(window.picBox.Width, window.picBox.Height)); // Рисуем новый кадр
                window.picBox.setRect(items);
            }));
            if (cframe.frameNum + 1 > frameCnt)
            {
                PLAY_FLAG = false;
                window.Invoke(new Action(() =>
                {
                    window.pauseButton.Enabled = false;
                    window.btn_Detect.Enabled  = true;
                }));

            }
            
            analyzeStarted = false;
        }

        public void StartAnalyzing(int frameN, int cc)
        {
            
            Utilities.debugmessage("Neuro thread STARTED!");
            CoupCount = cc;
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
                    // Отрисовываем кадры нормально (риалтайм)
                    window.picBoxSmall.ImageIpl = cframe.Frame.Resize(new OpenCvSharp.Size(window.picBoxSmall.Width, window.picBoxSmall.Height));
                }));
                // Чистим память после каждого кадра, а то насрёт намана так
                if ((DateTime.Now - timeDelta).Milliseconds < frameTime)
                {
                    Thread.Sleep(frameTime - (DateTime.Now - timeDelta).Milliseconds);
                    GC.Collect();

                    window.frameCnt.Invoke(new Action(() => {
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
            
            try
            {
               
                masTrackDcoup = null;
                cap.Dispose();
                //window.dataGridView1.BeginInvoke(new Action(() =>
                //{
                //    window.dataGridView1.ScrollBars = ScrollBars.Vertical;
                //    window.dataGridView1.FirstDisplayedScrollingRowIndex = window.dataGridView1.RowCount - 1;
                //}));
                window.Invoke(new Action(() =>
                {
                    
                    
                    if (window.isPaused)
                        window.Text = "Train Coup Detector - Paused";
                    else
                        window.Text = "Train Coup Detector - Ready";
                }));


            }
            catch { }
            Utilities.debugmessage("Neuro thread FINISHED!");
            GC.Collect();
        }

        public int trPhoto = 0;
        public string nameFile;


        public void StartAnalyzingPhotos()
        {
            DateTime timeDelta;
            Utilities.debugmessage("Neuro thread STARTED!");

            foreach (var path in listPhotos)
            {
                Console.WriteLine(path);
                nameFile = path;

                cap = VideoCapture.FromFile(path);

                cap.Read(cframe.Frame);
                do
                {
                    timeDelta = DateTime.Now;
                    if (!analyzeStarted)
                    {
                        analyzeStarted = true;
                        yoloThread = new Thread(AnalyzePhoto);
                        yoloThread.IsBackground = true;
                        yoloThread.Start();
                    }
                    if ((DateTime.Now - timeDelta).Milliseconds < frameTime)
                    {
                        Thread.Sleep(frameTime - (DateTime.Now - timeDelta).Milliseconds);
                        GC.Collect();
                    }
                }
                while (!cframe.Frame.Empty() && PLAY_FLAG);

                cap.Dispose();
                trPhoto++;
                Utilities.debugmessage("Neuro thread FINISHED!");
                GC.Collect();
            }
        }

        private void AnalyzePhoto()
        {
            var newImage = new Mat();
            var newFrame = new CFrame();

            newFrame = cframe;
            newImage = newFrame.Frame;
            newImage = newImage.CvtColor(ColorConversionCodes.BGR2GRAY);
            clh.Apply(newImage, newImage);
            
            List<YoloItem> items = yoloWrapper.Detect(newImage.Resize(new OpenCvSharp.Size(w, h)).ToBytes()).ToList();

            var coeffW = ((float)newImage.Width / w);
            var coeffH = ((float)newImage.Height / h);

            foreach (var itm in items)
            {
                if (itm.Confidence < 0.66) { break; } // Защищаемся от ложных срабатываний на 95%

                float x = ((itm.X * coeffW) + ((itm.Width * coeffW) + (itm.Y * coeffH))) / 2;
                float y = ((itm.Y * coeffH) + ((itm.Height * coeffH) + (itm.X * coeffW))) / 2;

                float[] center = new float[] { x, y };
                float width = itm.Width * coeffW;
                float height = itm.Height * coeffH;

                InJson inJson = new InJson(nameFile, center, width, height, window.tBoxTreatmentPath.Text);
                inJson.CreateJsonFile();
            }

            if (cframe.frameNum + 1 > frameCnt)
            {
                PLAY_FLAG = false;
            }

            analyzeStarted = false;
        }
    }
}
