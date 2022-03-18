using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class Entregas
    {
        public int TotalEntregasMes { get; set; }
        public SumaVolumenEntregadoMes SumaVolumenEntregadoMes {get; set;}
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalEntregasMes { get; set; }
        public List<Complemento> Complemento { get; set; }

    }
}
