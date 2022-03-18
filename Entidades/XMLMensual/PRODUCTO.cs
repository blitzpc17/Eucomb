using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class PRODUCTO
    {
        public string ClaveProducto { get; set; }
        public string ClaveSubProducto { get; set; }
        public Gasolina Gasolina { get; set; }
        public string MarcaComercial { get; set; }
      //  public TANQUE TANQUE { get; set; }  
       // public List<DISPENSARIO> Dispensario { get; set; }
       public REPORTEDEVOLUMENMENSUAL REPORTEDEVOLUMENMENSUAL { get; set; }


    }
}
