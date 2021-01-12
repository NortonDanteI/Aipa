using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Torre : Pieza
    {
        #region Constructor
        public Torre(Image image, UnColor color) : base(image, color)
        {
            Movimientos = new Movimiento[]
            {
                new Movimiento(0,1),
                new Movimiento(0,-1),
                new Movimiento(1,0),
                new Movimiento(-1,0),
            };
        }
        #endregion
    }
}
