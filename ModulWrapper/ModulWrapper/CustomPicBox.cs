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
        public CustomPicBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Paint += CustomPicBox_Paint;
        }


        public void setRect(List<YoloItem> itms)
        {
            objList = itms;
        }

        void CustomPicBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                foreach (var itm in objList)
                {
                    SizeF txtSize = e.Graphics.MeasureString(itm.Type, fnt);
                    e.Graphics.FillRectangle(Brushes.Red, new Rectangle(itm.X, itm.Y - 25, txtSize.ToSize().Width, txtSize.ToSize().Height));
                    e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(itm.X, itm.Y, itm.Width, itm.Height));
                    e.Graphics.DrawString(itm.Type, fnt, Brushes.White, itm.X, itm.Y - 25);
                }
            }catch{ return; }

        }


    }
}
