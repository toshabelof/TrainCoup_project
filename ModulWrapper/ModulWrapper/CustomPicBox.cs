using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alturos.Yolo.Model;
using OpenCvSharp.UserInterface;

namespace ModulWrapper
{
    public partial class CustomPicBox : PictureBoxIpl
    {
  
        private List<YoloItem> objList;
        private Font fnt = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point);
        private float coefW, coefH;

        public CustomPicBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Paint += CustomPicBox_Paint;
            this.coefW = 640.0f / Utilities.YOLO_DETECTOR_WIDTH;
            this.coefH = 480.0f / Utilities.YOLO_DETECTOR_HEIGHT;
        }


        public void setRect(List<YoloItem> itms)
        {
            objList = itms;
            
        }
        public void clearBBoxes()
        {
            objList = new List<YoloItem>();
            Invalidate();
        }
        void CustomPicBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                foreach (var itm in objList)
                {
                    SizeF txtSize = e.Graphics.MeasureString(itm.Type, fnt);
                    e.Graphics.FillRectangle(Brushes.Red, new RectangleF(itm.X * coefW, itm.Y * coefH - 25, txtSize.ToSize().Width, txtSize.ToSize().Height));
                    e.Graphics.DrawRectangles(new Pen(Color.Red), new RectangleF[] { new RectangleF(itm.X * coefW, itm.Y * coefH, itm.Width * coefW, itm.Height * coefH) });
                    e.Graphics.DrawString(itm.Type, fnt, Brushes.White, itm.X * coefW, itm.Y * coefH - 25);
                }
            }
            catch{ }

        }



    }
}
