using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.cls
{
    public class FACTURASDETALLE
    {
        public String folio_imp { get; set; }
        public String cliente { get; set; }
        public String nombre { get; set; }
        public decimal importe { get; set; }
        public string serie { get; set; }
        public string docto { get; set; }
        public string status { get; set; }  
        public DateTime fec_reg { get; set; }
        public String nombrep { get; set; }
        public decimal cant { get; set; }
        public decimal precio { get; set; }
        public decimal imported { get; set; }
        public String uuid { get; set; }

    }
}
