using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aipa.Controlador;
using System.Drawing;

namespace Aipa.Modelo
{
    /// <summary>
    /// Informacion del tablero de juego
    /// </summary>
    public class Tablero : Sprite
    {
        #region Constructor
        public Tablero(Image boardImage, Image mover_imagen) : base(boardImage, new Point())
        {
            this.Mover_imagen = mover_imagen;

            Celdas = new Celda_tablero[8, 8];

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    int _x = (x * 80) + 9 * (x + 1);
                    int _y = (y * 70) + 5 * (y + 1);

                    Celdas[x, y] = new Celda_tablero()
                    {
                        Posicion_en_tablero = new Point(_x, _y),
                    };
                    // indica la posicion en pantalla de cada celda del tablero
                }
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Imagen a dibujar en celdas que permiten el movimiento de la pieza
        /// </summary>
        private Image Mover_imagen { get; set; }
        /// <summary>
        /// Informacion de las celdas del tablero
        /// </summary>
        public Celda_tablero[,] Celdas { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// Desmarca las celdas que permiten movimiento
        /// </summary>
        public void Desmarcar_celdas()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    Celdas[x, y].Puede_moverse = false;
        }
        /// <summary>
        /// Dibuja todos los sprites en pantalla
        /// </summary>
        /// <param name="drawHandler">controlador de dibujado</param>
        public override void Draw(DrawHandler drawHandler)
        {
            drawHandler.Draw(this.Image, this.Posicion);

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    if (Celdas[x, y].Puede_moverse)
                        drawHandler.Draw(this.Mover_imagen, Celdas[x, y].Posicion_en_tablero);
                }
        }
        #endregion
    }
}
