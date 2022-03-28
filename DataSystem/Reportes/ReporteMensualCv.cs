using DataSystem.Recursos;
using Entidades.JSONMensual;
using ExcelWriter;
using LinqToExcel;
using Newtonsoft.Json;
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
        private ExcelQueryFactory urlConexion;
        public ReporteMensualCv()
        {
            InitializeComponent();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaDoc = openFileDialog.FileName;
                txtArchivoCargado.Text = openFileDialog.SafeFileName;
                if (!string.IsNullOrEmpty(rutaDoc))
                {
                    if (rbJson.Checked)
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
            LstControlVolumetricoMensual = LstControlVolumetricoMensual.OrderBy(x => x.NombreClienteOProveedor).ThenBy(x => x.CFDI).ThenBy(x => x.ValorNumerico).ToList();
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
            dgvDatosGasolinas.DataSource = LstProductos;

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
                    string rutaImportado = dlg.FileName;
                    urlConexion = new ExcelQueryFactory(rutaImportado);

                    var query = (from a in urlConexion.Worksheet<Entidades.cls.FACTURASDETALLE>(nombreArchivo[0])
                                 select a).ToList();
                    var prueba = query.Select(x => new Entidades.cls.FACTURASDETALLE
                    {
                        folio_imp = x.folio_imp,
                        cliente = x.cliente,
                        importe = x.importe,
                        serie = x.serie,
                        docto = x.docto,
                        status = x.status,
                        fec_reg = x.fec_reg,
                        nombrep = x.nombrep,
                        cant = Math.Round(x.cant, 2),
                        precio = x.precio,
                        imported = x.imported,
                        uuid = x.uuid != null ? x.uuid.ToUpper().Trim() : null,
                        nombre = x.nombre.Trim(),

                    }).ToList();
                    lstManuales = prueba;
                    lstManuales = lstManuales.Where(x => x.status == "P").OrderBy(x => x.status).ToList();
                    lstManuales = lstManuales.Where(x => x.nombrep != null && (x.nombrep.StartsWith("Gasolina")||x.nombrep.StartsWith("GASOLINA") || x.nombrep.StartsWith("Diesel")||x.nombrep.StartsWith("DIESEL"))).ToList();
                    lstManuales = lstManuales.OrderBy(x => x.nombre).ThenBy(x => x.uuid).ThenBy(x => x.cant).ToList();
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
            //UnirListados();
            ComparacionArchivos();
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
                            importe = ObjDetalle.importe,
                            serie = ObjDetalle.serie,
                            docto = ObjDetalle.docto,   
                            status = ObjDetalle.status, 
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
                                        importe = ObjDetalle.importe,
                                        serie = ObjDetalle.serie,
                                        docto = ObjDetalle.docto,
                                        status = ObjDetalle.status,
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
                                    importe = ObjDetalle.importe,
                                    serie = ObjDetalle.serie,
                                    docto = ObjDetalle.docto,
                                    status = ObjDetalle.status,
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
                            importe = ObjDetalle.importe,
                            serie = ObjDetalle.serie,
                            docto = ObjDetalle.docto,
                            status = ObjDetalle.status,
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
                            importe = itemDet.importe,
                            serie = itemDet.serie,
                            docto = itemDet.docto,
                            status = itemDet.status,
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
                                    importe = 0,
                                    serie = null,
                                    docto = null,
                                    status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = (ObjCv.NombreClienteOProveedor != itemDet.nombre),
                                    ComparaCfdi = (ObjCv.CFDI != itemDet.uuid),
                                    ComparaLts = (ObjCv.ValorNumerico != itemDet.cant),
                                    Observacion = "*No se encuentra registro en Archivo C.V.\n"
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
                                    importe = 0,
                                    serie = null,
                                    docto = null,
                                    status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = (ObjCv.NombreClienteOProveedor != itemDet.nombre),
                                    ComparaCfdi = (ObjCv.CFDI != itemDet.uuid),
                                    ComparaLts = (ObjCv.ValorNumerico != itemDet.cant),
                                    Observacion = "*No se encuentra registro en Archivo C.V.\r\n"
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
                            importe = 0,
                            serie = null,
                            docto = null,
                            status = null,
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

                IExport<Entidades.cls.clsResultadosMensual> AccountExport = new ExcelWriter<Entidades.cls.clsResultadosMensual>();
                byte[] excelResult = AccountExport.Export(LstResultados);
                File.WriteAllBytes(rutaSalida, excelResult);
                MessageBox.Show("Se han exportado los registros.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Se perdeta la información ingresada. ¿Deseas reestablecer el modulo?","Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LimpiarControlesFormulario();
                lstManuales = new List<Entidades.cls.FACTURASDETALLE>();
                LstControlVolumetricoMensual = new List<Entidades.cls.clsControlVolumetricoMensual>();
                LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            }           
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

                lstCoincidenciasManuales = lstCoincidenciasManuales.OrderBy(x => x.nombre).ThenBy(x => x.uuid).ThenBy(x => x.cant).ToList();
                lstCvCoincidencias = lstCvCoincidencias.OrderBy(x => x.NombreClienteOProveedor).ThenBy(x => x.CFDI).ThenBy(x => x.ValorNumerico).ToList();

                if (lstCoincidenciasManuales.Count == lstCvCoincidencias.Count)
                {
                    for (int i = 0; i < lstCoincidenciasManuales.Count; i++)
                    {
                        Entidades.cls.clsControlVolumetricoMensual ObjCv;
                        Entidades.cls.FACTURASDETALLE ObjDet;

                        ObjCv = lstCvCoincidencias[i];
                        ObjDet = lstCoincidenciasManuales[i];
                        decimal diferencia = decimal.Round(ObjDet.cant, 2) - ObjCv.ValorNumerico;
                        diferencia = diferencia < 0 ? (diferencia * -1):diferencia;
                        
                        ObjResultado = new Entidades.cls.clsResultadosMensual
                        {
                            RfcClienteOProveedor = ObjCv.RfcClienteOProveedor,
                            NombreClienteOPRoveedor = ObjCv.NombreClienteOProveedor,
                            CFDI = ObjCv.CFDI,
                            FechaYHoraTransaccion = ObjCv.FechaYHoraTransaccion,
                            VolumenNumerico = ObjCv.ValorNumerico,
                            Existe = diferencia > 0? "<>":null,
                            folio_Imp = ObjDet.uuid,
                            clavecli = ObjDet.cliente,
                            NombreCliente = ObjDet.nombre,
                            importe = ObjDet.importe,
                            serie = ObjDet.serie,
                            docto = ObjDet.docto,
                            status = ObjDet.status,
                            fecha_reg = ObjDet.fec_reg,
                            nombrep = ObjDet.nombrep,
                            Cant = decimal.Round(ObjDet.cant, 2),
                            precio = ObjDet.precio,
                            imported = ObjDet.imported,
                            UUID = ObjDet.uuid,
                            ComparaNombre = ObjCv.NombreClienteOProveedor == ObjDet.nombre? true:false,
                            ComparaCfdi = ObjCv.CFDI == ObjDet.uuid? true:false,
                            ComparaLts = diferencia>0?false:true,
                            Observacion = diferencia>0? "Las cantidades no cinciden.\r\n":null
                        };
                        LstResultados.Add(ObjResultado);

                    }
                }
                else
                {
                    if (lstCoincidenciasManuales.Count > lstCvCoincidencias.Count)
                    {
                        int indexInicioMan = 0;
                        int indexInicioManAux = 0;
                        Entidades.cls.FACTURASDETALLE ObjMan;
                        foreach (var objCv in lstCvCoincidencias)
                        {
                            decimal diferencia = 0;
                            if(indexInicioMan < lstCoincidenciasManuales.Count)
                            {
                                indexInicioManAux = indexInicioMan;
                            }
                            else
                            {
                                indexInicioMan = indexInicioManAux;
                            }
                            
                            while (indexInicioMan < lstCoincidenciasManuales.Count)
                            {
                                ObjMan = lstCoincidenciasManuales[(indexInicioMan)];
                                diferencia = decimal.Round(ObjMan.cant,2) - objCv.ValorNumerico;
                                if (diferencia < 0.10M)
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
                                        folio_Imp = ObjMan.uuid,
                                        clavecli = ObjMan.cliente,
                                        NombreCliente = ObjMan.nombre,
                                        importe = ObjMan.importe,
                                        serie = ObjMan.serie,
                                        docto = ObjMan.docto,
                                        status = ObjMan.status,
                                        fecha_reg = ObjMan.fec_reg,
                                        nombrep = ObjMan.nombrep,
                                        Cant = decimal.Round(ObjMan.cant, 2),
                                        precio = ObjMan.precio,
                                        imported = ObjMan.imported,
                                        UUID = ObjMan.uuid,
                                        ComparaNombre = ObjMan.nombre == objCv.NombreClienteOProveedor ? true : false,
                                        ComparaCfdi = ObjMan.uuid == objCv.CFDI ? true : false,
                                        ComparaLts = diferencia > 0 ? false : true,
                                        Observacion = diferencia > 0 ? "Las cantidades no cinciden.\r\n" : null
                                    };
                                    LstResultados.Add(ObjResultado);
                                    indexInicioMan++;                                    
                                    break;
                                }
                                else
                                {
                                    // se sigue  poque no encuentra similitud                                    
                                    indexInicioMan++;
                                }
                            }
                            if (indexInicioMan >= lstCoincidenciasManuales.Count)
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
                                    importe = 0,
                                    serie = null,
                                    docto = null,
                                    status = null,
                                    fecha_reg = null,
                                    nombrep = null,
                                    Cant = 0,
                                    precio = 0,
                                    imported = 0,
                                    UUID = null,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "No Existe en Archivo"
                                };
                                LstResultados.Add(ObjResultado);
                                //registras como que solo estan en cv
                            }
                        }
                    }
                    else
                    {
                        int indexInicioMan = 0;
                        int indexInicioManAux = 0;
                        Entidades.cls.clsControlVolumetricoMensual objCv;
                        foreach (var objMan in lstCoincidenciasManuales)
                        {
                            decimal diferencia = 0;
                            if (indexInicioMan < lstCvCoincidencias.Count)
                            {
                                indexInicioManAux = indexInicioMan;
                            }
                            else
                            {
                                indexInicioMan = indexInicioManAux;
                            }

                            while (indexInicioMan < lstCvCoincidencias.Count)
                            {
                                objCv = lstCvCoincidencias[(indexInicioMan)];
                                diferencia = decimal.Round(objCv.ValorNumerico, 2) - objMan.cant;
                                if (diferencia < 0.10M)
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
                                        folio_Imp = objMan.uuid,
                                        clavecli = objMan.cliente,
                                        NombreCliente = objMan.nombre,
                                        importe = objMan.importe,
                                        serie = objMan.serie,
                                        docto = objMan.docto,
                                        status = objMan.status,
                                        fecha_reg = objMan.fec_reg,
                                        nombrep = objMan.nombrep,
                                        Cant = decimal.Round(objMan.cant, 2),
                                        precio = objMan.precio,
                                        imported = objMan.imported,
                                        UUID = objMan.uuid,
                                        ComparaNombre = objMan.nombre == objCv.NombreClienteOProveedor ? true : false,
                                        ComparaCfdi = objMan.uuid == objCv.CFDI ? true : false,
                                        ComparaLts = diferencia > 0 ? false : true,
                                        Observacion = diferencia > 0 ? "Las cantidades no cinciden.\r\n" : null
                                    };
                                    LstResultados.Add(ObjResultado);
                                    indexInicioMan++;
                                    break;
                                }
                                else
                                {
                                    // se sigue  poque no encuentra similitud                                    
                                    indexInicioMan++;
                                }
                            }
                            if (indexInicioMan >= lstCvCoincidencias.Count)
                            {
                                ObjResultado = new Entidades.cls.clsResultadosMensual
                                {
                                    RfcClienteOProveedor = null,
                                    NombreClienteOPRoveedor = null,
                                    CFDI = null,
                                    FechaYHoraTransaccion =null,
                                    VolumenNumerico = 0,
                                    Existe = ">",
                                    folio_Imp = objMan.folio_imp,
                                    clavecli = objMan.cliente,
                                    NombreCliente = objMan.nombre,
                                    importe = objMan.importe,
                                    serie = objMan.serie,
                                    docto = objMan.docto,
                                    status = objMan.status,
                                    fecha_reg = objMan.fec_reg,
                                    nombrep = objMan.nombrep,
                                    Cant = objMan.cant,
                                    precio = objMan.precio,
                                    imported = objMan.precio,
                                    UUID = objMan.uuid,
                                    ComparaNombre = false,
                                    ComparaCfdi = false,
                                    ComparaLts = false,
                                    Observacion = "No Existe en C.V"
                                };
                                LstResultados.Add(ObjResultado);
                                //registras como que solo estan en cv
                            }
                        }
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
                            folio_Imp = itemMan.uuid,
                            clavecli = itemMan.cliente,
                            NombreCliente = itemMan.nombre,
                            importe = itemMan.importe,
                            serie = itemMan.serie,
                            docto = itemMan.docto,
                            status = itemMan.status,
                            fecha_reg = itemMan.fec_reg,
                            nombrep = itemMan.nombrep,
                            Cant = decimal.Round(itemMan.cant, 2),
                            precio = itemMan.precio,
                            imported = itemMan.imported,
                            UUID = itemMan.uuid,
                            ComparaNombre = false,
                            ComparaCfdi = false,
                            ComparaLts = false,
                            Observacion = "No Existe en C.V."
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
                            importe = 0,
                            serie = null,
                            docto = null,
                            status = null,
                            fecha_reg = null,
                            nombrep = null,
                            Cant = 0,
                            precio = 0,
                            imported = 0,
                            UUID = null,
                            ComparaNombre = false,
                            ComparaCfdi = false,
                            ComparaLts = false,
                            Observacion = "No Existe en Archivo"
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


            dgvErrores.DataSource = LstResultados.Where(x=>x.Observacion!=null&&x.DiferenciaCantidades>0.1M).ToList();
            tsErrores.Text =  dgvErrores.RowCount.ToString("N0");


        }


    }
}
