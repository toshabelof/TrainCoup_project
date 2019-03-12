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

        //Для массовой обработки фото
        TreatmentPhotos treatmentPhotos;
        int nextPhoto = 0;
        int countPhoto = 0;

        public bool START_FLAG = false;

        public void Form1_Load(object sender, EventArgs e)
        {
            toolStripTimer.Text = "Elapsed time: n/a";
            toolStripDetectionSystem.Text = "Detection System: n/a";
            toolStripCounter.Text = "Count: n/a";

            pauseButton.Enabled = false;
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
                    
                    try
                    {
                        // Подгружаем первое изображение видео ( первый кадр )
                        var cap = VideoCapture.FromFile(filePath);
                        var img = new Mat();
                        cap.Read(img);
                        if (neuroThread != null)
                        {
                            picBox.clearBBoxes();
                            btn_Detect.Enabled = true;
                            pauseButton.Text = "PAUSE";
                            pauseButton.Enabled = false;
                            netw.PLAY_FLAG = false;
                        }
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
                GC.Collect();
            }
        }

        public void NeuroNet()
        {
            netw = new NeuroNetwork(this);
            netw.StartAnalyzing();
        }
        
        public void NeuroNetLite()
        {
            Console.WriteLine("NeuroNet");
            NeuroNetworkLite netwLite = new NeuroNetworkLite(this);
            netwLite.StartAnalyzing();
        }
        // Запуск нейронки (создание потока в частности)
        public void btn_Detect_Click(object sender, EventArgs e)
        {

            if (neuroThread != null)
            {
                netw.PLAY_FLAG = false;
            }
            if (tBox_path.Text.Length > 0 && tBox_path.Text != "Укажите путь к видео-файлу")
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
            try
            {
                //Utilities.debugmessage("Playing state: " + neuroThread.ThreadState.ToString());
                if (neuroThread.ThreadState.HasFlag(System.Threading.ThreadState.Suspended))
                {

                    pauseButton.Text = "PAUSE";
                    neuroThread.Resume();
                    dataGridView1.ScrollBars = ScrollBars.None;
                }
                else
                {

                    pauseButton.Text = "RESUME";
                    neuroThread.Suspend();
                    dataGridView1.ScrollBars = ScrollBars.Vertical;
                }
            }
            catch { }
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
                if (neuroThread.ThreadState.HasFlag(System.Threading.ThreadState.Suspended))
                    neuroThread.Resume();
                neuroThread.Abort();
            }
        }

        //массовая обработка фото
        private void btnOpenFindPhotos_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            try
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                    tBox_Treatment.Text = filePath;

                    treatmentPhotos = new TreatmentPhotos(filePath, this);
                    countPhoto = treatmentPhotos.getCountPhotos();

                    lblCountPhotos.Text = "Обработано: " + nextPhoto + "/" + countPhoto;
                }
            }
            catch(Exception ex)
            {
                Utilities.showMsg(ex.ToString(), "Ошибка открытия папки");
            }

            filePath = null;
            GC.Collect();
            
        }

        private void btnStartTreatment_Click(object sender, EventArgs e)
        {
            
            btnStartTreatment.Enabled = false;
            btnOpenFindPhotos.Enabled = false;

            progressBar1.Value = 0;
            progressBar1.Maximum = countPhoto;


            List<string> listPhotos = new List<string>(treatmentPhotos.getListPhotos());

            

            foreach (var item in listPhotos)
            {
                //while (START_FLAG == true)  { }

                START_FLAG = true;
                tBox_path.Invoke(new Action(() => tBox_path.Text = item));

                var cap = VideoCapture.FromFile(item);
                var img = new Mat();
                cap.Read(img);
                
                picBox.clearBBoxes();
                   
                picBox.ImageIpl = img.Resize(new OpenCvSharp.Size(picBox.Width, picBox.Height));

                
                cap.Dispose();
                img.Dispose();

                tBox_path.Text = item;

                neuroThread = new Thread(NeuroNetLite);
                neuroThread.IsBackground = true;
                neuroThread.Start();
                neuroThread.Join();
                Thread.Sleep(100);

                progressBar1.Invoke(new Action(() => progressBar1.Value++));
                
            }

            btnStartTreatment.Enabled = true;
            btnOpenFindPhotos.Enabled = true;
          
        }

        

    }

}
