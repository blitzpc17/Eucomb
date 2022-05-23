using DataSystem.Recursos;
using Entidades.JSONMensual;
using ExcelWriter;
using LinqToExcel;
using Newtonsoft.Json;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace DataSystem.Reportes
{
    public partial class ReporteMensualCv : Form
    {
        private List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensual;
        private List<Entidades.cls.FACTURASDETALLE> lstManuales;
        List<Entidades.cls.clsResultadosMensual> LstResultados;
        List<Entidades.cls.clsResultadosMensual> LstResultadosAux;

        List<Entidades.cls.FACTURASDETALLE> LstFacturaDetalleSnFecha;

        private ExcelQueryFactory urlConexion;
        public ReporteMensualCv()
        {
            InitializeComponent();
        }

        private void CargarArchivo()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaDoc = openFileDialog.FileName;
                string nombreArchivo = openFileDialog.SafeFileName;
                String[]arrNombreArchivo = nombreArchivo.Split('.');
                if (!string.IsNullOrEmpty(rutaDoc))
                {
                    if (arrNombreArchivo[1].ToUpper()=="JSON")
                    {
                        LeerJson(rutaDoc);
                    }
                    else
                    {
                        LeerXml(rutaDoc);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }



        private void LeerJson(string RutaArchivo)
        {
            using (StreamReader r = new StreamReader(RutaArchivo))
            {
                string json = r.ReadToEnd();
                ControlesVolumetricos objControlVolumetrico = JsonConvert.DeserializeObject<ControlesVolumetricos>(json);
            }
        }

        private void LeerXml(string RutaArchivo)
        {
            List<Entidades.cls.clsGasolinasInfo> LstProductos = new List<Entidades.cls.clsGasolinasInfo>();
            XmlDocument doc = new XmlDocument();
            doc.Load(RutaArchivo);

            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("Covol", "https://repositorio.cloudb.sat.gob.mx/Covol/xml/Mensuales");

            XmlNode nodeVersion = doc.SelectSingleNode("//Covol:Version", nsm);
            XmlNode nodeRfcContribuyente = doc.SelectSingleNode("//Covol:RfcContribuyente", nsm);
            XmlNode nodeRfcRepresentanteLegal = doc.SelectSingleNode("//Covol:RfcRepresentanteLegal", nsm);
            XmlNode nodeRfcProveedor = doc.SelectSingleNode("//Covol:RfcProveedor", nsm);
            XmlNode nodeCaracter = doc.SelectSingleNode("//Covol:Caracter", nsm);
            XmlNode nodeClaveInstalacion = doc.SelectSingleNode("//Covol:ClaveInstalacion", nsm);
            XmlNode nodeDescripcionInstalacion = doc.SelectSingleNode("//Covol:DescripcionInstalacion", nsm);
            XmlNode nodeNumeroPozos = doc.SelectSingleNode("//Covol:NumeroPozos", nsm);
            XmlNode nodeNumeroTanques = doc.SelectSingleNode("//Covol:NumeroTanques", nsm);
            XmlNode nodeNumeroDuctosEntradaSalida = doc.SelectSingleNode("//Covol:NumeroDuctosEntradaSalida", nsm);
            XmlNode nodeNumeroDuctosTransporteDistribucion = doc.SelectSingleNode("//Covol:NumeroDuctosTransporteDistribucion", nsm);
            XmlNode nodeNumeroDispensarios = doc.SelectSingleNode("//Covol:NumeroDispensarios", nsm);
            XmlNode nodeFechaYHoraReporteMes = doc.SelectSingleNode("//Covol:FechaYHoraReporteMes", nsm);
            XmlNodeList nodePRODUCTO = doc.SelectNodes("//Covol:PRODUCTO", nsm);

            Entidades.XMLMensual.ControlesVolumetricos obj = new Entidades.XMLMensual.ControlesVolumetricos();
            obj.Version = nodeVersion.InnerText;
            obj.RfcContribuyente = nodeRfcContribuyente.InnerText;
            obj.RfcRepresentanteLegal = nodeRfcRepresentanteLegal.InnerText;
            obj.RfcProveedor = nodeRfcProveedor.InnerText;

            obj.Caracter = new Entidades.XMLMensual.Caracter
            {
                TipoCaracter = nodeCaracter.ChildNodes[0].InnerText,
                ModalidadPermiso = nodeCaracter.ChildNodes[1].InnerText,
                NumPermiso = nodeCaracter.ChildNodes[2].InnerText
            };
            obj.ClaveInstalacion = nodeClaveInstalacion.InnerText;
            obj.DescripcionInstalacion = nodeDescripcionInstalacion.InnerText;
            obj.NumeroPozos = int.Parse(nodeNumeroPozos.InnerText);
            obj.NumeroTanques = int.Parse(nodeNumeroTanques.InnerText);
            obj.NumeroDuctosEntradaSalida = int.Parse(nodeNumeroDuctosEntradaSalida.InnerText);
            obj.NumeroDuctosTransporteDistribucion = int.Parse(nodeNumeroDuctosTransporteDistribucion.InnerText);
            obj.NumeroDispensarios = int.Parse(nodeNumeroDispensarios.InnerText);
            obj.FechaYHoraCorte = DateTime.Parse(nodeFechaYHoraReporteMes.InnerText);

            obj.PRODUCTO = new List<Entidades.XMLMensual.PRODUCTO>();

            foreach (XmlNode pro in nodePRODUCTO)
            {

                Entidades.XMLMensual.PRODUCTO objProducto = new Entidades.XMLMensual.PRODUCTO
                {
                    ClaveProducto = pro.ChildNodes[0].InnerText,
                    ClaveSubProducto = pro.ChildNodes[1].InnerText,
                    MarcaComercial = pro.ChildNodes[3].InnerText,
                };              

                var nodeGasolina = pro.ChildNodes[2];
                objProducto.Gasolina = new Entidades.XMLMensual.Gasolina
                {
                    ComposOctanajeGasolina = nodeGasolina.ChildNodes[0].InnerText,
                    GasolinaConCombustibleNoFosil = nodeGasolina.ChildNodes[1] == null ? nodeGasolina.ChildNodes[0].InnerText : nodeGasolina.ChildNodes[1].InnerText
                };
                var nodeREPORTEVOLUMENMENSUAL = pro.ChildNodes[4];
                objProducto.REPORTEDEVOLUMENMENSUAL = new Entidades.XMLMensual.REPORTEDEVOLUMENMENSUAL();
                var subCONTROLEXISTENCIAS = nodeREPORTEVOLUMENMENSUAL.ChildNodes[0];
                objProducto.REPORTEDEVOLUMENMENSUAL.CONTROLDEEXISTENCIAS = new Entidades.XMLMensual.CONTROLDEEXISTENCIAS
                {
                    FechaYHoraEstaMedicionMes = DateTime.Parse(subCONTROLEXISTENCIAS.ChildNodes[1].InnerText),
                };
                var VolumenExistenciasMes = subCONTROLEXISTENCIAS.ChildNodes[0];
                objProducto.REPORTEDEVOLUMENMENSUAL.CONTROLDEEXISTENCIAS.VolumenExistenciasMes = new Entidades.XMLMensual.VolumenExistenciasMes
                {
                    ValorNumerico = decimal.Parse(VolumenExistenciasMes.ChildNodes[0].InnerText),
                };
                var RECEPCIONES = nodeREPORTEVOLUMENMENSUAL.ChildNodes[1];
                objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES = new Entidades.XMLMensual.RECEPCIONES
                {
                    TotalRecepcionesMes = int.Parse(RECEPCIONES.ChildNodes[0].InnerText),
                    TotalDocumentosMes = int.Parse(RECEPCIONES.ChildNodes[2].InnerText),
                    ImporteTotalRecepcionMensual = decimal.Parse(RECEPCIONES.ChildNodes[3].InnerText),
                };
                var SumaVolumenRecepcionMes = RECEPCIONES.ChildNodes[1];
                objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.SumaVolumenRecepcionMes = new Entidades.XMLMensual.SumaVolumenRecepcion
                {
                    ValorNumerico = decimal.Parse(SumaVolumenRecepcionMes.ChildNodes[0].InnerText),
                    UM = SumaVolumenRecepcionMes.ChildNodes[1].InnerText
                };
                Entidades.cls.clsGasolinasInfo objGasolina = new Entidades.cls.clsGasolinasInfo
                {
                    NombreProducto = pro.ChildNodes[3].InnerText,
                    Litros = decimal.Parse(SumaVolumenRecepcionMes.ChildNodes[0].InnerText),
                    Importe = decimal.Parse(RECEPCIONES.ChildNodes[3].InnerText),

                };
                LstProductos.Add(objGasolina);
                var Complemento = RECEPCIONES.ChildNodes[4];
                if (Complemento.ChildNodes.Count > 0)
                {
                    objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento = new Entidades.XMLMensual.Complemento();

                    objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio = new Entidades.XMLMensual.Complemento_Expendio();
                    objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL = new List<Entidades.XMLMensual.NACIONAL>();


                    foreach (XmlNode nac in Complemento.ChildNodes[0].ChildNodes)
                    {
                        Entidades.XMLMensual.NACIONAL objNac = new Entidades.XMLMensual.NACIONAL
                        {
                            RfcClienteOProveedor = nac.ChildNodes[0].InnerText,
                            NombreClienteOProveedor = nac.ChildNodes[1].InnerText,
                        };
                        var nodeCFDI = nac.ChildNodes[2];
                        objNac.CFDIs = new Entidades.XMLMensual.Cfdis
                        {
                            CFDI = nodeCFDI.ChildNodes[0].InnerText,
                            TipoCFDI = nodeCFDI.ChildNodes[1].InnerText,
                            PrecioCompra = decimal.Parse(nodeCFDI.ChildNodes[2].InnerText),
                            PrecioDeVentaAlPublico = decimal.Parse(nodeCFDI.ChildNodes[3].InnerText),
                            PrecioVenta = decimal.Parse(nodeCFDI.ChildNodes[4].InnerText),
                            FechaYHoraTransaccion = DateTime.Parse(nodeCFDI.ChildNodes[5].InnerText),
                        };
                        var nodoValorNumerico = nodeCFDI.ChildNodes[6];
                        objNac.CFDIs.VolumenDocumentado = new Entidades.XMLMensual.VolumenDocumentado
                        {
                            ValorNumerico = decimal.Parse(nodoValorNumerico.ChildNodes[0].InnerText),
                            UM = nodoValorNumerico.ChildNodes[1].InnerText
                        };
                        objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL.Add(objNac);

                    }
                }                

                var ENTREGAS = nodeREPORTEVOLUMENMENSUAL.ChildNodes[2];
                objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS = new Entidades.XMLMensual.ENTREGAS
                {
                    TotalEntregasMes = int.Parse(ENTREGAS.ChildNodes[0].InnerText),
                    TotalDocumentosMes = int.Parse(ENTREGAS.ChildNodes[2].InnerText),
                    ImporteTotalEntregasMes = decimal.Parse(ENTREGAS.ChildNodes[3].InnerText)
                };
                var SumaVolumenEntregadoMes = ENTREGAS.ChildNodes[1];
                objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS.SumaVolumenEntregado = new Entidades.XMLMensual.SumaVolumenEntregado
                {
                    ValorNumerico = decimal.Parse(SumaVolumenEntregadoMes.ChildNodes[0].InnerText),
                    UM = SumaVolumenEntregadoMes.ChildNodes[1].InnerText
                };

                var ComplementoEntregas = ENTREGAS.ChildNodes[4];
                objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS.Complemento = new Entidades.XMLMensual.Complemento();
                objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS.Complemento.Complemento_Expendio = new Entidades.XMLMensual.Complemento_Expendio();
                objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS.Complemento.Complemento_Expendio.NACIONAL = new List<Entidades.XMLMensual.NACIONAL>();

                if (ComplementoEntregas.HasChildNodes)
                {
                    foreach (XmlNode nacEnt in ComplementoEntregas.ChildNodes[0].ChildNodes)
                    {
                        Entidades.XMLMensual.NACIONAL objNacEnc = new Entidades.XMLMensual.NACIONAL
                        {
                            RfcClienteOProveedor = nacEnt.ChildNodes[0].InnerText,
                            NombreClienteOProveedor = nacEnt.ChildNodes[1].InnerText,
                        };
                        var nodeCfdiNac = nacEnt.ChildNodes[2];
                        objNacEnc.CFDIs = new Entidades.XMLMensual.Cfdis
                        {
                            CFDI = nodeCfdiNac.ChildNodes[0].InnerText,
                            TipoCFDI = nodeCfdiNac.ChildNodes[1].InnerText,
                            PrecioCompra = decimal.Parse(nodeCfdiNac.ChildNodes[2].InnerText),
                            PrecioDeVentaAlPublico = decimal.Parse(nodeCfdiNac.ChildNodes[3].InnerText),
                            PrecioVenta = decimal.Parse(nodeCfdiNac.ChildNodes[4].InnerText),
                            FechaYHoraTransaccion = DateTime.Parse(nodeCfdiNac.ChildNodes[5].InnerText)
                        };
                        var VolDocEntrega = nodeCfdiNac.ChildNodes[6];
                        objNacEnc.CFDIs.VolumenDocumentado = new Entidades.XMLMensual.VolumenDocumentado
                        {
                            ValorNumerico = decimal.Parse(VolDocEntrega.ChildNodes[0].InnerText),
                            UM = VolDocEntrega.ChildNodes[1].InnerText
                        };
                        objProducto.REPORTEDEVOLUMENMENSUAL.ENTREGAS.Complemento.Complemento_Expendio.NACIONAL.Add(objNacEnc);

                    }
                }
                else {
                    MessageBox.Show("El producto " + objProducto.ClaveSubProducto + " " + objProducto.MarcaComercial + " no tiene COMPLEMENTOS EN ENTREGAS.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }               

                obj.PRODUCTO.Add(objProducto);
            }

            LstControlVolumetricoMensual = new List<Entidades.cls.clsControlVolumetricoMensual>();
            foreach (var prod in obj.PRODUCTO)
            {
                foreach (var registros in prod.REPORTEDEVOLUMENMENSUAL.ENTREGAS.Complemento.Complemento_Expendio.NACIONAL)
                {
                    Entidades.cls.clsControlVolumetricoMensual objIntesis = new Entidades.cls.clsControlVolumetricoMensual
                    {
                        RfcClienteOProveedor = registros.RfcClienteOProveedor,
                        NombreClienteOProveedor = registros.NombreClienteOProveedor,
                        CFDI = registros.CFDIs.CFDI,
                        FechaYHoraTransaccion = registros.CFDIs.FechaYHoraTransaccion,
                        ValorNumerico = registros.CFDIs.VolumenDocumentado.ValorNumerico,
                    };
                    LstControlVolumetricoMensual.Add(objIntesis);
                }
            }
            LstControlVolumetricoMensual = LstControlVolumetricoMensual.OrderBy(x => x.NombreClienteOProveedor).ThenBy(x => x.CFDI).ThenBy(x => x.ValorNumerico).ThenBy(x=>x.FechaYHoraTransaccion).ToList();
            dgvRegistrosDiario.DataSource = LstControlVolumetricoMensual;
            tsTotalRegistros.Text = dgvRegistrosDiario.RowCount.ToString("N0");

            //llenar encabezado
            txtVersion.Text = obj.Version;
            txtRfcRepresentante.Text = obj.RfcRepresentanteLegal;
            txtRfcProveedor.Text = obj.RfcProveedor;
            txtRfcContrib.Text = obj.RfcContribuyente;
            txtNoPermiso.Text = obj.Caracter.NumPermiso;
            txtSucursal.Text = Enumeraciones.CatalogSucursales().Where(x => x.Value == obj.Caracter.NumPermiso).First().Key;
            txtCaracter.Text = obj.Caracter.TipoCaracter;
            txtModPermiso.Text = obj.Caracter.ModalidadPermiso;
            txtPeriodo.Text = obj.FechaYHoraCorte.ToString();

            limpiarPanelInventarios();

            int posicionDgvInventariosY = 0;
            foreach (var pro in obj.PRODUCTO.OrderByDescending(x => x.ClaveProducto).ToList())
            {

                DataGridView dgvEncabezadoInventario = new DataGridView();

                dgvEncabezadoInventario.Columns.Add("Texto", "");
                dgvEncabezadoInventario.Columns.Add("Valor", "");
                dgvEncabezadoInventario.Name = "dgvEncabezado" + pro.ClaveProducto;
                dgvEncabezadoInventario.Rows.Add("Producto:", pro.ClaveSubProducto + " " + pro.MarcaComercial);
                dgvEncabezadoInventario.Rows.Add("INVENTARIO EN TANQUE AL FINALIZAR EL MES:", pro.REPORTEDEVOLUMENMENSUAL.CONTROLDEEXISTENCIAS.VolumenExistenciasMes.ValorNumerico);
                dgvEncabezadoInventario.Rows.Add("NÚMERO DE VECES QUE ENTRO PRODUCTO AL TANQUE:", pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.TotalRecepcionesMes);
                dgvEncabezadoInventario.Rows.Add("TOTAL DE LITROS QUE MUESTRA LA FACTURA:", pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.SumaVolumenRecepcionMes.ValorNumerico);
                panelInventarios.Controls.Add(dgvEncabezadoInventario);
                dgvEncabezadoInventario.Location = new System.Drawing.Point(10, posicionDgvInventariosY + 30);
                dgvEncabezadoInventario.Width = 1107;
                posicionDgvInventariosY = dgvEncabezadoInventario.Location.Y + dgvEncabezadoInventario.Size.Height + 15;
                dgvEncabezadoInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)))));
                dgvEncabezadoInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridView dgvPartidas = new DataGridView();
                dgvPartidas.Columns.Add("numero", "No.");
                dgvPartidas.Columns.Add("nombreCliente", "Nombre Cliente Proveedor");
                dgvPartidas.Columns.Add("rfcCliente", "Rfc Cliente Proveedor");
                dgvPartidas.Columns.Add("cfdi", "CFDI");
                dgvPartidas.Columns.Add("fechaHora", "Fecha y Hora");
                dgvPartidas.Columns.Add("precioCompra", "Precio Compra");
                dgvPartidas.Columns.Add("precioVenta", "Precio Venta Púb.");
                dgvPartidas.Columns.Add("valorNumerico", "ValorNumerico");

                panelInventarios.Controls.Add(dgvPartidas);
                dgvPartidas.Location = new System.Drawing.Point(10, posicionDgvInventariosY + 30);
                dgvPartidas.Width = 1107;
                dgvPartidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)))));
                dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //foreach partidas
                int numeral = 1;
                decimal sumaRecepciones = 0;
                if (pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento == null){
                    sumaRecepciones = 0;
                    MessageBox.Show("El producto "+pro.ClaveSubProducto+" "+pro.MarcaComercial+" no tiene COMPLEMENTOS EN RECEPCIONES.","Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var part in pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL)
                    {
                        dgvPartidas.Rows.Add(numeral, part.NombreClienteOProveedor, part.RfcClienteOProveedor, part.CFDIs.CFDI, part.CFDIs.FechaYHoraTransaccion, part.CFDIs.PrecioCompra, part.CFDIs.PrecioDeVentaAlPublico, part.CFDIs.VolumenDocumentado.ValorNumerico);
                        sumaRecepciones += part.CFDIs.VolumenDocumentado.ValorNumerico;
                        numeral++;
                    }
                }
                

                decimal diferenciaEntregadoRecepcion = pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.SumaVolumenRecepcionMes.ValorNumerico - sumaRecepciones;
                dgvPartidas.Rows.Add(null,null,null,null, null,null, "TOTAL:",sumaRecepciones);
                dgvPartidas.Rows.Add(null, null, null, "VENTA LTS. POR MES:", pro.REPORTEDEVOLUMENMENSUAL.ENTREGAS.SumaVolumenEntregado.ValorNumerico, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "DIF- FACT. VS PIPAS:", diferenciaEntregadoRecepcion, null, null,null);
                dgvPartidas.Rows.Add(null, null, null, "LA FACTURA TRAE", diferenciaEntregadoRecepcion >= 0 ? " MÁS" : " MENOS");
                posicionDgvInventariosY = dgvPartidas.Location.Y + dgvPartidas.Size.Height + 15;
                
            }
            
            

        }

        private void btnImportarLayout_Click(object sender, EventArgs e)
        {            
            ImportarExcel();
        }

        private void ImportarExcel()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string[] nombreArchivo = dlg.SafeFileName.Split('.');
                    if (nombreArchivo[1] != "xlsx")
                    {
                        MessageBox.Show("EL ARCHIVO EXCEL TIENE QUE TENER  VERSION (.xlsx)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string rutaImportado = dlg.FileName;
                    urlConexion = new ExcelQueryFactory(rutaImportado);                  

                    var query = (from a in urlConexion.Worksheet<Entidades.cls.FACTURASDETALLE>(0)
                                 select a).ToList();
                    LstFacturaDetalleSnFecha = query.Where(x=>x.fyh_trans == "  -   -  : :").ToList();
                    query = query.Where(a => a.fyh_trans!= "  -   -  : :").ToList();
                    var prueba = query.Select(x => new Entidades.cls.FACTURASDETALLE
                    {
                        folio_imp = x.folio_imp,
                        cliente = x.cliente,
                        importe = x.importe,
                        serie = x.serie,
                        docto = x.docto,
                        status = x.status,
                        fec_reg = Convert.ToDateTime(x.fyh_trans),//(x.fyh_trans!="  -   -  : :")?Convert.ToDateTime(x.fyh_trans):DateTime.Today,
                        nombrep = x.nombrep,
                        cant = Math.Round(x.cant, 2),
                        precio = x.precio,
                        imported = x.imported,
                        uuid = x.uuid != null ? x.uuid.ToUpper().Trim() : null,
                        nombre = x.nombre.Trim(),
                        fyh_trans =x.fec_reg.ToString(),

                    }).ToList();

                    prueba.AddRange(LstFacturaDetalleSnFecha);
                    lstManuales = prueba;
                    lstManuales = lstManuales.Where(x => x.status == "P").OrderBy(x => x.status).ToList();
                    lstManuales = lstManuales.Where(x => x.nombrep != null && (x.nombrep.StartsWith("Gasolina")||x.nombrep.StartsWith("GASOLINA") || x.nombrep.StartsWith("Diesel")||x.nombrep.StartsWith("DIESEL")||x.nombrep.StartsWith("COMBUSTIBLE")||x.nombrep.StartsWith("Combustible")||x.nombrep.StartsWith("combustible"))).ToList();
                    lstManuales = lstManuales.OrderBy(x => x.nombre).ThenBy(x => x.uuid).ThenBy(x => x.cant).ThenBy(x=>x.fec_reg).ToList();
                    dgvManuales.DataSource = lstManuales;
                    tsManuales.Text = dgvManuales.RowCount.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }           

            
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            //  ComparacionArchivos();
            ComparacionSistema();
        }
        
        public void UnirListados()
        {
            if(lstManuales==null || LstControlVolumetricoMensual == null)
            {
                MessageBox.Show("No se han cargado los archivos de compracion (Archivo C.V y ReporteDetalle Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            if (LstControlVolumetricoMensual.Count < lstManuales.Count)
            {
               
                int posActual = 0;
                Entidades.cls.FACTURASDETALLE ObjDetalle;
                foreach(var itemCv in LstControlVolumetricoMensual)
                {
                    ObjDetalle = lstManuales[posActual];
                    //calculando diferencia en cantidades
                    decimal diferencia = itemCv.ValorNumerico - ObjDetalle.cant;
                    diferencia = diferencia < 0 ? diferencia * -1 : diferencia;
                    if(itemCv.CFDI == ObjDetalle.uuid && (diferencia<0.1M))
                    {
                        if (posActual > 0 && 
                            (LstControlVolumetricoMensual[posActual - 1].ValorNumerico == itemCv.ValorNumerico) && 
                            (lstManuales[posActual - 1].cant == LstControlVolumetricoMensual[posActual - 1].ValorNumerico)&&
                            (itemCv.ValorNumerico!=lstManuales[posActual].cant))
                        {
                            //crea registros en blanco

                            
                            LstResultados.Add(new Entidades.cls.clsResultadosMensual
                            {
                                RfcClienteOProveedor = itemCv.RfcClienteOProveedor,
                                NombreClienteOPRoveedor = itemCv.NombreClienteOProveedor,
                                CFDI = itemCv.CFDI,
                                FechaYHoraTransaccion = itemCv.FechaYHoraTransaccion,
                                VolumenNumerico = itemCv.ValorNumerico,
                                folio_Imp = null,
                                clavecli = null,
                                NombreCliente = null,
                                //importe = 0,
                                serie = null,
                                docto = null,
                                //status = null,
                                fecha_reg = null,
                                nombrep = null,
                                Cant = 0,
                                precio = 0,
                                imported = 0,
                                UUID = null,
                                ComparaNombre = true,
                                ComparaCfdi = true,
                                ComparaLts = true,
                                Observacion = "*No se encuentra registro en Intesis.\n"
                            });
                        }
                        //se registra
                        LstResultados.Add(new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = itemCv.RfcClienteOProveedor,                           
                            NombreClienteOPRoveedor = itemCv.NombreClienteOProveedor,
                            CFDI = itemCv.CFDI,
                            FechaYHoraTransaccion = itemCv.FechaYHoraTransaccion,
                            VolumenNumerico = itemCv.ValorNumerico,
                          //  Existe = itemCv==null? ">":"<>",
                            folio_Imp = ObjDetalle.folio_imp,
                            clavecli = ObjDetalle.cliente,
                            NombreCliente = ObjDetalle.nombre,                          
                            //importe = ObjDetalle.importe,
                            serie = ObjDetalle.serie,
                            docto = ObjDetalle.docto,   
                            //status = ObjDetalle.status, 
                            fecha_reg = ObjDetalle.fec_reg,
                            nombrep = ObjDetalle.nombrep,   
                            Cant = ObjDetalle.cant,
                            precio = ObjDetalle.precio,
                            imported = ObjDetalle.imported,
                            UUID = ObjDetalle.uuid,
                            ComparaNombre = (itemCv.NombreClienteOProveedor!=ObjDetalle.nombre),
                            ComparaCfdi = (itemCv.CFDI!=ObjDetalle.uuid),
                            ComparaLts = (itemCv.ValorNumerico!=ObjDetalle.cant)
                        });
                        posActual++;
                    }
                    else
                    {
                        if((itemCv.CFDI != lstManuales[posActual].uuid))
                        {
                            //ver si existe en el otro listdo                           
                            int elementosRestantes = lstManuales.Count - posActual;
                            var lstprueba = lstManuales.GetRange(posActual, elementosRestantes).ToList();
                            bool existeEnManuales = lstManuales.GetRange(posActual, elementosRestantes).Any(x=>x.uuid==itemCv.CFDI);

                            if (existeEnManuales)
                            {
                                while ((itemCv.CFDI != lstManuales[posActual].uuid))
                                {
                                    //crea registros en blanco
                                    ObjDetalle = lstManuales[posActual];
                                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                                    {
                                        RfcClienteOProveedor = null,
                                        NombreClienteOPRoveedor = null,
                                        CFDI = null,
                                        FechaYHoraTransaccion = null,
                                        VolumenNumerico = 0,
                                        Existe = ">",
                                        folio_Imp = ObjDetalle.folio_imp,
                                        clavecli = ObjDetalle.cliente,
                                        NombreCliente = ObjDetalle.nombre,
                                        //importe = ObjDetalle.importe,
                                        serie = ObjDetalle.serie,
                                        docto = ObjDetalle.docto,
                                        //status = ObjDetalle.status,
                                        fecha_reg = ObjDetalle.fec_reg,
                                        nombrep = ObjDetalle.nombrep,
                                        Cant = ObjDetalle.cant,
                                        precio = ObjDetalle.precio,
                                        imported = ObjDetalle.imported,
                                        UUID = ObjDetalle.uuid,
                                        ComparaNombre = (itemCv.NombreClienteOProveedor != ObjDetalle.nombre),
                                        ComparaCfdi = (itemCv.CFDI != ObjDetalle.uuid),
                                        ComparaLts = (itemCv.ValorNumerico != ObjDetalle.cant),
                                        Observacion = "*No se encuentra registro en Archivo C.V.\n"
                                    });
                                    posActual++;
                                }
                            }                           
                        }
                        else if((diferencia > 0.1M))
                        {
                            //si esta igual el cfdi entonces esta mal la cantidad
                            //validar si hay mas renglones asociados a ese cfdi
                            int noCfdis = lstManuales.Where(x => x.uuid == itemCv.CFDI).Count();                            

                            while (diferencia>0.1M && noCfdis>1)
                            {                               
                                //crea registros en blanco
                               
                                LstResultados.Add(new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = null,
                                    NombreClienteOPRoveedor = null,
                                    CFDI = null,
                                    FechaYHoraTransaccion = null,
                                    VolumenNumerico = 0,
                                    Existe =  ">",
                                    folio_Imp = ObjDetalle.folio_imp,
                                    clavecli = ObjDetalle.cliente,
                                    NombreCliente = ObjDetalle.nombre,
                                    //importe = ObjDetalle.importe,
                                    serie = ObjDetalle.serie,
                                    docto = ObjDetalle.docto,
                                    //status = ObjDetalle.status,
                                    fecha_reg = ObjDetalle.fec_reg,
                                    nombrep = ObjDetalle.nombrep,
                                    Cant = ObjDetalle.cant,
                                    precio = ObjDetalle.precio,
                                    imported = ObjDetalle.imported,
                                    UUID = ObjDetalle.uuid,
                                    ComparaNombre = (itemCv.NombreClienteOProveedor != ObjDetalle.nombre),
                                    ComparaCfdi = (itemCv.CFDI != ObjDetalle.uuid),
                                    ComparaLts = (itemCv.ValorNumerico != ObjDetalle.cant),
                                    Observacion = "*No se encuentra registro en Archivo C.V.\r\n"
                                });

                              
                                posActual++;
                                ObjDetalle = lstManuales[posActual];
                                diferencia = itemCv.ValorNumerico - ObjDetalle.cant;
                                diferencia = diferencia < 0 ? diferencia * -1 : diferencia;
                            }
                        }

                        //agrega amos porque ya encontro
                        ObjDetalle = lstManuales[posActual];
                        LstResultados.Add(new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = null,
                            NombreClienteOPRoveedor = null,
                            CFDI = null,
                            FechaYHoraTransaccion = null,
                            VolumenNumerico = 0,
                            folio_Imp = ObjDetalle.folio_imp,
                            clavecli = ObjDetalle.cliente,
                            NombreCliente = ObjDetalle.nombre,
                            //importe = ObjDetalle.importe,
                            serie = ObjDetalle.serie,
                            docto = ObjDetalle.docto,
                            //status = ObjDetalle.status,
                            fecha_reg = ObjDetalle.fec_reg,
                            nombrep = ObjDetalle.nombrep,
                            Cant = ObjDetalle.cant,
                            precio = ObjDetalle.precio,
                            imported = ObjDetalle.imported,
                            UUID = ObjDetalle.uuid,
                            ComparaNombre = (itemCv.NombreClienteOProveedor != ObjDetalle.nombre),
                            ComparaCfdi = (itemCv.CFDI != ObjDetalle.uuid),
                            ComparaLts = (itemCv.ValorNumerico != ObjDetalle.cant),

                        });
                        posActual++;

                    }
                }

                
                
            }
            else
            {

                int posActual = 0;
               // Entidades.cls.FACTURASDETALLE ObjDetalle;
                Entidades.cls.clsControlVolumetricoMensual ObjCv;
                foreach (var itemDet in lstManuales)
                {
                    ObjCv = LstControlVolumetricoMensual[posActual];
                    //calculando diferencia en cantidades
                    decimal diferencia = itemDet.cant - ObjCv.ValorNumerico;
                    diferencia = diferencia < 0 ? diferencia * -1 : diferencia;
                    if (itemDet.uuid == ObjCv.CFDI && (diferencia < 0.1M))
                    {
                        //se registra
                        LstResultados.Add(new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = ObjCv.RfcClienteOProveedor,
                            NombreClienteOPRoveedor = ObjCv.NombreClienteOProveedor,
                            CFDI = ObjCv.CFDI,
                            FechaYHoraTransaccion = ObjCv.FechaYHoraTransaccion,
                            VolumenNumerico = ObjCv.ValorNumerico,
                            //  Existe = itemCv==null? ">":"<>",
                            folio_Imp = itemDet.folio_imp,
                            clavecli = itemDet.cliente,
                            NombreCliente = itemDet.nombre,
                            //importe = itemDet.importe,
                            serie = itemDet.serie,
                            docto = itemDet.docto,
                            //status = itemDet.status,
                            fecha_reg = itemDet.fec_reg,
                            nombrep = itemDet.nombrep,
                            Cant = itemDet.cant,
                            precio = itemDet.precio,
                            imported = itemDet.imported,
                            UUID = itemDet.uuid,
                            ComparaNombre = (ObjCv.NombreClienteOProveedor != itemDet.nombre),
                            ComparaCfdi = (ObjCv.CFDI != itemDet.uuid),
                            ComparaLts = (ObjCv.ValorNumerico != itemDet.cant)
                        });
                        posActual++;
                    }
                    else
                    {
                        if ((ObjCv.CFDI != LstControlVolumetricoMensual[posActual].CFDI))
                        {
                            while ((itemDet.uuid != LstControlVolumetricoMensual[posActual].CFDI))
                            {
                                //crea registros en blanco
                                ObjCv = LstControlVolumetricoMensual[posActual];
                                LstResultados.Add(new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = ObjCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = ObjCv.NombreClienteOProveedor,
                                    CFDI = ObjCv.CFDI,
                                    FechaYHoraTransaccion = ObjCv.FechaYHoraTransaccion,
                                    VolumenNumerico = ObjCv.ValorNumerico,
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    //importe = 0,
                                    serie = null,
                                    docto = null,
                                    //status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = (ObjCv.NombreClienteOProveedor != itemDet.nombre),
                                    ComparaCfdi = (ObjCv.CFDI != itemDet.uuid),
                                    ComparaLts = (ObjCv.ValorNumerico != itemDet.cant),
                                    Observacion = "*No se encuentra registro en Intesis.\n"
                                });
                                posActual++;
                            }
                        }
                        else if ((diferencia > 0.1M))
                        {
                            //si esta igual el cfdi entonces esta mal la cantidad
                            //validar si hay mas renglones asociados a ese cfdi
                            int noCfdis = lstManuales.Where(x => x.uuid == itemDet.uuid).Count();

                            while (diferencia > 0.1M && noCfdis > 1)
                            {
                                //crea registros en blanco

                                LstResultados.Add(new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = ObjCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = ObjCv.NombreClienteOProveedor,
                                    CFDI = ObjCv.CFDI,
                                    FechaYHoraTransaccion = ObjCv.FechaYHoraTransaccion,
                                    VolumenNumerico = ObjCv.ValorNumerico,
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    //importe = 0,
                                    serie = null,
                                    docto = null,
                                    //status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = (ObjCv.NombreClienteOProveedor != itemDet.nombre),
                                    ComparaCfdi = (ObjCv.CFDI != itemDet.uuid),
                                    ComparaLts = (ObjCv.ValorNumerico != itemDet.cant),
                                    Observacion = "*No se encuentra registro en Intesis.\r\n"
                                });


                                posActual++;
                                ObjCv = LstControlVolumetricoMensual[posActual];
                                diferencia = itemDet.cant - ObjCv.ValorNumerico;
                                diferencia = diferencia < 0 ? diferencia * -1 : diferencia;
                            }
                        }

                        //agrega amos porque ya encontro
                        ObjCv = LstControlVolumetricoMensual[posActual];
                        LstResultados.Add(new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = ObjCv.RfcClienteOProveedor,
                            NombreClienteOPRoveedor = ObjCv.NombreClienteOProveedor,
                            CFDI = ObjCv.CFDI,
                            FechaYHoraTransaccion = ObjCv.FechaYHoraTransaccion,
                            VolumenNumerico = ObjCv.ValorNumerico,
                            //  Existe = itemCv == null ? ">" : "<>",
                            folio_Imp = null,
                            clavecli = null,
                            NombreCliente = null,
                            //importe = 0,
                            serie = null,
                            docto = null,
                            //status = null,
                            fecha_reg = null,
                            nombrep = null,
                            Cant = 0,
                            precio = 0,
                            imported = 0,
                            UUID = null,
                            ComparaNombre = (itemDet.nombre != ObjCv.NombreClienteOProveedor),
                            ComparaCfdi = (itemDet.uuid != ObjCv.CFDI),
                            ComparaLts = (itemDet.cant != ObjCv.ValorNumerico),

                        });
                        posActual++;

                    }
                }







            }

            foreach (var registro in LstResultados)
            {
                string observacion = "";
                if (registro.Cant != registro.VolumenNumerico && registro.CFDI != null)
                {
                    observacion += "*Las cantidades no cinciden.\r\n";
                    registro.Existe = "<>";
                }

                if (registro.NombreCliente != registro.NombreClienteOPRoveedor && registro.CFDI != null)
                {
                    observacion += "*El nombre del cliente no coincide.\r\n";
                    registro.Existe = "<>";
                }

                if (!string.IsNullOrEmpty(observacion))
                {
                    registro.Observacion = observacion;
                }
                registro.DiferenciaCantidades = (registro.Cant - registro.VolumenNumerico) < 0 ? ((registro.Cant - registro.VolumenNumerico) * -1) : registro.Cant - registro.VolumenNumerico;
            }
            LstResultados = LstResultados.Where(x => x.Observacion != null && x.DiferenciaCantidades > .01M).ToList();

            dgvErrores.DataSource = LstResultados;
            tsErrores.Text = dgvErrores.RowCount.ToString("N0");

            MessageBox.Show("Comparacion exitosa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvErrores.RowCount <= 0)
            {
                MessageBox.Show("No se han cargado los archivos de compracion (Archivo CV y Detalle Mensual)", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Rev Inf Arch Mensual";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string rutaSalida = dlg.FileName+".xlsx";
                //byte[] excelResult;
                SLDocument sl = new SLDocument();
                sl.SetCellValue("A1", "CONTENIDO DE ARCHIVO CONTROL VOLUMETRICO");
                sl.SetCellValue("G1", "CONTENIDO EN FACTURACION");
                sl.SetCellValue("R1", "COMPARACIÓN");
                SLStyle styleEncabezados = sl.CreateStyle();
                SLStyle styleCantidades = sl.CreateStyle();
                SLStyle styleColorCV = sl.CreateStyle();
                SLStyle styleColorFacturacion = sl.CreateStyle();
                SLStyle styleColorComparacion = sl.CreateStyle();

                //colores
                SLThemeSettings themeFacturacion = new SLThemeSettings();
                themeFacturacion.ThemeName = "colorFacturacion";
                themeFacturacion.Accent1Color = System.Drawing.Color.Aquamarine;
                themeFacturacion.Accent2Color = System.Drawing.Color.Yellow;
                //stylo encabezados
                styleEncabezados.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                styleEncabezados.Font.Bold = true;
                //stylo cantidades
                styleCantidades.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //stylo color cv
                styleColorCV.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Accent1Color, SLThemeColorIndexValues.Accent2Color);
                //color facturacion
                styleColorFacturacion.Fill.SetPattern(PatternValues.Solid, themeFacturacion.Accent1Color, themeFacturacion.Accent1Color);
                //color comparacion
                styleColorComparacion.Fill.SetPattern(PatternValues.Solid, themeFacturacion.Accent2Color, themeFacturacion.Accent2Color);
                sl.SetCellStyle("A1", styleEncabezados);
                sl.SetCellStyle("G1", styleEncabezados);
                sl.SetCellStyle("R1", styleEncabezados);               

                sl.MergeWorksheetCells("A1", "E1");
                sl.MergeWorksheetCells("G1","Q1");
                sl.SetCellStyle("R1", styleColorComparacion);
                sl.MergeWorksheetCells("R1", "V1");

                sl.SetCellValue("A2", "RFC");
                sl.SetCellValue("B2", "Nombre Cliente");
                sl.SetCellValue("C2", "CFDI Cliente");
                sl.SetCellValue("D2", "Fecha Y Hora de Generación");
                sl.SetCellValue("E2", "Litros de Venta");
                sl.SetCellValue("F2", "");
                sl.SetCellValue("G2", "folio_imp");
                sl.SetCellValue("H2", "No. Cliente");
                sl.SetCellValue("I2", "Nom. Cliente");
                sl.SetCellValue("J2", "Serie Factura");
                sl.SetCellValue("K2", "Folio Factura");
                sl.SetCellValue("L2", "Fecha Y Hora Generación");
                sl.SetCellValue("M2", "Producto");
                sl.SetCellValue("N2", "Litros por Venta");
                sl.SetCellValue("O2", "Precio por Litro");
                sl.SetCellValue("P2", "Importe de Venta");
                sl.SetCellValue("Q2", "CFDI");
                sl.SetCellValue("R2", "Compara Nombre");
                sl.SetCellValue("S2", "Compara CFDI");
                sl.SetCellValue("T2", "Compara Litros");
                sl.SetCellValue("U2", "Diferencia de Lts o ML");                
                sl.SetCellValue("V2", "Observaciones");

                int noRows = 2;

                if (chkMargen.Checked)
                {
                    LstResultadosAux = LstResultados.Where(x => x.Observacion != null && x.DiferenciaCantidades > 0.1M).ToList();
                    noRows += LstResultadosAux.Count();
                    //IExport<Entidades.cls.clsResultadosMensual> AccountExport = new ExcelWriter<Entidades.cls.clsResultadosMensual>();
                    //excelResult = AccountExport.Export(LstResultadosAux);
                    GenerarRowsExcel(LstResultadosAux, sl);
                }
                else {
                    //IExport<Entidades.cls.clsResultadosMensual> AccountExport = new ExcelWriter<Entidades.cls.clsResultadosMensual>();
                    //excelResult = AccountExport.Export(LstResultados);
                    noRows += LstResultados.Count();
                    GenerarRowsExcel(LstResultados, sl);
                }

                sl.SetColumnStyle(5, styleCantidades);
                sl.SetColumnStyle(8, styleCantidades);
                sl.SetColumnStyle(11, styleCantidades);
                sl.SetColumnStyle(14, styleCantidades);
                sl.SetColumnStyle(15, styleCantidades);
                sl.SetColumnStyle(16, styleCantidades);
                sl.SetColumnStyle(21, styleCantidades);
                
                sl.SetCellStyle(3, 1,noRows, 5, styleColorCV );
                sl.SetCellStyle(3, 7, noRows, 17, styleColorFacturacion);

                sl.SetColumnWidth(1,22,27);
                sl.SetColumnWidth(6, 3);

                sl.SetRowStyle(2, styleEncabezados);


                sl.SaveAs(rutaSalida);
                //File.WriteAllBytes(rutaSalida, excelResult);
                MessageBox.Show("Se han exportado los registros.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void GenerarRowsExcel(List<Entidades.cls.clsResultadosMensual> LstResultadosExcel, SLDocument objExcel)
        {
            int index = 3;
            foreach(var row in LstResultadosExcel)
            {
                objExcel.SetCellValue("A" + index, row.RfcClienteOProveedor);
                objExcel.SetCellValue("B"+index, row.NombreClienteOPRoveedor);
                objExcel.SetCellValue("C"+index, row.CFDI);
                objExcel.SetCellValue("D"+index, row.FechaYHoraTransaccion.ToString());
                objExcel.SetCellValue("E"+index, row.VolumenNumerico);
                objExcel.SetCellValue("F"+index, row.Existe);
                objExcel.SetCellValue("G"+index, row.folio_Imp);
                objExcel.SetCellValue("H"+index, row.clavecli);
                objExcel.SetCellValue("I"+index, row.NombreCliente);
                objExcel.SetCellValue("J"+index, row.serie);
                objExcel.SetCellValue("K"+index, row.docto);
                objExcel.SetCellValue("L"+index, row.fecha_reg.ToString());
                objExcel.SetCellValue("M"+index, row.nombrep);
                objExcel.SetCellValue("N"+index, row.Cant);
                objExcel.SetCellValue("O"+index, row.precio);
                objExcel.SetCellValue("P"+index, row.imported);
                objExcel.SetCellValue("Q"+index, row.UUID);
                objExcel.SetCellValue("R"+index, row.ComparaNombre);
                objExcel.SetCellValue("S"+index, row.ComparaCfdi);
                objExcel.SetCellValue("T"+index, row.ComparaLts);
                objExcel.SetCellValue("U"+index, row.DiferenciaCantidades);
                objExcel.SetCellValue("V"+index, row.Observacion);

                index++;
            }
            
        }

        private void LimpiarFormulario()
        {
            if (MessageBox.Show("Se perderá la información ingresada. ¿Deseas continuar?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LimpiarControlesFormulario();
                lstManuales = new List<Entidades.cls.FACTURASDETALLE>();
                LstControlVolumetricoMensual = new List<Entidades.cls.clsControlVolumetricoMensual>();
                LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();  
        }

        private void LimpiarControlesFormulario()
        {
            foreach(var cont in this.Controls)
            {
                if(cont is GroupBox)
                {
                    foreach (var gctrl in ((GroupBox)cont).Controls)
                    {
                        if (gctrl is TextBox)
                        {
                            ((TextBox)gctrl).Clear();
                        }
                        else if (gctrl is DataGridView)
                        {
                            ((DataGridView)gctrl).DataSource = null;
                        }
                        else if (gctrl is ToolStrip)
                        {
                            ((ToolStrip)gctrl).Items[1].Text = 0.ToString("N0");
                        }
                    }
                }else if(cont is TabControl)
                {
                    foreach(var tabPage in ((TabControl)cont).TabPages)
                    {
                        foreach (var gctrl in ((TabPage)tabPage).Controls)
                        {
                            if(gctrl is Panel)
                            {
                                ((Panel)gctrl).Controls.Clear();
                            }
                            if (gctrl is GroupBox)
                            {
                                foreach (var tgctrl in ((GroupBox)gctrl).Controls)
                                {
                                    if (tgctrl is TextBox)
                                    {
                                        ((TextBox)tgctrl).Clear();
                                    }
                                    else if (tgctrl is DataGridView)
                                    {
                                        ((DataGridView)tgctrl).DataSource = null;
                                    }
                                    else if (tgctrl is ToolStrip)
                                    {
                                        ((ToolStrip)tgctrl).Items[1].Text = 0.ToString("N0");
                                    }
                                }
                            }
                            else if (gctrl is TextBox)
                            {
                                ((TextBox)gctrl).Clear();
                            }
                            else if (gctrl is DataGridView)
                            {
                                ((DataGridView)gctrl).DataSource = null;
                            }
                            else if (gctrl is ToolStrip)
                            {
                                ((ToolStrip)gctrl).Items[1].Text = 0.ToString("N0");
                            }
                        }
                    }
                   
                }
            }
            
            
        }

        private void ComparacionArchivos()
        {
            if (lstManuales == null || LstControlVolumetricoMensual == null)
            {
                MessageBox.Show("No se han cargado los archivos de compracion (Archivo C.V y ReporteDetalle Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensualAux = LstControlVolumetricoMensual;
            List<Entidades.cls.FACTURASDETALLE> lstManualesAux = lstManuales;

            Entidades.cls.clsResultadosMensual ObjResultado;

            var lstUUIDsManuales = lstManualesAux.Select(x => x.uuid).Distinct().ToList();
            var lstCFDICv = LstControlVolumetricoMensualAux.Select(x => x.CFDI).Distinct().ToList();

            var lstUnionListas =
            lstUUIDsManuales.Intersect(lstCFDICv).ToList();

            var lstNoexisten = lstUUIDsManuales.Except(lstUnionListas).ToList();
            lstNoexisten.AddRange(lstCFDICv.Except(lstUnionListas).ToList());


            //excuir en las listas principales
            lstManualesAux = (
                    from m in lstManuales
                    where !lstNoexisten.Contains(m.uuid)
                    select m).ToList();

            //reordenar listas
            LstControlVolumetricoMensualAux = (
                from cv in LstControlVolumetricoMensual
                where !lstNoexisten.Contains(cv.CFDI)
                select cv).ToList();



            foreach (var itemE in lstUnionListas)
            {
                var lstCoincidenciasManuales = lstManualesAux.Where(x => x.uuid == itemE).ToList();
                var lstCvCoincidencias = LstControlVolumetricoMensualAux.Where(x => x.CFDI == itemE).ToList();

                lstCoincidenciasManuales = lstCoincidenciasManuales.OrderBy(x => x.nombre).ThenBy(x => x.uuid).ThenBy(x=>x.fec_reg).ThenBy(x => x.cant).ToList();
                lstCvCoincidencias = lstCvCoincidencias.OrderBy(x => x.NombreClienteOProveedor).ThenBy(x => x.CFDI).ThenBy(x=>x.FechaYHoraTransaccion).ThenBy(x => x.ValorNumerico).ToList();
                                        
                        lstCoincidenciasManuales = lstCoincidenciasManuales.Where(x => x.uuid == "64D0F321-3EF4-4182-B8B4-352EA757DA3C").OrderBy(x => x.cant).ToList();
                        lstCvCoincidencias = lstCvCoincidencias.Where(x => x.CFDI == "64D0F321-3EF4-4182-B8B4-352EA757DA3C").OrderBy(x => x.ValorNumerico).ToList();
                       
                        int indexInicioMan = 0;
                        int indexInicioManAux = 0;
                        Entidades.cls.FACTURASDETALLE ObjMan;
                        List<Entidades.cls.clsControlVolumetricoMensual> LstAuxCv = new List<Entidades.cls.clsControlVolumetricoMensual>();
                        List<Entidades.cls.FACTURASDETALLE> LstAuxManual = new List<Entidades.cls.FACTURASDETALLE>();
                        LstAuxCv.AddRange(lstCvCoincidencias);
                        LstAuxManual.AddRange(lstCoincidenciasManuales);
                        int indexInicioCv = 0;
                        foreach (var objCv in lstCvCoincidencias)
                        {
                            indexInicioCv++;
                            decimal diferencia = 0;
                            if(indexInicioMan < lstCoincidenciasManuales.Count)
                            {
                                indexInicioManAux = indexInicioMan;
                            }
                            else
                            {
                                indexInicioMan = indexInicioManAux;
                            }
                            bool existe = false;
                            while (indexInicioMan < lstCoincidenciasManuales.Count)
                            {
                                ObjMan = lstCoincidenciasManuales[(indexInicioMan)];                              
                                diferencia = decimal.Round(ObjMan.cant,2) - objCv.ValorNumerico;
                            //agregar condicion de fierencia negativa
                                if (diferencia < 0M)
                                {
                                    diferencia = diferencia * -1;
                                }
                                if (diferencia < 0.10M && objCv.FechaYHoraTransaccion==ObjMan.fec_reg)
                                {


                            //registro normal
                            ObjResultado = new Entidades.cls.clsResultadosMensual
                                            {
                                                RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                                NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                                CFDI = objCv.CFDI,
                                                FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                                VolumenNumerico = objCv.ValorNumerico,
                                                Existe = diferencia > 0 ? "<>" : null,
                                                folio_Imp = ObjMan.folio_imp,
                                                clavecli = ObjMan.cliente,
                                                NombreCliente = ObjMan.nombre,
                                                //importe = ObjMan.importe,
                                                serie = ObjMan.serie,
                                                docto = ObjMan.docto,
                                                //status = ObjMan.status,
                                                fecha_reg = ObjMan.fec_reg,
                                                nombrep = ObjMan.nombrep,
                                                Cant = decimal.Round(ObjMan.cant, 2),
                                                precio = ObjMan.precio,
                                                imported = ObjMan.imported,
                                                UUID = ObjMan.uuid,
                                                ComparaNombre = ObjMan.nombre == objCv.NombreClienteOProveedor ? true : false,
                                                ComparaCfdi = ObjMan.uuid == objCv.CFDI ? true : false,
                                                ComparaLts = diferencia > 0 ? false : true,
                                                Observacion = diferencia > 0 ? "Las cantidades no coinciden.\r\n" : null
                                            };
                                        LstResultados.Add(ObjResultado);
                                        existe = true;
                                        indexInicioMan++;
                                        int index = LstAuxManual.FindIndex(x => x.cant == ObjMan.cant);
                                        if (index > -1)
                                        {
                                            LstAuxManual.RemoveAt(index);
                                        } 
                                        break;
                                }
                                else
                                {
                                    // se sigue  poque no encuentra similitud                                    
                                    indexInicioMan++;                                    
                                }
                                
                            }
                          
                            if (indexInicioMan >= lstCoincidenciasManuales.Count&&!existe)//y agregar que si esta en la lista
                            {
                                ObjResultado = new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                    CFDI = objCv.CFDI,
                                    FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                    VolumenNumerico = objCv.ValorNumerico,
                                    Existe = "<",
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    //importe = 0,
                                    serie = null,
                                    docto = null,
                                    //status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "NO EXISTE EN FACTURACIÓN"
                                };
                                LstResultados.Add(ObjResultado);
                                //registras como que solo estan en cv
                            }
                            LstAuxCv.RemoveAt(LstAuxCv.FindIndex(x=>x.ValorNumerico==objCv.ValorNumerico&&x.FechaYHoraTransaccion==objCv.FechaYHoraTransaccion));
                        } 
                        int cntCv = LstAuxCv.Count;
                        int cntman = LstAuxManual.Count;
                        if (cntman > 0)
                        {
                            foreach(var sob in LstAuxManual)
                            {
                                ObjResultado = new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = null,
                                    NombreClienteOPRoveedor = null,
                                    CFDI = null,
                                    FechaYHoraTransaccion = null,
                                    VolumenNumerico = 0,
                                    Existe = ">",
                                    folio_Imp = sob.folio_imp,
                                    clavecli = sob.cliente,
                                    NombreCliente = sob.nombre,
                                    //importe = objMan.importe,
                                    serie = sob.serie,
                                    docto = sob.docto,
                                    //status = objMan.status,
                                    fecha_reg = sob.fec_reg,
                                    nombrep = sob.nombrep,
                                    Cant = sob.cant,
                                    precio = sob.precio,
                                    imported = sob.precio,
                                    UUID = sob.uuid,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "NO EXISTE EN ARCHIVO XML"
                                };
                                LstResultados.Add(ObjResultado);
                            }
                        }else if (cntCv>0)
                        {
                            foreach (var sob in LstAuxCv)
                            {
                                ObjResultado = new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = sob.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = sob.NombreClienteOProveedor,
                                    CFDI = sob.CFDI,
                                    FechaYHoraTransaccion = sob.FechaYHoraTransaccion,
                                    VolumenNumerico = sob.ValorNumerico,
                                    Existe = "<",
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    //importe = 0,
                                    serie = null,
                                    docto = null,
                                    //status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "NO EXISTE EN FACTURACIÓN"
                                };
                                LstResultados.Add(ObjResultado);

                            }                        
                        }
            }


            //registrar no existentes

            foreach(var itemNoExiste in lstNoexisten)
            {
                if (lstManuales.Any(x=>x.uuid ==itemNoExiste))
                {
                    var lstNoMan = lstManuales.Where(x => x.uuid == itemNoExiste).ToList();
                    foreach (var itemMan in lstNoMan)
                    {
                        ObjResultado = new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = null,
                            NombreClienteOPRoveedor = null,
                            CFDI = null,
                            FechaYHoraTransaccion = null,
                            VolumenNumerico = 0,
                            Existe = ">",
                            folio_Imp = itemMan.folio_imp,
                            clavecli = itemMan.cliente,
                            NombreCliente = itemMan.nombre,
                            //importe = itemMan.importe,
                            serie = itemMan.serie,
                            docto = itemMan.docto,
                            //status = itemMan.status,
                            fecha_reg = itemMan.fec_reg,
                            nombrep = itemMan.nombrep,
                            Cant = decimal.Round(itemMan.cant, 2),
                            precio = itemMan.precio,
                            imported = itemMan.imported,
                            UUID = itemMan.uuid,
                            ComparaNombre = false,
                            ComparaCfdi = false,
                            ComparaLts = false,
                            Observacion = "NO EXISTE EN ARCHIVO XLM"
                        };
                        LstResultados.Add(ObjResultado);
                    }
                }
                else
                {
                    var lstNoCv = LstControlVolumetricoMensual.Where(x => x.CFDI == itemNoExiste).ToList();
                    foreach (var itemCv in lstNoCv)
                    {
                        ObjResultado = new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = itemCv.RfcClienteOProveedor,
                            NombreClienteOPRoveedor = itemCv.NombreClienteOProveedor,
                            CFDI = itemCv.CFDI,
                            FechaYHoraTransaccion = itemCv.FechaYHoraTransaccion,
                            VolumenNumerico = itemCv.ValorNumerico,
                            Existe = "<",
                            folio_Imp = null,
                            clavecli = null,
                            NombreCliente = null,
                            //importe = 0,
                            serie = null,
                            docto = null,
                            //status = null,
                            fecha_reg = null,
                            nombrep = null,
                            Cant = 0,
                            precio = 0,
                            imported = 0,
                            UUID = null,
                            ComparaNombre = false,
                            ComparaCfdi = false,
                            ComparaLts = false,
                            Observacion = "NO EXISTE EN FACTURACIÓN"
                        };
                        LstResultados.Add(ObjResultado);
                    }
                }
                //registrar de lado de los cv
               
            }

            foreach(var res in LstResultados)
            {
                decimal diferencia = decimal.Round(res.Cant, 2) - res.VolumenNumerico;
                diferencia = diferencia < 0 ? (diferencia * -1) : diferencia;
                res.DiferenciaCantidades = diferencia;
            }

            if (chkMargen.Checked)
            {
                LstResultados = LstResultados.Where(x => x.Observacion != null && x.DiferenciaCantidades >= 0.1M).ToList();
            }
            else
            {
                LstResultados = LstResultados.Where(x => x.Observacion != null && x.DiferenciaCantidades > 0.01M).ToList();
            }
            dgvErrores.DataSource = LstResultados;
            tsErrores.Text =  dgvErrores.RowCount.ToString("N0");


        }

        private void btnXmlJson_Click(object sender, EventArgs e)
        {            
            CargarArchivo();
        }

        private void limpiarPanelInventarios()
        {
            foreach (var cont in this.Controls)
            {
                
                if (cont is TabControl)
                {
                    foreach (var tabPage in ((TabControl)cont).TabPages)
                    {
                        foreach (var gctrl in ((TabPage)tabPage).Controls)
                        {
                            
                            if (gctrl is Panel)
                            {
                                if(((Panel)gctrl).Name!= "panelInventariosMain")
                                {
                                    ((Panel)gctrl).Controls.Clear();
                                }
                                
                            }                           
                        }
                    }

                }
            }
        }


        private void ComparacionSistema()
        {
            if (lstManuales == null || LstControlVolumetricoMensual == null)
            {
                MessageBox.Show("No se han cargado los archivos de compracion (Archivo C.V y ReporteDetalle Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Entidades.cls.FACTURASDETALLE>lstManualesAux = lstManuales;
            List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensualAux = LstControlVolumetricoMensual;

            var lstUUIDsManuales = lstManualesAux.Select(x => x.uuid).Distinct().ToList();
            var lstCFDICv = LstControlVolumetricoMensualAux.Select(x => x.CFDI).Distinct().ToList();

            var lstUnionListas =
                    lstUUIDsManuales.Intersect(lstCFDICv).ToList();


                    var lstNoexisten = lstUUIDsManuales.Except(lstUnionListas).ToList();
            lstNoexisten.AddRange(lstCFDICv.Except(lstUnionListas).ToList()); //aqui hay une rror porque esta duplicando las que no existen


            //excuir en las listas principales
            lstManualesAux = (
                    from m in lstManuales
                    where !lstNoexisten.Contains(m.uuid)
                    select m).ToList();

            //reordenar listas
            LstControlVolumetricoMensualAux = (
                from cv in LstControlVolumetricoMensual
                where !lstNoexisten.Contains(cv.CFDI)
                select cv).ToList();

            //obteniendo y excluyendo facturas directas (no estan en xlm)
            List<Entidades.cls.FACTURASDETALLE> lstFacturasDirectasManuales = lstManualesAux.Where(x => x.fyh_trans == "  -   -  : :").ToList();
            lstManualesAux = lstManualesAux.Where(x => x.fyh_trans != "  -   -  : :").ToList();


            List<Entidades.cls.clsResultadosMensual> LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            
            foreach (var itemCfdi in lstUnionListas)
            {                
                var lstFechaManuales = lstManualesAux.Where(x=>x.uuid==itemCfdi).Select(x=>x.fec_reg).Distinct().ToList();
                var lstFechaCv = LstControlVolumetricoMensualAux.Where(x => x.CFDI == itemCfdi).Select(x=>x.FechaYHoraTransaccion).Distinct().ToList();

                if (lstFechaManuales.Count != lstFechaCv.Count)
                {
                    var fechas = (from fman in lstFechaManuales
                                 join fcv in lstFechaCv on fman equals fcv into fmc
                                 from fcv in fmc.DefaultIfEmpty()
                                 select new
                                 {
                                     fman,fcv
                                 }).ToList();

                    var fechas2 = (from fcv in lstFechaCv
                                   join fman in lstFechaManuales on fcv equals fman into fmc
                                   from fman in fmc.DefaultIfEmpty()
                                   select new
                                   {
                                       fcv, fman
                                   }).ToList();

                    //fman
                    var lstIrregulares = fechas.Where(x=>x.fcv!=x.fman).ToList();
                    //fcv
                    var lstIrregulares2 = fechas2.Where(x => x.fcv != x.fman).ToList();

                    foreach(var item in lstIrregulares)
                    {
                        foreach(var noObjDet in lstManuales.Where(x=>x.uuid==itemCfdi&&x.fec_reg == item.fman).ToList())
                        {
                            LstResultados.Add(
                           new Entidades.cls.clsResultadosMensual
                           {
                               RfcClienteOProveedor = null,
                               NombreClienteOPRoveedor = null,
                               CFDI = null,
                               FechaYHoraTransaccion = null,
                               VolumenNumerico = 0,
                               Existe = ">",
                               folio_Imp = noObjDet.folio_imp,
                               clavecli = noObjDet.cliente,
                               NombreCliente = noObjDet.nombre,
                               serie = noObjDet.serie,
                               docto = noObjDet.docto,
                               fecha_reg = noObjDet.fec_reg,
                               nombrep = noObjDet.nombrep,
                               Cant = noObjDet.cant,
                               precio = noObjDet.precio,
                               imported = noObjDet.imported,
                               UUID = noObjDet.uuid,
                               ComparaNombre = true,
                               ComparaCfdi = true,
                               ComparaLts = true,
                               Observacion = "NO EXISTE EN ARCHIVO C.V.",
                               DiferenciaCantidades = noObjDet.cant
                           });
                        }                        
                    }

                    foreach (var item in lstIrregulares2)
                    {
                        foreach(var objCv in LstControlVolumetricoMensual.Where(x=>x.CFDI==itemCfdi&&x.FechaYHoraTransaccion==item.fcv))
                        {
                            LstResultados.Add(
                                new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                    CFDI = objCv.CFDI,
                                    FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                    VolumenNumerico = objCv.ValorNumerico,
                                    Existe = "<",
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    serie = null,
                                    docto = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "NO EXISTE EN FACTURACIÓN",
                                    DiferenciaCantidades = objCv.ValorNumerico
                                });
                        }
                    }
                }

             

                //unir fechas
                var lstUnionFechas = lstFechaManuales.Intersect(lstFechaCv).OrderBy(x=>x).ToList();//ver q pasa aqui
                var lstCoincidenFecha = lstUnionFechas.Except(lstFechaCv).ToList();//las q no coinciden

                foreach(var fecha in lstUnionFechas)
                {

                    var lstCoincidenciasMan = lstManualesAux.Where(x=>x.uuid==itemCfdi&&x.fec_reg== fecha).OrderBy(x=>x.nombre).ThenBy(x=>x.cant).ToList();
                    var lstCoincidenciasCv = LstControlVolumetricoMensualAux.Where(x=>x.CFDI==itemCfdi&&x.FechaYHoraTransaccion==fecha).OrderBy(x=>x.NombreClienteOProveedor).ThenBy(x=>x.FechaYHoraTransaccion).ToList();

                  
                    foreach (var objCv in lstCoincidenciasCv)
                    {
                        int indexMan = 0;
                        bool existe = false;
                        Entidades.cls.FACTURASDETALLE objDet = new Entidades.cls.FACTURASDETALLE();
                        decimal diferencia = 0;
                        while (indexMan < lstCoincidenciasMan.Count)
                        {
                            if(objCv.FechaYHoraTransaccion == lstCoincidenciasMan[indexMan].fec_reg)
                            {
                                diferencia = objCv.ValorNumerico - lstCoincidenciasMan[indexMan].cant;
                                if (diferencia < 0)
                                {
                                    diferencia = diferencia * -1;
                                }
                                if (diferencia < 0.10M)
                                {
                                    //existe
                                    objDet = lstCoincidenciasMan[indexMan];
                                    existe = true;
                                    break;
                                }
                                
                            }
                            indexMan++;

                        }
                        if (existe)
                        {
                            lstCoincidenciasMan.RemoveAt(indexMan);
                            LstResultados.Add(
                                new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                    CFDI = objCv.CFDI,
                                    FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                    VolumenNumerico = objCv.ValorNumerico,
                                    Existe = diferencia!=0?"<>":"",
                                    folio_Imp = objDet.folio_imp,
                                    clavecli = objDet.cliente,
                                    NombreCliente = objDet.nombre,
                                    serie = objDet.serie,
                                    docto = objDet.docto,
                                    fecha_reg = objDet.fec_reg,
                                    nombrep = objDet.nombrep,
                                    Cant = objDet.cant,
                                    precio = objDet.precio,
                                    imported = objDet.imported,
                                    UUID = objDet.uuid,
                                    ComparaNombre = true,
                                    ComparaCfdi = true,
                                    ComparaLts = true,
                                    Observacion = diferencia!=0?"Las cantidades no cuadran.":"",
                                    DiferenciaCantidades = diferencia
                                });
                        }
                        else
                        {
                            //registrar NO EXISTE EN FACTURACION
                            LstResultados.Add(
                                new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                    NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                    CFDI = objCv.CFDI,
                                    FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                    VolumenNumerico = objCv.ValorNumerico,
                                    Existe = "<",
                                    folio_Imp = null,
                                    clavecli = null,
                                    NombreCliente = null,
                                    serie = null,
                                    docto = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "NO EXISTE EN FACTURACIÓN",
                                    DiferenciaCantidades = diferencia
                                });
                        }

                        
                        
                    }

                    if (lstCoincidenciasMan.Count > 0)
                    {
                        //recorres y registras NO EXISTE EN CV
                        foreach (var noObjDet in lstCoincidenciasMan)
                        {
                            LstResultados.Add(
                           new Entidades.cls.clsResultadosMensual
                           {
                               RfcClienteOProveedor = null,
                               NombreClienteOPRoveedor = null,
                               CFDI = null,
                               FechaYHoraTransaccion = null,
                               VolumenNumerico = 0,
                               Existe = ">",
                               folio_Imp = noObjDet.folio_imp,
                               clavecli = noObjDet.cliente,
                               NombreCliente = noObjDet.nombre,
                               serie = noObjDet.serie,
                               docto = noObjDet.docto,
                               fecha_reg = noObjDet.fec_reg,
                               nombrep = noObjDet.nombrep,
                               Cant = noObjDet.cant,
                               precio = noObjDet.precio,
                               imported = noObjDet.imported,
                               UUID = noObjDet.uuid,
                               ComparaNombre = true,
                               ComparaCfdi = true,
                               ComparaLts = true,
                               Observacion = "NO EXISTE EN ARCHIVO C.V.",
                               DiferenciaCantidades = noObjDet.cant
                           });
                        }
                    }

                }


            }
            
            //recorrer los que no existen
            foreach(var rowNoExiste in lstNoexisten)
            {
                if (lstManuales.Any(x => x.uuid == rowNoExiste))
                {
                    var noObjDet = lstManuales.Where(x => x.uuid == rowNoExiste).First();

                    LstResultados.Add(
                          new Entidades.cls.clsResultadosMensual
                          {
                              RfcClienteOProveedor = null,
                              NombreClienteOPRoveedor = null,
                              CFDI = null,
                              FechaYHoraTransaccion = null,
                              VolumenNumerico = 0,
                              Existe = ">",
                              folio_Imp = noObjDet.folio_imp,
                              clavecli = noObjDet.cliente,
                              NombreCliente = noObjDet.nombre,
                              serie = noObjDet.serie,
                              docto = noObjDet.docto,
                              fecha_reg = noObjDet.fec_reg,
                              nombrep = noObjDet.nombrep,
                              Cant = noObjDet.cant,
                              precio = noObjDet.precio,
                              imported = noObjDet.imported,
                              UUID = noObjDet.uuid,
                              ComparaNombre = true,
                              ComparaCfdi = true,
                              ComparaLts = true,
                              Observacion = "NO EXISTE EN ARCHIVO C.V.",
                              DiferenciaCantidades = noObjDet.cant
                          });

                }
                else if(LstControlVolumetricoMensual.Any(x=>x.CFDI == rowNoExiste))
                {
                    var objCv = LstControlVolumetricoMensual.Where(x=>x.CFDI==rowNoExiste).First();
                    LstResultados.Add(
                               new Entidades.cls.clsResultadosMensual
                               {
                                   RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                                   NombreClienteOPRoveedor = objCv.NombreClienteOProveedor,
                                   CFDI = objCv.CFDI,
                                   FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                                   VolumenNumerico = objCv.ValorNumerico,
                                   Existe = "<",
                                   folio_Imp = null,
                                   clavecli = null,
                                   NombreCliente = null,
                                   serie = null,
                                   docto = null,
                                   fecha_reg = null,
                                   nombrep = null,
                                   Cant = 0,
                                   precio = 0,
                                   imported = 0,
                                   UUID = null,
                                   ComparaNombre = false,
                                   ComparaCfdi = false,
                                   ComparaLts = false,
                                   Observacion = "NO EXISTE EN FACTURACIÓN",
                                   DiferenciaCantidades = objCv.ValorNumerico
                               });
                   
                }
            }

            //recorrer los que no tienen fy h registro 
            foreach(var noObjDet in lstFacturasDirectasManuales)
            {
                LstResultados.Add(
                        new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = null,
                            NombreClienteOPRoveedor = null,
                            CFDI = null,
                            FechaYHoraTransaccion = null,
                            VolumenNumerico = 0,
                            Existe = ">",
                            folio_Imp = noObjDet.folio_imp,
                            clavecli = noObjDet.cliente,
                            NombreCliente = noObjDet.nombre,
                            serie = noObjDet.serie,
                            docto = noObjDet.docto,
                            fecha_reg = noObjDet.fec_reg,
                            nombrep = noObjDet.nombrep,
                            Cant = noObjDet.cant,
                            precio = noObjDet.precio,
                            imported = noObjDet.imported,
                            UUID = noObjDet.uuid,
                            ComparaNombre = true,
                            ComparaCfdi = true,
                            ComparaLts = true,
                            Observacion = "NO EXISTE EN ARCHIVO C.V.",
                            DiferenciaCantidades = noObjDet.cant
                        });
            }


            if (!chkTodo.Checked)
            {
                if (chkMargen.Checked)
                {
                    LstResultados = LstResultados.Where(x => x.DiferenciaCantidades > 0.1M).ToList();
                }
                else
                {
                    LstResultados = LstResultados.Where(x => x.DiferenciaCantidades >= 0.01M).ToList();
                }

            }



            dgvErrores.DataSource = LstResultados.OrderBy(x => x.NombreCliente).ThenBy(x => x.UUID).ThenBy(x => x.Cant).ToList(); ;
            tsErrores.Text = dgvErrores.RowCount.ToString();
            this.LstResultados = LstResultados;

        }




    }
}
