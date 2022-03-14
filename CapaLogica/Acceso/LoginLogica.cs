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
            if ((usuario == "SA" && contrasena == "123456") ||
                (usuario == "MARIO" && contrasena == "1234")) return true; 
                    return false;
        }
    }
}
