using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.cls
{
    public class clsPartidasInventario
    {
        public int Consecutivo { get; set; }
        public string NombreClienteProveedor { get; set; }
        public string RfcClienteProveedor { get; set; }
        public string Cfdi { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal ValorNumerico { get; set; }
    }
}
