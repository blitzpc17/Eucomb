using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class clsReporteMensual
    {
        public String FolioImp { get; set; }
        public String NombreCliente { get; set; }
        public decimal Importe { get; set; }
        public String Serie { get; set; }
        public String Docto { get; set; }
        public String Status { get; set; }
        public DateTime FechaReg { get;set; }
        public String NombreRepresentante { get; set; }
        public decimal Cantidad { get; set; }   
        public decimal Precio { get; set; }
     //   public decimal Importe { get; set; }
        public String Uuid { get; set; }
    }
}
