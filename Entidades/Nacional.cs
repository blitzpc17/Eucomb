using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Nacional
    {
        public String RfcClienteOProveedor { get; set; }
        public String NombreClienteOProveedor { get; set; }
        public String PermisoProveedor { get; set; }
        public Cfdis   Cfdis { get; set; }
    }
}
