using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Aipa.Controlador;

namespace Aipa.Modelo
{
    /*Clase abstracta*/
    /// <summary>
    /// Pieza de ajedrez
    /// </summary>
    public abstract class Pieza : Sprite
    {
        #region Constructor
        public Pieza(Image image, UnColor color) : base(image, new Point())
        {
            this.Color = color;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Color de la pieza
        /// </summary>
        public UnColor Color { get; set; }

        /// <summary>
        /// Coordenada de la pieza en el tablero
        /// </summary>
        public Point Ubicacion { get; set; }

        /// <summary>
        /// Determina si la pieza esta seleccionada
        /// </summary>
        public bool Seleccionada { get; set; }

        /// <summary>
        /// Movimientos que puede realizar la pieza en el tablero
        /// </summary>
        public Movimiento[] Movimientos { get; set; }

        /// <summary>
        /// Movimientoss habilitados
        /// </summary>
        public Point[] Movimientos_permitidos { get; set; }

        /// <summary>
        /// Imagen a dibujar si la pieza esta seleccionada
        /// </summary>
        public Image SelectedImage { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// Dibuja todos los sprites en pantalla
        /// </summary>
        /// <param name="drawHandler">controlador de dibujado</param>
        public override void Draw(DrawHandler drawHandler)
        {
            if (this.Seleccionada)
                drawHandler.Draw(this.SelectedImage, this.Posicion);

            base.Draw(drawHandler);
        }
        #endregion
    }

    #region Clase enumerada
    /// <summary>
    /// Colores de las piezas 
    /// </summary>
    public enum UnColor
    {
        Negro,
        Blanco
    }
    #endregion
}
