using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModulWrapper
{
    public partial class TreatmentPhotos : Form
    {
        public TreatmentPhotos()
        {
            InitializeComponent();
        }

        private void TreatmentPhotos_Load(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
