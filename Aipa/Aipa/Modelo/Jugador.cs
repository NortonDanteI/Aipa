using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aipa.Modelo
{
    public class Jugador
    {
        /// <summary>
        /// Instancia a un jugador
        /// </summary>
        /// <param name="color">Color de fichas a utilizar</param>
        /// <param name="tipo_jugador">Tipo de jugador</param>
        /// <param name="numero">Numero del jugador</param>
        public Jugador(UnColor color, Tipo_de_jugador tipo_jugador, int numero)
        {
            this.Color = color;
            this.Tipo_jugador = tipo_jugador;
            this.Numero = numero;
        }

        /// <summary>
        /// Color de fichas que utiliza el jugador
        /// </summary>
        public UnColor Color { get; set; }

        /// <summary>
        /// Tipo de jugador
        /// </summary>
        public Tipo_de_jugador Tipo_jugador { get; set; }

        /// <summary>
        /// Numero de jugador
        /// </summary>
        public int Numero { get; set; }
    }

    public enum Tipo_de_jugador
    {
        Agente,
        Humano
    }
}
