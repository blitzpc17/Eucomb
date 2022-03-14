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
        }

        private void LeerXml(String rutaXlm)
        {
            contexto.ObjControlVolumetrico = contexto.LeerReporteCvXml(rutaXlm);
            if (contexto.ObjControlVolumetrico == null) return;
            LlenarEncabezado(contexto.ObjControlVolumetrico);
            LlenarDgvDiario(contexto.ObjControlVolumetrico);
        }

        private void LlenarDgvDiario(ControlVolumetrico obj)
        {
            dgvRegistrosDiario.Columns.Add("TotalEntregas", "Total entregas");
            dgvRegistrosDiario.Columns.Add("ValorNumerico3", "Valor Núm.");
            dgvRegistrosDiario.Columns.Add("UM3", "UM");
            dgvRegistrosDiario.Columns.Add("TotalDocumento", "Total Doc.");
            dgvRegistrosDiario.Columns.Add("ImporteTotalEntregas", "Importe Total Entrega");
            dgvRegistrosDiario.Columns.Add("RFCClienteProveedor", "Rfc cliente");
            dgvRegistrosDiario.Columns.Add("NombreClienteProveedor", "Nombre Cliente");
            dgvRegistrosDiario.Columns.Add("CFDI", "Cfdi");
            dgvRegistrosDiario.Columns.Add("TipoCfdi", "Tipo Cfdi");
            dgvRegistrosDiario.Columns.Add("PrecioCompra", "Precio Compra");
            dgvRegistrosDiario.Columns.Add("PrecioVentaPublico", "Precio Vta. Pub.");
            dgvRegistrosDiario.Columns.Add("PrecioVenta", "Precio Venta");
            dgvRegistrosDiario.Columns.Add("FechaHoraTrans", "Fecha Hora Op.");
            dgvRegistrosDiario.Columns.Add("ValorNumerico14", "Valor Núm.");
            dgvRegistrosDiario.Columns.Add("UM15", "UM15");
            dgvRegistrosDiario.Columns.Add("DieselCombustibleNoFosil", "Combustible no fosil");
            //dgvRegistrosDiario.Columns.Add("", "");
            foreach (var pro  in obj.Productos)
            {
                foreach(var dis in pro.Dispensario)
                {
                    foreach(var man in dis.Manguera)
                    {
                        foreach(var entrega in man.Entregas.Entrega)
                        {
                            foreach(var complemento in entrega.Complemento.Complemento_Expendio)
                            {
                                clsReporte registro = new clsReporte
                                {
                                    TotalEntregas = man.Entregas.TotalEntregas,
                                    ValorNumerico3 = man.Entregas.SumaVolumenEntregado.ValorNumerico,
                                    UM4 = man.Entregas.SumaVolumenEntregado.UM,
                                    TotalDucumentos = man.Entregas.TotalDocumentos,
                                    ImporteTotalEntregas = man.Entregas.SumaVolumenEntregado.ValorNumerico,
                                    RfcCliente = complemento.Nacional.RfcClienteOProveedor,
                                    NombreCliente = complemento.Nacional.NombreClienteOProveedor,
                                    Cfdi = complemento.Nacional.Cfdis.Cfdi,
                                    TipoCfdi = complemento.Nacional.Cfdis.TipoCfdi,
                                    PrecioCompra = complemento.Nacional.Cfdis.PrecioCompra,
                                    PrecioVentaPublico = complemento.Nacional.Cfdis.PrecioDeVentaAlPublico,
                                    PrecioVenta = complemento.Nacional.Cfdis.PrecioVenta,
                                    FechaHoraTransaccion = complemento.Nacional.Cfdis.FechaYHoraTransaccion,
                                    ValorNumerico14 = complemento.Nacional.Cfdis.VolumenDocumentado.ValorNumerico,
                                    UM15 = complemento.Nacional.Cfdis.VolumenDocumentado.UM

                                };

                                dgvRegistrosDiario.Rows.Add(registro.TotalEntregas, registro.ValorNumerico3, registro.UM4, registro.TotalDucumentos, 
                                    registro.ImporteTotalEntregas,registro.RfcCliente, registro.NombreCliente, registro.Cfdi, registro.TipoCfdi, registro.PrecioCompra,
                                    registro.PrecioVentaPublico, registro.PrecioVenta, registro.FechaHoraTransaccion, registro.ValorNumerico14, registro.UM15);

                            }
                        }
                    }
                }
            }

           

            tsTotalRegistros.Text = dgvRegistrosDiario.RowCount.ToString("N0");

        }

        private void LlenarEncabezado(ControlVolumetrico obj)
        {
            txtVersion.Text = obj.Version;
            txtRfcRepresentante.Text = obj.RfcRepresentanteLegal;
            txtRfcProveedor.Text = obj.RfcProveedor;
            txtNoPermiso.Text = obj.Caracter.NumPermiso;
            txtPeriodo.Text = obj.FechaYHoraCorte.ToString();
            txtRfcContrib.Text = obj.RfcContribuyente;
            txtCaracter.Text = obj.Caracter.TipoCaracter;
            txtModPermiso.Text = obj.Caracter.ModalidadPermiso;


        }
    }
}
