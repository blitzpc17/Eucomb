using DataSystem.Reportes;
using DataSystem.Utilidades;
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

        public MDISistema()
        {
            InitializeComponent();
        }

      
       

        private void reporteMensualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReporteMensualCv reporte = new ReporteMensualCv("REPORTE MENSUAL - DIGITAL PUMP", (int)Enumeraciones.Sucursales.DIGITALPUMP);
            reporte.MdiParent = this;
            reporte.Show();
        }

        private void reporteDiarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReporteCv reporte = new ReporteCv();
            reporte.MdiParent = this;
            reporte.Show();
        }

        private void reporteMensualToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ReporteMensualCv reporte = new ReporteMensualCv("REPORTE MENSUAL - AIVIC", (int)Enumeraciones.Sucursales.AIVIC);
            reporte.MdiParent = this;
            reporte.Show();
        }

        private void reporteMensualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteMensualCv reporte = new ReporteMensualCv("REPORTE MENSUAL - ATIO", (int)Enumeraciones.Sucursales.ATIO);
            reporte.MdiParent = this;
            reporte.Show();
        }

        private void mensualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteMensualCv reporte = new ReporteMensualCv("REPORTE MENSUAL - NEXUS", (int)Enumeraciones.Sucursales.NEXUS);
            reporte.MdiParent = this;
            reporte.Show();
        }
    }
}
