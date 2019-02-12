using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alturos.Yolo.Model;
using OpenCvSharp;
using OpenCvSharp.UserInterface;

namespace ModulWrapper
{
    public partial class CustomPicBox : PictureBoxIpl
    {
  
        private List<YoloItem> objList = new List<YoloItem>();
        //private RectangleF rectFF;
        private Font fnt = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point);
        private float coefW, coefH;

        public CustomPicBox(IContainer container)
        {
            container.Add(this);
           
            InitializeComponent();
            Paint += CustomPicBox_Paint;

       
        }


        public void setRect(List<YoloItem> itms)
        {
            coefW = (float)Utilities.picBoxW / Utilities.YOLO_DETECTOR_WIDTH;
            coefH = (float)Utilities.picBoxH / Utilities.YOLO_DETECTOR_HEIGHT;
            objList = itms;
        }

        //public void trackBox(Rect2d rect)
        //{
        //    var rx = (float) rect.X;
        //    var ry = (float) rect.Y;
        //    var rw = (float) rect.Width;
        //    var rh = (float) rect.Height;
            
        //    var coeffW = (float) Utilities.picBoxSmallW / Utilities.YOLO_DETECTOR_WIDTH;
        //    var coeffH = (float) Utilities.picBoxSmallH / Utilities.YOLO_DETECTOR_HEIGHT;


        //    rectFF = new RectangleF(rx * coeffW, ry * coeffH, rw * coeffW, rh * coeffH);

        //    Utilities.debugmessage("trackBoxfunc:" + rectFF.ToString());
        //}

        public void clearBBoxes()
        {
            objList = new List<YoloItem>();
            Invalidate();
        }
        void CustomPicBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            foreach (var itm in objList)
            {
                if (itm.Confidence < 0.65) { return; }
                SizeF txtSize = e.Graphics.MeasureString(itm.Type, fnt);
                e.Graphics.FillRectangle(Brushes.Red, new RectangleF(itm.X * coefW, itm.Y * coefH - 25, txtSize.ToSize().Width, txtSize.ToSize().Height));
                e.Graphics.DrawRectangles(new Pen(Color.Red), new RectangleF[] { new RectangleF(itm.X * coefW, itm.Y * coefH, itm.Width * coefW, itm.Height * coefH) });
                e.Graphics.DrawString(itm.Type, fnt, Brushes.White, itm.X * coefW, itm.Y * coefH - 25);
            }

        }

    }
}
