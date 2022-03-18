using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class ENTREGAS
    {
        public int TotalEntregasMes { get; set; }
        public SumaVolumenEntregado SumaVolumenEntregado { get; set; }
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalEntregasMes { get; set; }
        public Complemento Complemento { get; set; }
    }
}
