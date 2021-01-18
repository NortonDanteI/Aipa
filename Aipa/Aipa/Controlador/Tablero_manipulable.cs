using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aipa.Modelo;
using Aipa.Vista;
using System.Drawing;
using System.Windows.Forms;

namespace Aipa.Controlador
{
    class Tablero_manipulable
    {
        #region Constructor
        public Tablero_manipulable()
        {
            Initialize();
            Comenzar_juego();
        }
        #endregion

        #region Propiedades

        /// <summary>
        /// Recursos de imagenes a usar en el juego
        /// </summary>
        public Recursos Resources { get; set; }

        /// <summary>
        /// Tablero de juego
        /// </summary>
        public Tablero Board { get; set; }

        /// <summary>
        /// Lista de piezas de ajedrez
        /// </summary>
        public List<Pieza> Piezas { get; set; }

        /// <summary>
        /// Informacion del juegador 1
        /// </summary>
        public Jugador Player1 { get; set; }

        /// <summary>
        ///  Informacion del juegador 2
        /// </summary>
        public Jugador Player2 { get; set; }

        /// <summary>
        /// Jugador que tiene el turno actual
        /// </summary>
        public Jugador Jugador_jugando { get; set; }

        /// <summary>
        /// Registro de las acciones realizadas en el utlimo turno
        /// </summary>
        public List<Historial_acciones> ActionLog { get; set; }

        /// <summary>
        /// Estado del juego
        /// </summary>
        public Estado GameState { get; set; }
        #endregion

        #region Metodos basicos

        /// <summary>
        /// Inicializa el juego cargando los recursos
        /// </summary>
        private void Initialize()
        {
            string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes");
            this.Resources = new Recursos()
            {
                Imagen_tablero = Cargar_imagen($"{directory}/Tablerov2.png"),
                Imagen_movimiento_resaltado = Cargar_imagen($"{directory}/Movimiento_resaltado.png"),
                Imagen_seleccion_resaltado = Cargar_imagen($"{directory}/Seleccion_resaltado.png"),
                Imagen_peon_blanco = Cargar_imagen($"{directory}/Peon_blanco.png"),
                Imagen_torre_blanca = Cargar_imagen($"{directory}/Torre_blanca.png"),
                Imagen_caballo_blanco = Cargar_imagen($"{directory}/Caballo_blanco.png"),
                Imagen_alfil_blanco = Cargar_imagen($"{directory}/Alfil_blanco.png"),
                Imagen_reina_blanca = Cargar_imagen($"{directory}/Reina_blanca.png"),
                Imagen_rey_blanco = Cargar_imagen($"{directory}/Rey_blanco.png"),
                Imagen_peon_negro = Cargar_imagen($"{directory}/Peon_negro.png"),
                Imagen_torre_negra = Cargar_imagen($"{directory}/Torre_negra.png"),
                Imagen_caballo_negro = Cargar_imagen($"{directory}/Caballo_negro.png"),
                Imagen_alfil_negro = Cargar_imagen($"{directory}/Alfil_negro.png"),
                Imagen_reina_negra = Cargar_imagen($"{directory}/Reina_negra.png"),
                Imagen_rey_negro = Cargar_imagen($"{directory}/Rey_negro.png")
            };
        }

        /// <summary>
        /// Carga una imagen 
        /// </summary>
        /// <param name="path">ruta de la imagen a cargar</param>
        /// <returns></returns>
        private Image Cargar_imagen(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show("Load File Error\n" + path);
                return null;
            }
        }

