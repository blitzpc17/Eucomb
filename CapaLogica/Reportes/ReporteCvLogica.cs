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
    public class ReporteCvLogica
    {
        public ControlVolumetrico ObjControlVolumetrico;
        public ReporteCvLogica()
        {

        }

        public ControlVolumetrico LeerReporteCvXml(String rutaArchivo)
        {
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
                producto.Dispensario = new List<Dispensario>();
              //  var nodosDispensarios = nodoPro.SelectSingleNode("//Covol:DISPENSARIO", nsm);
                for(int a = 5; a<nodoPro.ChildNodes.Count;a++)
                {
                    var nodoDispensario = nodoPro.ChildNodes[a];
                    Dispensario objDispensario = new Dispensario
                    {
                        ClaveDispensario = nodoDispensario.ChildNodes[0].InnerText
                    };
                    objDispensario.Manguera = new List<Manguera>();
                    for (int b = 2; b < nodoDispensario.ChildNodes.Count; b++)
                    {
                        var nodoManguera = nodoDispensario.ChildNodes[b];
                        Manguera objManguera = new Manguera
                        {
                            IdentificadorManguera = nodoManguera.ChildNodes[0].InnerText,
                        };
                        var nodeEntregas = nodoManguera.ChildNodes[1];

                        Entregas objEntregas = new Entregas
                        {
                            TotalEntregas = int.Parse(nodeEntregas.ChildNodes[0].InnerText),
                            TotalDocumentos = int.Parse(nodeEntregas.ChildNodes[2].InnerText),
                        };
                        var subNodoSumaVolEntregas = nodeEntregas.ChildNodes[1];
                        SumaVolumenEntregado objVolumenEntregado = new SumaVolumenEntregado
                        {
                            ValorNumerico = decimal.Parse(subNodoSumaVolEntregas.ChildNodes[0].InnerText),
                            UM = subNodoSumaVolEntregas.ChildNodes[1].InnerText
                        };
                        objEntregas.SumaVolumenEntregado = objVolumenEntregado;
                        objEntregas.Entrega = new List<Entrega>();
                        for(int c = 3; c < nodeEntregas.ChildNodes.Count; c++)
                        {
                            var nodoEntrega = nodeEntregas.ChildNodes[c];

                            Entrega objEntrega = new Entrega
                            {
                                NumeroDeRegistro = int.Parse(nodoEntrega.ChildNodes[0].InnerText),
                                TipoDeRegistro = nodoEntrega.ChildNodes[1].InnerText,
                                FechaYHoraEntrega = DateTime.Parse(nodoEntrega.ChildNodes[4].InnerText),
                            };
                            var nodoEntregadoTotalizadorAcum = nodoEntrega.ChildNodes[2];
                            VolumenEntregadoTotalizadorAcum objEntregadoTotalizadorAcum = new VolumenEntregadoTotalizadorAcum
                            {
                                ValorNumerico = decimal.Parse(nodoEntregadoTotalizadorAcum.ChildNodes[0].InnerText),
                                UM = nodoEntregadoTotalizadorAcum.ChildNodes[1].InnerText
                            };
                            objEntrega.VolumenEntregadoTotalizadorAcum = objEntregadoTotalizadorAcum;
                            var nodoEntregadoTotalizadorInsta = nodoEntrega.ChildNodes[3];
                            VolumenEntregadoTotalizadorInsta objEntregadoTotalizadorInsta = new VolumenEntregadoTotalizadorInsta
                            {
                               ValorNumerico = decimal.Parse(nodoEntregadoTotalizadorInsta.ChildNodes[0].InnerText),
                               UM = nodoEntregadoTotalizadorInsta.ChildNodes[1].InnerText
                            };
                            objEntrega.VolumenEntregadoTotalizadorInsta = objEntregadoTotalizadorInsta;

                            var nodoComplemento = nodoEntrega.ChildNodes[5];
                            Complemento objComplemento = new Complemento();
                            objComplemento.Complemento_Expendio = new List<Complemento_Expendio>();
                            for(int d = 0; d < nodoComplemento.ChildNodes.Count; d++)
                            {
                                var nodoComplementoExpendio = nodoComplemento.ChildNodes[d];
                                Complemento_Expendio objComplementoExpendio = new Complemento_Expendio();
                                var nodoNacional = nodoComplementoExpendio.ChildNodes[0];
                                Nacional objNacional = new Nacional
                                {
                                    RfcClienteOProveedor = nodoNacional.ChildNodes[0].InnerText,
                                    NombreClienteOProveedor = nodoNacional.ChildNodes[1].InnerText,
                                    PermisoProveedor = nodoNacional.ChildNodes[2].InnerText,
                                };
                                var nodoCfdis = nodoNacional.ChildNodes[3];
                                Cfdis objCfdis = new Cfdis
                                {
                                    Cfdi = nodoCfdis.ChildNodes[0].InnerText,
                                    TipoCfdi = nodoCfdis.ChildNodes[1].InnerText,
                                    PrecioCompra = decimal.Parse(nodoCfdis.ChildNodes[2].InnerText),
                                    PrecioDeVentaAlPublico = decimal.Parse(nodoCfdis.ChildNodes[3].InnerText),
                                    PrecioVenta = decimal.Parse(nodoCfdis.ChildNodes[4].InnerText),
                                    FechaYHoraTransaccion = DateTime.Parse(nodoCfdis.ChildNodes[5].InnerText),
                                };                                
                                var nodeVolumenDocumentado = nodoCfdis.ChildNodes[6];
                                VolumenDocumentado objVolumenDocumentado = new VolumenDocumentado
                                {
                                    ValorNumerico = decimal.Parse(nodeVolumenDocumentado.ChildNodes[0].InnerText),
                                    UM = nodeVolumenDocumentado.ChildNodes[1].InnerText
                                };
                                objCfdis.VolumenDocumentado = objVolumenDocumentado;
                                objNacional.Cfdis = objCfdis;

                                objComplementoExpendio.Nacional = objNacional;
                                
                                objComplemento.Complemento_Expendio.Add(objComplementoExpendio);
                            }
                            

                            objEntrega.Complemento = objComplemento;

                            objEntregas.Entrega.Add(objEntrega);
                        }


                        objManguera.Entregas = objEntregas;
                        objDispensario.Manguera.Add(objManguera);
                    }

                    producto.Dispensario.Add(objDispensario);
                }                

                LstProductos.Add(producto);
            }
            objControlVolumetrico.Productos = LstProductos;


            return objControlVolumetrico;
        }

    }
}
