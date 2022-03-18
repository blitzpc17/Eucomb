﻿using Entidades;
using Entidades.JSONMensual;
using LinqToExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DataSystem.Reportes
{
    public partial class ReporteMensualCv : Form
    {
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
            MessageBox.Show("No se ha seleccionado ningun archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

           
           
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
            XmlNodeList nodePRODUCTO = doc.SelectNodes("//Covol:PRODUCTO",nsm);

            Entidades.XMLMensual.ControlesVolumetricos obj = new Entidades.XMLMensual.ControlesVolumetricos();
            obj.Version = nodeVersion.InnerText;
            obj.RfcContribuyente = nodeRfcContribuyente.InnerText;
            obj.RfcRepresentanteLegal = nodeRfcRepresentanteLegal.InnerText;
            obj.RfcProveedor = nodeRfcProveedor.InnerText;
            
            obj.Caracter = new Entidades.XMLMensual.Caracter
            {
                  TipoCaracter = nodeCaracter.ChildNodes[0].InnerText,
                  ModalidadPermiso = nodeCaracter.ChildNodes[1].InnerText,
                  NumPermiso   =    nodeCaracter.ChildNodes[2].InnerText
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
            
            foreach( XmlNode pro  in nodePRODUCTO)
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
                    GasolinaConCombustibleNoFosil = nodeGasolina.ChildNodes[1].InnerText
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
                var Complemento = RECEPCIONES.ChildNodes[4];
                objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento = new Entidades.XMLMensual.Complemento();

                objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio = new Entidades.XMLMensual.Complemento_Expendio();
                objProducto.REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL = new List<Entidades.XMLMensual.NACIONAL>();


                foreach ( XmlNode nac in Complemento.ChildNodes[0].ChildNodes)
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
                    TotalEntregasMes =int.Parse(ENTREGAS.ChildNodes[0].InnerText),
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
                        PrecioCompra = decimal.Parse( nodeCfdiNac.ChildNodes[2].InnerText),
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


            



        }

        private void btnImportarLayout_Click(object sender, EventArgs e)
        {
            ImportarExcel();
        }

        private void ImportarExcel()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string rutaImportado = dlg.FileName;
                urlConexion = new ExcelQueryFactory(rutaImportado);

                var query = from a in urlConexion.Worksheet<Entidades.cls.FACTURASDETALLE>("Hoja1")
                            select a;


                dataGridView1.DataSource = query.ToList();
                

            }
        }
    }
}