using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsReporte
    {
        public int TotalEntregas { get; set; }
        public decimal ValorNumerico3 { get; set;}
        public String UM4 { get; set; }
        public int TotalDucumentos { get; set; }
        public decimal ImporteTotalEntregas { get; set; }
        public string RfcCliente { get; set; }
        public String NombreCliente { get; set; }
        public String Cfdi { get; set; }
        public String TipoCfdi { get; set; }
        public decimal PrecioCompra { get; set; }   
        public decimal PrecioVentaPublico { get; set; } 
        public decimal PrecioVenta { get; set; }    
        public DateTime FechaHoraTransaccion { get; set; }  
        public decimal ValorNumerico14 { get; set; }
        public String UM15 { get; set; }    
    }
}
