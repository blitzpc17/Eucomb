using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class Recepciones
    {
        public int TotalRecepcionesMes { get; set; }
        public SumaVolumenRecepcionesMes SumaVolumenRecepcionMes { get; set; }
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalRecepcionesMensual { get; set; }
        public List<Complemento> Complemento { get; set; }

    }
}
