using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Entregas
    {
        public int TotalEntregas { get; set; }
        public SumaVolumenEntregado SumaVolumenEntregado { get; set; }
        public int TotalDocumentos { get; set; }
        public List<Entrega> Entrega { get; set; }
    }
}
