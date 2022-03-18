using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class ENTREGA
    {
        public int NumeroDeRegistro { get; set; }
        public string TipoDeRegistro { get; set; }
        public VolumenEntregadoTotalizadorAcum VolumenEntregadoTotalizadorAcum { get; set; }
        public VolumenEntregadoTotalizadorInsta VolumenEntregadoTotalizadorInsta { get; set; }
        public DateTime FechaYHoraEntrega { get; set; }
        public Complemento Complemento { get; set; }
    }
}
