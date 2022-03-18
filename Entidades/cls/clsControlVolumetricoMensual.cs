using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.cls
{
    public class clsControlVolumetricoMensual
    {
        public String RfcClienteOProveedor { get; set; }
        public String NombreClienteOProveedor { set; get; }
        public String CFDI { get; set; }
        public DateTime FechaYHoraTransaccion { get; set; }
        public Decimal ValorNumerico { get; set; }
    }
}
