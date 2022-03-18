using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class ReporteDeVolumenMensual
    {
        public ControlDeExistencias ControlDeExistencias { get; set; }
        public Recepciones Recepciones { get; set; }
        public Entregas Entregas { get; set; }  
        
    }
}
