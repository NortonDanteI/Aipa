using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Celda_tablero
    {
        /// <summary>
        /// Posicion en pantalla de la celda
        /// </summary>
        public Point Posicion_en_tablero { get; set; }
        /// <summary>
        /// Determina si la ficha seleccionada puede moverse a esta celda
        /// </summary>
        public bool Puede_moverse { get; set; }
    }
}
