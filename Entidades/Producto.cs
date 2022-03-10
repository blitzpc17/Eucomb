using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public String ClaveProducto { get; set; }
        public String ClaveSubProduto { get; set; }
        public Gasolina Gasolina { get; set; }
        public String MarcaComercial { get; set; }
    }
}
