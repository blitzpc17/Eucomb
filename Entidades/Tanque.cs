using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Tanque
    {
        public String ClaveIdentificacionTanque { get; set; }
        public String LocalizacionYODescripcionTanque { get; set; }
        public DateTime VigenciaCalibracionTanque { get; set; }
        public CapacidadTotalTanque CapacidadTotalTanque { get; set; }
        public CapacidadOperativaTanque CapacidadOperativaTanque { get; set; }
        public CapacidadUtilTanque CapacidadUtilTanque { get; set; }
        public VolumenMinimoOperacion VolumenMinimoOperacion { get; set; }
        public int EstadoTanque { get; set; }
        public MedicionTanque MedicionTanque { get; set; }
        public Existencias Existencias { get; set; }
        public Recepciones Recepciones { get; set; }
        public Entregas Entregas { get; set; }

    }
}
