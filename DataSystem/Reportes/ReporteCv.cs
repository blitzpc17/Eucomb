using CapaLogica.Reportes;
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
        private ReporteCvLogica contexto;
        private List<Sucursales> _lstSucursales;
        public ReporteCv()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();            

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string rutaXlm = fd.FileName;
                LeerXml(rutaXlm);
            }
        }

        private void InicializarFormulario()
        {
            this.Text = "EUCOMB GASOLINERAS MÉXICO | " + "Reportes Cv";
            contexto =  new ReporteCvLogica();
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

        private void LeerXml(String rutaXlm)
        {
            LlenarDgvDiario(contexto.LeerReporteCvXml(rutaXlm));
        }

        private void LlenarDgvDiario(ControlVolumetrico obj)
        {
            dgvRegistrosDiario.Columns.Add("version", "Versión");
            dgvRegistrosDiario.Columns.Add("rfcContribuyente", "Rfc Contrib.");
            dgvRegistrosDiario.Columns.Add("rfcRepresentanteLegal", "Rfc Rep. Legal");
            dgvRegistrosDiario.Columns.Add("rfcProveedor", "Rfc Prov.");
            dgvRegistrosDiario.Columns.Add("tipoCaracter", "Tipo Caract.");
            dgvRegistrosDiario.Columns.Add("modalidadPermiso", "Mod. Permiso");
            dgvRegistrosDiario.Columns.Add("NumPermiso", "No. Permiso");
            dgvRegistrosDiario.Columns.Add("claveInstalacion", "Clave Inst.");
            dgvRegistrosDiario.Columns.Add("descripcionInstalacion", "Descrip. Inst.");
            dgvRegistrosDiario.Columns.Add("numeroPozos", "No. Pozos");
            dgvRegistrosDiario.Columns.Add("numeroTanques", "No. Tanques");
            dgvRegistrosDiario.Columns.Add("numeroDuctosEntradaSalida", "No. Dctos. E/S");
            dgvRegistrosDiario.Columns.Add("numeroDuctosTransporteDistribucion", "No. Dctos. Trans. Distrib,");
            dgvRegistrosDiario.Columns.Add("fechaYHoraReporte", "Fecha / Hora Reporte");
            //dgvRegistrosDiario.Columns.Add("", "");

            dgvRegistrosDiario.Rows.Add(obj.Version, obj.RfcContribuyente, obj.RfcRepresentanteLegal, obj.RfcProveedor, obj.Caracter.TipoCaracter, obj.Caracter.ModalidadPermiso, obj.Caracter.NumPermiso,obj.ClaveInstalacion, obj.DescripcionInstalacion, obj.NumeroPozos, obj.NumeroTanques,obj.NumeroDuctosEntradaSalida, obj.NumeroDuctosTransporteDistribucion, obj.FechaYHoraCorte);

            tsTotalRegistros.Text = dgvRegistrosDiario.RowCount.ToString("N0");

        }
    }
}
