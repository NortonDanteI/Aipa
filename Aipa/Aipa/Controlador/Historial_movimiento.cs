using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aipa.Modelo;
using System.Drawing;

namespace Aipa.Controlador
{
    public class Historial_movimiento
    {
        /// <summary>
        /// Pieza que realizo el movimiento
        /// </summary>
        public Pieza Pieza_ { get; set; }

        /// <summary>
        /// Coordenada en el tablero a donde estaba la pieza antes del movimiento
        /// </summary>
        public Point Ubicacion_original { get; set; }

        /// <summary>
        /// Coordenada del tablero donde donde se desplazo la pieza
        /// </summary>
        public Point Ubicacion_nueva { get; set; }
    }
}
