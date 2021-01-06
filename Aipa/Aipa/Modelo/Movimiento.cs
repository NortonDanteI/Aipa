using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    /// <summary>
    /// Informacion del movimiento que puede realizar una pieza
    /// </summary>
    public class Movimiento
    {
        #region Constructor
        public Movimiento(int x, int y, bool isLinear = true, Tipo_de_movimiento type = Tipo_de_movimiento.normal)
        {
            this.Direction = new Point(x, y);
            this.Type = type;
            this.IsLinear = isLinear;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Direccion en la cual se realiza el movimiento
        /// </summary>
        public Point Direction { get; set; }
        /// <summary>
        /// Tipo de movimiento
        /// </summary>
        public Tipo_de_movimiento Type { get; set; }
        /// <summary>
        /// Determina si el movimiento es lineal
        /// </summary>
        public bool IsLinear { get; set; }
        #endregion
    }

    #region Clase enumerada
    /// <summary>
    /// Tipo de movimiento que puede hacer una pieza
    /// </summary>
    public enum Tipo_de_movimiento
    {
        normal = 1, // Movimiento y ataque a celda destino
        especial = 2 // Movimiento que depende de la posicion de la piesa en el tablero
    }
    #endregion
}

