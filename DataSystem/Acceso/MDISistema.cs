using DataSystem.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSystem.Acceso
{
    public partial class MDISistema : Form
    {
        private int childFormNumber = 0;

        public MDISistema()
        {
            InitializeComponent();
        }

        private void reportesCvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteCv reporte = new ReporteCv();
            reporte.MdiParent = this;
            reporte.Show();
        }
    }
}
