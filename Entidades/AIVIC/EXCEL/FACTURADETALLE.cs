using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.AIVIC.EXCEL
{
    public class FACTURADETALLE
    {
        public string Numero { get; set; }
        public string Estacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }
        public decimal Iva { get; set; }
        public decimal Ieps { get; set; }
        public decimal Total { get; set; }
        public string UUID { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }


    }
}
