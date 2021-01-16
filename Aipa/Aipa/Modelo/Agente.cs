using Aipa.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Agente(int dificultad, UnColor color, Tipo_de_jugador tipo_jugador, int numero) : base(color, tipo_jugador, numero)
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

        private int profundidad { get; set; }
        #endregion

        public void Obtener_movimiento_optimo(List<Pieza> Piezas) {
           
            Pieza[,] tablero = new Pieza[8, 8];

            //inicializo arreglo vacio
            for (int a = 0; a < 8 ;a++) {
                for (int b = 0; b < 8; b++) {
                    tablero[a, b] = null;
                }
            }

            //obtengo piezas y asigno a tablero
            foreach (var piezita in Piezas) {
                tablero[piezita.Ubicacion.X, piezita.Ubicacion.Y] = piezita;         
            }

            //Console.WriteLine("Profundidad:" + this.dificultad_+"\n");
            //Console.WriteLine("Tablero:");

            //imprimir_tablero(tablero)

            //Ya tenemos el tablero
            Funcion_eval(tablero);
            Funcion_min_max(tablero);
        }

        private float Funcion_eval(Pieza[,] tablero)
        {
            int puntaje_negra=0;
            int puntaje_blanca=0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {

                    if (tablero[x, y] != null)
                    {
                        if (tablero[x, y].Color == base.Color)
                        {
                            puntaje_negra += tablero[x,y].valor_pieza;
                        }
                        else {
                            puntaje_blanca += tablero[x,y].valor_pieza;
                        }
                    }
                }
            }

            Console.WriteLine("Puntaje piezas negras: " + puntaje_negra);
            Console.WriteLine("Puntaje piezas blancas: " + puntaje_blanca);
            Console.WriteLine("Puntaje nodo: "+ (puntaje_negra - puntaje_blanca));
            return (puntaje_negra-puntaje_blanca);

        }

        private Point Funcion_min_max(Pieza[,] tablero) {
            Pieza[,] inicial = tablero;
            Pieza[,] resultado;

            Point mejor_accion = new Point(-1,-1); 
            float mejor_utilidad = 0;
            float utilidad;
            profundidad = 0;

            //sacar las piezas del jugador
            foreach (Pieza piezita in tablero)
            {
                if (piezita != null) {
                    if (piezita.Color == base.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                       
                        foreach (Point accion in acciones) {
                            Console.WriteLine("LA PIEZITA: " + piezita.GetType());
                            Console.WriteLine("UNA ACCION: "+accion);
                            
                            resultado = Modificar_tablero(inicial, piezita, accion);
                            
                            utilidad = Valor_min(resultado); 
                            /*
                            if (utilidad > mejor_utilidad)
                            {
                                mejor_accion = accion;
                                mejor_utilidad = utilidad;
                            }
                            */
                        }
                    }
                }
            }
            return mejor_accion;    
        }

        private float Valor_min(Pieza[,] nuevo_tablero) {      
            float utilidad=0;
            profundidad++;
            
            if (Es_terminal(nuevo_tablero) || profundidad==dificultad_) {
                return Funcion_eval(nuevo_tablero);
            }
            
            float menor_valor = 100000000000000;
            /*
            for accion in nuevo_tablero { //por cada pieza hacer una accion
                resultado = Modificar_tablero(nuevo_tablero, accion);
                utilidad = Valor_max(resultado);
                if (utilidad < menor_valor)
                {
                    menor_valor = utilidad;
                }
            }
            */
            return menor_valor;
        }

        private float Valor_max(Pieza[,] nuevo_tablero)
        {
            float utilidad = 0;
            profundidad++;
            /*
            if (Es_jaque(nuevo_tablero) || profundidad == dificultad_)
            {
                return Funcion_eval(nuevo_tablero);
            }
            */
            float mayor_valor = -100000000000000;
            /*
            for accion in nuevo_tablero { //por cada pieza hacer una accion
                resultado = Modificar_tablero(nuevo_tablero, accion);
                utilidad = Valor_min(resultado);
                if (utilidad > mayor_valor)
                {
                    mayor_valor = utilidad;
                }
            }
            */
            return mayor_valor;
        }

        private Pieza[,] Modificar_tablero(Pieza[,] inicial, Pieza piezita, Point accion) {
            //buscar piezita en inicial
            inicial[piezita.Ubicacion.X, piezita.Ubicacion.Y] = null;
            piezita.Ubicacion = new Point(accion.X, accion.Y);
            inicial[accion.X, accion.Y] = piezita;
            

            //recalcular movs
            //Recalcular(inicial);
            imprimir_console(inicial);

            return inicial;
        }
        private Pieza[,] Recalcular(Pieza[,] un_tablero) {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    //recalcular para peon
                    if (un_tablero[x, y].GetType() == typeof(Peon)) { 

                    }
                }
            }
            return un_tablero;
        }
        private void imprimir_tablero(Pieza[,] tablerito) {
            //muestro tablero
            for (int x = 0; x < 8; x++)
            {
                Console.WriteLine("\n------------------------");
                for (int y = 0; y < 8; y++)
                {

                    if (tablerito[x, y] == null)
                    {
                        Console.Write("Vacio|");
                        Console.WriteLine(x + "|" + y);
                    }
                    else
                    {
                        Console.Write(tablerito[x, y].GetType()+"|");
                        Console.WriteLine(x + "|" + y);
                    }
                }
            }
        }

        private Boolean Es_terminal(Pieza[,] nuevo_tablero) {
            /*
            var lstPiezas = nuevo_tablero.Where(x =>
                x.Color != piece.Color &&
                x.Ubicacion != newLocation
            ).ToList();// obtiene las piezas rivales para analizar si alguna deja en jaque al rey
            */
            return true;
        }

        private void imprimir_console(Pieza[,] tablero)
        {
            int i = -1;
            string tipo_pieza = "none";
            string color_pieza = "none";
            foreach (Pieza pieza in tablero)
            {
                i++;
                if (i % 8 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(" ---------------------------------");
                    Console.Write((8 - (i / 8)));
                }

                //string tipo_pieza = pieza.GetType().Name.ToString();
                if (pieza != null)
                {
                    tipo_pieza = (pieza.GetType().Name).ToString();
                    color_pieza = pieza.Color.ToString();
                }
                else tipo_pieza = "none";
                string str;

                switch (tipo_pieza)
                {
                    case "Peon":
                        {
                            str = "| " + "P ";
                            break;
                        }
                    case "Caballo":
                        {
                            str = "| " + "N ";
                            break;
                        }
                    case "Alfil":
                        {
                            str = "| " + "B ";
                            break;
                        }
                    case "Torre":
                        {
                            str = "| " + "R ";
                            break;
                        }

                    case "Reina":
                        {
                            str = "| " + "Q ";
                            break;
                        }

                    case "Rey":
                        {
                            str = "| " + "K ";
                            break;
                        }
                    default:
                        {
                            str = "| " + "  ";
                            break;
                        }
                }

                if (color_pieza == "Negro")
                {
                    str = str.ToLower();
                }

                Console.Write(str);

                if (i % 8 == 7)
                {
                    Console.Write("|");
                }
            }

            Console.WriteLine();
            Console.WriteLine(" ---------------------------------");
            Console.WriteLine("   A   B   C   D   E   F   G   H");
        }
    }
}