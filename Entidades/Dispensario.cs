using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Dispensario
    {
        public String ClaveDispensario { get; set; }
        public MedicionDispensario MedicionDispensario { get; set;}
        public List<Manguera> Manguera { get; set; } 

    }
}
