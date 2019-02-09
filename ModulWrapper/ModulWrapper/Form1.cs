using Alturos.Yolo;
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

namespace ModulWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bmp;

        string gui_default_path_text = "Enter the path to the photo and to press Enter key";
        Color gui_default_path_color = Color.Gray;

        string DetectSys = null;
        string time = null;
        Stopwatch watch = new Stopwatch();

        private void tBox_path_Enter(object sender, EventArgs e)
        {
            if (tBox_path.Text == gui_default_path_text)
            {
                tBox_path.Text = null;
                tBox_path.ForeColor = Color.Black;
                tBox_path.Font = new Font(tBox_path.Font, FontStyle.Regular);
            }
        }

        private void tBox_path_Leave(object sender, EventArgs e)
        {
            if (tBox_path.Text == "")
            {
                tBox_path.Text = gui_default_path_text;
                tBox_path.ForeColor = gui_default_path_color;
                tBox_path.Font = new Font(tBox_path.Font, FontStyle.Italic);
            }
        }

        private void tBox_path_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                picBox.Image = Image.FromFile(tBox_path.Text);
            }
        }

        [STAThread]
        private void btn_Detect_Click(object sender, EventArgs e)
        {
            using (var yoloWrapper = new YoloWrapper("yolov3-obj.cfg", "yolov3-obj_8000.weights", "obj.names"))
            {
                
                watch.Start();
                DetectSys = yoloWrapper.DetectionSystem.ToString();
                var items = yoloWrapper.Detect(ImageToByte(picBox.Image));
                List<Alturos.Yolo.Model.YoloItem> a = new List<Alturos.Yolo.Model.YoloItem>(items);

                Rectangle ee = new Rectangle(a[0].X, a[0].Y, a[0].Width, a[0].Height);
                Graphics g = picBox.CreateGraphics();
                SizeF textSize = g.MeasureString(a[0].Type, new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point));
                Rectangle rectType = new Rectangle(a[0].X, a[0].Y-25, textSize.ToSize().Width, textSize.ToSize().Height);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawRectangle(pen, ee);
                    g.FillRectangle(Brushes.Red, rectType);
                    g.DrawString(a[0].Type, new Font("Arial", 16, FontStyle.Regular), Brushes.White, a[0].X, a[0].Y - 25);
                }
                watch.Stop();
                
                time = watch.ElapsedMilliseconds.ToString();
            }

            toolStripDetectionSystem.Text = "Detection System: " + DetectSys;
            toolStripTimer.Text = "Timer: " + time + " ms";

            watch.Reset(); //fix - обнуление таймера
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //picBox.Image = Image.FromFile(@"D:\_GitHubProjects\TrainCoup_project\ModulWrapper\ModulWrapper\bin\x64\Release\Img\1.jpg");

            toolStripDetectionSystem.Text = "Detection System: n/a";
            toolStripTimer.Text = "Timer: 0 ms";
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }

}
