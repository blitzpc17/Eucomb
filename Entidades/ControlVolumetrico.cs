using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ControlVolumetrico
    {
        public String Version { get; set; }
        public String RfcContribuyente { get; set; }
        public String RfcRepresentanteLegal { get; set; }
        public String RfcProveedor { get; set; }
        public Caracter Caracter { get; set; }
        public String ClaveInstalacion { get; set; }
        public String DescripcionInstalacion { get; set; }
        public int NumeroPozos { get; set; }
        public int NumeroTanques { get; set; }
        public int NumeroDuctosEntradaSalida { get; set; }
        public int NumeroDuctosTransporteDistribucion { get; set; }    
        public int NumeroDispensarios { get; set; }  
        public DateTime FechaYHoraCorte { get; set; }
        List<Producto> Productos { get; set; }


    }
}
