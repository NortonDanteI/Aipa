using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Alfil : Pieza
    {
        #region Constructor
        public Alfil(Image image, UnColor color) : base(image, color)
        {
            Movimientos = new Movimiento[]
            {
                new Movimiento(1,1),
                new Movimiento(-1,1),
                new Movimiento(1,-1),
                new Movimiento(-1,-1),
            };
        }
        #endregion
    }
}
