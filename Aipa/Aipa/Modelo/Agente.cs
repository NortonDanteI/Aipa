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
        public Agente(int dificultad, Jugador Jugador_jugando, List<Historial_acciones> ActionLog_, Estado GameState) : base(Jugador_jugando.Color, Jugador_jugando.Tipo_jugador, Jugador_jugando.Numero)
        {
            this.ActionLog_ = ActionLog_;
            this.Dificultad_ = dificultad;
            this.GameState = GameState;
            this.Jugador_actual_ = Jugador_jugando;
        }
        #endregion

        #region propiedades
        public List<Historial_acciones> ActionLog_ { get; set; }
        private Jugador Jugador_actual_ { get; set; }
        public Point Cell_location { get; set; }
        public Pieza Pieza_a_mover{ get; set; }
        private int Dificultad_ { get; set; }
        private int Profundidad { get; set; }
        private Estado GameState { get; set; }
        #endregion

        #region Alfa_Beta
        public void Alfa_Beta(List<Pieza> Piezas)
        {
            Pieza mejor_pieza;
            Point mejor_accion;
            float utilidad_final;
            Pieza[,] tablero = new Pieza[8, 8];

            foreach (var piezita in Piezas)
            {
                tablero[piezita.Ubicacion.X, piezita.Ubicacion.Y] = piezita;
            }
            
            (mejor_pieza, mejor_accion, utilidad_final) = Valor_max(tablero, -9900000, 9900000);
            this.Pieza_a_mover = mejor_pieza;
            this.Cell_location = mejor_accion;
            Console.WriteLine("UTILIDAD FINAL: "+utilidad_final);
        }

        private (Pieza, Point,float) Valor_max(Pieza[,] _tablero,float Alfa,float Beta)
        {

            if (GameState == Estado.Jaque || Profundidad >= Dificultad_)
            {
                Pieza p = null;
                Point a = new Point(99,99);
                float v = Funcion_eval(_tablero);
                Profundidad--;
                GameState = Estado.Normal;

                //Console.WriteLine("Alcanzo Profundidad");
                return (p,a,v);

                #region respaldo recuperacion variables
                /*
                Pieza pieza_null = null;
                Point punto_null = new Point(-100,-100);
                return (pieza_null, punto_null, valor);
                */
                #endregion
            }

            float mayor_valor = -1000000;
            Point mejor_accion = new Point(-100,-100);
            Point accion_;
            Pieza mejor_pieza = null;
            Pieza[,] tablero_inicial_max = new Pieza[8, 8];
            Array.Copy(_tablero, tablero_inicial_max, 64);
            Pieza[,] tablero_resultado = new Pieza[8, 8];

            Jugador_actual_.Color = UnColor.Negro;
            Jugador_actual_.Numero = 2;
            Jugador_actual_.Tipo_jugador = Tipo_de_jugador.Agente;
            Profundidad++;          

            foreach (Pieza piezita in tablero_inicial_max)
            {
                if (piezita != null)
                {
                    if (piezita.Color == Jugador_actual_.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                        foreach (Point accion in acciones)
                        {
                            Point accion_aux = accion;// new Point(accion.X, accion.Y);
                            Console.WriteLine("MAX nivel: " + Profundidad);
                            Array.Copy(
                                Modificar_tablero(tablero_inicial_max, piezita, accion_aux), 
                                tablero_resultado, 
                                64);


                            float utilidad;
                            Pieza piezita_;
                            (piezita_, accion_, utilidad) = Valor_min(tablero_resultado, Alfa, Beta);
                            if (piezita_ == null)
                            {
                                accion_ = accion_aux; 
                                piezita_ = piezita;
                            }

                            this.ActionLog_.RemoveAt(this.ActionLog_.Count() - 1);

                            #region cambiar jugador
                            Jugador_actual_.Color = UnColor.Negro;
                            Jugador_actual_.Numero = 2;
                            Jugador_actual_.Tipo_jugador = Tipo_de_jugador.Agente;
                            #endregion

                            if (utilidad > mayor_valor)
                            {
                                mayor_valor = utilidad;
                                mejor_accion = accion_;
                                mejor_pieza = piezita_;
                            }

                            if (mayor_valor >= Beta)
                            {
                                Console.WriteLine("\t Corto la rama");
                                Profundidad--;
                                return (mejor_pieza, mejor_accion, mayor_valor);
                            }
                            if (mayor_valor > Alfa)
                            {
                                Alfa = mayor_valor;
                            }
                        }
                    }
                }
            }
            Profundidad--;
            return (mejor_pieza, mejor_accion, mayor_valor);
        }
        private (Pieza, Point, float) Valor_min(Pieza[,] _tablero, float Alfa, float Beta)
        {
            if (GameState == Estado.Jaque || Profundidad >= Dificultad_)
            {
                Pieza p = null;
                Point a = new Point(99, 99);
                float v = Funcion_eval(_tablero);
                Profundidad--;
                GameState = Estado.Normal;

                Console.WriteLine("Alcanzo Profundidad");
                #region respaldo atributos minimax
                /*
                float valor = Funcion_eval(_tablero);

                Pieza pieza_null = null;
                Point punto_null = new Point(-100,-100);

                return (pieza_null,punto_null,valor);
                */
                #endregion
            }

            float menor_valor = 1000000;
            Point mejor_accion = new Point(-100, -100);
            Point accion_;
            Pieza mejor_pieza = null;
            Pieza[,] tablero_inicial_min = new Pieza[8, 8];
            Array.Copy(_tablero, tablero_inicial_min, 64);
            Pieza[,] tablero_resultado = new Pieza[8, 8];
            float utilidad;
            Pieza pieza_;

            Jugador_actual_.Color = UnColor.Blanco;
            Jugador_actual_.Numero = 1;
            Jugador_actual_.Tipo_jugador = Tipo_de_jugador.Humano;
            Profundidad++;

            foreach (Pieza piezita in _tablero)
            {
                if (piezita != null)
                {
                    if (piezita.Color == Jugador_actual_.Color)
                    {
                        Point[] acciones = piezita.Movimientos_permitidos;
                        foreach (Point accion in acciones)
                        {

                            Point accion_aux = accion;// new Point(accion.X, accion.Y);
                            Console.WriteLine("Min nivel: " + Profundidad);
                            Array.Copy(Modificar_tablero(tablero_inicial_min, piezita, accion_aux), tablero_resultado, 64);

                            (pieza_, accion_, utilidad) = Valor_max(tablero_resultado, Alfa, Beta);
                            if (pieza_ == null)
                            {
                                accion_ = accion_aux;
                                pieza_ = piezita;
                            }


                            this.ActionLog_.RemoveAt(this.ActionLog_.Count() - 1);

                            #region cambiar jugador
                            Jugador_actual_.Color = UnColor.Blanco;
                            Jugador_actual_.Numero = 1;
                            Jugador_actual_.Tipo_jugador = Tipo_de_jugador.Humano;
                            #endregion

                            if (utilidad < menor_valor) {
                                Console.WriteLine("Mejor utilidad blancas:" + utilidad);
                                menor_valor = utilidad;
                                mejor_accion = accion_;
                            }
                            if (menor_valor <= Alfa) {
                                Console.WriteLine("\t Corto la rama");
                                Profundidad--;
                                return (mejor_pieza, mejor_accion, menor_valor);
                            }
                            if (menor_valor > Beta) {
                                Beta = menor_valor;
                            }
                        }
                    }
                }
            }
            Profundidad--;
            return (mejor_pieza,mejor_accion, menor_valor);
        }
    
        private float Funcion_eval(Pieza[,] tablero)
        {
            int puntaje_negra = 0;
            int puntaje_blanca = 0;
            if (GameState == Estado.Jaque || GameState == Estado.Jaquemate ) {
                if (Jugador_actual_.Color == UnColor.Negro) {
                    //Console.WriteLine("es jaque -20k en negras");
                    puntaje_negra = -20000; 
                }
                else if(Jugador_actual_.Color == UnColor.Blanco){
                    //Console.WriteLine("es jaque -20k en blancas");
                    puntaje_blanca = -20000;
                }
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (tablero[x, y] != null)
                    {
                        if (tablero[x, y].Color == base.Color)
                            puntaje_negra += tablero[x, y].valor_pieza;
                        else
                            puntaje_blanca += tablero[x, y].valor_pieza;
                    }
                }
            }
            return (puntaje_negra - puntaje_blanca);
        }
        #endregion

        #region modificar tablero
        private Pieza[,] Modificar_tablero(Pieza[,] _tablero, Pieza piezita, Point accion)
        {
            //buscar piezita en inicial
            Pieza[,] tablero_mod = new Pieza[8, 8];
            Array.Copy(_tablero, tablero_mod, 64);

            Pieza pieza_aux = null;
            switch ((piezita.GetType().Name).ToString())
            {
                case "Peon":
                    {
                        pieza_aux = new Peon(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };
                        //pieza_aux.Seleccionada = 

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;

                        break;
                    }
                case "Caballo":
                    {
                        pieza_aux = new Caballo(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;
                        break;
                    }
                case "Alfil":
                    {
                        pieza_aux = new Alfil(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;
                        break;
                    }
                case "Torre":
                    {
                        pieza_aux = new Torre(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;
                        break;
                    }
                case "Reina":
                    {
                        Reina reina = new Reina(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };
                        pieza_aux = reina;

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;
                        break;
                    }
                case "Rey":
                    {
                        Rey rey = new Rey(piezita.Image, piezita.Color)
                        {
                            Ubicacion = new Point(piezita.Ubicacion.X, piezita.Ubicacion.Y),
                            Posicion = new Point(piezita.Posicion.X, piezita.Posicion.Y)
                        };
                        pieza_aux = rey;

                        Movimiento[] movimientos_aux = new Movimiento[piezita.Movimientos.Length];
                        Array.Copy(piezita.Movimientos, movimientos_aux, piezita.Movimientos.Length);
                        pieza_aux.Movimientos = movimientos_aux;

                        Point[] permitidos_aux = new Point[piezita.Movimientos_permitidos.Length];
                        Array.Copy(piezita.Movimientos_permitidos, permitidos_aux, piezita.Movimientos_permitidos.Length);
                        pieza_aux.Movimientos_permitidos = permitidos_aux;
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

            #region insertar al ActionLog_
            var ActionLog_ = new Historial_acciones();
            ActionLog_.movimientos.Add(new Historial_movimiento
            {
                Pieza_ = pieza_aux,
                Ubicacion_original = pieza_aux.Ubicacion,
                Ubicacion_nueva = accion
            });
            this.ActionLog_.Add(ActionLog_);

            #endregion

            //recalcular movs validos
            tablero_mod = Recalcular(tablero_mod); // SE RECALCULA PARA AMBOS. SALIDA Y ENTRADA
            //imprimir_console(tablero_mod);

            return tablero_mod;
        }

        private Pieza[,] Recalcular(Pieza[,] un_tablero)
        {
            List<Pieza> lst_tablero = new List<Pieza>();
            Pieza pieza_aux = null;

            foreach (Pieza p in un_tablero)
            {
                if (p != null)
                {
                    switch ((p.GetType().Name).ToString())
                    {
                        case "Peon":
                            {
                                pieza_aux = new Peon(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;

                                break;
                            }
                        case "Caballo":
                            {
                                pieza_aux = new Caballo(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;
                                break;
                            }
                        case "Alfil":
                            {
                                pieza_aux = new Alfil(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;
                                break;
                            }
                        case "Torre":
                            {
                                pieza_aux = new Torre(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;
                                break;
                            }
                        case "Reina":
                            {
                                pieza_aux = new Reina(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;
                                break;
                            }
                        case "Rey":
                            {
                                pieza_aux = new Rey(p.Image, p.Color)
                                {
                                    Ubicacion = new Point(p.Ubicacion.X, p.Ubicacion.Y),
                                    Posicion = new Point(p.Posicion.X, p.Posicion.Y)
                                };

                                Movimiento[] movimientos_aux = new Movimiento[p.Movimientos.Length];
                                Array.Copy(p.Movimientos, movimientos_aux, p.Movimientos.Length);
                                pieza_aux.Movimientos = movimientos_aux;

                                Point[] permitidos_aux = new Point[p.Movimientos_permitidos.Length];
                                Array.Copy(p.Movimientos_permitidos, permitidos_aux, p.Movimientos_permitidos.Length);
                                pieza_aux.Movimientos_permitidos = permitidos_aux;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    lst_tablero.Add(pieza_aux);
                }
            }

            lst_tablero = Set_movimientos_posibles(lst_tablero);

            int moves = lst_tablero.Where(x => x.Color == Jugador_actual_.Color).Sum(x => x.Movimientos_permitidos.Count());
           
            //FirstOrDefault Devuelve un elemento si encuentra un elemento que cumple con la condición
            var king = lst_tablero.FirstOrDefault(x => x.Color == Jugador_actual_.Color && x.GetType() == typeof(Rey));
   
            //Contains Devuelve un booleano si un elemento contiene un valor especifico en su secuencia
            //Any Devuelve un booleano si algun elemento de la secuencia satisface la condicion
            var isCheck = lst_tablero.Any(x => x.Color != Jugador_actual_.Color && x.Movimientos_permitidos.Contains(king.Ubicacion));//Si alguno de los movimientos contiene la ubicacion del rey                                                                                                                                   // valida si en la jugada anterior se dejo al rey en jaque

            if (isCheck)
            {
                Console.WriteLine("\n\tENTROOOOOOOOOOOOOOOOOO AL CHECK");
                this.GameState = Estado.Jaque;
                if (moves == 0) // si nos quedamos sin movimientos es jaque mate
                    this.GameState = Estado.Jaquemate;
            }
            else
            {
                if (moves == 0) // si nos quedamos sin movimientos y no es jaque, el juego queda en empate
                    this.GameState = Estado.Empate;
            }

            Pieza[,] salida = new Pieza[8, 8];
            foreach (Pieza pi in lst_tablero)
            {
                salida[pi.Ubicacion.X, pi.Ubicacion.Y] = pi;
            }

            return salida;
        }

        #endregion
        #region validacion de movimientos


        private List<Pieza> Set_movimientos_posibles(List<Pieza> piezas)
        {
            piezas.ForEach(x => x.Movimientos_permitidos = Obtener_movimientos_posibles(x, piezas)); // movimientos habilitados que posee la pieza

            // valida los movimientos disponibles que puede realizar el jugador actual
            piezas.Where(x => x.Color == Jugador_actual_.Color).ToList().ForEach(p =>
            {
                p.Movimientos_permitidos = p.Movimientos_permitidos.Where(loc => Validar_movimiento(piezas, p, loc)).ToArray();
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
                                         boardPiezas, piece, x, _targetPiece))
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
                                                /*, List<Historial_acciones> ActionLog_, Estado GameState */)
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
                if ((move.Direccion.X == -1 || move.Direccion.X == 1) && move.Direccion.Y == -1) // ataque a pieza rival en diagonal
                {
                    //last entrega el ultimo elemento de la lista
                    var lastAction = ActionLog_.LastOrDefault();
                    if (rivalPiece == null && lastAction != null) // COMER AL PASO
                    {
                        var Last_Move = lastAction.movimientos.Last();
                        // El peon rival debe estar en la misma fila que la pieza a mover, en una columna adyacente y debe haberce desplazado en el ultimo turno juegado 2 casilleros
                        if (Jugador_actual_.Color == UnColor.Blanco && piece.Ubicacion.Y == 3)  // el peon se desplaza hica arriba
                        {
                            //first devuelve el primer elemento que cumple la condicion especificada
                            var rival = piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X + move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == 2)
                                return true; // Habilita comer al paso
                        }
                        if (Jugador_actual_.Color == UnColor.Negro && piece.Ubicacion.Y == 4) // // el peon se desplaza hica abajo
                        {
                            var rival = piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X - move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == -2)
                                return true; // Habilita comer al paso
                        }
                    }

                    if (rivalPiece != null && rivalPiece.Color != piece.Color)
                        return true; // el movimiento se habilita solo si hay un rival en la posicion destino
                }
                #endregion
            }

            else if (piece.GetType() == typeof(Rey))
            {
                #region movimientos especiales rey
                if (GameState != Estado.Normal) // el rey no puede estar en jaque
                    return false;

                //Enrrosque
                bool kingFirstMove = !ActionLog_.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. del rey
                if (!kingFirstMove)
                    return false;

                Point _moveDirection = Jugador_actual_.Numero == 1 ? move.Direccion : new Point(move.Direccion.X * -1, move.Direccion.Y * -1);
                Pieza torre = null;

                if (_moveDirection.X < 0) // enrrosque largo
                    torre = piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                else // enrrosque corto
                    torre = piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);
                if (torre == null) // si no existe la torre se asume que fue eliminada o movida de su casillero original
                    return false;
                //any inspecciona todos todos los elementos
                bool torreFirstMove = !ActionLog_.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. de la torre
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
            return false;
        }

        private bool Validar_movimiento(List<Pieza> piezas, Pieza piece, Point newLocation)
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

        #endregion
    }
}