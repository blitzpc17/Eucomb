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
        [ExportCustom("RFC", 1)]
        public String RfcClienteOProveedor { get; set; }

        [ExportCustom("Nombre Cliente", 2)]
        public String NombreClienteOPRoveedor { get; set; }

        [ExportCustom("CFDI Cliente", 3)]
        public String CFDI { get; set; }

        [ExportCustom("Fecha Y Hora de Generación", 4)]
        public DateTime? FechaYHoraTransaccion { get; set; }

        [ExportCustom("Litros de Venta", 5)]
        public decimal VolumenNumerico { get; set; }

        [ExportCustom(" ", 6)]
        public String Existe { get; set; }

        [ExportCustom("folio_imp", 7)]
        public String folio_Imp { get; set; }

        [ExportCustom("No. Cliente", 8)]
        public String clavecli { get; set; }

        [ExportCustom("Nom. Cliente",9)]
        public String NombreCliente { get; set; }

        [ExportCustom("Serie Factura",10)]
        public String serie { get; set; }

        [ExportCustom("Folio Factura", 11)]
        public String docto { get; set; }

        [ExportCustom("Fecha y Hora Generación", 12)]
        public DateTime? fecha_reg { get; set; }

        [ExportCustom("Producto", 13)]
        public String nombrep { get; set; }

        [ExportCustom("Litros por Venta", 14)]
        public decimal Cant { get; set; }

        [ExportCustom("Precio por Litro", 15)]
        public decimal precio { get; set; }

        [ExportCustom("Importe de Venta", 16)]
        public decimal imported { get; set; }

        [ExportCustom("CFDI", 17)]
        public String UUID { get; set; }

        [ExportCustom("Compara Nombre", 18)]
        public bool ComparaNombre { get; set; }

        [ExportCustom("Compra CFDI", 19)]
        public bool ComparaCfdi { get; set; }

        [ExportCustom("Compara Litros", 20)]
        public bool ComparaLts { get; set; }

        [ExportCustom("Diferencia de LTs o ML", 21)]
        public decimal DiferenciaCantidades { get; set; }

        [ExportCustom("Observaciones", 22)]
        public String Observacion { get; set; }
    }
}
