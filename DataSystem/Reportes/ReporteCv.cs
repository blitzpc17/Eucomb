using DataSystem.Recursos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSystem.Reportes
{
    public partial class ReporteCv : Form
    {
        private List<Sucursales> _lstSucursales;
        public ReporteCv()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
        }

        private void InicializarFormulario()
        {
            this.Text = "EUCOMB GASOLINERAS MÉXICO | " + "Reportes Cv";
            LlenarComboSucursales();  

        }

        private void LlenarComboSucursales()
        {
            _lstSucursales = (from e in Enum.GetValues(typeof(Enumeraciones.Sucursales)).Cast<Enumeraciones.Sucursales>()
                              select new Sucursales() { Id = Convert.ToInt32(e), Nombre = e.ToString() }).ToList();

            cbxSucursales.DataSource = _lstSucursales;
            cbxSucursales.DisplayMember = "Nombre";
            cbxSucursales.ValueMember = "Id";
        }

        private void LeerXml()
        {

        }
    }
}
