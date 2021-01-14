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
        public List<Pieza> lstPiecesPlayer1 = new List<Pieza>();
        public List<Pieza> lstPiecesPlayer2 = new List<Pieza>();

        public List<Pieza> LstPiecesPlayer1 
        { 
            get { return lstPiecesPlayer1; } 
            set { lstPiecesPlayer1 = value.ToList(); } 
        }
        public List<Pieza> LstPiecesPlayer2
        {
            get { return lstPiecesPlayer2; }
            set { lstPiecesPlayer2 = value.ToList(); }
        }

        public Agente(UnColor color, Tipo_de_jugador tipo_De_Jugador) : base(color, tipo_De_Jugador, 1) { }

        private Movimiento minimax()
        {
            return new Movimiento(0, 0, true, Tipo_de_movimiento.normal);
        }
    }
}
