using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSONMensual
{
    public class BitacoraMensual
    {
        public int NumeroRegistro { get; set; }
        public DateTime FechaYHoraEvento { get; set; }
        public String UsuarioResponsable { get; set; }
        public String DescripcionEvento { get; set; }
    }
}
