using ExcelWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.cls
{
    public class clsResultadosMensual
    {
        /*
        public String Contafolio_imp { get; set; }
        public String Contacliente { get; set; }
        public String Contanombre { get; set; }
        public decimal? Contaimporte { get; set; }
        public string Contaserie { get; set; }
        public string Contadocto { get; set; }
        public string Contastatus { get; set; }
        public DateTime? Contafec_reg { get; set; }
        public String Contanombrep { get; set; }
        public decimal? Contacant { get; set; }
        public decimal? Contaprecio { get; set; }
        public decimal? Contaimported { get; set; }
        public String Contauuid { get; set; }
        public String SysRfcClienteOProveedor { get; set; }
        public String SysNombreClienteOProveedor { set; get; }
        public String SysCFDI { get; set; }
        public DateTime? SysFechaYHoraTransaccion { get; set; }
        public Decimal? SysValorNumerico { get; set; }*/
        [ExportCustom("CFDI", 1)]
        public String CFDI { get; set; }
        [ExportCustom("Nombre Cliente PRoveedor", 2)]
        public String NombreClienteOPRoveedor { get; set; }
        [ExportCustom("Valor númerico", 3)]
        public decimal VolumenNumerico { get; set; }
        [ExportCustom("Folio_Imp", 4)]
        public String folio_Imp { get; set; }
        [ExportCustom("UUID", 5)]
        public String UUID { get; set; }
        [ExportCustom("Nombre Cliente", 6)]
        public String NombreCliente { get; set; }
        [ExportCustom("Cant", 7)]
        public decimal Cant { get; set; }
        [ExportCustom("Observaciones", 8)]
        public String Observacion { get; set; }
    }
}
