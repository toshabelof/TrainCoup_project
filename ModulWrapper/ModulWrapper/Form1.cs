using Alturos.Yolo;
using OpenCvSharp;
using OpenCvSharp.UserInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace ModulWrapper
{
    
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        Thread neuroThread; // Пробуем распоточить
        NeuroNetwork netw;

        public void Form1_Load(object sender, EventArgs e)
        {
            toolStripTimer.Text = "Elapsed time: n/a";
            toolStripDetectionSystem.Text = "Detection System: n/a";
        }

        // Выбор файла
        private void btn_filePick_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Получаем путь
                    filePath = openFileDialog.FileName;
                }
                if (filePath != "")
                {
                    tBox_path.Text = filePath;
                    try
                    {
                        // Подгружаем первое изображение видео ( первый кадр )
                        var cap = VideoCapture.FromFile(filePath);
                        var img = new Mat();

                        cap.Read(img);

                        picBox.ImageIpl = img;

                        //picBox.Image = Image.FromFile(filePath);
                        Console.WriteLine("File has " + cap.FrameCount + " frames!");
                        cap.Dispose();
                        img.Dispose();
                    }
                    catch { }

                }
            }
        }

        public void NeuroNet()
        {
            netw = new NeuroNetwork(this);
            netw.StartAnalyzing();
        }
        // Запуск нейронки (создание потока в частности)
        public void btn_Detect_Click(object sender, EventArgs e)
        {
            try{ 
                neuroThread.Abort();
            }
            catch { }
            if (tBox_path.Text.Length > 0 && tBox_path.Text != "Press 'File' Button")
            {
                neuroThread = new Thread(NeuroNet);
                neuroThread.IsBackground = true;
                neuroThread.Start();
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Utilities.debugmessage("Playing state: " + neuroThread.ThreadState.ToString());
                if (neuroThread.ThreadState.HasFlag(System.Threading.ThreadState.Suspended))
                {
                    pauseButton.Text = "PAUSE";
                    neuroThread.Resume();
                }
                else
                {
                    pauseButton.Text = "RESUME";
                    neuroThread.Suspend();

                }
            }
            catch { }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                netw.PLAY_FLAG = false;
            }
            catch { }
        }
    }

}
