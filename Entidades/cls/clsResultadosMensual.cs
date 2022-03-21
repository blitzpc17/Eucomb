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
        [ExportCustom("Diferencia Cantidades", 8)]
        public decimal DiferenciaCantidades { get; set; }
        [ExportCustom("Observaciones", 9)]
        public String Observacion { get; set; }
    }
}
