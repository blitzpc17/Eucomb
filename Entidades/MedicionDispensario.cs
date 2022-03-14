using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MedicionDispensario
    {
        public String SistemaMedicionDispensario { get; set; }  
        public String LocalizODescripSistMedicionDispensario { get; set; }
        public DateTime VigenciaCalibracionSistMedicionDispensario { get; set; }
        public decimal IncertidumbreMedicionSistMedicionDispensario { get; set; }

    }
}
