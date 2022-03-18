using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class TANQUE
    {
        public string ClaveIdentificacionTanque { get; set; }
        public string LocalizacionYODescripcionTanque { get; set; }
        public string VigenciaCalibracionTanque { get; set; }
        public CapacidadTotalTanque CapacidadTotalTanque { get; set; }
        public CapacidadOperativaTanque CapacidadOperativaTanque { get; set; }
        public CapacidadUtilTanque CapacidadUtilTanque { get; set; }
        public VolumenMinimoOperacion VolumenMinimoOperacion { get; set; }
        public string EstadoTanque { get; set; }
        public MedicionTanque MedicionTanque { get; set; }
        public EXISTENCIAS EXISTENCIAS { get; set; }
        public RECEPCIONES RECEPCIONES { get; set; }




    }
}
