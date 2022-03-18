using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class NacionalEntrega
    {
        public String RfcClienteOProveedor { get; set; }
        public String  NombreClienteOProveedor { set; get; }
        public List<CfdiEntrega> CFDIs { get; set; }
    }
}
