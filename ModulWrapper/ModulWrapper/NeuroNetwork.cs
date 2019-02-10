using Alturos.Yolo;
using Alturos.Yolo.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ModulWrapper
{
    class NeuroNetwork
    {

        // Класс для хранения текущего кадра
        private class CFrame
        {
            public Mat Frame = new Mat(); // Само изображение в формате OpenCV
            public int frameNum; // Номер кадра
        }

        private Thread yoloThread;
        private YoloWrapper yoloWrapper;
        static bool analyzeStarted = false;
        private CFrame cframe = new CFrame();
        private Form1 window;
        public bool PLAY_FLAG = true;
        private VideoCapture cap = new VideoCapture();
        private int w = Utilities.YOLO_DETECTOR_WIDTH, h = Utilities.YOLO_DETECTOR_HEIGHT;
        int CoupCount = 0;
        int[] masTrackDcoup = { };
        private int frameCnt = 0;
        //bool vector = false; //для направления
        //bool vectorInRight = false; //для направления - тип слева на право

        public NeuroNetwork(Form1 f1)
        {
            this.window = f1;
            Init();
        }

        private void Init()
        {
            var watch = new Stopwatch();

            watch.Start();
            yoloWrapper = new YoloWrapper(Utilities.YOLO_CONFIG, Utilities.YOLO_WEIGHTS, Utilities.YOLO_NAMES);
            watch.Stop();

            // Настроечки
            var DetectSys = yoloWrapper.DetectionSystem.ToString();
            window.toolStripDetectionSystem.Text = "Detection System: " + DetectSys;
            window.toolStripTimer.Text = "Yolo loaded in " + watch.ElapsedMilliseconds/1000 + " seconds";


            watch = null;
            GC.Collect();
        }


        // Функция анализа
        private void AnalyzeVideo()
        {

            //Utilities.debugmessage("Analyze thread Started");
            var newImage = new Mat();
            var newFrame = new CFrame();

            newFrame = cframe;
            newImage = newFrame.Frame;
            newImage = newImage.CvtColor(ColorConversionCodes.BGR2GRAY);
            var st = new Stopwatch();
            st.Start();
            List<YoloItem> items = yoloWrapper.Detect(newImage.Resize(new Size(w, h)).ToBytes()).ToList();
            st.Stop();

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

                
                if (itm.Type == "dcoup")
                {
                    
                    string[] _toAdd = { newFrame.frameNum.ToString(), (itm.X * coeffW).ToString(), (itm.Y * coeffH).ToString(), (itm.Width * coeffW + (itm.Y * coeffH)).ToString(), (itm.Height * coeffH + (itm.X * coeffW)).ToString() };

                    if (masTrackDcoup.Length != 0)
                    {
                        if (Math.Abs(masTrackDcoup[0] - itm.X) < 50)
                        {
                            masTrackDcoup[0] = itm.X;
                            window.dataGridView1.Invoke(new Action(() => window.dataGridView1.Rows.Add(_toAdd)));
                        }
                        else
                        {
                            masTrackDcoup[0] = itm.X;
                            CoupCount++;
                        }
                    }
                    else
                    {
                        window.dataGridView1.Invoke(new Action(() => window.dataGridView1.Rows.Add(_toAdd)));
                        masTrackDcoup = new int[1] { itm.X };
                        CoupCount++;
                    }
                }
            }


            window.toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";
            window.toolStripCounter.Text = "Count: " + CoupCount;

            window.picBox.ImageIpl = newImage.Resize(new OpenCvSharp.Size(window.picBox.Width, window.picBox.Height)); // Рисуем новый кадр
            window.picBox.setRect(items);

            if(cframe.frameNum + 1 > frameCnt)
                PLAY_FLAG = false;

            
            analyzeStarted = false;
        }

        public void StartAnalyzing()
        {
            Utilities.debugmessage("Neuro thread Started");
            

            cap = VideoCapture.FromFile(window.tBox_path.Text);

            cap.Read(cframe.Frame);
            cframe.frameNum++;

            DateTime timeDelta;
            int frameTime = (int)(1000 / cap.Fps);
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

                // Отрисовываем кадры нормально (риалтайм)
                window.picBoxSmall.ImageIpl = cframe.Frame.Resize(new OpenCvSharp.Size(window.picBoxSmall.Width, window.picBoxSmall.Height));
              
                // Чистим память после каждого кадра, а то насрёт намана так
                if ((DateTime.Now - timeDelta).Milliseconds < frameTime)
                {
                    Thread.Sleep(frameTime - (DateTime.Now - timeDelta).Milliseconds);
                    GC.Collect();

                    window.frameCnt.Invoke(new Action(() =>
                        window.frameCnt.Text = "Frames: " + cframe.frameNum + "/" + cap.FrameCount
                    ));

                    if (cframe.frameNum + 1 < frameCnt)
                    {
                        cap.Read(cframe.Frame);
                        cframe.frameNum++;
                    }
                }
              

            } while (!cframe.Frame.Empty() && PLAY_FLAG);

            window.btn_Detect.Invoke(new Action(() => {
                window.btn_Detect.Enabled = true;
                window.pauseButton.Enabled = false;
                window.dataGridView1.ScrollBars = ScrollBars.Vertical;
            }));
            masTrackDcoup = null;
            cap.Dispose();
            yoloWrapper.Dispose();
            Utilities.debugmessage("Neuro thread FINISHED");
            GC.Collect();
        }

    }
}
