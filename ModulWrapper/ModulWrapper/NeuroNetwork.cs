﻿using Alturos.Yolo;
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
            window.toolStripTimer.Text = "Yolo loaded in " + watch.ElapsedMilliseconds*1000 + " seconds";


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

            var st = new Stopwatch();
            st.Start();
            List<YoloItem> items = yoloWrapper.Detect(newImage.ToBytes()).ToList();
            st.Stop();

            window.toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";

        
            window.picBox.setRect(items);

            window.picBox.ImageIpl = newImage; // Рисуем новый кадр
           

            st = null;
            items = null;
            newImage = null;
            newFrame = null;
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

                    cap.Read(cframe.Frame);
                    cframe.frameNum++;
                }


            } while (!cframe.Frame.Empty() && PLAY_FLAG);

            if(yoloThread != null)
            {
                yoloThread = null;
            }
            cap.Dispose();
            cap = null;
            cframe = null;
            yoloWrapper.Dispose();
            yoloWrapper = null;
            Utilities.debugmessage("Neuro thread FINISHED");
            GC.Collect();
        }

    }
}