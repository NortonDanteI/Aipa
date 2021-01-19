using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aipa.Vista
{
    public partial class Ventana_manual : Form
    {
        int lx, ly;
        int sw, sh;
        int posY = 0;
        int posX = 0;

        public Ventana_manual()
        {
            InitializeComponent();
        }

        private void Boton_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro que quieres salir", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Barratitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left += (e.X - posX);
                Top += (e.Y - posY);
            }
        }
        private void boton_cerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void boton_maximizar_Click_1(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            /*hago no visibile y visible para que el icono se maximizar se vea y una ves presionado desaparesca y aparesca el de restaurar*/
            boton_maximizar.Visible = false;
            boton_restaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void boton_restaurar_Click_1(object sender, EventArgs e)
        {
            boton_maximizar.Visible = true;
            boton_restaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }
    }
}
