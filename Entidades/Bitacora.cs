using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Bitacora
    {
        public int NumeroRegistro { get; set; }
        public DateTime FechaYHoraEvento { get; set; }
        public String UsuarioResponsable { get; set; }
        public int TipoEvento { get; set; }
        public string DescripcionEvento { get; set; }
    }
}
