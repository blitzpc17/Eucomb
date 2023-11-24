using DataSystem.Recursos;
using Entidades.JSONMensual;
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
using DataSystem.Utilidades;
using System.Text;
using System.Diagnostics;

namespace DataSystem.Reportes
{
    public partial class ReporteMensualCv : Form
    {
        private List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensual;
        //facturadetalles
        private List<Entidades.cls.FACTURASDETALLE> lstManuales;
        List<Entidades.AIVIC.EXCEL.FACTURADETALLE> ListaFacturaDetalleAivic;

        //resultados
        List<Entidades.cls.clsResultadosMensual> LstResultados;

        //lstfacturasnfecha
        List<Entidades.cls.FACTURASDETALLE> LstFacturaDetalleSnFecha;

        List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensualAux;

        List<KeyValuePair<string, string>> LstCfdisErrores;

        private ExcelQueryFactory urlConexion;
        private string argumentoBackground = "";
        private string rutaImportado = "";

        private int sucursal = 0;
        /*Sucursales
         
            DIGITAL PUMP = 1,
            ATIO =2,
            AIVIC = 3,
            NEXUS = 5
         
         */
        public ReporteMensualCv(string tituloModulo, int sucursal)
        {
            InitializeComponent();
            Text = tituloModulo;
            this.sucursal = sucursal;
            LstCfdisErrores = new List<KeyValuePair<string, string>>();
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

                    if (LstCfdisErrores.Count > 0)
                    {
                        DescargarErrores();
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void DescargarErrores()
        {
            MessageBox.Show("Se han detectado incongruencias. Genere el reporte de de errores.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de texto (*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string pathError = "";
                pathError = sfd.FileName;                              

                //recorrer lista de errores
                string contenido = "";
                foreach (var item in LstCfdisErrores)
                {
                    contenido += item.Key + "  " + item.Value + "\n\r";
                }

                File.WriteAllText(pathError, "Se detectaron los siguientes errores en la lectura del xml: \r\n" + contenido);

                Process.Start("notepad.exe", pathError);
            }
            else
            {
                MessageBox.Show("No se genero el archivo de errores, vuelva a cargar el reporte e intentelo de nuevo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void LeerJson(string RutaArchivo)
        {
            using (StreamReader r = new StreamReader(RutaArchivo))
            {
                string json = r.ReadToEnd();
                ControlesVolumetricos objControlVolumetrico = JsonConvert.DeserializeObject<ControlesVolumetricos>(json);
                LstControlVolumetricoMensual = new List<Entidades.cls.clsControlVolumetricoMensual>();
                foreach(var prod in objControlVolumetrico.Producto)
                {
                    foreach(var registro in prod.ReporteDeVolumenMensual.Entregas.Complemento)
                    {
                        if (registro.Nacional == null) continue;
                        foreach(var reg in registro.Nacional)
                        {
                            Entidades.cls.clsControlVolumetricoMensual objIntesis = new Entidades.cls.clsControlVolumetricoMensual
                            {
                                RfcClienteOProveedor = reg.RfcClienteOProveedor,
                                NombreClienteOProveedor = reg.NombreClienteOProveedor,
                                CFDI = reg.CFDIs.First().Cfdi,
                                FechaYHoraTransaccion = reg.CFDIs.First().FechaYhoraTransaccion,
                                ValorNumerico = reg.CFDIs.First().VolumenDocumentado.ValorNumerico
                            };
                            LstControlVolumetricoMensual.Add(objIntesis);
                        }
                        
                    }
                    
                }

                LstControlVolumetricoMensual.OrderBy(x => x.NombreClienteOProveedor).ThenBy(x => x.CFDI).ThenBy(x => x.ValorNumerico).ToList();
                dgvRegistrosDiario.DataSource = LstControlVolumetricoMensual;
                tsTotalRegistros.Text = dgvRegistrosDiario.RowCount.ToString();
                ImprimirInventarioJson(objControlVolumetrico);

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
                string ClaveProducto = null;
                string ClaveSubProducto = null;
                string MarcaComercial = null;

                Entidades.XMLMensual.PRODUCTO objProducto = new Entidades.XMLMensual.PRODUCTO();

                ClaveProducto = pro.ChildNodes[0].InnerText;
                ClaveSubProducto= pro.ChildNodes[1].InnerText;
                MarcaComercial = ((int)Enumeraciones.Sucursales.NEXUS==sucursal)?null:pro.ChildNodes[3].InnerText;

                objProducto.ClaveProducto = ClaveProducto;
                objProducto.ClaveSubProducto = ClaveSubProducto;
                objProducto.MarcaComercial = MarcaComercial;


                var nodeGasolina = pro.ChildNodes[2];
                objProducto.Gasolina = new Entidades.XMLMensual.Gasolina
                {
                    ComposOctanajeGasolina = nodeGasolina.ChildNodes[0].InnerText,
                    GasolinaConCombustibleNoFosil = nodeGasolina.ChildNodes[1] == null ? nodeGasolina.ChildNodes[0].InnerText : nodeGasolina.ChildNodes[1].InnerText
                };
                var nodeREPORTEVOLUMENMENSUAL = ((int)Enumeraciones.Sucursales.NEXUS == sucursal) ?pro.ChildNodes[3]:pro.ChildNodes[4];
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
                   
                    if(sucursal == (int)Enumeraciones.Sucursales.NEXUS)
                    {
                      
                       for(int i = 0; i<Complemento.ChildNodes.Count; i++)
                        {
                            var nodoComplemento = Complemento.ChildNodes[i];

                            if (nodoComplemento.ChildNodes[0].Name.Equals("exp:ACLARACION")) continue;

                            XmlNode nodoNacional;
                            if (nodoComplemento.ChildNodes[0].Name.Equals("exp:NACIONAL"))
                            {
                                nodoNacional = nodoComplemento.ChildNodes[0];
                            }
                            else if (nodoComplemento.ChildNodes[2].Name.Equals("exp:NACIONAL"))
                            {
                                nodoNacional = nodoComplemento.ChildNodes[2];
                            }else
                            {
                                continue;
                            }

                            Entidades.XMLMensual.NACIONAL objNac = new Entidades.XMLMensual.NACIONAL
                            {
                                RfcClienteOProveedor = nodoNacional.ChildNodes[0].InnerText,
                                NombreClienteOProveedor = nodoNacional.ChildNodes[1].InnerText
                            };
                            XmlNode nodeCFDI;
                            nodeCFDI = nodoNacional.ChildNodes[3];
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
                    else
                    {
                        foreach (XmlNode nac in Complemento.ChildNodes[0].ChildNodes)
                        {
                            if (nac.Name.Equals("exp:TERMINALALMYDIST")) continue;
                            if (nac.ChildNodes[0].Name.Equals("exp:ACLARACION")|| nac.ChildNodes[0].Name.Equals("exp:Aclaracion")) continue;

                            Entidades.XMLMensual.NACIONAL objNac = new Entidades.XMLMensual.NACIONAL
                            {
                                RfcClienteOProveedor = nac.ChildNodes[0].InnerText,
                                NombreClienteOProveedor = nac.ChildNodes[1].InnerText
                            };

                            XmlNode nodeCFDI;
                            nodeCFDI = nac.ChildNodes[2];
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
                    if(sucursal == (int)Enumeraciones.Sucursales.NEXUS)
                    {
                        for(int nodo = 0; nodo<ComplementoEntregas.ChildNodes.Count; nodo++)
                        {
                            Entidades.XMLMensual.NACIONAL objNacEnc = new Entidades.XMLMensual.NACIONAL
                            {
                                RfcClienteOProveedor = ComplementoEntregas.ChildNodes[nodo].FirstChild.ChildNodes[0].InnerText,
                                NombreClienteOProveedor = ComplementoEntregas.ChildNodes[nodo].FirstChild.ChildNodes[1].InnerText,
                            };
                            var nodeCfdiNac = ComplementoEntregas.ChildNodes[nodo].FirstChild.ChildNodes[2];//NACIONAL
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
                    else
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
            ImprimirInventario(obj);

        }

        private void btnImportarLayout_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;

            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] nombreArchivo = dlg.SafeFileName.Split('.');
                if (nombreArchivo[1] != "xlsx")
                {
                    MessageBox.Show("EL ARCHIVO EXCEL TIENE QUE TENER  VERSION (.xlsx)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                rutaImportado = dlg.FileName;

                lblEstado.Text = "Importando archivo...";
                backgroundWorker1.RunWorkerAsync("importarExcel");
              
            }

        }

        private void ImportarExcel(string rutaImportado)
        {            
            try
            {
                urlConexion = new ExcelQueryFactory(rutaImportado);

                var query = (from a in urlConexion.Worksheet<Entidades.cls.FACTURASDETALLE>(0)
                             select a).ToList();

                backgroundWorker1.ReportProgress(15);

                LstFacturaDetalleSnFecha = query.Where(x => x.fyh_trans == "  -   -  : :").ToList();
                query = query.Where(a => a.fyh_trans != "  -   -  : :").ToList();
                var prueba = query.Select(x => new Entidades.cls.FACTURASDETALLE
                {
                    folio_imp = x.folio_imp,
                    cliente = x.cliente,
                    importe = x.importe,
                    serie = x.serie,
                    docto = x.docto,
                    status = x.status,
                    fec_reg = Convert.ToDateTime(x.fyh_trans),
                    nombrep = x.nombrep,
                    cant = Math.Round(x.cant, 2),
                    precio = x.precio,
                    imported = x.imported,
                    uuid = x.uuid != null ? x.uuid.ToUpper().Trim() : null,
                    nombre = x.nombre.Trim(),
                    fyh_trans = x.fec_reg.ToString(),
                    idtrans = x.idtrans,

                }).ToList();

                backgroundWorker1.ReportProgress(50);

                prueba.AddRange(LstFacturaDetalleSnFecha);
                lstManuales = prueba;
                lstManuales = lstManuales.Where(x => x.status == "P").OrderBy(x => x.status).ToList();
                lstManuales = lstManuales.Where(x => x.nombrep != null && (x.nombrep.StartsWith("Gasolina") || x.nombrep.StartsWith("GASOLINA") || x.nombrep.StartsWith("Diesel") || x.nombrep.StartsWith("DIESEL") || x.nombrep.StartsWith("COMBUSTIBLE") || x.nombrep.StartsWith("Combustible") || x.nombrep.StartsWith("combustible"))).ToList();
                lstManuales = lstManuales.OrderBy(x => x.nombre).ThenBy(x => x.uuid).ThenBy(x => x.cant).ThenBy(x => x.fec_reg).ToList();
                backgroundWorker1.ReportProgress(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }           

            
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
            if(sucursal== (int)Enumeraciones.Sucursales.DIGITALPUMP && (lstManuales == null|| LstControlVolumetricoMensual == null))
            {
                MessageBox.Show("No se han cargado los archivos de comparación (Archivo C.V y ReporteDetalle Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (sucursal== (int)Enumeraciones.Sucursales.AIVIC && (ListaFacturaDetalleAivic == null || LstControlVolumetricoMensual == null))
            {
                MessageBox.Show("No se han cargado los archivos de comparación (Archivo C.V y Reporte Facturacion AIVIC Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblEstado.Text = "Comparando archivos...";
            backgroundWorker1.RunWorkerAsync("comparar");

        
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
                sl.SetCellValue("S1", "COMPARACIÓN");
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
                sl.SetCellStyle("S1", styleEncabezados);               

                sl.MergeWorksheetCells("A1", "E1");
                sl.MergeWorksheetCells("G1","R1");
                sl.SetCellStyle("S1", styleColorComparacion);
                sl.MergeWorksheetCells("S1", "W1");

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
                sl.SetCellValue("R2", "IdTrans");
                sl.SetCellValue("S2", "Compara Nombre");
                sl.SetCellValue("T2", "Compara CFDI");
                sl.SetCellValue("U2", "Compara Litros");
                sl.SetCellValue("V2", "Diferencia de Lts o ML");                
                sl.SetCellValue("W2", "Observaciones");

                int noRows = 2;
        
                if (!chkTodo.Checked)
                {
                    LstResultados = LstResultados.Where(x => x.DiferenciaCantidades >= 0.01M).ToList();
                }
                noRows += LstResultados.Count();
                GenerarRowsExcel(LstResultados, sl);

                sl.SetColumnStyle(5, styleCantidades);
                sl.SetColumnStyle(8, styleCantidades);
                sl.SetColumnStyle(11, styleCantidades);
                sl.SetColumnStyle(14, styleCantidades);
                sl.SetColumnStyle(15, styleCantidades);
                sl.SetColumnStyle(16, styleCantidades);
                sl.SetColumnStyle(21, styleCantidades);
                sl.SetCellStyle(3, 1,noRows, 5, styleColorCV );
                sl.SetCellStyle(3, 7, noRows, 18, styleColorFacturacion);
                sl.SetColumnWidth(1,22,27);
                sl.SetColumnWidth(6, 3);
                sl.SetRowStyle(2, styleEncabezados);
                sl.SaveAs(rutaSalida);
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
                objExcel.SetCellValue("R"+ index, row.IdTrans);
                objExcel.SetCellValue("S"+index, row.ComparaNombre);
                objExcel.SetCellValue("T"+index, row.ComparaCfdi);
                objExcel.SetCellValue("U"+index, row.ComparaLts);
                objExcel.SetCellValue("V"+index, row.DiferenciaCantidades);
                objExcel.SetCellValue("W"+index, row.Observacion);

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

        private void btnXmlJson_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
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
            List<Entidades.cls.FACTURASDETALLE>lstManualesAux = lstManuales;
           // List<Entidades.cls.clsControlVolumetricoMensual> LstControlVolumetricoMensualAux = LstControlVolumetricoMensual;

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

            backgroundWorker1.ReportProgress(10);
            
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
                               ComparaNombre = false,
                               ComparaCfdi = false,
                               ComparaLts = false,
                               Observacion = "NO EXISTE EN ARCHIVO C.V.",
                               DiferenciaCantidades = noObjDet.cant,
                               IdTrans = noObjDet.idtrans
                              
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
                                    DiferenciaCantidades = objCv.ValorNumerico,
                                    IdTrans = 0
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
                                    DiferenciaCantidades = diferencia,
                                    IdTrans = objDet.idtrans
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
                                    DiferenciaCantidades = diferencia,
                                    IdTrans = 0
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
                               ComparaNombre = false,
                               ComparaCfdi = false,
                               ComparaLts = false,
                               Observacion = "NO EXISTE EN ARCHIVO C.V.",
                               DiferenciaCantidades = noObjDet.cant,
                               IdTrans = noObjDet.idtrans
                           });
                        }
                    }

                }


            }

            backgroundWorker1.ReportProgress(65);
            
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
                              ComparaNombre = false,
                              ComparaCfdi = false,
                              ComparaLts = false,
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

            backgroundWorker1.ReportProgress(75);

            //recorrer los que no tienen fy h registro 
            foreach (var noObjDet in lstFacturasDirectasManuales)
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
                            ComparaNombre = false,
                            ComparaCfdi = false,
                            ComparaLts = false,
                            Observacion = "NO EXISTE EN ARCHIVO C.V.",
                            DiferenciaCantidades = noObjDet.cant
                        });
            }

            backgroundWorker1.ReportProgress(85);

            this.LstResultados = LstResultados;         
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            argumentoBackground = e.Argument.ToString();
            switch (argumentoBackground)
            {
                case "importarExcel":
                    switch (sucursal)
                    {
                        case 1: ImportarExcel(rutaImportado); break;

                        case 2: ImportarExcelAivic(); break;                    }
                    
                    break;

                case "comparar":
                    LstControlVolumetricoMensualAux = LstControlVolumetricoMensual;
                    switch (sucursal)
                    {
                        case 1: ComparacionSistema(); break;
                        case 2: ComparacionAivic(); break;  
                    }
                    
                    break;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            switch (argumentoBackground)
            {
                case "importarExcel":
                    if(sucursal == (int)Enumeraciones.Sucursales.ATIO)
                    {
                        dgvManuales.DataSource = ListaFacturaDetalleAivic;
                    }
                    else
                    {
                        dgvManuales.DataSource = lstManuales;
                    }
                    
                    tsManuales.Text = dgvManuales.RowCount.ToString("N0");
                    lblEstado.Text = "¡Listo!";
                    MessageBox.Show("Importación del excel completada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    break;

                case "comparar":

                    if (!chkTodo.Checked)
                    {                       
                        LstResultados = LstResultados.Where(x => x.DiferenciaCantidades >= 0.01M).ToList();
                    }
                    progressBar1.Value = 95;

                    dgvErrores.DataSource = LstResultados.OrderBy(x => x.NombreCliente).ThenBy(x => x.UUID).ThenBy(x => x.Cant).ToList(); ;
                    tsErrores.Text = dgvErrores.RowCount.ToString();
                    progressBar1.Value = 100;

                    MessageBox.Show("Comparación realizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblEstado.Text = "¡Listo!";
                    progressBar1.Value = 0;
                    break;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void ImportarExcelAivic()
        {
            try
            {
                urlConexion = new ExcelQueryFactory(rutaImportado);

                var query = (from a in urlConexion.Worksheet<Entidades.AIVIC.EXCEL.FACTURADETALLE>(0)
                             select a).ToList();

                backgroundWorker1.ReportProgress(15);

               
                ListaFacturaDetalleAivic = query.Select(x=>new Entidades.AIVIC.EXCEL.FACTURADETALLE
                {

                }).ToList(); //new List<Entidades.AIVIC.EXCEL.FACTURADETALLE>();
                var prueba = query.Select(x => new Entidades.AIVIC.EXCEL.FACTURADETALLE
                {
                    Numero = x.Numero,
                    Estacion = x.Estacion,
                    Fecha = x.Fecha,
                    Cliente = x.Cliente,
                    Producto = x.Producto,
                    Cantidad = x.Cantidad,
                    Monto = x.Monto,
                    Iva = x.Iva,
                    Ieps = x.Ieps,
                    Total = x.Total,
                    UUID = x.UUID,
                    Estado = x.Estado,

                }).ToList();

                backgroundWorker1.ReportProgress(50);
                
                ListaFacturaDetalleAivic = prueba;
                ListaFacturaDetalleAivic = ListaFacturaDetalleAivic.Where(x => x.Producto.Contains("EFITEC92") || x.Producto.Contains("EFITEC87")).ToList();
                ListaFacturaDetalleAivic = ListaFacturaDetalleAivic.OrderBy(x => x.Cliente).ThenBy(x => x.UUID).ThenBy(x => x.Cantidad).ThenBy(x => x.Fecha).ToList();
                backgroundWorker1.ReportProgress(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComparacionAivic()
        {
            // LstControlVolumetricoMensual
            List<Entidades.cls.clsControlVolumetricoMensual> LstCvNoExisten = new List<Entidades.cls.clsControlVolumetricoMensual>();
         //   List<Entidades.AIVIC.EXCEL.FACTURADETALLE> LstFactDetalleNoExisten = new List<Entidades.AIVIC.EXCEL.FACTURADETALLE>();
            LstResultados = new List<Entidades.cls.clsResultadosMensual>();


            //buscar cuales estan en XML y no en AIVIC
            foreach (var objFact in ListaFacturaDetalleAivic)
            {
                string observacion = "";
                var objNe = objFact;
                bool comparaNombre=false;
                bool comparaCfdi = false;
                bool comparaLts = false;
                Entidades.cls.clsControlVolumetricoMensual ObjRegistroCV;             

                if (LstControlVolumetricoMensual.Any(x => x.CFDI == objFact.UUID)){
                    ObjRegistroCV = LstControlVolumetricoMensual.First(x=>x.CFDI==objFact.UUID);
                    DateTime fechaRegistroCv = DateTime.Parse(ObjRegistroCV.FechaYHoraTransaccion.ToShortDateString());
                    if (fechaRegistroCv != objFact.Fecha)
                    {
                        observacion = "EXISTE EN ARCHIVO C.V., PERO LAS FECHAS NO COICIDEN.";
                    }
                    else if(Math.Round(ObjRegistroCV.ValorNumerico, 2) != objFact.Cantidad)
                    {
                        observacion = "EXISTE EN ARCHIVO C.V PERO LAS CANTIDADES NO COINCIDEN";
                        comparaLts = false;
                    }

                }else
                {
                    ObjRegistroCV=null;
                    observacion = "NO EXISTE EN ARCHIVO C.V.";
                    comparaCfdi = false;
                    
                }

                if (!string.IsNullOrEmpty(observacion)&&ObjRegistroCV==null)
                {
                    //LstFactDetalleNoExisten.Add(objNe);
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = null,
                        NombreCliente = null,
                        CFDI = null,
                        FechaYHoraTransaccion = null,
                        VolumenNumerico = 0,
                        Existe = ">",
                        folio_Imp = objFact.Numero,
                        clavecli = "",
                        NombreClienteOPRoveedor = objFact.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = objFact.Fecha,
                        nombrep = "",
                        Cant = objFact.Cantidad,
                        precio = 0,
                        imported = objFact.Total,
                        UUID = objFact.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = objFact.Cantidad
                    });
                }
                else
                {
                    if (ObjRegistroCV == null) return;
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = ObjRegistroCV.RfcClienteOProveedor,
                        NombreCliente = ObjRegistroCV.NombreClienteOProveedor,
                        CFDI = ObjRegistroCV.CFDI,
                        FechaYHoraTransaccion = ObjRegistroCV.FechaYHoraTransaccion,
                        VolumenNumerico = ObjRegistroCV.ValorNumerico,
                        Existe = "<>",
                        folio_Imp = objFact.Numero,
                        clavecli = "",
                        NombreClienteOPRoveedor = objFact.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = objFact.Fecha,
                        nombrep = "",
                        Cant = Math.Round(objFact.Cantidad,2),
                        precio = 0,
                        imported = objFact.Total,
                        UUID = objFact.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = (ObjRegistroCV.ValorNumerico - objFact.Cantidad)<0? (ObjRegistroCV.ValorNumerico - objFact.Cantidad)*-1: (ObjRegistroCV.ValorNumerico - objFact.Cantidad)
                    });
                }
                

            }

            //buscar cuales no estan del xml  en el excel de AIVIC
            foreach(var objCv in LstControlVolumetricoMensual)
            {
                string observacion = "";
                var objNe = objCv;
                bool comparaNombre = false;
                bool comparaCfdi = false;
                bool comparaLts = false;
                Entidades.AIVIC.EXCEL.FACTURADETALLE ObjRegistroFactura;

                if (ListaFacturaDetalleAivic.Any(x => x.UUID==objCv.CFDI))
                {
                    ObjRegistroFactura = ListaFacturaDetalleAivic.First(x=>x.UUID ==objCv.CFDI);
                    DateTime fechaRegistroFactura = DateTime.Parse(objCv.FechaYHoraTransaccion.ToShortDateString());
                    if(ObjRegistroFactura.Fecha != fechaRegistroFactura)
                    {
                        observacion = "EXISTE EN FACTURA ATIO PERO LAS FECHAS NO COINCIDEN";

                    }else if(Math.Round(objCv.ValorNumerico, 2) == ObjRegistroFactura.Cantidad)
                    {
                        observacion = "EXISTE EN FACTURA ATIO PERO LAS CANTIDADES NO COINCIDEN";
                        comparaLts = false;
                    }
                }
                else
                {
                    ObjRegistroFactura = null;
                    observacion = "NO EXISTE EN FACTURA ATIO.";
                    comparaCfdi = false;
                }


                if (!string.IsNullOrEmpty(observacion) && ObjRegistroFactura == null)
                {
                  //  LstFactDetalleNoExisten.Add(objNe);
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                        NombreCliente = objCv.NombreClienteOProveedor,
                        CFDI = objCv.CFDI,
                        FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                        VolumenNumerico = 0,
                        Existe = "<",
                        folio_Imp = null,
                        clavecli = null,
                        NombreClienteOPRoveedor = null,
                        serie = null,
                        docto = null,
                        fecha_reg = null,
                        nombrep = null,
                        Cant = 0,
                        precio = 0,
                        imported = 0,
                        UUID = null,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = Math.Round(objCv.ValorNumerico,2)
                    });
                }
                else
                {
                    if (ObjRegistroFactura == null) return;
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                        NombreCliente = objCv.NombreClienteOProveedor,
                        CFDI = objCv.CFDI,
                        FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                        VolumenNumerico = objCv.ValorNumerico,
                        Existe = "<>",
                        folio_Imp = ObjRegistroFactura.Numero,
                        clavecli = "",
                        NombreClienteOPRoveedor = ObjRegistroFactura.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = ObjRegistroFactura.Fecha,
                        nombrep = "",
                        Cant = Math.Round(ObjRegistroFactura.Cantidad, 2),
                        precio = 0,
                        imported = ObjRegistroFactura.Total,
                        UUID = ObjRegistroFactura.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = (objCv.ValorNumerico - ObjRegistroFactura.Cantidad) < 0 ? (objCv.ValorNumerico - ObjRegistroFactura.Cantidad) * -1 : (objCv.ValorNumerico - ObjRegistroFactura.Cantidad)
                    });
                }

            }


        }


        private void ImprimirInventario(Entidades.XMLMensual.ControlesVolumetricos obj)
        {
            txtVersion.Text = obj.Version;
            txtRfcRepresentante.Text = obj.RfcRepresentanteLegal;
            txtRfcProveedor.Text = obj.RfcProveedor;
            txtRfcContrib.Text = obj.RfcContribuyente;
            txtNoPermiso.Text = obj.Caracter.NumPermiso;
            txtSucursal.Text = Parametros.CatalogSucursales().Where(x => x.Value == obj.Caracter.NumPermiso).First().Key;
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
                dgvPartidas.Columns.Add("long", "Long.");
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
                if (pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento == null)
                {
                    sumaRecepciones = 0;
                    MessageBox.Show("El producto " + pro.ClaveSubProducto + " " + pro.MarcaComercial + " no tiene COMPLEMENTOS EN RECEPCIONES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var part in pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL)
                    {
                        //validar cfdi
                        if (part.CFDIs.CFDI.Length != 36)
                        {
                            LstCfdisErrores.Add(new KeyValuePair<string, string>(part.CFDIs.CFDI, "Error de longitud: " + part.CFDIs.CFDI.Length.ToString("N0")));
                        }
                        //imprimi
                        dgvPartidas.Rows.Add(numeral, part.NombreClienteOProveedor, part.RfcClienteOProveedor, part.CFDIs.CFDI,part.CFDIs.CFDI.Length, part.CFDIs.FechaYHoraTransaccion, part.CFDIs.PrecioCompra, part.CFDIs.PrecioDeVentaAlPublico, part.CFDIs.VolumenDocumentado.ValorNumerico);
                        sumaRecepciones += part.CFDIs.VolumenDocumentado.ValorNumerico;
                        numeral++;
                    }
                }


                decimal diferenciaEntregadoRecepcion = pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.SumaVolumenRecepcionMes.ValorNumerico - sumaRecepciones;
                dgvPartidas.Rows.Add(null, null, null, null, null, null, "TOTAL:", sumaRecepciones);
                dgvPartidas.Rows.Add(null, null, null, "VENTA LTS. POR MES:", pro.REPORTEDEVOLUMENMENSUAL.ENTREGAS.SumaVolumenEntregado.ValorNumerico, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "DIF- FACT. VS PIPAS:", diferenciaEntregadoRecepcion, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "LA FACTURA TRAE", diferenciaEntregadoRecepcion >= 0 ? " MÁS" : " MENOS");
                posicionDgvInventariosY = dgvPartidas.Location.Y + dgvPartidas.Size.Height + 15;

            }
        }


        private void ImprimirInventarioJson(ControlesVolumetricos obj)
        {
            txtVersion.Text = obj.Version;
            txtRfcRepresentante.Text = obj.RfcRepresentanteLegal;
            txtRfcProveedor.Text = obj.RfcProveedor;
            txtRfcContrib.Text = obj.RfcContribuyente;
            txtNoPermiso.Text = obj.NumPermiso;
            txtSucursal.Text = Parametros.CatalogSucursales().Where(x => x.Value == obj.NumPermiso).First().Key;
            txtCaracter.Text = obj.Caracter;
            txtModPermiso.Text = obj.NumPermiso;
            txtPeriodo.Text = obj.FechaYHoraReporteMes.ToString();

            limpiarPanelInventarios();

            int posicionDgvInventariosY = 0;
            foreach (var pro in obj.Producto.OrderByDescending(x=>x.ClaveProducto).ToList())
            {

                DataGridView dgvEncabezadoInventario = new DataGridView();

                dgvEncabezadoInventario.Columns.Add("Texto", "");
                dgvEncabezadoInventario.Columns.Add("Valor", "");
                dgvEncabezadoInventario.Name = "dgvEncabezado" + pro.ClaveProducto;
                dgvEncabezadoInventario.Rows.Add("Producto:", pro.ClaveSubProducto + " " + pro.MarcaComercial);
                dgvEncabezadoInventario.Rows.Add("INVENTARIO EN TANQUE AL FINALIZAR EL MES:", pro.ReporteDeVolumenMensual.ControlDeExistencias.VolumenExistenciasMes);
                dgvEncabezadoInventario.Rows.Add("NÚMERO DE VECES QUE ENTRO PRODUCTO AL TANQUE:", pro.ReporteDeVolumenMensual.Recepciones.TotalDocumentosMes);
                dgvEncabezadoInventario.Rows.Add("TOTAL DE LITROS QUE MUESTRA LA FACTURA:", pro.ReporteDeVolumenMensual.Recepciones.SumaVolumenRecepcionMes.ValorNumerico);
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
                if (pro.ReporteDeVolumenMensual.Recepciones.Complemento == null)
                {
                    sumaRecepciones = 0;
                    MessageBox.Show("El producto " + pro.ClaveSubProducto + " " + pro.MarcaComercial + " no tiene COMPLEMENTOS EN RECEPCIONES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var part in pro.ReporteDeVolumenMensual.Recepciones.Complemento /*pro.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL*/)
                    {
                        if (part.Nacional == null) continue;
                        foreach(var par in part.Nacional)
                        {
                            /*
                            dgvPartidas.Rows.Add(numeral, par.NombreClienteOProveedor, par.RfcClienteOProveedor, par.CFDIs.First().Cfdi, par.CFDIs.First().FechaYhoraTransaccion, par.CFDIs.First().PrecioCompra, par.CFDIs.First().PrecioCompra, par.CFDIs.First().VolumenDocumentado.ValorNumerico);
                            sumaRecepciones += par.CFDIs.First().VolumenDocumentado.ValorNumerico;
                            numeral++;
                            */

                            string NombreClienteOProveedor = par.NombreClienteOProveedor;
                            string RfcClienteOProveedor = par.RfcClienteOProveedor;
                            foreach(var cfdi in par.CFDIs)
                            {
                                //validar cfdi
                                if (cfdi.Cfdi.Length != 36  )
                                {
                                    LstCfdisErrores.Add(new KeyValuePair<string, string>(cfdi.Cfdi, "Error de longitud: "+cfdi.Cfdi.Length.ToString("N0")));
                                }
                                //generar row
                                dgvPartidas.Rows.Add(numeral, NombreClienteOProveedor, RfcClienteOProveedor, cfdi.Cfdi, cfdi.FechaYhoraTransaccion, cfdi.PrecioCompra, 
                                    cfdi.PrecioCompra, cfdi.VolumenDocumentado.ValorNumerico);
                                sumaRecepciones += cfdi.VolumenDocumentado.ValorNumerico;
                                numeral++;
                            }

                        }
                        
                    }
                }


                decimal diferenciaEntregadoRecepcion = pro.ReporteDeVolumenMensual.Recepciones.SumaVolumenRecepcionMes.ValorNumerico - sumaRecepciones;
                dgvPartidas.Rows.Add(null, null, null, null, null, null, "TOTAL:", sumaRecepciones);
                dgvPartidas.Rows.Add(null, null, null, "VENTA LTS. POR MES:",   pro.ReporteDeVolumenMensual.Entregas.SumaVolumenEntregadoMes.ValorNumerico, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "DIF- FACT. VS PIPAS:", diferenciaEntregadoRecepcion, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "LA FACTURA TRAE", diferenciaEntregadoRecepcion >= 0 ? " MÁS" : " MENOS");
                posicionDgvInventariosY = dgvPartidas.Location.Y + dgvPartidas.Size.Height + 15;

            }
        }




    }
}
