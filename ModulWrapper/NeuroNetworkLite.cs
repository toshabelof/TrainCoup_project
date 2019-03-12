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
    class NeuroNetworkLite
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
        string[] _toAdd;

        public NeuroNetworkLite(Form1 f1)
        {
            this.window = f1;
            Init();
        }

        private void Init()
        {
            yoloWrapper = new YoloWrapper(Utilities.YOLO_CONFIG, Utilities.YOLO_WEIGHTS, Utilities.YOLO_NAMES);
           
            GC.Collect();
        }
        

        // Функция анализа
        private void AnalyzeVideo()
        {
            Utilities.debugmessage("Analyze thread Started");
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
            

            foreach (var itm in items)
            {
                if (itm.Type == "dcoup")
                {

                    _toAdd = new string[]{ newFrame.frameNum.ToString(), (itm.X * coeffW).ToString(), (itm.Y * coeffH).ToString(), (itm.Width * coeffW + (itm.Y * coeffH)).ToString(), (itm.Height * coeffH + (itm.X * coeffW)).ToString() };
                    
                    if (masTrackDcoup.Length != 0)
                    {
                        if (Math.Abs(masTrackDcoup[0] - itm.X) < 50)
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
            
            window.picBox.ImageIpl = newImage.Resize(new OpenCvSharp.Size(window.picBox.Width, window.picBox.Height)); // Рисуем новый кадр
            window.picBox.setRect(items);

            if (cframe.frameNum + 1 > frameCnt)
                PLAY_FLAG = false;


            analyzeStarted = false;
            window.START_FLAG = false;

            items.Clear();
            GC.Collect();
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
                    
                    if (cframe.frameNum + 1 < frameCnt)
                    {
                        cap.Read(cframe.Frame);
                        cframe.frameNum++;
                    }
                }


            } while (!cframe.Frame.Empty() && PLAY_FLAG);

            masTrackDcoup = null;
            cap.Dispose();
            yoloWrapper.Dispose();

            Utilities.debugmessage("Neuro thread FINISHED");
            GC.Collect();
        }

        public string[] getYoloItem()
        {
            return _toAdd;
        }

    }
}
