using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class EXISTENCIAS
    {
        public VolumenExistenciaAnterior VolumenExistenciasAnterior { get; set; }
        public VolumenAcumOpsRecepcion VolumenAcumOpsRecepcion { get; set; }
        public string HoraRecepcionAcumulado { get; set; }
        public VolumenAcumOpsEntrega VolumenAcumOpsEntrega { get; set; }
        public string HoraEntregaAcumulado { get; set; }
        public VolumenExistencias VolumenExistencias { get; set; }
        public DateTime FechaYHoraEstaMedicion { get; set; }
        public DateTime FechaYHoraMedicionAnterior { get; set; }
    }
}
