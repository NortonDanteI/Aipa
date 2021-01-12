using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Modelo
{
    public class Peon : Pieza
    {
        #region Constructor
        public Peon(Image image, UnColor color) : base(image, color)
        {
            Movimientos = new Movimiento[]
            {
                new Movimiento(0,-1, false, Tipo_de_movimiento.especial), // al frente solo puede mover, no atacar
                new Movimiento(0,-2, false, Tipo_de_movimiento.especial), // al frente 2 casilleros solo puede mover, no atacar y se puede realizar si es el primer movimiento del peon
                new Movimiento(-1,-1, false, Tipo_de_movimiento.especial), // en diagonal solo puede atacar, no mover
                new Movimiento(1,-1, false, Tipo_de_movimiento.especial), // en diagonal solo puede atacar, no mover
            };
        }
        #endregion
    }
}
