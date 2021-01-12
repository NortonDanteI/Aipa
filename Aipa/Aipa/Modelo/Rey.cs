using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Rey : Pieza
    {
        #region Constructor
        public Rey(Image image, UnColor color) : base(image, color)
        {
            Movimientos = new Movimiento[]
            {
                new Movimiento(-1,-1, false),
                new Movimiento(-1,0, false),
                new Movimiento(-1,1, false),
                new Movimiento(0,-1, false),
                new Movimiento(0,1, false),
                new Movimiento(1,-1, false),
                new Movimiento(1,0, false),
                new Movimiento(1,1, false),
                new Movimiento(2, 0, false, Tipo_de_movimiento.especial), // enroque
                new Movimiento(-2, 0, false, Tipo_de_movimiento.especial),// enroque
            };
        }
        #endregion
    }
}
