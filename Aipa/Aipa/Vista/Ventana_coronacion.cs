using System;
using Aipa.Modelo;
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
    public partial class Ventana_coronacion : Form
    {
        #region Constructor

        /// <summary>
        /// Formulario donde se selecciona la pieza en la cual se convertira el peon
        /// </summary>
        /// <param name="recursos">Recursos graficos del juego</param>
        /// <param name="color">Color de la pieza</param>
        public Ventana_coronacion(Recursos recursos, UnColor color)
        {
            InitializeComponent();
            this.Color = color;

            if (color == UnColor.Blanco)
            {
                pieza_reina.Image = recursos.Imagen_reina_blanca;
                pieza_alfil.Image = recursos.Imagen_alfil_blanco;
                pieza_caballo.Image = recursos.Imagen_caballo_blanco;
                pieza_torre.Image = recursos.Imagen_torre_blanca;
            }
            else
            {
                pieza_reina.Image = recursos.Imagen_reina_negra;
                pieza_alfil.Image = recursos.Imagen_alfil_negro;
                pieza_caballo.Image = recursos.Imagen_caballo_negro;
                pieza_torre.Image = recursos.Imagen_torre_negra;
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Color de la pieza
        /// </summary>
        private UnColor Color { get; set; }

        /// <summary>
        /// Pieza seleccionada
        /// </summary>
        public Pieza pieza_seleccionada { get; set; }
        #endregion

        #region Events
        private void Reina_click(object sender, EventArgs e)
        {
            pieza_seleccionada = new Reina(pieza_reina.Image, this.Color);
            Close();
        }

        private void Alfil_click(object sender, EventArgs e)
        {
            pieza_seleccionada = new Alfil(pieza_alfil.Image, this.Color);
            Close();
        }

        private void Caballo_click(object sender, EventArgs e)
        {
            pieza_seleccionada = new Caballo(pieza_caballo.Image, this.Color);
            Close();
        }

        private void Torre_click(object sender, EventArgs e)
        {
            pieza_seleccionada = new Torre(pieza_torre.Image, this.Color);
            Close();
        }
        #endregion
    }
}
