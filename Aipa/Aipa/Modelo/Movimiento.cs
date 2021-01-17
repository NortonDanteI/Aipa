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
        public Movimiento(int x, int y, bool isLinear = true, Tipo_de_movimiento tipo_de_movimiento = Tipo_de_movimiento.normal)
        {
            this.Direccion = new Point(x, y);
            this.Tipo_de_mov = tipo_de_movimiento;
            this.IsLinear = isLinear;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Direccion en la cual se realiza el movimiento
        /// </summary>
        public Point Direccion { get; set; }

        /// <summary>
        /// Tipo de movimiento
        /// </summary>
        public Tipo_de_movimiento Tipo_de_mov { get; set; }

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
        normal = 1,     // Movimiento y ataque a celda destino
        especial = 2    // Movimiento que depende de la posicion de la pieza en el tablero
    }
    #endregion
}

