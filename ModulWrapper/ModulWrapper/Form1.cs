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

        Task neuroThread; // Главный поток программы
        NeuroNetwork netw;
        public static int lastFrame = 0;
        public bool isPaused = false;

        public YoloWrapper yoloWrapper;

        async private void LoadYolo()
        {
            await Task.Run(() => {

                this.Invoke(new Action(() => {
                    this.Text = "Train Coup Detector - Loading...";
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
                catch(Exception e)
                {
                    Utilities.showMsg("Unknown unexpected error!\n" + e.ToString(), "Error!");
                    this.Invoke(new Action(() => this.Close()));
                }


                this.Invoke(new Action(() => {
                    this.Text = "Train Coup Detector - Ready";
                    filePickBtn.Enabled = true;
                    btn_Detect.Enabled = true;
                    stopButton.Enabled = true;
                }));
            });
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Utilities.picBoxW = picBox.Width;
            Utilities.picBoxH = picBox.Height;
            Utilities.picBoxSmallW = picBoxSmall.Width;
            Utilities.picBoxSmallH = picBoxSmall.Height;
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
                openFileDialog.FilterIndex = 2;
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
        // Запуск
        private void NeuroRun()
        {
            if (tBox_path.Text.Length > 0 && tBox_path.Text != "Press 'File' Button")
            {
                if (!isPaused)
                    dataGridView1.Rows.Clear();
                dataGridView1.ScrollBars = ScrollBars.None;
                btn_Detect.Enabled = false;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = true;
                neuroThread = Task.Factory.StartNew(NeuroNet, TaskCreationOptions.LongRunning);
            }
        }

        // Создание класса нейронки
        public void NeuroNet()
        {
            netw = new NeuroNetwork(this, yoloWrapper);
            netw.StartAnalyzing(lastFrame);
        }
        // Запуск нейронки (создание потока в частности)
        public void btn_Detect_Click(object sender, EventArgs e)
        {

            if (neuroThread != null)
            {
                netw.PLAY_FLAG = false;
            }
            NeuroRun();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                NeuroRun();
                isPaused = false;
            }
            else
            {
                pauseButton.Text = "RESUME";
                lastFrame = netw.cframe.frameNum;
                netw.PLAY_FLAG = false;
                isPaused = true;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            lastFrame = 0;
            isPaused = false;
            if (neuroThread != null)
            {
                this.Text = "Train Coup Detector - Ready";
                btn_Detect.Enabled = true;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = false;
                netw.PLAY_FLAG = false;
                dataGridView1.ScrollBars = ScrollBars.Vertical;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(neuroThread != null && (neuroThread.Status == TaskStatus.RanToCompletion || neuroThread.Status == TaskStatus.Faulted || neuroThread.Status == TaskStatus.Canceled))
            {
                neuroThread.Dispose();
            }
        }
    }

}
