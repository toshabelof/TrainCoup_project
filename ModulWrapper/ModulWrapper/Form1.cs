using Alturos.Yolo;
using OpenCvSharp;
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

        // Класс для хранения текущего кадра
        public class CFrame
        {
            public Mat Frame = new Mat(); // Само изображение в формате OpenCV
            public int frameNum; // Номер кадра
        }

        // Переменные
        string gui_default_path_text = "Press 'File' button";
        Color gui_default_path_color = Color.Gray;
        YoloWrapper yoloWrapper;
        string DetectSys = null;
        CFrame cframe = new CFrame();
        Thread neuroThread; // Пробуем распоточить
        Thread yoloThread; // Пробуем распоточить
        static bool analyzeStarted = false;
 
        private void Form1_Load(object sender, EventArgs e)
        {
            /* TO-DO */
            /* 
             * 
             * Перенести инициализацию йолы на старт начала обработки
             * 
            */
            // Инициализируем йолу (засекаем время скока запускалось)
            var watch = new Stopwatch();

            watch.Start();
            yoloWrapper = new YoloWrapper(Utilities.YOLO_CONFIG, Utilities.YOLO_WEIGHTS, Utilities.YOLO_NAMES);
            watch.Stop();

            // Настроечки
            toolStripDetectionSystem.Text = "Detection System: n/a";
            toolStripTimer.Text = "Elapsed time: " + watch.ElapsedMilliseconds + " ms";
        
            DetectSys = yoloWrapper.DetectionSystem.ToString();
            toolStripDetectionSystem.Text = "Detection System: " + DetectSys;

            // Мутим Канвас, винформы, как я вас неновижу
            picCanvBox.Parent = picBox;
            picCanvBox.Location = new System.Drawing.Point(0, 0);
            picCanvBox.BackColor = Color.Transparent;

            GC.Collect();

            //graphicPane = picBox.CreateGraphics();
            //coupRect = new Rectangle(0, 0, 0, 0);
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
                        // Программа не работает с ЖЫПЕГАМИ потому что костыльное преобразование изображения
                        // (Господи дай канвасы)
                        // Подгружаем первое изображение видео ( первый кадр )
                        var cap = VideoCapture.FromFile(filePath);
                        var img = new Mat();

                        cap.Read(img);

                        picBox.Image = MatToBitmap(img);

                        //picBox.Image = Image.FromFile(filePath);
                        Console.WriteLine("File has " + cap.FrameCount + " frames!");
                        cap.Dispose();
                        img.Dispose();
                    }
                    catch { }

                }
            }
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
            var items = yoloWrapper.Detect(newImage.ToBytes());
            st.Stop();

            toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";
            /* TO-DO */
            /*
             * 
             * Вот тут надо придумать адекватный способ сбора полученных объектов
             * 
             */
            List<Alturos.Yolo.Model.YoloItem> itemList = new List<Alturos.Yolo.Model.YoloItem>(items);


            //Utilities.debugmessage("Count of objects " + a.Count());


            using (var graphicPane = picCanvBox.CreateGraphics())
            {
                foreach (var itm in itemList)
                {
                    // Отрисовываем все найденные объекты
                    SizeF textSize = graphicPane.MeasureString(itm.Type, new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point));
                    graphicPane.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(itm.X, itm.Y, itm.Width, itm.Height));
                    graphicPane.FillRectangle(Brushes.Red, new Rectangle(itm.X, itm.Y - 25, textSize.ToSize().Width, textSize.ToSize().Height));
                    graphicPane.DrawString(itm.Type, new Font("Arial", 16, FontStyle.Regular), Brushes.White, itm.X, itm.Y - 25);
                }
            }
            Thread.Sleep(50); // Костыль чтобы сильно не моргало ( временный )

            picBox.Image = MatToBitmap(newImage); // Рисуем новый кадр
            analyzeStarted = false;
        }

        private void NeuroThread()
        {
           
            Utilities.debugmessage("Neuro thread Started");
            var cap = new VideoCapture();

            cap = VideoCapture.FromFile(tBox_path.Text);

            cap.Read(cframe.Frame);
            cframe.frameNum++;

            DateTime timeDelta;
            int frameTime = (int)(1000 / cap.Fps);
            
            do {
                timeDelta = DateTime.Now;
                if (!analyzeStarted)
                {
                    analyzeStarted = true;
                    yoloThread = new Thread(AnalyzeVideo);
                    yoloThread.IsBackground = true;
                    yoloThread.Start();
                }
                // Отрисовываем кадры нормально (риалтайм)
                picBoxSmall.Image = MatToBitmap(cframe.Frame.Resize(new OpenCvSharp.Size(picBoxSmall.Width, picBoxSmall.Height)));

                // Ждём пока кадр обработается, потому что так надо
                if ((DateTime.Now - timeDelta).Milliseconds < frameTime)
                {
                    
                    Thread.Sleep(frameTime - (DateTime.Now - timeDelta).Milliseconds);
                    GC.Collect();
                    cap.Read(cframe.Frame);
                    
                    cframe.frameNum++;
                  
                }

            } while (!cframe.Frame.Empty());

            // Убиваем трэд если видео закончилось
            try
            {
                neuroThread.Abort();
            }
            catch { }
        }

        // Запуск нейронки (создание потока в частности)
        private void btn_Detect_Click(object sender, EventArgs e)
        {
          
            if (tBox_path.Text.Count() > 0)
            {
                neuroThread = new Thread(NeuroThread);
                neuroThread.IsBackground = true;
                neuroThread.Start();
            }
            
            /* СТАРЫЙ КУСОК КОДА оставлю для примера */

            // var cap = VideoCapture.FromFile(tBox_path.Text);

            //var st = new Stopwatch();
            //st.Start();
            //var items = yoloWrapper.Detect(ImageToByte(picBox.Image));
            //st.Stop();
            //toolStripTimer.Text = "Elapsed time: " + st.ElapsedMilliseconds + " ms";

            //List<Alturos.Yolo.Model.YoloItem> a = new List<Alturos.Yolo.Model.YoloItem>(items);

            //if (a.Count() > 0)
            //{
            //    Rectangle ee = new Rectangle(a[0].X, a[0].Y, a[0].Width, a[0].Height);
            //    Graphics g = picBox.CreateGraphics();
            //    SizeF textSize = g.MeasureString(a[0].Type, new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point));
            //    Rectangle rectType = new Rectangle(a[0].X, a[0].Y - 25, textSize.ToSize().Width, textSize.ToSize().Height);
            //    using (Pen pen = new Pen(Color.Red, 2))
            //    {
            //        g.DrawRectangle(pen, ee);
            //        g.FillRectangle(Brushes.Red, rectType);
            //        g.DrawString(a[0].Type, new Font("Arial", 16, FontStyle.Regular), Brushes.White, a[0].X, a[0].Y - 25);
            //    }

            //    g.Dispose();
            //}

        }

     
        // Не нужно за имением .ToBytes()
        //public static byte[] ImageToByte(Image img)
        //{
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}

        // Костыль преобразования картинок, не умеет в ЖЫПЕГ
        private static Bitmap MatToBitmap(Mat mat)
        {
            using (var ms = mat.ToMemoryStream())
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }

    }

}
