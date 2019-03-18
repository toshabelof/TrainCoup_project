using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Alturos.Yolo;
using System.Drawing;
using OpenCvSharp;

namespace ModulWrapper
{
    class TreatmentPhoto
    {
        public Form1 GlobalForm;
        TreatmentPhotos treatmentPhotos;

        public YoloWrapper yoloWrapper;
        public List<string> listPhotos = new List<string>();

        int countPhotos;


        public TreatmentPhoto(Form1 f1, List<string> lp, YoloWrapper yw)
        {
            this.GlobalForm = f1;
            this.yoloWrapper = yw;
            this.listPhotos = lp;
            this.countPhotos = lp.Count;
        }

        public void formShow()
        {
            treatmentPhotos = new TreatmentPhotos();
            treatmentPhotos.Show();
            treatmentPhotos.Invoke(new Action(() => {
                treatmentPhotos.lblStatus.Text = "Start treatment photos...";
                treatmentPhotos.lblCount.Text = "Photo: 0/" + countPhotos;
            }));
        }



        Task neuroThread;
        NeuroNetwork netw;
        /// <summary>
        /// Create and launch analyzing in neuro network
        /// </summary>
        public void startTreatment()
        {
            neuroThread = Task.Factory.StartNew(NeuroNet, TaskCreationOptions.LongRunning);
        }

        public void NeuroNet()
        {
            netw = new NeuroNetwork(GlobalForm, yoloWrapper, listPhotos);
            netw.StartAnalyzingPhotos(treatmentPhotos);

            treatmentPhotos.Invoke(new Action(() => {
                treatmentPhotos.pictureBox1.Image = Properties.Resources.kisspng_fingerprint_comcast_circle_symbol_technology_tick_5acb37d7297ac2_3455009315232675431699;
                treatmentPhotos.lblStatus.Text = "Done!";
                treatmentPhotos.btnClose.Enabled = true;
                treatmentPhotos.btnClose.BackColor = Color.Green;
            }));

        }
    }
}
            
            
        
    

