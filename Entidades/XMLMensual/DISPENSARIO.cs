using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class DISPENSARIO
    {       
        public string ClaveDispensario { get; set; }
        public MedicionDispensario MedicionDispensario { get; set; }
        public List<MANGUERA> MANGUERA { get; set; }
      

        


       

    }
}
