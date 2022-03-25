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
        [ExportCustom("ns2:RfcClienteOProveedor6", 1)]
        public String RfcClienteOProveedor { get; set; }

        [ExportCustom("ns2:NombreClienteOProveedor7", 2)]
        public String NombreClienteOPRoveedor { get; set; }

        [ExportCustom("ns2:CFDI8", 3)]
        public String CFDI { get; set; }

        [ExportCustom("ns2:FechaYHoraTransaccion13", 4)]
        public DateTime? FechaYHoraTransaccion { get; set; }

        [ExportCustom("Valor númerico", 5)]
        public decimal VolumenNumerico { get; set; }

        [ExportCustom(" ", 6)]
        public String Existe { get; set; }

        [ExportCustom("folio_imp", 7)]
        public String folio_Imp { get; set; }

        [ExportCustom("cliente", 8)]
        public String clavecli { get; set; }

        [ExportCustom("nombre",9)]
        public String NombreCliente { get; set; }

        [ExportCustom("importe", 10)]
        public Decimal importe { get; set; }

        [ExportCustom("serie",11)]
        public String serie { get; set; }

        [ExportCustom("docto", 12)]
        public String docto { get; set; }

        [ExportCustom("status", 13)]
        public String status { get; set; }

        [ExportCustom("fecha_reg", 14)]
        public DateTime fecha_reg { get; set; }

        [ExportCustom("nombrep", 15)]
        public String nombrep { get; set; }

        [ExportCustom("cant", 16)]
        public decimal Cant { get; set; }

        [ExportCustom("precio", 17)]
        public decimal precio { get; set; }

        [ExportCustom("imported", 18)]
        public decimal imported { get; set; }

        [ExportCustom("UUID", 19)]
        public String UUID { get; set; }

        [ExportCustom("Compara Nombre", 20)]
        public bool ComparaNombre { get; set; }

        [ExportCustom("Compra CFDI", 21)]
        public bool ComparaCfdi { get; set; }

        [ExportCustom("Comprara LTS", 22)]
        public bool ComparaLts { get; set; }

        [ExportCustom("Diferencia Cantidades", 23)]
        public decimal DiferenciaCantidades { get; set; }

        [ExportCustom("Observaciones", 24)]
        public String Observacion { get; set; }
    }
}
