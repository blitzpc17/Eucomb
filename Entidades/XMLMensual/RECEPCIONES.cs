using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class RECEPCIONES
    {
        public int TotalRecepcionesMes { get; set; }
        public SumaVolumenRecepcion SumaVolumenRecepcionMes { get; set; }
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalRecepcionMensual { get; set; }
        public Complemento Complemento { get; set; }
    }
}