        /// <summary>
        /// Inicia una nueva partida 
        /// </summary>
        private void Comenzar_juego()
        {
            this.ActionLog = new List<Historial_acciones>();
            this.Piezas = new List<Pieza>();
            this.Board = new Tablero(this.Resources.Imagen_tablero, this.Resources.Imagen_movimiento_resaltado);
            this.GameState = Estado.Normal;

            //Blancas
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Añadir_pieza(new Torre(this.Resources.Imagen_torre_blanca, UnColor.Blanco));
            Añadir_pieza(new Caballo(this.Resources.Imagen_caballo_blanco, UnColor.Blanco));
            Añadir_pieza(new Alfil(this.Resources.Imagen_alfil_blanco, UnColor.Blanco));
            Añadir_pieza(new Reina(this.Resources.Imagen_reina_blanca, UnColor.Blanco));
            Añadir_pieza(new Rey(this.Resources.Imagen_rey_blanco, UnColor.Blanco));
            Añadir_pieza(new Alfil(this.Resources.Imagen_alfil_blanco, UnColor.Blanco));
            Añadir_pieza(new Caballo(this.Resources.Imagen_caballo_blanco, UnColor.Blanco));
            Añadir_pieza(new Torre(this.Resources.Imagen_torre_blanca, UnColor.Blanco));
            //Negras
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Añadir_pieza(new Torre(this.Resources.Imagen_torre_negra, UnColor.Negro));
            Añadir_pieza(new Caballo(this.Resources.Imagen_caballo_negro, UnColor.Negro));
            Añadir_pieza(new Alfil(this.Resources.Imagen_alfil_negro, UnColor.Negro));
            Añadir_pieza(new Rey(this.Resources.Imagen_rey_negro, UnColor.Negro));
            Añadir_pieza(new Reina(this.Resources.Imagen_reina_negra, UnColor.Negro));
            Añadir_pieza(new Alfil(this.Resources.Imagen_alfil_negro, UnColor.Negro));
            Añadir_pieza(new Caballo(this.Resources.Imagen_caballo_negro, UnColor.Negro));
            Añadir_pieza(new Torre(this.Resources.Imagen_torre_negra, UnColor.Negro));

            Player1 = new Jugador(UnColor.Blanco, Tipo_de_jugador.Humano, 1);
            Player2 = new Jugador(UnColor.Negro, Tipo_de_jugador.Agente, 2);


            Jugador_jugando = Player1.Color == UnColor.Blanco ? Player1 : Player2; // inicia la partida el jugador que use las fichas blancas

            // Asigna las coordenadas de las piezas del jugador 1
            var lstPiezasPlayer1 = this.Piezas.Where(x => x.Color == Player1.Color).ToList();
            lstPiezasPlayer1[0].Ubicacion = new Point(0, 6);
            lstPiezasPlayer1[1].Ubicacion = new Point(1, 6);
            lstPiezasPlayer1[2].Ubicacion = new Point(2, 6);
            lstPiezasPlayer1[3].Ubicacion = new Point(3, 6);
            lstPiezasPlayer1[4].Ubicacion = new Point(4, 6);
            lstPiezasPlayer1[5].Ubicacion = new Point(5, 6);
            lstPiezasPlayer1[6].Ubicacion = new Point(6, 6);
            lstPiezasPlayer1[7].Ubicacion = new Point(7, 6);
            lstPiezasPlayer1[8].Ubicacion = new Point(0, 7);
            lstPiezasPlayer1[9].Ubicacion = new Point(1, 7);
            lstPiezasPlayer1[10].Ubicacion = new Point(2, 7);
            lstPiezasPlayer1[11].Ubicacion = new Point(3, 7);
            lstPiezasPlayer1[12].Ubicacion = new Point(4, 7);
            lstPiezasPlayer1[13].Ubicacion = new Point(5, 7);
            lstPiezasPlayer1[14].Ubicacion = new Point(6, 7);
            lstPiezasPlayer1[15].Ubicacion = new Point(7, 7);

            // Asigna las coordenadas de las piezas del jugar 2
            var lstPiezasPlayer2 = this.Piezas.Where(x => x.Color == Player2.Color).ToList();
            lstPiezasPlayer2[0].Ubicacion = new Point(7, 1);
            lstPiezasPlayer2[1].Ubicacion = new Point(6, 1);
            lstPiezasPlayer2[2].Ubicacion = new Point(5, 1);
            lstPiezasPlayer2[3].Ubicacion = new Point(4, 1);
            lstPiezasPlayer2[4].Ubicacion = new Point(3, 1);
            lstPiezasPlayer2[5].Ubicacion = new Point(2, 1);
            lstPiezasPlayer2[6].Ubicacion = new Point(1, 1);
            lstPiezasPlayer2[7].Ubicacion = new Point(0, 1);
            lstPiezasPlayer2[8].Ubicacion = new Point(7, 0);
            lstPiezasPlayer2[9].Ubicacion = new Point(6, 0);
            lstPiezasPlayer2[10].Ubicacion = new Point(5, 0);
            lstPiezasPlayer2[11].Ubicacion = new Point(4, 0);
            lstPiezasPlayer2[12].Ubicacion = new Point(3, 0);
            lstPiezasPlayer2[13].Ubicacion = new Point(2, 0);
            lstPiezasPlayer2[14].Ubicacion = new Point(1, 0);
            lstPiezasPlayer2[15].Ubicacion = new Point(0, 0);      
        }

