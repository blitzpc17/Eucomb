using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSON.Mensual
{
    public class VentaPorFecha
    {
        public string CodigoCliente { get; set; }    
        public string Cliente { get; set; }          
        public decimal Credito { get; set; }         
        public int IdFactura { get; set; }           
        public string NumeroFactura { get; set; }    
        public DateTime FechaVenta { get; set; }     
        public DateTime FechaFactura { get; set; }   
        public DateTime FechaTimbrado { get; set; }  
        public string UUID { get; set; }             
        public decimal Venta { get; set; }           
        public string TipoVenta { get; set; }        
        public string TipoFactura { get; set; }      
        public string Producto { get; set; }         
        public decimal Cantidad { get; set; }            
        public decimal IvaFactura { get; set; }      
        public decimal TotalFactura { get; set; }    
        public decimal SubTotalSinIva { get; set; }  
        public decimal SubTotalVenta { get; set; }   
        public decimal IvaVenta { get; set; }        
        public decimal IepsVenta { get; set; }       
        public decimal TotalVenta { get; set; }
    }
}
