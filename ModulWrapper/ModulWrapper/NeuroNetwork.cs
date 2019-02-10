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
        public class CFrame
        {
            public Mat Frame = new Mat(); // Само изображение в формате OpenCV
            public int frameNum; // Номер кадра
        }

        Thread yoloThread;
        public YoloWrapper yoloWrapper;
        static bool analyzeStarted = false;
        CFrame cframe = new CFrame();
        Form1 window;
        public bool PLAY_FLAG = true;

        int CoupCount = 0;
        int[] masTrackDcoup = { };
        bool vector = false; //для направления
        bool vectorInRight = false; //для направления - тип слева на право

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
            window.toolStripDetectionSystem.Text = "Detection System: n/a";

            var DetectSys = yoloWrapper.DetectionSystem.ToString();
            window.toolStripDetectionSystem.Text = "Detection System: " + DetectSys;

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

            foreach (var l in items)
            { 
                if (masTrackDcoup.Length == 0)
                {
                    if (l.Type == "dcoup")
                    {
                        masTrackDcoup = new int[1] { l.X };
                        CoupCount++;
                        //masTrackDcoup.Append(l.Y);
                    }
                }
                else
                {
                    if (vector == false) //определяли направление? Нет - определяем
                    {
                        if (masTrackDcoup[0] < l.X)
                        {
                            vectorInRight = true;
                            vector = true;
                        }
                        else
                        {
                            vectorInRight = false;
                            vector = true;
                        }
                       

                    }

                    //если определили, то проверяем координаты
                    //если новая координата изменилась не больше чем на 30 пикседей, то перезаписываем массив и чекаем дальше
                    if (vectorInRight == true &&  (l.X - masTrackDcoup[0]) > (0) || vectorInRight == false && (masTrackDcoup[0] - l.X) > (0))
                    {
                        masTrackDcoup[0] = l.X;
                        //masTrackDcoup.Append(l.Y); 
                    }
                    else if(vectorInRight == true && (l.X - masTrackDcoup[0]) < (-100) || vectorInRight == false && (masTrackDcoup[0] - l.X) < (-100))
                    {
                        masTrackDcoup[0] = l.X;
                        CoupCount++; //иначе +1 в счётчик
                    }

                    if (l.Type == "ocoup") //встретили одинарную сцепку вконце
                    {
                        CoupCount++; 
                    }
                }
               
            }


            window.toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";
            window.toolStripCounter.Text = "Counter: " + CoupCount;

            window.picBox.setRect(items);

            window.picBox.ImageIpl = newImage; // Рисуем новый кадр

           
            analyzeStarted = false;
        }

        public void StartAnalyzing()
        {
            Utilities.debugmessage("Neuro thread Started");
            var cap = new VideoCapture();

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
                    cap.Read(cframe.Frame);
                    cframe.frameNum++;

                }

            } while (!cframe.Frame.Empty() && PLAY_FLAG);

            // Убиваем последний трэд если остался
            try
            {
                yoloThread.Abort();
            }
            catch { }
            yoloWrapper.Dispose();

            Utilities.debugmessage("Neuro thread FINISHED");
            GC.Collect();
        }

    }
}
