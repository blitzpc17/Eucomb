using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class ControlesVolumetricos
    {
        public String Version { get; set; }
        public String RfcContribuyente { get; set; }
        public String RfcRepresentanteLegal { get; set; }
        public String RfcProveedor { get; set; }
        public String Caracter { get; set; }
        public String ModalidadPermiso { get; set; }
        public String NumPermiso { get; set; }
        public String ClaveInstralacion { get; set; }
        public String DescripcionInstalacion { get; set; }
        public int NumeroPozoa { get; set; }
        public int NumeroTanques { get; set; }
        public int NumeroDuctosEntradaSalida { get; set; }
        public int NumeroDuctosTransporteDistribucion { get; set; }
        public int NumeroDispensarios { get; set; }
        public DateTime FechaYHoraReporteMes { get; set; } 
        public List<Producto> Producto { get; set; }
        public List<BitacoraMensual> BitacoraMensual { get; set; }

    }
}
