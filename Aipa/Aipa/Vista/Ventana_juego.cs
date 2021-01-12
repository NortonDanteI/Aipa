using Aipa.Modelo;
using Aipa.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aipa.Vista
{
    public partial class Ventana_juego : Form
    {

        #region Objects
        private Tiempo _gameTime;
        /// <summary>
        /// Timer que refresca la imagen del juego
        /// </summary>
        private Timer _timer;
        #endregion

        #region Constructor
        public Ventana_juego()
        {
            InitializeComponent();
            _gameTime = new Tiempo();
            _timer = new Timer();
            _timer.Interval = 1000 / 30; // PFS (el intervalo no siempre se respeta en winforms)
            _timer.Tick += (sender, e) =>
            {
                var _now = DateTime.Now;
                _gameTime.FrameMillisegundos = (int)(_now - _gameTime.Frame_fecha_actual).TotalMilliseconds;
                _gameTime.Frame_fecha_actual = _now;

                Application.DoEvents();
                this.Update(_gameTime);  // ejecuta logica propia del juego

                using (DrawHandler drawHandler = new DrawHandler(this.Canvas.Width, this.Canvas.Height))
                {
                    this.Draw(drawHandler);    // Actualiza la imagen en cada cuadro
                    Canvas.Image = drawHandler.Imagen_base; // asigna la imagen del nuevo cuadro al picture box
                }
            };

            _timer.Start(); // inicia el juego
            Initialize();
            Start_Match();
        }
        #endregion

        #region Propiedades

        int lx, ly;
        int sw, sh;

        int posY = 0;
        int posX = 0;
        /// <summary>
        /// Recursos de imagenes a usar en el juego
        /// </summary>
        public Recursos Resources { get; set; }
        /// <summary>
        /// Tablero de juego
        /// </summary>
        private Tablero Board { get; set; }
        /// <summary>
        /// Lista de piezas de ajedrez
        /// </summary>
        private List<Pieza> Pieces { get; set; }
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
        public Jugador CurrentPlayer { get; set; }
        /// <summary>
        /// Registro de las acciones realizadas en el utlimo turno
        /// </summary>
        public List<Historial_acciones> ActionLog { get; set; }
        /// <summary>
        /// Estado del juego
        /// </summary>
        public State GameState { get; set; }
        #endregion

        #region funcionalidades basicas
        private void Boton_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro que quieres salir", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Boton_minimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Barratitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left += (e.X - posX);
                Top += (e.Y - posY);
            }
        }
        private void Boton_maximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            /*hago no visibile y visible para que el icono se maximizar se vea y una ves presionado desaparesca y aparesca el de restaurar*/
            boton_maximizar.Visible = false;
            boton_restaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }
        private void Boton_restaurar_Click(object sender, EventArgs e)
        {
            boton_maximizar.Visible = true;
            boton_restaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }
        private void Boton_manual_usuario_click(object sender, EventArgs e)
        {
            boton_manual_usuario.BackColor = SystemColors.MenuHighlight;
            emoticon_manual.BackColor = SystemColors.MenuHighlight;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start_Match();
        }
        /// <summary>
        /// Evento que se desencadena al liberar el boton del mouse sobre el lienzo
        /// </summary>
        private void Ventana_juego_Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Point _mouseLocation = new Point(e.Location.X - 5, e.Location.Y - 5); // resto los bordes del tablero
            Console.WriteLine(_mouseLocation.X + "|" + _mouseLocation.Y);
            var cell_Location = new Point(_mouseLocation.X / 80, _mouseLocation.Y / 80); // cada celda tiene un tamaños de  100x100 + 5x5 de borde
            Console.WriteLine(cell_Location.X +"|"+  cell_Location.Y);
            // Obtengo la coordenada del tablero donde se realizo click
            if (!Move_Piece(cell_Location)) // si existe una pieza seleccionada, intenta moverla a la celda donde se realizo click
                Set_SelectedPiece(cell_Location); // si la pieza seleccionada no se puede mover a la celda destino, se intenta seleccionar otra pieza
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inicializa el juego cargando los recursos
        /// </summary>
        private void Initialize()
        {
            string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes");
            this.Resources = new Recursos()
            {
                Imagen_tablero = Load_Image($"{directory}/Tablerov2.png"),
                Imagen_movimiento_resaltado = Load_Image($"{directory}/Movimiento_resaltado.png"),
                Imagen_seleccion_resaltado = Load_Image($"{directory}/Seleccion_resaltado.png"),
                Imagen_peon_blanco = Load_Image($"{directory}/Peon_blanco.png"),
                Imagen_torre_blanca = Load_Image($"{directory}/Torre_blanca.png"),
                Imagen_caballo_blanco = Load_Image($"{directory}/Caballo_blanco.png"),
                Imagen_alfil_blanco = Load_Image($"{directory}/Alfil_blanco.png"),
                Imagen_reina_blanca = Load_Image($"{directory}/Reina_blanca.png"),
                Imagen_rey_blanco = Load_Image($"{directory}/Rey_blanco.png"),
                Imagen_peon_negro = Load_Image($"{directory}/Peon_negro.png"),
                Imagen_torre_negra = Load_Image($"{directory}/Torre_negra.png"),
                Imagen_caballo_negro = Load_Image($"{directory}/Caballo_negro.png"),
                Imagen_alfil_negro = Load_Image($"{directory}/Alfil_negro.png"),
                Imagen_reina_negra = Load_Image($"{directory}/Reina_negra.png"),
                Imagen_rey_negro = Load_Image($"{directory}/Rey_negro.png")
            };
        }
        #region Methods
        /// <summary>
        /// Carga una imagen 
        /// </summary>
        /// <param name="path">ruta de la imagen a cargar</param>
        /// <returns></returns>
        private Image Load_Image(string path)
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
        #endregion

        /// <summary>
        /// Inicia una nueva partida 
        /// </summary>
        private void Start_Match()
        {
            this.ActionLog = new List<Historial_acciones>();
            this.Pieces = new List<Pieza>();
            this.Board = new Tablero(this.Resources.Imagen_tablero, this.Resources.Imagen_movimiento_resaltado);
            this.GameState = State.Normal;

            //Blancas
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Peon(this.Resources.Imagen_peon_blanco, UnColor.Blanco));
            Add_Piece(new Torre(this.Resources.Imagen_torre_blanca, UnColor.Blanco));
            Add_Piece(new Caballo(this.Resources.Imagen_caballo_blanco, UnColor.Blanco));
            Add_Piece(new Alfil(this.Resources.Imagen_alfil_blanco, UnColor.Blanco));
            Add_Piece(new Reina(this.Resources.Imagen_reina_blanca, UnColor.Blanco));
            Add_Piece(new Rey(this.Resources.Imagen_rey_blanco, UnColor.Blanco));
            Add_Piece(new Alfil(this.Resources.Imagen_alfil_blanco, UnColor.Blanco));
            Add_Piece(new Caballo(this.Resources.Imagen_caballo_blanco, UnColor.Blanco));
            Add_Piece(new Torre(this.Resources.Imagen_torre_blanca, UnColor.Blanco));
            //Negras
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Peon(this.Resources.Imagen_peon_negro, UnColor.Negro));
            Add_Piece(new Torre(this.Resources.Imagen_torre_negra, UnColor.Negro));
            Add_Piece(new Caballo(this.Resources.Imagen_caballo_negro, UnColor.Negro));
            Add_Piece(new Alfil(this.Resources.Imagen_alfil_negro, UnColor.Negro));
            Add_Piece(new Rey(this.Resources.Imagen_rey_negro, UnColor.Negro));
            Add_Piece(new Reina(this.Resources.Imagen_reina_negra, UnColor.Negro));
            Add_Piece(new Alfil(this.Resources.Imagen_alfil_negro, UnColor.Negro));
            Add_Piece(new Caballo(this.Resources.Imagen_caballo_negro, UnColor.Negro));
            Add_Piece(new Torre(this.Resources.Imagen_torre_negra, UnColor.Negro));

            Player1 = new Jugador(UnColor.Blanco, Tipo_de_jugador.Humano, 1);
            Player2 = new Jugador(UnColor.Negro, Tipo_de_jugador.Agente, 2);
            CurrentPlayer = Player1.Color == UnColor.Blanco ? Player1 : Player2; // inicia la partida el jugador que use las fichas blancas

            // Asigna las coordenadas de las piezas del juegar 1
            var lstPiecesPlayer1 = this.Pieces.Where(x => x.Color == Player1.Color).ToList();
            lstPiecesPlayer1[0].Ubicacion = new Point(0, 6);
            lstPiecesPlayer1[1].Ubicacion = new Point(1, 6);
            lstPiecesPlayer1[2].Ubicacion = new Point(2, 6);
            lstPiecesPlayer1[3].Ubicacion = new Point(3, 6);
            lstPiecesPlayer1[4].Ubicacion = new Point(4, 6);
            lstPiecesPlayer1[5].Ubicacion = new Point(5, 6);
            lstPiecesPlayer1[6].Ubicacion = new Point(6, 6);
            lstPiecesPlayer1[7].Ubicacion = new Point(7, 6);
            lstPiecesPlayer1[8].Ubicacion = new Point(0, 7);
            lstPiecesPlayer1[9].Ubicacion = new Point(1, 7);
            lstPiecesPlayer1[10].Ubicacion = new Point(2, 7);
            lstPiecesPlayer1[11].Ubicacion = new Point(3, 7);
            lstPiecesPlayer1[12].Ubicacion = new Point(4, 7);
            lstPiecesPlayer1[13].Ubicacion = new Point(5, 7);
            lstPiecesPlayer1[14].Ubicacion = new Point(6, 7);
            lstPiecesPlayer1[15].Ubicacion = new Point(7, 7);

            // Asigna las coordenadas de las piezas del juegar 2
            var lstPiecesPlayer2 = this.Pieces.Where(x => x.Color == Player2.Color).ToList();
            lstPiecesPlayer2[0].Ubicacion = new Point(7, 1);
            lstPiecesPlayer2[1].Ubicacion = new Point(6, 1);
            lstPiecesPlayer2[2].Ubicacion = new Point(5, 1);
            lstPiecesPlayer2[3].Ubicacion = new Point(4, 1);
            lstPiecesPlayer2[4].Ubicacion = new Point(3, 1);
            lstPiecesPlayer2[5].Ubicacion = new Point(2, 1);
            lstPiecesPlayer2[6].Ubicacion = new Point(1, 1);
            lstPiecesPlayer2[7].Ubicacion = new Point(0, 1);
            lstPiecesPlayer2[8].Ubicacion = new Point(7, 0);
            lstPiecesPlayer2[9].Ubicacion = new Point(6, 0);
            lstPiecesPlayer2[10].Ubicacion = new Point(5, 0);
            lstPiecesPlayer2[11].Ubicacion = new Point(4, 0);
            lstPiecesPlayer2[12].Ubicacion = new Point(3, 0);
            lstPiecesPlayer2[13].Ubicacion = new Point(2, 0);
            lstPiecesPlayer2[14].Ubicacion = new Point(1, 0);
            lstPiecesPlayer2[15].Ubicacion = new Point(0, 0);

            Next_Turn(true);
            base.Enabled = true;
        }
        /// <summary>
        /// Obtiene la posicion de pantalla que posee la pieza
        /// </summary>
        /// <param name="location">Coordenada dentro del tablero</param>
        /// <returns></returns>
        private Point Get_PiecePosition(Point location)
        {
            int _x = (location.X * 78) + 6 * (location.X +1);
            int _y = (location.Y * 78) + 4 * (location.Y +1);
            return new Point(_x, _y);
        }
        /// <summary>
        /// Agrega una pieza al tablero
        /// </summary>
        /// <param name="piece">Pieza a agregar</param>
        private void Add_Piece(Pieza piece, Historial_acciones log = null)
        {
            piece.SelectedImage = this.Resources.Imagen_seleccion_resaltado; // asigna la imagen a mostrar en caso de ser seleccionada
            this.Pieces.Add(piece);  // Agrega la pieza a la lista de piezas en el tablero
            if (log != null)
                log.Pieza_añadida = piece;
        }
        /// <summary>
        /// Elimina una pieza del juego
        /// </summary>
        /// <param name="piece">Pieza a eliminar</param>
        /// <param name="log">Log de movimiento realizado</param>
        private void Remove_Piece(Pieza piece, Historial_acciones log)
        {
            this.Pieces.Remove(piece);  // Elimina la pieza de la lista de piezas del tablero
            log.Pieza_removida.Add(piece);
        }
        /// <summary>
        /// Finaliza el turno actual
        /// </summary>
        /// <param name="firstTurn">Indica si es el primer turno</param>
        private void Next_Turn(bool firstTurn)
        {
            this.GameState = State.Normal;
            label_estado.Text = string.Empty;
            //lblMoves.Text = string.Empty;

            if (!firstTurn)
                CurrentPlayer = CurrentPlayer.Numero == 1 ? Player2 : Player1;
            Set_MovesLocations(); // recalcula los movimientos habilitados para cada una de las piezas

            int moves = this.Pieces.Where(x => x.Color == CurrentPlayer.Color).Sum(x => x.Movimientos_permitidos.Count());
            //lblMoves.Text = moves.ToString();

            var king = this.Pieces.FirstOrDefault(x => x.Color == CurrentPlayer.Color && x.GetType() == typeof(Rey));
            var isCheck = this.Pieces.Any(x => x.Color != CurrentPlayer.Color && x.Movimientos_permitidos.Contains(king.Ubicacion));
            // valida si en la juegada anterior se dejo al rey en jaque
            if (isCheck)
            {
                this.GameState = State.Check;
                if (moves == 0) // si nos quedamos sin movimientos es jaque mate
                    this.GameState = State.Checkmate;
            }
            else
            {
                if (moves == 0) // si nos quedamos sin movimientos y no es jaque, el juego queda en tabla
                    this.GameState = State.Draw;
            }

            label_estado.Text = this.GameState.ToString();
            if (GameState == State.Checkmate || GameState == State.Draw)
            {
                MessageBox.Show(this.GameState.ToString(), "Chess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Selecciona una pieza del tablero
        /// </summary>
        /// <param name="cell_Location">Coordenadas de la celda seleccionada</param>
        private void Set_SelectedPiece(Point cell_Location)
        {
            Board.Desmarcar_celdas();
            this.Pieces.ForEach(x => x.Seleccionada = false); // deselecciono todas las fichas
            Pieza _selectedPiece = this.Pieces.FirstOrDefault(x => x.Ubicacion == cell_Location && x.Color == CurrentPlayer.Color);
            // busco una ficha para la coordenada donde se hizo click, solo si es del color correspondiente al jugador que tiene el turno

            if (_selectedPiece != null)
            {
                _selectedPiece.Seleccionada = true; // se selecciona la ficha
                Array.ForEach(_selectedPiece.Movimientos_permitidos, loc => Board.Celdas[loc.X, loc.Y].Puede_moverse = true); // colorea las celdas habilitadas 
            }
        }

        //METODOS QUE OBTIENEN LAS CELDAS HABILITADAS
        /// <summary>
        /// Obtiene
        /// </summary>
        private void Set_MovesLocations()
        {
            this.Pieces.ForEach(x => x.Movimientos_permitidos = Get_MovesLocations(x, this.Pieces)); // movimientos habilitados que posee la pieza

            // valida los movimientos disponibles que puede realizar el juegador actual
            this.Pieces.Where(x => x.Color == CurrentPlayer.Color).ToList().ForEach(p =>
            {
                p.Movimientos_permitidos = p.Movimientos_permitidos.Where(loc => Valid_MovesLocations_Check(p, loc)).ToArray();
                // valida que el rey no quede en jaque al realizar el movimiento
            });
        }
        /// <summary>
        /// Obtiene las celdas a la cual puede desplazarce la pieza seleccionada
        /// </summary>
        /// <param name="piece">Pieza seleccionada</param>
        /// <param name="boardPieces">Lista de piezas que obstaculizan los movimientos de la la pieza seleccionada</param>
        private Point[] Get_MovesLocations(Pieza piece, List<Pieza> boardPieces)
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

                        var _targetPiece = boardPieces.FirstOrDefault(y => y.Ubicacion == _location);

                        if (x.Tipo_de_mov.HasFlag(Tipo_de_movimiento.especial)) // movimiento especial de pieza
                        {
                            if (!Get_MovesLocations_Special(piece, x, _targetPiece))
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
        private bool Valid_MovesLocations_Check(Pieza piece, Point newLocation)
        {
            Point _originalLocation = piece.Ubicacion; // posicion actual de la pieza
            piece.Ubicacion = newLocation; // asigna a la pieza la posicion nueva para analizar si el rey queda en jaque

            bool _result = true;

            var lstPieces = this.Pieces.Where(x =>
                x.Color != piece.Color &&
                x.Ubicacion != newLocation
            )
            .ToList();// obtiene las piezas rivales para analizar si alguna deja en jaque al rey

            if (lstPieces.Any()) // lista de piezas rival que atacan a la pieza seleccionada
            {
                var king = this.Pieces.First(x => x.Color == piece.Color && x.GetType() == typeof(Rey));

                // recalculo los movimientos habilitados de las piezas para determinar si alguna pone en jaque al rey
                var lstBoardPieces = this.Pieces.Where(x => !(x.Color != piece.Color && x.Ubicacion == newLocation)).ToList(); // obtiene todas las piezas del tabler ignorando la pieza que se asume fue eliminada al realizar el movimiento
                foreach (var p in lstPieces)
                {
                    var lstMoves = Get_MovesLocations(p, lstBoardPieces); //  obtiene las ubicaciones de desplazamiento de cada una de las piezas del rival
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
        /// Obtiene las celdas a la cual puede desplazarce la pieza seleccionada utilizando un movimiento especial
        /// </summary>
        /// <param name="piece">Pieza a validar</param>
        /// <param name="move">Movimiento a validar</param>
        /// <param name="rivalPiece">Pieza rival que se encuentra en la coordenada destino</param>
        /// <returns></returns>
        private bool Get_MovesLocations_Special(Pieza piece, Movimiento move, Pieza rivalPiece)
        {
            // CAMBIAR LOGICA
            // retornar tru en cada validacion

            if (piece.GetType() == typeof(Peon))
            {
                #region Special Moves
                if (move.Direccion.X == 0 && move.Direccion.Y == -1) // desplazamiento frontal 1 casillero
                    return rivalPiece == null; // el casillero de en frente debe estar vacio

                if (move.Direccion.X == 0 && move.Direccion.Y == -2) // desplazamiento frontal 2 casillero, no permite atacar
                {
                    bool _condicion1 = rivalPiece == null; // la casilla destino debe estar vacia
                    bool _condicion2 = !this.Pieces.Any(p => p.Ubicacion.X == piece.Ubicacion.X && p.Ubicacion.Y == (CurrentPlayer.Numero == 1 ? 5 : 2)); // la casilla frontal debe esta vacia
                    bool _condicion3 = (CurrentPlayer.Numero == 1 && piece.Ubicacion.Y == 6) || (CurrentPlayer.Numero == 2 && piece.Ubicacion.Y == 1); // debe ser el primer movimiento del peon

                    return _condicion1 && _condicion2 && _condicion3;
                }

                if ((move.Direccion.X == -1 || move.Direccion.X == 1) && move.Direccion.Y == -1) // ataque a pieza rival en diagonal
                {
                    var lastAction = ActionLog.LastOrDefault();
                    if (rivalPiece == null && lastAction != null) // COMER AL PASO
                    {
                        var Last_Move = lastAction.movimientos.Last();
                        // El peon rival debe estar en la misma fila que la pieza a mover, en una columna adyacente y debe haberce desplazado en el ultimo turno juegado 2 casilleros
                        if (CurrentPlayer.Numero == 1 && piece.Ubicacion.Y == 3)  // el peon se desplaza hica arriba
                        {
                            var rival = this.Pieces.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X + move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == 2)
                                return true; // Habilita comer al paso
                        }
                        if (CurrentPlayer.Numero == 2 && piece.Ubicacion.Y == 4) // // el peon se desplaza hica abajo
                        {
                            var rival = this.Pieces.FirstOrDefault(p => p.Color != piece.Color && p.Ubicacion == new Point(piece.Ubicacion.X - move.Direccion.X, piece.Ubicacion.Y));
                            if (rival != null && Last_Move.Pieza_.Equals(rival) && (Last_Move.Ubicacion_nueva.Y - Last_Move.Ubicacion_original.Y) == -2)
                                return true; // Habilita comer al paso
                        }
                    }

                    if (rivalPiece != null && rivalPiece.Color != piece.Color)
                        return true; // el movimiento se habilita solo si hay un rival en la posiscion destino
                }
                #endregion
            }
            else if (piece.GetType() == typeof(Rey))
            {
                if (GameState != State.Normal) // el rey no puede estar en jaque
                    return false;

                //Enrrosque
                bool kingFirstMove = !ActionLog.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. del rey
                if (!kingFirstMove)
                    return false;

                Point _moveDirection = CurrentPlayer.Numero == 1 ? move.Direccion : new Point(move.Direccion.X * -1, move.Direccion.Y * -1);
                Pieza _rock = null;
                if (_moveDirection.X < 0) // enrrosque largo
                    _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                else // enrrosque corto
                    _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);
                if (_rock == null) // si no existe la torre se asume que fue eliminada o movida de su casillero original
                    return false;

                bool rookFirstMove = !ActionLog.Any(x => x.movimientos.Any(y => y.Pieza_.Equals(piece))); // debe ser el primer mov. de la torre
                if (!rookFirstMove) // debe ser el primer mov. de la torre
                    return false;

                int _moveX = _moveDirection.X < 0 ? -1 : 1;
                Point _location = new Point(piece.Ubicacion.X + _moveX, piece.Ubicacion.Y);
                while (_location != _rock.Ubicacion)
                {
                    bool _existPiece = this.Pieces.Any(p => p.Ubicacion == _location);
                    if (_existPiece)
                        return false; // no debe existir pieza entre el rey y la torre

                    bool _attackLoc = this.Pieces.Any(p => p.Color != piece.Color && p.Movimientos_permitidos != null && p.Movimientos_permitidos.Any(y => y == _location));
                    if (_attackLoc)
                        return false; // la ubicacion no puede estar bajo ataque de una pieza rival

                    _location = new Point(_location.X + _moveX, _location.Y);
                }

                return true; // el enroque es valido
            }

            return false;
        }

        //METODOS QUE DESPLAZAN LA FICHA SELECCIONADA
        /// <summary>
        /// Desplaza la pieza seleccionada a la celda indicada
        /// </summary>
        /// <param name="cell_Location">Coordenadas de la celda seleccionada</param>
        /// <returns></returns>
        private bool Move_Piece(Point cell_Location)
        {
            if (cell_Location.X > 7 || cell_Location.Y > 7 || cell_Location.X < 0 || cell_Location.Y < 0)
                return false; // coordenada fuera del tablero "NO PERMITE MOVIMIENTO"

            var selectedPiece = this.Pieces.FirstOrDefault(x => x.Seleccionada);
            if (selectedPiece != null)
            {
                if (Board.Celdas[cell_Location.X, cell_Location.Y].Puede_moverse) // determina si la pieza seleccionada pueda moverse a la coordenada indicada
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

                    Move_Piece_Special(selectedPiece, cell_Location, actionLog);
                    // ejecuta un comportamiento especial si la jugada lo requiere

                    var targetPiece = this.Pieces.FirstOrDefault(x => x.Color != selectedPiece.Color && x.Ubicacion == cell_Location);
                    if (targetPiece != null)
                        Remove_Piece(targetPiece, actionLog); // elimina la pieza que se encuentre en la posicion destino

                    selectedPiece.Ubicacion = cell_Location; // mueve la pieza seleccionada a la coordenada destino
                    selectedPiece.Seleccionada = false;
                    Board.Desmarcar_celdas();

                    //Valid_Check(selectedPiece); // valida si hay jaque al rey contrincante tras el movimiento

                    Next_Turn(false); //Luego de mover la pieza comienza el turno del otro jugador
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
        private void Move_Piece_Special(Pieza piece, Point targetLocation, Historial_acciones log)
        {
            if (piece.GetType() == typeof(Peon))
            {
                #region Sprecial Move
                if (targetLocation.Y == (piece.Color == Player1.Color ? 0 : 7)) // el player uno corona la pieza en la fila superior y el player 2 en la fila inferior
                {
                    Pieza _newPiece = null;
                    if (CurrentPlayer.Tipo_jugador == Tipo_de_jugador.Humano)
                    {
                        while (_newPiece == null)
                        {
                            var form = new Selector(this.Resources, piece.Color);
                            form.ShowDialog();
                            _newPiece = form.pieza_seleccionada;
                        }
                        _newPiece.Ubicacion = targetLocation;
                    }

                    Remove_Piece(piece, log); // Elimina el peon
                    Add_Piece(_newPiece, log); // agrega la nueva pieza
                }

                // si el peon se desplaza en diagonal a una celda vacia se asume que utilizo el movimiento especial "Comer al Paso"
                var displacement = new Point(targetLocation.X - piece.Ubicacion.X, targetLocation.Y - piece.Ubicacion.Y);
                if ((displacement.X == -1 || displacement.X == 1) && displacement.Y == (CurrentPlayer.Equals(Player1) ? -1 : 1) && !this.Pieces.Any(x => x.Ubicacion == targetLocation))
                {
                    Point rivalLocation = new Point(targetLocation.X, targetLocation.Y + (CurrentPlayer.Equals(Player1) ? 1 : -1));
                    var romovePiece = this.Pieces.First(p => p.Ubicacion == rivalLocation);
                    Remove_Piece(romovePiece, log); // elimina el peon rival
                }
                #endregion
            }
            if (piece.GetType() == typeof(Rey))
            {
                if (Math.Abs(piece.Ubicacion.X - targetLocation.X) == 2) // enroque
                {
                    Pieza _rock = null;
                    if ((targetLocation.X - piece.Ubicacion.X) < 0) // enrrosque largo
                        _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 0 && p.Ubicacion.Y == piece.Ubicacion.Y);
                    else // enrrosque corto
                        _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Torre) && p.Ubicacion.X == 7 && p.Ubicacion.Y == piece.Ubicacion.Y);

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

        #region Update
        /// <summary>
        /// Metodo que donde se escribe la logica del juego
        /// </summary>
        /// <param name="gameTime">informacion de tiempo</param>
        protected void Update(Tiempo gameTime)
        {
            this.Pieces.ForEach(x => x.Posicion = Get_PiecePosition(x.Ubicacion));
            // Actualiza la posicion en pantalla de cada pieza segun su coordenada en el tablero
            label_jugador.Text = $"Player { (CurrentPlayer.Equals(Player1) ? "1" : "2") }";
        }
        #endregion

        #region Draw
        /// <summary>
        /// Dibuja todos los sprites en pantalla
        /// </summary>
        /// <param name="drawHandler">controlador de dibujado</param>
        public void Draw(DrawHandler drawHandler)
        {
            this.Board.Draw(drawHandler);
            this.Pieces.ForEach(x => x.Draw(drawHandler));
        }
        #endregion
    }
    public enum State
    {
        Normal,
        Check,
        Checkmate,
        Draw
    }
}
