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

namespace ModulWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string gui_default_path_text = "Enter the path to the photo and to press Enter key";
        Color gui_default_path_color = Color.Gray;

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

        private void btn_Detect_Click(object sender, EventArgs e)
        {
            using (var yoloWrapper = new YoloWrapper("Yolo-obj.cfg", "yolo-obj_1000.weights", "Anton.names"))
            {
                var items = yoloWrapper.Detect(@"0.Jpeg");
                List<Alturos.Yolo.Model.YoloItem> a = new List<Alturos.Yolo.Model.YoloItem>(items);

                Rectangle ee = new Rectangle(a[1].X, a[1].Y, a[1].Width, a[1].Height);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    Graphics g = picBox.CreateGraphics();
                    g.DrawRectangle(pen, ee);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picBox.Image = Image.FromFile(@"D:\_GitHubProjects\TrainCoup_project\ModulWrapper\ModulWrapper\bin\x64\Release\Img\1.jpg");
            picBox.Image.Save(@"0.Jpeg", ImageFormat.Jpeg);
        }
    }

}
