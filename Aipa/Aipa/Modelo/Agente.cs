using Aipa.Controlador;
using Aipa.Modelo;
using Aipa.Vista;
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
        private Jugador jugador_actual { get; set; }

        /// <summary>
        /// proundidad a la que va a llegar el agente
        /// </summary>
        private int dificultad_ { get; set; }
        private int profundidad { get; set; }
        #endregion

        public List<Pieza> Obtener_movimiento_optimo(List<Pieza> Piezas) {
           
            Pieza[,] tablero = new Pieza[8, 8];
            Pieza[,] mejor_tablero;

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
            mejor_tablero = Funcion_min_max(tablero);
            return Array_to_List(mejor_tablero);


        }

        private List<Pieza> Array_to_List(Pieza[,] piezas)
        {
            List<Pieza> salida=null ;
            foreach (Pieza p in piezas)
            {
                if (p != null)
                    salida.Add(p);
            }
            return salida;
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

        private Pieza[,] Funcion_min_max(Pieza[,] tablero_entrada) {
            Console.WriteLine("\t MIN-MAX FUNCTION ");
            Pieza[,] tablero_inicial_minimax = new Pieza[8,8];
            Array.Copy(tablero_entrada, tablero_inicial_minimax, 64);
            Pieza[,] tablero_resultado,  mejor_piezas = null;
            jugador_actual = new Jugador(UnColor.Negro, Tipo_de_jugador.Agente, 2);

            Point mejor_accion = new Point(-1,-1); 
            float mejor_utilidad = -1;
            float utilidad;
            profundidad = 0;

            //sacar las piezas del jugador
            foreach (Pieza piezita in tablero_inicial_minimax)
            {
                if (piezita != null) {
                    Pieza pieza_aux = null;
                    switch ((piezita.GetType().Name).ToString())
                    {
                        case "Peon":
                            {
                                pieza_aux = new Peon(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Caballo":
                            {
                                pieza_aux = new Caballo(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Alfil":
                            {
                                pieza_aux = new Alfil(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                break;
                            }
                        case "Torre":
                            {
                                pieza_aux = new Torre(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Reina":
                            {
                                pieza_aux = new Reina(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Rey":
                            {
                                pieza_aux = new Rey(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    if (piezita.Color == jugador_actual.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                        foreach (Point accion in acciones) {
                            Point accion_aux = new Point(accion.X, accion.Y);
                            Console.WriteLine("MAX nivel: " + profundidad);
                            tablero_resultado = Modificar_tablero(tablero_inicial_minimax, pieza_aux, accion_aux);
                            utilidad = Valor_min(tablero_resultado);
                            jugador_actual.Color = UnColor.Negro;
                            jugador_actual.Numero = 2;
                            jugador_actual.Tipo_jugador = Tipo_de_jugador.Agente;
                            if (utilidad > mejor_utilidad)
                            {
                                mejor_piezas = tablero_resultado;
                                mejor_accion = accion;
                                mejor_utilidad = utilidad;
                            }
                        }
                    }
                }
            }
            return mejor_piezas;    
        }

        private float Valor_min(Pieza[,] _tablero)
        {
            Console.WriteLine("\t MIN FUNCTION ");
            float utilidad=0;
            Pieza[,] tablero_inicial_min = new Pieza[8, 8];
            Array.Copy(_tablero, tablero_inicial_min, 64);
            Pieza[,] tablero_resultado = new Pieza[8,8];
            jugador_actual.Color = UnColor.Blanco ;
            jugador_actual.Numero = 1;
            jugador_actual.Tipo_jugador = Tipo_de_jugador.Humano;
            profundidad++;
            
            if (Es_terminal(tablero_inicial_min) || profundidad >= dificultad_) {
                profundidad--;
                return Funcion_eval(tablero_inicial_min);
            }            
            float menor_valor = 100000000000000;

            foreach (Pieza piezita in _tablero)
            {
                if (piezita != null)
                {
                    Pieza pieza_aux = null;
                    switch ((piezita.GetType().Name).ToString())
                    {
                        case "Peon":
                            {
                                pieza_aux = new Peon(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos            = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos           , pieza_aux.Movimientos           , piezita.Movimientos.Length);
                                break;
                            }
                        case "Caballo":
                            {
                                pieza_aux = new Caballo(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Alfil":
                            {
                                pieza_aux = new Alfil(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                break;
                            }
                        case "Torre":
                            {
                                pieza_aux = new Torre(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Reina":
                            {
                                pieza_aux = new Reina(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        case "Rey":
                            {
                                pieza_aux = new Rey(piezita.Image, piezita.Color);
                                pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                                pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                                pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                                pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                                Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                                Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    if (piezita.Color == jugador_actual.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                        foreach (Point accion in acciones)
                        {
                            Point accion_aux = new Point(accion.X, accion.Y);
                            Console.WriteLine("MIN nivel: " + profundidad);
                            tablero_resultado = Modificar_tablero(tablero_inicial_min, pieza_aux, accion_aux);
                            utilidad = Valor_max(tablero_resultado);

                            jugador_actual.Color = UnColor.Blanco;
                            jugador_actual.Numero = 1;
                            jugador_actual.Tipo_jugador = Tipo_de_jugador.Humano;

                            if (utilidad < menor_valor)
                            {
                                menor_valor = utilidad;
                            }
                        }

                    }
                }
            }
            profundidad--;
            return menor_valor;
        }

        private float Valor_max(Pieza[,] _tablero)
        {
            Console.WriteLine("\t MAX FUNCTION ");
            float utilidad = 0;
            Pieza[,] tablero_inicial_max = new Pieza[8,8];
            Array.Copy(_tablero, tablero_inicial_max, 64);
            Pieza[,] tablero_resultado = new Pieza[8, 8];
            jugador_actual.Color = UnColor.Negro;
            jugador_actual.Numero = 2;
            jugador_actual.Tipo_jugador = Tipo_de_jugador.Agente;
            profundidad++;

            if (Es_terminal(tablero_inicial_max) || profundidad >= dificultad_)
            {
                profundidad--;
                return Funcion_eval(tablero_inicial_max);
            }
            float mayor_valor = 100000000000000;

            foreach (Pieza piezita in tablero_inicial_max)
            {
                Pieza pieza_aux = null;
                switch ((piezita.GetType().Name).ToString())
                {
                    case "Peon":
                        {
                            pieza_aux = new Peon(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            break;
                        }
                    case "Caballo":
                        {
                            pieza_aux = new Caballo(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            break;
                        }
                    case "Alfil":
                        {
                            pieza_aux = new Alfil(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            break;
                        }
                    case "Torre":
                        {
                            pieza_aux = new Torre(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            break;
                        }
                    case "Reina":
                        {
                            pieza_aux = new Reina(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            break;
                        }
                    case "Rey":
                        {
                            pieza_aux = new Rey(piezita.Image, piezita.Color);
                            pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                            pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                            pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                            pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                            Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                            Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


                if (piezita != null)
                {
                    if (piezita.Color == jugador_actual.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                        foreach (Point accion in acciones)
                        {
                            Point accion_aux = new Point(accion.X, accion.Y);
                            Console.WriteLine("MAX nivel: " + profundidad);
                            tablero_resultado = Modificar_tablero(tablero_inicial_max, pieza_aux, accion_aux);
                            utilidad = Valor_min(tablero_resultado);

                            jugador_actual.Color = UnColor.Negro;
                            jugador_actual.Numero = 2;
                            jugador_actual.Tipo_jugador = Tipo_de_jugador.Agente;

                            if (utilidad > mayor_valor)
                                mayor_valor = utilidad;
                        }
                    }
                }
            }
            profundidad--;
            return mayor_valor;
        }

        private Pieza[,] Modificar_tablero(Pieza[,] _tablero, Pieza piezita, Point accion) {
            //buscar piezita en inicial
            Pieza[,] tablero_mod = new Pieza[8, 8];
            Array.Copy(_tablero, tablero_mod, 64); // NOTA. EL TABLERO HAY QUE CLONARO PARTE POR PARTE.

            Pieza pieza_aux = null;
            switch ((piezita.GetType().Name).ToString())
            {
                case "Peon":
                    {
                        pieza_aux = new Peon(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        break;
                    }
                case "Caballo":
                    {
                        pieza_aux = new Caballo(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        break;
                    }
                case "Alfil":
                    {
                        pieza_aux = new Alfil(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        break;
                    }
                case "Torre":
                    {
                        pieza_aux = new Torre(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        break;
                    }
                case "Reina":
                    {
                        pieza_aux = new Reina(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        break;
                    }
                case "Rey":
                    {
                        pieza_aux = new Rey(piezita.Image, piezita.Color);
                        pieza_aux.Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y);
                        pieza_aux.Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y);
                        pieza_aux.Movimientos = new Movimiento[piezita.Movimientos.Length];
                        pieza_aux.Movimientos_permitidos = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, pieza_aux.Movimientos_permitidos, piezita.Movimientos_permitidos.Length);
                        Array.Copy(piezita.Movimientos, pieza_aux.Movimientos, piezita.Movimientos.Length);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            tablero_mod[piezita.Ubicacion.X, piezita.Ubicacion.Y] = null;
            pieza_aux.Ubicacion = accion;
            tablero_mod[accion.X, accion.Y] = pieza_aux;
            //recalcular movs validos
            tablero_mod = Recalcular(tablero_mod);
            imprimir_console(tablero_mod);

            return tablero_mod;
        }

        private Pieza[,] Recalcular(Pieza[,] un_tablero) {
            List<Pieza> lst_tablero = new List<Pieza>();

            foreach (Pieza p in un_tablero) 
            {
                if (p != null)
                {
                    lst_tablero.Add(p);
                }
            }
            lst_tablero = Set_movimientos_posibles(lst_tablero);

            Pieza[,] salida = new Pieza [8,8];
            foreach (Pieza pi in lst_tablero)
            {
                salida[pi.Ubicacion.X, pi.Ubicacion.Y] = pi;
            }

            return un_tablero;
        }

        private Boolean Es_terminal(Pieza[,] nuevo_tablero) {
            /*
            var lstPiezas = nuevo_tablero.Where(x =>
                x.Color != piece.Color &&
                x.Ubicacion != newLocation
            ).ToList();// obtiene las piezas rivales para analizar si alguna deja en jaque al rey
            */
            return false;
        }

        private void imprimir_console(Pieza[,] tablero)
        {
            int i = 65;
            string tipo_pieza = "none";
            string color_pieza = "none";
            Console.Write("\n   0   1   2   3   4   5   6   7");
            foreach (Pieza pieza in tablero)
            {
                i--;
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

                if (i % 8 == 1)
                {
                    Console.Write("|");
                }
            }

            Console.WriteLine();
            Console.WriteLine(" ---------------------------------");
            Console.WriteLine("   0   1   2   3   4   5   6   7");
            Console.WriteLine("   A   B   C   D   E   F   G   H");
        }

        private List<Pieza> Set_movimientos_posibles(List <Pieza> piezas)
        {
            piezas.ForEach(x => x.Movimientos_permitidos = Obtener_movimientos_posibles(x, piezas)); // movimientos habilitados que posee la pieza

            // valida los movimientos disponibles que puede realizar el jugador actual
            piezas.Where(x => x.Color == jugador_actual.Color).ToList().ForEach(p =>
            {

                p.Movimientos_permitidos = p.Movimientos_permitidos.Where(loc => Validar_movimiento(piezas, p, loc, jugador_actual.Color)).ToArray();
                // valida que el rey no quede en jaque al realizar el movimiento
            });
            return piezas;
        }

        public Point[] Obtener_movimientos_posibles(Pieza piece, List<Pieza> boardPiezas)
        {
            List<Point> lstAvailableCell = new List<Point>();
            if (piece != null)
            {
                Array.ForEach(piece.Movimientos, x =>
                {
                    var displacement = x.Direccion;
                    if (piece.Color == UnColor.Negro)
                    {
                        displacement = new Point(displacement.X * -1, displacement.Y * -1);
                        // si el turno es del jugado 2 invierto el desplazamiento para que las fichas bajen en vez de subir 
                    }

                    Point _location = piece.Ubicacion; // posicion inicial desde la cual se comienza a validar las celdas
                    while (true)
                    {
                        _location = new Point(_location.X + displacement.X, _location.Y + displacement.Y);
                        //posicion de celda a validar

                        if (_location.X > 7 || _location.Y > 7 || _location.X < 0 || _location.Y < 0)
                            break; // coordenada fuera del tablero "NO PERMITE MOVIMIENTO"

                        var _targetPiece = boardPiezas.FirstOrDefault(y => y.Ubicacion == _location);

                        if (x.Tipo_de_mov.HasFlag(Tipo_de_movimiento.especial)) // movimiento especial de pieza
                        {
                            /* !!! */
                            if (!Validar_movimiento_especial(
                                         boardPiezas,   piece,  x,  _targetPiece))
                            {
                                break; // "NO PERMITE MOVIMIENTO"
                            }
                        }
                        else
                        {
                            if (_targetPiece != null && _targetPiece.Color == piece.Color) // en la celda destino hay otra pieza
                                break; // la pieza destino es del mismo color que la pieza seleccionada "NO PERMITE MOVIMIENTO"
                        }

                        lstAvailableCell.Add(_location); // agrega la coordenada a la lista de celdas habilitadas "PERMITE MOVIMIENTO"

                        if (_targetPiece != null && _targetPiece.Color != piece.Color)
                            break; // no permite movimientos posteriores a la posicion de la pieza rival

                        if (!x.IsLinear) // si el movimiento de la ficha es lineal repite la operacion hasta encontrar una celda no habilitada
                            break;
                    }
                });
            }

            return lstAvailableCell.ToArray();
        }

        public bool Validar_movimiento_especial(List<Pieza> piezas, Pieza piece, Movimiento move, Pieza rivalPiece
                                                /*, List<Historial_acciones> ActionLog, Estado GameState */)
        {
            if (piece.GetType() == typeof(Peon))
            {
                #region movimientos espciales peon
                if (move.Direccion.X == 0 && move.Direccion.Y == -1) // desplazamiento frontal 1 casillero
                    return rivalPiece == null; // el casillero de en frente debe estar vacio

                if (move.Direccion.X == 0 && move.Direccion.Y == -2) // desplazamiento frontal 2 casillero, no permite atacar
                {
                    bool _condicion1 = rivalPiece == null; // la casilla destino debe estar vacia

                    bool _condicion2 = !piezas.Any(p => p.Ubicacion.X == piece.Ubicacion.X && p.Ubicacion.Y 
                    == (piece.Color == UnColor.Blanco ? 5 : 2)); // la casilla frontal debe esta vacia

                    bool _condicion3 = (piece.Color == UnColor.Blanco && piece.Ubicacion.Y == 6) 
                        || (piece.Color == UnColor.Negro && piece.Ubicacion.Y == 1); // debe ser el primer movimiento del peon

                    return _condicion1 && _condicion2 && _condicion3;
                }
                /*
                if ((move.Direccion.X == -1 || move.Direccion.X == 1) && move.Direccion.Y == -1) // ataque a pieza rival en diagonal
                {
                    //last entrega el ultimo elemento de la lista
                    var lastAction = ActionLog.LastOrDefault();
                    if (rivalPiece == null && lastAction != null) // COMER AL PASO
                    {
                        var Last_Move = lastAction.movimientos.Last();
                        // El peon rival debe estar en la misma fila que la pieza a mover, en una columna adyacente y debe haberce desplazado en el ultimo turno juegado 2 casilleros
                        if (color_jugador == UnColor.Blanco && piece.Ubicacion.Y == 3)  // el peon se desplaza hica arriba
                        {
                            //first devuelve el primer elemento que cumple la condicion especificada
                            var rival = piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X + move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == 2)
                                return true; // Habilita comer al paso
                        }
                        if (color_jugador == UnColor.Negro && piece.Ubicacion.Y == 4) // // el peon se desplaza hica abajo
                        {
                            var rival = piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X - move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == -2)
                                return true; // Habilita comer al paso
                        }
                    }

                    if (rivalPiece != null && rivalPiece.Color != piece.Color)
                        return true; // el movimiento se habilita solo si hay un rival en la posicion destino
                }*/
                #endregion
            }/*
            else if (piece.GetType() == typeof(Rey))
            {
                #region movimientos especiales rey
                if (GameState != Estado.Normal) // el rey no puede estar en jaque
                    return false;

                //Enrrosque
                bool kingFirstMove = !ActionLog.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. del rey
                if (!kingFirstMove)
                    return false;

                Point _moveDirection = color_jugador == UnColor.Blanco ? move.Direccion : new Point(move.Direccion.X * -1, move.Direccion.Y * -1);
                Pieza torre = null;

                if (_moveDirection.X < 0) // enrrosque largo
                    torre = piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                else // enrrosque corto
                    torre = piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);
                if (torre == null) // si no existe la torre se asume que fue eliminada o movida de su casillero original
                    return false;
                //any inspecciona todos todos los elementos
                bool torreFirstMove = !ActionLog.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. de la torre
                if (!torreFirstMove) // debe ser el primer mov. de la torre
                    return false;

                int _moveX = _moveDirection.X < 0 ? -1 : 1;
                Point _location = new Point(piece.Ubicacion.X + _moveX, piece.Ubicacion.Y);
                while (_location != torre.Ubicacion)
                {
                    bool _existPiece = piezas.Any(p => p.Ubicacion == _location);
                    if (_existPiece)
                        return false; // no debe existir pieza entre el rey y la torre

                    bool _attackLoc = piezas.Any(p => p.Color != piece.Color && p.Movimientos_permitidos != null && p.Movimientos_permitidos.Any(y => y == _location));
                    if (_attackLoc)
                        return false; // la ubicacion no puede estar bajo ataque de una pieza rival

                    _location = new Point(_location.X + _moveX, _location.Y);
                }

                return true; // el enroque es valido
                #endregion
            }
            */
            return false;
        }

        private bool Validar_movimiento(List<Pieza> piezas, Pieza piece, Point newLocation, UnColor color_jugador_actual)
        {
            Point _originalLocation = piece.Ubicacion; // posicion actual de la pieza
            piece.Ubicacion = newLocation; // asigna a la pieza la posicion nueva para analizar si el rey queda en jaque

            bool _result = true;

            var lstPiezas = piezas.Where(x =>
                x.Color != piece.Color &&
                x.Ubicacion != newLocation
            )
            .ToList();// obtiene las piezas rivales para analizar si alguna deja en jaque al rey

            if (lstPiezas.Any()) // lista de piezas rival que atacan a la pieza seleccionada
            {
                //Devuelve el primer elemento que cumple con la condicion
                var king = piezas.First(x => x.Color == piece.Color && x.GetType() == typeof(Rey));

                // recalculo los movimientos habilitados de las piezas para determinar si alguna pone en jaque al rey
                var lstBoardPiezas = piezas.Where(x => !(x.Color != piece.Color && x.Ubicacion == newLocation)).ToList(); // obtiene todas las piezas del tablero ignorando la pieza que se asume fue eliminada al realizar el movimiento
                foreach (var p in lstPiezas)
                {
                    var lstMoves = Obtener_movimientos_posibles(p, lstBoardPiezas); //  obtiene las ubicaciones de desplazamiento de cada una de las piezas del rival
                    if (lstMoves.Any(x => x == king.Ubicacion))
                    {
                        _result = false; // si al menos una ficha rival queda al alcance del rey el movimiento se invalida
                        break;
                    }
                }
            }
            piece.Ubicacion = _originalLocation; // asigno la ubicacion original de la pieza
            return _result;
        }

        private void Imprimir_movimientos_permitidos(Point[] punto)
        {
            foreach (var p in punto)
            {
                //Console.WriteLine("-------Imprimiendo movimientos permitidos-------:");
                Console.Write("(" + p.X + ",");
                Console.Write(p.Y + ")");
            }
        }
    }
}