using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class Cfdis
    {
        public string CFDI { get; set; }
        public string TipoCFDI { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioDeVentaAlPublico { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaYHoraTransaccion { get; set; }
        public VolumenDocumentado VolumenDocumentado { get; set; }
    }
}
