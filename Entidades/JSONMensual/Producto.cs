using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class Producto
    {
        public string ClaveProducto { get; set; }  
        public string ClaveSubProducto { get; set; }
        public string ComposOctanajeGasolina { get; set; }
        public string MarcaComercial { get; set; }
        public ReporteDeVolumenMensual ReporteDeVolumenMensual { get; set; }

    }
}
