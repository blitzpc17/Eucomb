using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica.Acceso
{
    public class LoginLogica
    {
        public LoginLogica()
        {

        }

        public bool ValidarAcceso(String usuario, String contrasena)
        {
            return (usuario == "SA" && contrasena == "123456");
        }
    }
}
