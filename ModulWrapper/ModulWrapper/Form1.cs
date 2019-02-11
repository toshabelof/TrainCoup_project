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

        Thread neuroThread; // Главный поток программы
        NeuroNetwork netw;
       
        public YoloWrapper yoloWrapper;

        async private void LoadYolo()
        {
            await Task.Run(() => {

                this.Invoke(new Action(() => {
                    this.Text = "Train Coup - Loading...";
                    filePickBtn.Enabled = false;
                    btn_Detect.Enabled = false;
                    stopButton.Enabled = false;
                }));
                


                try
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    yoloWrapper = new YoloWrapper(Utilities.YOLO_CONFIG, Utilities.YOLO_WEIGHTS, Utilities.YOLO_NAMES);
                    watch.Stop();


                    var DetectSys = yoloWrapper.DetectionSystem.ToString();
                    toolStripDetectionSystem.Text = "Detection System: " + DetectSys;
                    toolStripTimer.Text = "Yolo loaded in " + watch.ElapsedMilliseconds / 1000.0 + " seconds";
                }
                catch(System.IO.FileNotFoundException)
                {
                    Utilities.showMsg("Configurations file(s) not found!", "Error!");
                    this.Invoke(new Action(() => this.Close()));
                } catch (System.NotSupportedException)
                {
                    Utilities.showMsg("Only 64-bit systems are supported!", "Error!");
                    this.Invoke(new Action(() => this.Close()));
                }


                this.Invoke(new Action(() => {
                    this.Text = "Train Coup - Ready";
                    filePickBtn.Enabled = true;
                    btn_Detect.Enabled = true;
                    stopButton.Enabled = true;
                }));
            });
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            LoadYolo();

            // Настроечки
            toolStripTimer.Text = "Elapsed time: n/a";
            toolStripDetectionSystem.Text = "Detection System: n/a";
            toolStripCounter.Text = "Count: n/a";


            pauseButton.Enabled = false;
        }

        // Выбор файла
        private void btn_filePick_Click(object sender, EventArgs e)
        {
            stopButton_Click(sender, e);
            
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Filter = ".avi files (*.avi)|*.avi|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Получаем путь
                    filePath = openFileDialog.FileName;
                }
                if (filePath != "")
                {
                    
                    try
                    {
                        
                        // Подгружаем первое изображение видео ( первый кадр )
                        var cap = VideoCapture.FromFile(filePath);
                        var img = new Mat();
                        cap.Read(img);
                        
                        picBox.ImageIpl = img.Resize(new OpenCvSharp.Size(picBox.Width, picBox.Height));

                        Utilities.debugmessage("File has " + cap.FrameCount + " frames!");
                        frameCnt.Text = "Frames: 0/" + cap.FrameCount;
                        cap.Dispose();
                        img.Dispose();

                        tBox_path.Text = filePath;
                        
                    }
                    catch {
                        Utilities.showMsg("This file cannot be loaded", "Error!");
                        frameCnt.Text = "Frames: 0/0";
                        picBox.ImageIpl = null;
                        picBoxSmall.ImageIpl = null;
                    }

                }
                filePath = null;
                picBox.clearBBoxes();
                GC.Collect();
            }
        }

        public void NeuroNet()
        {
            netw = new NeuroNetwork(this, yoloWrapper);
            netw.StartAnalyzing();
        }
        // Запуск нейронки (создание потока в частности)
        public void btn_Detect_Click(object sender, EventArgs e)
        {

            if (neuroThread != null)
            {
                netw.PLAY_FLAG = false;
            }
            if (tBox_path.Text.Length > 0 && tBox_path.Text != "Press 'File' Button")
            {
                dataGridView1.ScrollBars = ScrollBars.None;
                btn_Detect.Enabled = false;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = true;
                neuroThread = new Thread(NeuroNet);
                neuroThread.IsBackground = true;
                neuroThread.Start();
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            /* TO-DO */
            /*
             * Реализация паузы
             */
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (neuroThread != null)
            {
                btn_Detect.Enabled = true;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = false;
                netw.PLAY_FLAG = false;
                dataGridView1.ScrollBars = ScrollBars.Vertical;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(neuroThread != null)
            {
                neuroThread.Abort();
            }
        }
    }

}
