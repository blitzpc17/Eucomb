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

            XmlNode nodeClaveInstalacion = doc.SelectSingleNode("//Covol:ClaveInstalacion", nsm);
            XmlNode nodeDescripcionInstalacion = doc.SelectSingleNode("//Covol:DescripcionInstalacion", nsm);
            XmlNode nodeNumeroPozos = doc.SelectSingleNode("//Covol:NumeroPozos", nsm);
            XmlNode nodeNumeroTanques = doc.SelectSingleNode("//Covol:NumeroTanques", nsm);
            XmlNode nodeNumeroDuctosEntradaSalida = doc.SelectSingleNode("//Covol:NumeroDuctosEntradaSalida", nsm);
            XmlNode nodeNumeroDuctosTransporteDistribucion = doc.SelectSingleNode("//Covol:NumeroDuctosTransporteDistribucion", nsm);
            XmlNode nodeNumeroDispensarios = doc.SelectSingleNode("//Covol:NumeroDispensarios", nsm);
            XmlNode nodeFechaYHoraCorte = doc.SelectSingleNode("//Covol:FechaYHoraCorte", nsm);

            //String RfcContribuyente = nodeComprobante.InnerText;

            ControlVolumetrico objControlVolumetrico = new ControlVolumetrico
            {
                Version = nodeVersion.InnerText,
                RfcContribuyente = nodeRfcContribuyente.InnerText,
                RfcRepresentanteLegal = nodeRfcRepresentanteLegal.InnerText,
                RfcProveedor = nodeRfcProveedor.InnerText,
                ClaveInstalacion = nodeClaveInstalacion.InnerText,
                DescripcionInstalacion = nodeDescripcionInstalacion.InnerText,
                NumeroPozos = int.Parse(nodeNumeroPozos.InnerText),
                NumeroTanques = int.Parse(nodeNumeroTanques.InnerText),
                NumeroDuctosEntradaSalida = int.Parse(nodeNumeroDuctosEntradaSalida.InnerText),
                NumeroDuctosTransporteDistribucion = int.Parse(nodeNumeroDuctosTransporteDistribucion.InnerText),
                NumeroDispensarios = int.Parse(nodeNumeroDispensarios.InnerText),
                FechaYHoraCorte = DateTime.Parse(nodeFechaYHoraCorte.InnerText),
            };
            Caracter objCaracter = new Caracter
            {
                TipoCaracter = nodeCaracter.ChildNodes[0].InnerText,
                ModalidadPermiso = nodeCaracter.ChildNodes[1].InnerText,
                NumPermiso = nodeCaracter.ChildNodes[2].InnerText
            };
            objControlVolumetrico.Caracter = objCaracter;




            return objControlVolumetrico;
        }

    }
}
