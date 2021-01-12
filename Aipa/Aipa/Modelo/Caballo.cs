using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Caballo : Pieza
    {

        #region Constructor
        public Caballo(Image image, UnColor color) : base(image, color)
        {
            Movimientos = new Movimiento[]
            {
                new Movimiento(-1,-2, false),
                new Movimiento(1,-2, false),

                new Movimiento(-1,2, false),
                new Movimiento(1,2, false),

                new Movimiento(2,-1, false),
                new Movimiento(2,1, false),

                new Movimiento(-2,-1, false),
                new Movimiento(-2,1, false),
            };
        }
        #endregion
    }
}
