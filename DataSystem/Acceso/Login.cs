using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica.Acceso;

namespace DataSystem.Acceso
{
    public partial class Login : Form
    {
        private LoginLogica contexto;
        public Login()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            Loguear();
        }



        private void InicializarFormulario()
        {
            contexto = new LoginLogica();
        }
        private void Loguear()
        {
            bool validacion = contexto.ValidarAcceso(txtUsuario.Text, txtContrasena.Text);
            if (validacion)
            {
                MessageBox.Show("¡Bienvenido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MDISistema dash = new MDISistema();
                this.Hide();
                dash.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Verifica que tus datos sean correctos.","¡Acceso denegado!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
