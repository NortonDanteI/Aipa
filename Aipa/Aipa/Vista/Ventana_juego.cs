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
    public partial class Ventana_juego : Form
    {
        public Ventana_juego()
        {
            InitializeComponent();
        }

        #region Funcionalidades del formulario arrastrar, y redimensionar
        /*Metodo para re dimensionar el tamaño de un formulario en tiempo de ejecucion, el tamaño de la chuleta (rectangulo infrerior) sera definido en 10*/
        private readonly int tolerance = 10;
        /*El mensaje WM_NCHITTEST se envía a una ventana cuando el cursor se mueve o cuando se pulsa o se suelta uno de los botones del ratón.*/
        private const int WM_NCHITTEST = 132;
        /*la pantalla se modifica en todos los sentidos con el valor 17*/
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    /*LParam para obtener las coordenadas x,y*/
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        /*Dibujo el rectangulo pero solo su interior */
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            /*Excluyo el exterior del rectangulo*/
            region.Exclude(sizeGripRectangle);
            this.panel_contenedor.Region = region;
            this.Invalidate();
        }
        /*Color y Grip (las lineas, la maya) del rectangulo*/
        protected override void OnPaint(PaintEventArgs e)
        {
            /*color del rencgulo*/
            SolidBrush blueBrush = new SolidBrush(SystemColors.ControlLightLight);
            /*relleno el interior*/
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            /*lo pinto*/
            base.OnPaint(e);
            /*incluyo una maya, las linea horizontales dentro del rectangulo para que se pueda ver por el usuario es decir hacerlo mas intuitivo*/
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        #endregion

        #region Eventos
        private void Boton_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro que quieres salir", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Boton_minimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
      

        /*declaro variables para capturar posicion y tamaño antes de maximizar, para poder restaurar despues a la posicion y tamaño que tenia antes de maximizar*/
        int lx, ly;
        int sw, sh;

        int posY = 0;
        int posX = 0;

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

        private void Boton_maximizar_Click(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Boton_restaurar_Click(object sender, EventArgs e)
        {
            boton_maximizar.Visible = true;
            boton_restaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }
        private void Boton_manual_usuario_click(object sender, EventArgs e)
        {
            boton_manual_usuario.BackColor = SystemColors.MenuHighlight;
            emoticon_manual.BackColor = SystemColors.MenuHighlight;
        }
        #endregion
    }
}
