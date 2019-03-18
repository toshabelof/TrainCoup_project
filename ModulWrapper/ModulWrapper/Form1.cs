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
using System.IO;

namespace ModulWrapper
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        Task neuroThread; // Main thread of neuro network
        NeuroNetwork netw; // Network instance
        public static int lastFrame = 0; // Save frame nubmer for pause
        public static int coupCount = 0; // Save coup count nubmer for pause
        public static int coupCoord = -999; // Save coup coord for pause
        public bool isPaused = false; // Pause flag

        public YoloWrapper yoloWrapper;

        /// <summary> Initalize YoloWrapper </summary>
        async private void LoadYolo()
        {
            await Task.Run(() => {

                this.Invoke(new Action(() => {
                    this.Text = "Train Coup Detector - Loading...";
                    filePickBtn.Enabled = false;
                    btn_Detect.Enabled = false;
                    stopButton.Enabled = false;
                    tBoxTreatmentPath.Enabled = false;
                    btnTreatment.Enabled = false;
                }));
                


                try
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    yoloWrapper = new YoloWrapper(Utilities.YOLO_CONFIG, Utilities.YOLO_WEIGHTS, Utilities.YOLO_NAMES);
                    watch.Stop();


                    var DetectSys = yoloWrapper.DetectionSystem.ToString();
                    toolStripDetectionSystem.Text = "Detection System: " + DetectSys;
                    toolStripTimer.Text = "Yolo loaded in: " + watch.ElapsedMilliseconds / 1000.0 + " second(s)";
                }
                catch(System.IO.FileNotFoundException)
                {
                    Utilities.showMsg("Configuration file(s) not found!", "Error!");
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
                    tBoxTreatmentPath.Enabled = true;
                    btnTreatment.Enabled = true;
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
         
            toolStripTimer.Text = "Elapsed time: n/a";
            toolStripDetectionSystem.Text = "Detection System: n/a";
            toolStripCounter.Text = "Count: n/a";

            stopButton_Click(sender, e); // IDK why it's WORKS O_O (SOME MAGIC need to fix bug)
            pauseButton.Enabled = false;
        }

        // FilePicker for video files (or images; It's not including array of images)
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
                    filePath = openFileDialog.FileName;
                }
                if (filePath != "")
                {

                    try
                    {

                        // Loading first frame of video like preview
                        var cap = VideoCapture.FromFile(filePath);
                        var img = new Mat();
                        cap.Read(img);

                        picBox.ImageIpl = img.Resize(new OpenCvSharp.Size(picBox.Width, picBox.Height));

                        Utilities.debugmessage("File has " + cap.FrameCount + " frames!");
                        frameCnt.Text = "Frames: 0/" + cap.FrameCount;
                        cap.Dispose();
                        img.Dispose();

                        tBox_path.Text = filePath;

                        // Creating file in LOGS folder to store data of founded coups
                        string logFile = @"LOGS\" + Path.GetFileName(tBox_path.Text) + ".txt";

                        if (!Directory.Exists("LOGS"))
                            Directory.CreateDirectory("LOGS");


                        // Will store data in CSV mode
                        using (StreamWriter sw = new StreamWriter(logFile,false,Encoding.UTF8))
                        {
                            sw.WriteLine("frame,x1,y1,x2,y2,time");
                            sw.Close();
                        }
                        
                        
                    }
                    catch
                    {
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
        // Starting an neuro network thread
        private void NeuroRun()
        {
            if (tBox_path.Text.Length > 0 && tBox_path.Text != "Press 'File' Button")
            {
                if (!isPaused)
                {
                    listView1.Clear();
                    listView1.Columns.Add("Frame");
                    listView1.Columns.Add("X1");
                    listView1.Columns.Add("Y1");
                    listView1.Columns.Add("X2");
                    listView1.Columns.Add("Y2");
                    listView1.Columns.Add("Time");
                }
                btn_Detect.Enabled = false;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = true;
                neuroThread = Task.Factory.StartNew(NeuroNet, TaskCreationOptions.LongRunning);
            }
        }

        /// <summary>
        /// Create and launch analyzing in neuro network
        /// </summary>
        public void NeuroNet()
        {
            netw = new NeuroNetwork(this, yoloWrapper, null);
            netw.StartAnalyzingVideo(lastFrame, coupCount, coupCoord);
        }
       

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
                coupCount = netw.CoupCount;
                coupCoord = netw.masTrackDcoup;
                netw.PLAY_FLAG = false;
                isPaused = true; 
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            lastFrame = 0;
            coupCount = 0;
            coupCoord = -999;
            isPaused = false;
            if (neuroThread != null)
            {
                this.Text = "Train Coup Detector - Ready";
                btn_Detect.Enabled = true;
                pauseButton.Text = "PAUSE";
                pauseButton.Enabled = false;
                netw.PLAY_FLAG = false;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(neuroThread != null && (neuroThread.Status == TaskStatus.RanToCompletion || neuroThread.Status == TaskStatus.Faulted || neuroThread.Status == TaskStatus.Canceled))
            {
                netw.PLAY_FLAG = false;
                neuroThread.Dispose();
            }
        }

        // Redefine settings for window
        private void Form1_Resize(object sender, EventArgs e)
        {

            Utilities.picBoxW = picBox.Width;
            Utilities.picBoxH = picBox.Height;
            Utilities.picBoxSmallW = picBoxSmall.Width;
            Utilities.picBoxSmallH = picBoxSmall.Height;
        }

        // Choose folder and
        // Start analyze array of images
        private void btnTreatment_Click(object sender, EventArgs e)
        {
            stopButton_Click(sender, e);
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            List<string> listPhotos = new List<string>();

            string filePath = string.Empty;

            try
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                    tBoxTreatmentPath.Text = filePath;

                    DirectoryInfo dir = new DirectoryInfo(filePath);
                    foreach (var item in dir.GetFiles())
                    {
                        // Add only those images that are listed in Utilities.IMAGE_TYPES
                        if (Utilities.IMAGE_TYPES.Contains(Path.GetExtension(item.ToString())))
                        {
                            listPhotos.Add(dir.FullName + @"\" + item.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.showMsg("Folder cannot be open\n" + ex.ToString(), "Error!");
            }

            // Start an photo processing
            if (filePath != string.Empty)
            {
                TreatmentPhoto treatmentPhoto = new TreatmentPhoto(this, listPhotos, yoloWrapper);
                treatmentPhoto.formShow();
                treatmentPhoto.startTreatment();
            }
        }
    }

}
