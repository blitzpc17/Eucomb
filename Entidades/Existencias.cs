using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Existencias
    {
        public VolumenExistenciasAnterior VolumenExistenciasAnterior { get; set; }
        public VolumenAcumOpsRecepcion VolumenAcumOpsRecepcion { get; set; }
        public DateTime HoraRecepcionAcumulado { get; set; }
        public VolumenAcumOpsEntrega VolumenAcumOpsEntrega { get; set; }
        public  DateTime HoraEntregaAcumulado { get; set; }
        public VolumenExistencias VolumenExistencias { get; set; }
        public DateTime FechaYHoraEstaMedicion { get; set; }
        public DateTime FechaYHoraMedicionAnterior { get; set; }



    }
}
