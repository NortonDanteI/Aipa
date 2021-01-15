using Aipa.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aipa.Modelo
{
    class Agente : Jugador
    {
        #region constructor
        public Agente(UnColor color, Tipo_de_jugador tipo_jugador, int numero) : base(color, tipo_jugador, numero)
        {

        }
        #endregion

        #region propiedades

        private List<Pieza> piezas { get; set; }
        #endregion

        private void Decidir_movimiento() { 
            //Consultar_piezas_del_tablero() lista de piezas
        }
    }
}


/*

private Movimiento minimax()
{
    return new Movimiento(0, 0, true, Tipo_de_movimiento.normal);
}

*/