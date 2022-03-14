using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CapaLogica.Reportes
{
    public class reporte_respaldo
    {
        public ControlVolumetrico ObjControlVolumetrico;
        public reporte_respaldo()
        {

        }

        public ControlVolumetrico LeerReporteCvXml(String rutaArchivo)
        {
            /*
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);

            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("Covol", "https://repositorio.cloudb.sat.gob.mx/Covol/xml/Diarios");

            XmlNode nodeVersion = doc.SelectSingleNode("//Covol:Version", nsm);
            XmlNode nodeRfcContribuyente = doc.SelectSingleNode("//Covol:RfcContribuyente", nsm);
            XmlNode nodeRfcRepresentanteLegal = doc.SelectSingleNode("//Covol:RfcRepresentanteLegal", nsm);
            XmlNode nodeRfcProveedor = doc.SelectSingleNode("//Covol:RfcProveedor", nsm);
            XmlNode nodeCaracter = doc.SelectSingleNode("//Covol:Caracter", nsm);
            XmlNode nodeFechaYHoraCorte = doc.SelectSingleNode("//Covol:FechaYHoraCorte", nsm);

            ControlVolumetrico objControlVolumetrico = new ControlVolumetrico
            {
                Version = nodeVersion.InnerText,
                RfcContribuyente = nodeRfcContribuyente.InnerText,
                RfcRepresentanteLegal = nodeRfcRepresentanteLegal.InnerText,
                RfcProveedor = nodeRfcProveedor.InnerText,
                FechaYHoraCorte = DateTime.Parse(nodeFechaYHoraCorte.InnerText),

            };

            Caracter objCaracter = new Caracter
            {
                TipoCaracter = nodeCaracter.ChildNodes[0].InnerText,
                ModalidadPermiso = nodeCaracter.ChildNodes[1].InnerText,
                NumPermiso = nodeCaracter.ChildNodes[2].InnerText
            };
            objControlVolumetrico.Caracter = objCaracter;

            var nodeProductos = doc.SelectNodes("//Covol:PRODUCTO", nsm);
            int NoProds = nodeProductos.Count;
            List<Producto> LstProductos = new List<Producto>();
            Producto producto;
            for (int i = 1; i <= NoProds; i++)
            {
                var nodoPro = nodeProductos.Item(i - 1);
                producto = new Producto
                {
                    ClaveProducto = nodoPro.ChildNodes[0].InnerText,
                    ClaveSubProduto = nodoPro.ChildNodes[1].InnerText,
                    MarcaComercial = nodoPro.ChildNodes[3].InnerText,

                };
                List<Dispensario> lstDispensarios = new List<Dispensario>();
                var nodosDispensarios = nodoPro.SelectNodes("//Covol:DISPENSARIO", nsm);
                for (int j = 1; j <= nodosDispensarios.Count; j++)
                {
                    var nodoDispensario = nodosDispensarios.Item(j - 1);
                    Dispensario dispensario = new Dispensario
                    {
                        ClaveDispensario = nodoDispensario.ChildNodes[0].InnerText
                    };
                    var nodosMangueras = nodoDispensario.SelectNodes("//Covol:MANGUERA", nsm);
                    dispensario.Manguera = new List<Manguera>();
                    for (int k = 1; k <= nodosMangueras.Count; k++)
                    {
                        var nodoManguera = nodosMangueras.Item(k - 1);

                        Manguera manguera = new Manguera
                        {
                            IdentificadorManguera = nodoManguera.ChildNodes[0].InnerText
                        };

                        var nodeEntregas = nodoManguera.SelectNodes("//Covol:ENTREGAS", nsm);
                        manguera.Entregas = new List<Entregas>();
                        Entregas objEntregas;
                        for (int l = 1; l <= nodeEntregas.Count; l++)
                        {
                            var nodoEntregas = nodeEntregas.Item(l - 1);
                            objEntregas = new Entregas
                            {
                                TotalEntregas = int.Parse(nodoEntregas.ChildNodes[0].InnerText),
                                TotalDocumentos = int.Parse(nodoEntregas.ChildNodes[2].InnerText),
                            };
                            var subnodeVolumen = nodoEntregas.ChildNodes[1];
                            SumaVolumenEntregado objSumaVolumenEntregado = new SumaVolumenEntregado
                            {
                                ValorNumerico = decimal.Parse(subnodeVolumen.ChildNodes[0].InnerText),
                                UM = subnodeVolumen.ChildNodes[1].InnerText
                            };
                            objEntregas.SumaVolumenEntregado = objSumaVolumenEntregado;

                            var nodosEntrega = nodoEntregas.SelectNodes("//Covol:ENTREGA", nsm);
                            objEntregas.Entrega = new List<Entrega>();
                            Entrega objEntrega;
                            for (int m = 1; m <= nodosEntrega.Count; m++)
                            {
                                var nodoEntrega = nodosEntrega.Item(m - 1);
                                objEntrega = new Entrega
                                {
                                    NumeroDeRegistro = int.Parse(nodoEntrega.ChildNodes[0].InnerText),
                                    TipoDeRegistro = nodoEntrega.ChildNodes[1].InnerText,
                                    FechaYHoraEntrega = DateTime.Parse(nodoEntrega.ChildNodes[4].InnerText)
                                };
                                var subNodoTotalAcum = nodoEntrega.ChildNodes[2];
                                VolumenEntregadoTotalizadorAcum objTotalAcum = new VolumenEntregadoTotalizadorAcum
                                {
                                    ValorNumerico = decimal.Parse(subNodoTotalAcum.ChildNodes[0].InnerText),
                                    UM = subNodoTotalAcum.ChildNodes[1].InnerText
                                };
                                objEntrega.VolumenEntregadoTotalizadorAcum = objTotalAcum;
                                var subNodoTotalInsta = nodoEntrega.ChildNodes[3];
                                VolumenEntregadoTotalizadorInsta objTotalInsta = new VolumenEntregadoTotalizadorInsta
                                {
                                    ValorNumerico = decimal.Parse(subNodoTotalInsta.ChildNodes[0].InnerText),
                                    UM = subNodoTotalInsta.ChildNodes[1].InnerText
                                };
                                objEntrega.VolumenEntregadoTotalizadorInsta = objTotalInsta;
                                var subNodoComplemento = nodoEntrega.ChildNodes[5];
                                Complemento objComplemento = new Complemento();
                                objComplemento.Complemento_Expendio = new List<Complemento_Expendio>();
                                var subNodosComplementos = subNodoComplemento.ChildNodes;
                                for (int n = 1; n <= subNodosComplementos.Count; n++)
                                {
                                    var nodoComplementoExpendio = subNodosComplementos.Item(n - 1);
                                    Complemento_Expendio objComplementoExpendio = new Complemento_Expendio();
                                    var nodoNacional = nodoComplementoExpendio.ChildNodes[0];
                                    Nacional objNacional = new Nacional
                                    {
                                        RfcClienteOProveedor = nodoNacional.ChildNodes[0].InnerText,
                                        NombreClienteOProveedor = nodoNacional.ChildNodes[1].InnerText,
                                        PermisoProveedor = nodoNacional.ChildNodes[2].InnerText,
                                    };
                                    var subNodoCFDI = nodoNacional.ChildNodes[3];
                                    Cfdis objCfdis = new Cfdis
                                    {
                                        Cfdi = subNodoCFDI.ChildNodes[0].InnerText,
                                        TipoCfdi = subNodoCFDI.ChildNodes[1].InnerText,
                                        PrecioCompra = decimal.Parse(subNodoCFDI.ChildNodes[2].InnerText),
                                        PrecioDeVentaAlPublico = decimal.Parse(subNodoCFDI.ChildNodes[3].InnerText),
                                        PrecioVenta = decimal.Parse(subNodoCFDI.ChildNodes[4].InnerText),
                                        FechaYHoraTransaccion = DateTime.Parse(subNodoCFDI.ChildNodes[5].InnerText)
                                    };
                                    var subNodoVolumenDoc = subNodoCFDI.ChildNodes[6];
                                    VolumenDocumentado objVolumenDocumentado = new VolumenDocumentado
                                    {
                                        ValorNumerico = decimal.Parse(subNodoVolumenDoc.ChildNodes[0].InnerText),
                                        UM = subNodoVolumenDoc.ChildNodes[1].InnerText
                                    };
                                    objCfdis.VolumenDocumentado = objVolumenDocumentado;
                                    objNacional.Cfdis = objCfdis;
                                    objComplementoExpendio.Nacional = objNacional;

                                    objComplemento.Complemento_Expendio.Add(objComplementoExpendio);
                                }

                                objEntregas.Entrega.Add(objEntrega);

                            }

                            manguera.Entregas.Add(objEntregas);
                        }

                        dispensario.Manguera.Add(manguera);

                    }

                    lstDispensarios.Add(dispensario);
                }
                producto.Dispensario = lstDispensarios;

                LstProductos.Add(producto);
            }
            objControlVolumetrico.Productos = LstProductos;


            return objControlVolumetrico;

            */
            return null;
        }

    }
}
