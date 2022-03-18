using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class cdfi
    {
        public String Cfdi { get; set; }
        public String TipoCfdi { get; set; }
        public decimal PrecioCompra { get; set; }
        public DateTime FechaYhoraTransaccion { get; set; }
        public VolumenDocumentado VolumenDocumentado { get; set; }
    }
}
