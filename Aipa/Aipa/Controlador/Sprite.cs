using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Controlador
{
    /// <summary>
    /// Sprite que se dibujara en pantalla
    /// </summary>
    public class Sprite
    {
        #region Constructor
        /// <summary>
        /// Instancia al Sprite a dibujar
        /// </summary>
        /// <param name="image">Imagen a dibujar</param>
        /// <param name="position">Posicion en pantalla donde se dibujara</param>
        public Sprite(Image image, Point position)
        {
            this.Image = image;
            this.Posicion = position;

            Visible = true;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Imagen a dibujar
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Posicion en pantalla donde se dibujara la imagen
        /// </summary>
        public Point Posicion { get; set; }
        /// <summary>
        /// Determina si se debe dibujar o no la imagen
        /// </summary>
        public bool Visible { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// Dibuja todos los sprites en pantalla
        /// </summary>
        /// <param name="baseImage">Imagen base a donde se dibujara</param>
        /// <param name="g">Clase con metodos de dibujado</param>
        public virtual void Draw(DrawHandler drawHandler)
        {
            if (this.Visible)
                drawHandler.Draw(this.Image, this.Posicion);
        }
        #endregion
    }
}