        /// <summary>
        /// Finaliza el turno actual
        /// </summary>
        /// <param name="firstTurn">Indica si es el primer turno</param>
        public void Siguiente_turno(bool firstTurn)
        {
            this.GameState = Estado.Normal;
            //label_estado.Text = string.Empty;
            //lblMoves.Text = string.Empty;

            if (!firstTurn)
            {
                Jugador_jugando = Jugador_jugando.Numero == 1 ? Player2 : Player1;
            }

            Set_movimientos_posibles(); // recalcula los movimientos habilitados para cada una de las piezas

            int moves = this.Piezas.Where(x => x.Color == Jugador_jugando.Color).Sum(x => x.Movimientos_permitidos.Count());
            //lblMoves.Text = moves.ToString();

            //FirstOrDefault Devuelve un elemento si encuentra un elemento que cumple con la condición
            var king = this.Piezas.FirstOrDefault(x => x.Color == Jugador_jugando.Color && x.GetType() == typeof(Rey));

            //Contains Devuelve un booleano si un elemento contiene un valor especifico en su secuencia
            //Any Devuelve un booleano si algun elemento de la secuencia satisface la condicion
            var isCheck = this.Piezas.Any(x => x.Color != Jugador_jugando.Color && x.Movimientos_permitidos.Contains(king.Ubicacion));//Si alguno de los movimientos contiene la ubicacion del rey
            // valida si en la jugada anterior se dejo al rey en jaque
            if (isCheck)
            {
                this.GameState = Estado.Jaque;
                if (moves == 0) // si nos quedamos sin movimientos es jaque mate
                    this.GameState = Estado.Jaquemate;
            }
            else
            {
                if (moves == 0) // si nos quedamos sin movimientos y no es jaque, el juego queda en empate
                    this.GameState = Estado.Empate;
            }

            //label_estado.Text = this.GameState.ToString();
            if (GameState == Estado.Jaquemate || GameState == Estado.Empate)
            {
                MessageBox.Show(this.GameState.ToString(), "Aipa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Obtiene la posicion de pantalla que posee la pieza
        /// </summary>
        /// <param name="location">Coordenada dentro del tablero</param>
        /// <returns></returns>
        public Point Obtener_ubicacion_pieza(Point location)
        {
            int _x = (location.X * 82) + 27;
            int _y = (location.Y * 82) + 4;
            return new Point(_x, _y);
        }

        /// <summary>
        /// Agrega una pieza al tablero
        /// </summary>
        /// <param name="piece">Pieza a agregar</param>
        private void Añadir_pieza(Pieza piece, Historial_acciones log = null)
        {
            piece.SelectedImage = this.Resources.Imagen_seleccion_resaltado; // asigna la imagen a mostrar en caso de ser seleccionada
            this.Piezas.Add(piece);  // Agrega la pieza a la lista de piezas en el tablero
            if (log != null)
                log.Pieza_añadida = piece;
        }

        /// <summary>
        /// Elimina una pieza del juego
        /// </summary>
        /// <param name="piece">Pieza a eliminar</param>
        /// <param name="log">Log de movimiento realizado</param>
        private void Remover_pieza(Pieza piece, Historial_acciones log)
        {
            this.Piezas.Remove(piece);  // Elimina la pieza de la lista de piezas del tablero
            log.Pieza_removida.Add(piece);
        }

        /// <summary>
        /// Selecciona una pieza del tablero
        /// </summary>
        /// <param name="cell_Location">Coordenadas de la celda seleccionada</param>
        public void Set_pieza_seleccionada(Point cell_Location)
        {
            Board.Desmarcar_celdas();
            this.Piezas.ForEach(x => x.Seleccionada = false); // deselecciono todas las fichas
            Pieza _selectedPiece = this.Piezas.FirstOrDefault(x => x.Ubicacion == cell_Location && x.Color == Jugador_jugando.Color);
            // busco una ficha para la coordenada donde se hizo click, solo si es del color correspondiente al jugador que tiene el turno

            if (_selectedPiece != null)
            {
                _selectedPiece.Seleccionada = true; // se selecciona la ficha
                //Array.ForEach permite realizar una accion determinada para cada elemento de un objeto arreglo
                Array.ForEach(_selectedPiece.Movimientos_permitidos, loc => Board.Celdas[loc.X, loc.Y].Puede_moverse = true); // colorea las celdas habilitadas 
            }
        }
        #endregion

        #region Metodos para obtener movimientos validos

        /// <summary>
        /// Setea los movimientos para cada pieza
        /// </summary>
        private void Set_movimientos_posibles()
        {
            this.Piezas.ForEach(x => x.Movimientos_permitidos = Obtener_movimientos_posibles(x, this.Piezas)); // movimientos habilitados que posee la pieza

            // valida los movimientos disponibles que puede realizar el jugador actual
            this.Piezas.Where(x => x.Color == Jugador_jugando.Color).ToList().ForEach(p =>
            {
                p.Movimientos_permitidos = p.Movimientos_permitidos.Where(loc => Validar_movimiento(p, loc)).ToArray();
                // valida que el rey no quede en jaque al realizar el movimiento
            });
        }

        /// <summary>
        /// Obtiene las celdas a la cual puede desplazarce la pieza seleccionada
        /// </summary>
        /// <param name="piece">Pieza seleccionada</param>
        /// <param name="boardPiezas">Lista de piezas que obstaculizan los movimientos de la la pieza seleccionada</param>
        private Point[] Obtener_movimientos_posibles(Pieza piece, List<Pieza> boardPiezas)
        {
            List<Point> lstAvailableCell = new List<Point>();
            if (piece != null)
            {
                Array.ForEach(piece.Movimientos, x =>
                {
                    var displacement = x.Direccion;
                    if (piece.Color == Player2.Color)
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
                            if (!Validar_movimiento_especial(piece, x, _targetPiece))
                                break; // "NO PERMITE MOVIMIENTO"
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

        /// <summary>
        /// Valida si al realizar el movimiento de la pieza el rey queda en jaque
        /// </summary>
        /// <param name="piece">Pieza a desplazar</param>
        /// <param name="newLocation">Coordenada del tablero a validar</param>
        /// <returns></returns>
        private bool Validar_movimiento(Pieza piece, Point newLocation)
        {
            Point _originalLocation = piece.Ubicacion; // posicion actual de la pieza
            piece.Ubicacion = newLocation; // asigna a la pieza la posicion nueva para analizar si el rey queda en jaque

            bool _result = true;

            var lstPiezas = this.Piezas.Where(x =>
                x.Color != piece.Color &&
                x.Ubicacion != newLocation
            )
            .ToList();// obtiene las piezas rivales para analizar si alguna deja en jaque al rey

            if (lstPiezas.Any()) // lista de piezas rival que atacan a la pieza seleccionada
            {
                //Devuelve el primer elemento que cumple con la condicion
                var king = this.Piezas.First(x => x.Color == piece.Color && x.GetType() == typeof(Rey));

                // recalculo los movimientos habilitados de las piezas para determinar si alguna pone en jaque al rey
                var lstBoardPiezas = this.Piezas.Where(x => !(x.Color != piece.Color && x.Ubicacion == newLocation)).ToList(); // obtiene todas las piezas del tablero ignorando la pieza que se asume fue eliminada al realizar el movimiento
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

        /// <summary>
        /// Revisa si puede desplazarse la pieza seleccionada utilizando un movimiento especial
        /// </summary>
        /// <param name="piece">Pieza a validar</param>
        /// <param name="move">Movimiento a validar</param>
        /// <param name="rivalPiece">Pieza rival que se encuentra en la coordenada destino</param>
        /// <returns></returns>
        private bool Validar_movimiento_especial(Pieza piece, Movimiento move, Pieza rivalPiece)
        {
            if (piece.GetType() == typeof(Peon))
            {
                #region movimientos espciales peon
                if (move.Direccion.X == 0 && move.Direccion.Y == -1) // desplazamiento frontal 1 casillero
                    return rivalPiece == null; // el casillero de en frente debe estar vacio

                if (move.Direccion.X == 0 && move.Direccion.Y == -2) // desplazamiento frontal 2 casillero, no permite atacar
                {
                    bool _condicion1 = rivalPiece == null; // la casilla destino debe estar vacia
                    bool _condicion2 = !this.Piezas.Any(p => p.Ubicacion.X == piece.Ubicacion.X && p.Ubicacion.Y == (Jugador_jugando.Numero == 1 ? 5 : 2)); // la casilla frontal debe esta vacia
                    bool _condicion3 = (Jugador_jugando.Numero == 1 && piece.Ubicacion.Y == 6) || (Jugador_jugando.Numero == 2 && piece.Ubicacion.Y == 1); // debe ser el primer movimiento del peon

                    return _condicion1 && _condicion2 && _condicion3;
                }

                if ((move.Direccion.X == -1 || move.Direccion.X == 1) && move.Direccion.Y == -1) // ataque a pieza rival en diagonal
                {
                    //last entrega el ultimo elemento de la lista
                    var lastAction = ActionLog.LastOrDefault();
                    if (rivalPiece == null && lastAction != null) // COMER AL PASO
                    {
                        var Last_Move = lastAction.movimientos.Last();
                        // El peon rival debe estar en la misma fila que la pieza a mover, en una columna adyacente y debe haberce desplazado en el ultimo turno juegado 2 casilleros
                        if (Jugador_jugando.Numero == 1 && piece.Ubicacion.Y == 3)  // el peon se desplaza hica arriba
                        {
                            //first devuelve el primer elemento que cumple la condicion especificada
                            var rival = this.Piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X + move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == 2)
                                return true; // Habilita comer al paso
                        }
                        if (Jugador_jugando.Numero == 2 && piece.Ubicacion.Y == 4) // // el peon se desplaza hica abajo
                        {
                            var rival = this.Piezas.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X - move.Direccion.X, piece.Ubicacion.Y));
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
                bool kingFirstMove = !ActionLog.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. del rey
                if (!kingFirstMove)
                    return false;

                Point _moveDirection = Jugador_jugando.Numero == 1 ? move.Direccion : new Point(move.Direccion.X * -1, move.Direccion.Y * -1);
                Pieza torre = null;

                if (_moveDirection.X < 0) // enrrosque largo
                    torre = this.Piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                else // enrrosque corto
                    torre = this.Piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);
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
                    bool _existPiece = this.Piezas.Any(p => p.Ubicacion == _location);
                    if (_existPiece)
                        return false; // no debe existir pieza entre el rey y la torre

                    bool _attackLoc = this.Piezas.Any(p => p.Color != piece.Color && p.Movimientos_permitidos != null && p.Movimientos_permitidos.Any(y => y == _location));
                    if (_attackLoc)
                        return false; // la ubicacion no puede estar bajo ataque de una pieza rival

                    _location = new Point(_location.X + _moveX, _location.Y);
                }

                return true; // el enroque es valido
                #endregion
            }

            return false;
        }
        #endregion

        #region Metodos para dezplazar piezas
        /// <summary>
        /// Desplaza la pieza seleccionada a la celda indicada
        /// </summary>
        /// <param name="cell_Location">Coordenadas de la celda seleccionada</param>
        /// <returns></returns>
        public bool Mover_pieza(Point cell_Location)
        {
            if (cell_Location.X > 7 || cell_Location.Y > 7 || cell_Location.X < 0 || cell_Location.Y < 0)
                return false; // coordenada fuera del tablero "NO PERMITE MOVIMIENTO"

            var selectedPiece = this.Piezas.FirstOrDefault(x => x.Seleccionada);
            
            if (selectedPiece != null)
            {
                // determina si la pieza seleccionada puede moverse a la coordenada indicada
                if (Board.Celdas[cell_Location.X, cell_Location.Y].Puede_moverse)
                {
                    var actionLog = new Historial_acciones();
                    actionLog.movimientos.Add(new Historial_movimiento
                    {
                        Pieza_ = selectedPiece,
                        Ubicacion_original = selectedPiece.Ubicacion,
                        Ubicacion_nueva = cell_Location
                    });
                    this.ActionLog.Add(actionLog);
                    // Registra el ultimo movimiento realizado para poder validar movimiento "Comer al Paso"

                    Movimiento_especial_pieza(selectedPiece, cell_Location, actionLog);
                    // ejecuta un comportamiento especial si la jugada lo requiere

                    var targetPiece = this.Piezas.FirstOrDefault(x => x.Color != selectedPiece.Color && x.Ubicacion == cell_Location);
                    if (targetPiece != null)
                        Remover_pieza(targetPiece, actionLog); // elimina la pieza que se encuentre en la posicion destino

                    selectedPiece.Ubicacion = cell_Location; // mueve la pieza seleccionada a la coordenada destino
                    selectedPiece.Seleccionada = false;
                    Board.Desmarcar_celdas();

                    //Valid_Check(selectedPiece); // valida si hay jaque al rey contrincante tras el movimiento

                    Siguiente_turno(false); //Luego de mover la pieza comienza el turno del otro jugador
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ejecuta un movimiento especial de una pieza
        /// </summary>
        /// <param name="piece">Pieza que realiza el movimiento</param>
        /// <param name="targetLocation">Coordenada del tablero a donde se dezplaza la pieza</param>
        /// <param name="log">Log de movimiento realizado</param>
        private void Movimiento_especial_pieza(Pieza piece, Point targetLocation, Historial_acciones log)
        {
            if (piece.GetType() == typeof(Peon))
            {
                #region movimiento especial
                if (targetLocation.Y == (piece.Color == Player1.Color ? 0 : 7)) // el player uno corona la pieza en la fila superior y el player 2 en la fila inferior
                {
                    Pieza _newPiece = null;
                    if (Jugador_jugando.Tipo_jugador == Tipo_de_jugador.Humano)
                    {
                        while (_newPiece == null)
                        {
                            var form = new Ventana_coronacion(this.Resources, piece.Color);
                            form.ShowDialog();
                            _newPiece = form.pieza_seleccionada;
                        }
                        _newPiece.Ubicacion = targetLocation;
                    }
                    else
                    {
                        /*joshua llamar a agente para que decida a que transformar*/
                        _newPiece = new Reina(this.Resources.Imagen_reina_negra, piece.Color);
                        _newPiece.Ubicacion = targetLocation;
                    }

                    Remover_pieza(piece, log); // Elimina el peon
                    Añadir_pieza(_newPiece, log); // agrega la nueva pieza
                }

                // si el peon se desplaza en diagonal a una celda vacia se asume que utilizo el movimiento especial "Comer al Paso"
                var displacement = new Point(targetLocation.X - piece.Ubicacion.X, targetLocation.Y - piece.Ubicacion.Y);
                if ((displacement.X == -1 || displacement.X == 1) && displacement.Y == (Jugador_jugando.Equals(Player1) ? -1 : 1) && !this.Piezas.Any(x => x.Ubicacion == targetLocation))
                {
                    Point rivalLocation = new Point(targetLocation.X, targetLocation.Y + (Jugador_jugando.Equals(Player1) ? 1 : -1));
                    var romovePiece = this.Piezas.First(p => p.Ubicacion == rivalLocation);
                    Remover_pieza(romovePiece, log); // elimina el peon rival
                }
                #endregion
            }
            if (piece.GetType() == typeof(Rey))
            {
                if (Math.Abs(piece.Ubicacion.X - targetLocation.X) == 2) // enroque
                {
                    Pieza _rock = null;
                    if ((targetLocation.X - piece.Ubicacion.X) < 0) // enrrosque largo
                        _rock = this.Piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                    else // enrrosque corto
                        _rock = this.Piezas.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);

                    var _newRookLoc = _rock.Ubicacion.X == 7 ?
                        new Point(_rock.Ubicacion.X - 2, _rock.Ubicacion.Y) :
                        new Point(_rock.Ubicacion.X + 3, _rock.Ubicacion.Y);

                    log.movimientos.Add(new Historial_movimiento()
                    {
                        Pieza_ = _rock,
                        Ubicacion_original = _rock.Ubicacion,
                        Ubicacion_nueva = _newRookLoc
                    });

                    _rock.Ubicacion = _newRookLoc;
                }
            }
        }
        #endregion

    }
    public enum Estado
    {
        Normal,
        Jaque,
        Jaquemate,
        Empate
    }
}
