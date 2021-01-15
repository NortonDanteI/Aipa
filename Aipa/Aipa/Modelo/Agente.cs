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

        /// <summary>
        /// Agente
        /// </summary>
        /// <param name="color">Color de las piezas del agente</param>
        /// <param name="tipo_jugador">Tipo de jugador</param>
        /// <param name="numero">Numero del jugador</param>
        public Agente(int dificultad,UnColor color, Tipo_de_jugador tipo_jugador, int numero) : base(color, tipo_jugador, numero)
        {
            this.dificultad_ = dificultad;
        }
        #endregion

        #region propiedades

        private List<Pieza> Mis_piezas { get; set; }
        private List<Pieza> Piezas_rival { get; set; }

        /// <summary>
        /// proundidad a la que va a llegar el agente
        /// </summary>
        private int dificultad_ { get; set; }
        #endregion

        public void Obtener_movimiento_optimo(List<Pieza> Piezas) {
            Console.WriteLine("MIS PIEZAS");
            Mis_piezas = Piezas.FindAll(x => x.Color == base.Color);
            foreach (var dto in Mis_piezas)
            {
                Console.WriteLine(dto.GetType());
                Console.WriteLine(dto.Movimientos_permitidos.Count());
            }

            int moves = Piezas.Where(x => x.Color == base.Color).Sum(x => x.Movimientos_permitidos.Count());
            Console.WriteLine(moves + "QUE CARAJO");

            int movess = Piezas.Where(x => x.Color != base.Color).Sum(x => x.Movimientos_permitidos.Count());
            Console.WriteLine(movess + "QUE CARAJO version 2xd");

            Console.WriteLine("PIEZAS DEL RIVAL");
            Piezas_rival = Piezas.FindAll(y => y.Color != base.Color);
            foreach (var dto2 in Piezas_rival)
            {
                Console.WriteLine(dto2.GetType());
                Console.WriteLine(dto2.Movimientos_permitidos.Count());
            }


        }
    }
}