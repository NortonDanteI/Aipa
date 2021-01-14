using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aipa.Controlador
{
    /// <summary>
    /// Clase con la logica de dibujado
    /// </summary>
    public class DrawHandler : IDisposable
    {
        #region Constructor
        public DrawHandler(int width, int height)
        {
            try
            {
                Imagen_base = new Bitmap(width, height);
                graphics = Graphics.FromImage(Imagen_base);
            }
            catch { 
            }
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Imagen base sobre la cual se dibujan las demas imagenes
        /// </summary>
        public Image Imagen_base { get; private set; }

        /// <summary>
        /// Clase con funciones de dibujado
        /// </summary>
        private Graphics graphics { get; set; }
        #endregion

        #region Metodos
        public void Dispose()
        {
            if (graphics != null)
            {
                graphics.Dispose();
                Imagen_base = null;
            }
                
        }

        /// <summary>
        /// Dibuja una imagen en pantalla
        /// </summary>
        /// <param name="image">Imagen a dibujar</param>
        /// <param name="position">Posicion de la imagen en pantalla</param>
        public void Draw(Image image, Point position)
        {
            if (graphics != null) {
                graphics.DrawImage(image, position.X, position.Y, image.Width, image.Height);
            }
        }
        #endregion
    }
}
