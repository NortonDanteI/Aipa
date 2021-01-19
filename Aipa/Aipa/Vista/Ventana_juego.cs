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
        //private Agente agente = new Agente(0,UnColor.Negro,Tipo_de_jugador.Agente,2,null,Estado.Normal);
        private Tablero_manipulable val = new Tablero_manipulable();
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
            _timer = new Timer
            {
                Interval = 1000 / 30 // FPS (el intervalo no siempre se respeta en winforms)
            };
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
            Comenzar_juego();
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
        private List<Pieza> Piezas { get; set; }

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

        #region Funcionalidades de eventos basicos
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

            Ventana_manual Formulario = new Ventana_manual();
            Formulario.Show();
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Start();
            val = new Tablero_manipulable();
            Comenzar_juego();
        }
        #endregion

        #region evento principal
        /// <summary>
        /// Evento que se desencadena al liberar el boton del mouse sobre el lienzo
        /// </summary>  
        private void Ventana_juego_Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!(this.GameState == Estado.Jaquemate && this.GameState == Estado.Empate))
            {
                if (Jugador_jugando.Tipo_jugador == Tipo_de_jugador.Humano)
                {
                    Point _mouseLocation = new Point(e.Location.X - 27, e.Location.Y - 4); // resto los bordes del tablero        
                    var cell_Location = new Point(_mouseLocation.X / 82, _mouseLocation.Y / 82); // cada celda tiene un tamaños de  100x100 + 5x5 de borde                                                                                          
                    if (!val.Mover_pieza(cell_Location)) // si existe una pieza seleccionada, intenta moverla a la celda donde se realizo click
                    {
                        val.Set_pieza_seleccionada(cell_Location); // si la pieza seleccionada no se puede mover a la celda destino, se intenta seleccionar otra pieza
                    }
                    else
                    {
                        this.Board = val.Board;
                        this.Piezas = val.Piezas;
                        this.Player1 = val.Player1;
                        this.Player2 = val.Player2;
                        this.Jugador_jugando = val.Jugador_jugando;
                        this.ActionLog = val.ActionLog;
                        this.GameState = val.GameState;
                    }

                    if (Jugador_jugando.Tipo_jugador == Tipo_de_jugador.Agente)
                    {
                        int dificultad = 3;
                        Agente agente = new Agente(dificultad, Jugador_jugando, ActionLog, GameState, Board);
                        //obtengo optimo
                        label1.Visible = true;
                        Refresh();
                        agente.Alfa_Beta(this.Piezas);
                        label1.Visible = false;
                        Refresh();

                        //donde quiero mover la pieza
                        var cell_Locationn = agente.Cell_location;
                        //seteo tablero
                        if (agente.Pieza_a_mover != null)
                        {
                            val.Set_pieza_seleccionada(agente.Pieza_a_mover.Ubicacion);
                            //muevo la pieza
                            if (val.Mover_pieza(cell_Locationn)) // si existe una pieza seleccionada, intenta moverla a la celda donde se realizo click
                            {
                                this.Board = val.Board;
                                this.Piezas = val.Piezas;
                                this.Player1 = val.Player1;
                                this.Player2 = val.Player2;
                                this.Jugador_jugando = val.Jugador_jugando;
                                this.ActionLog = val.ActionLog;
                                this.GameState = val.GameState;
                            }
                        }
                        else
                        {
                            val = new Tablero_manipulable();
                            Comenzar_juego();
                        }
                    }
                }
            }
            else {
                val = new Tablero_manipulable();
                Comenzar_juego();
            }
        }
        #endregion

        #region metodos
        /// <summary>
        /// Inicializa el juego cargando los recursos
        /// </summary>
        /// 

        public String numero_a_coordenada(int x, int y)
        {
            String salida = "";

            switch (x)
            {
                case 1: salida += "A"; break;
                case 2: salida += "B"; break;
                case 3: salida += "C"; break;
                case 4: salida += "D"; break;
                case 5: salida += "E"; break;
                case 6: salida += "F"; break;
                case 7: salida += "G"; break;
                case 8: salida += "H"; break;
            }
            return salida + "" + y;
        }

        private void Initialize()
        {
            _ = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes");
            this.Resources = val.Resources;
        }

        /// <summary>
        /// Inicia una nueva partida 
        /// </summary>
        private void Comenzar_juego()
        {
            this.ActionLog = val.ActionLog;
            this.Piezas = val.Piezas;
            this.Board = val.Board;
            this.GameState = val.GameState;
            this.Jugador_jugando = val.Jugador_jugando;
            this.Player1 = val.Player1;
            this.Player2 = val.Player2;
            Siguiente_turno();
        }

        private void Siguiente_turno()
        {
            val.Siguiente_turno(true);
            this.Piezas = val.Piezas;
            this.GameState = val.GameState;           
            label_estado.Text = this.GameState.ToString();
        }

        private void boton_activar_consejos_Click(object sender, EventArgs e)
        {
            int ayuda = 3;
            Agente agenteAyuda = new Agente(ayuda, Jugador_jugando, ActionLog, GameState, Board);
            //obtengo optimo
            agenteAyuda.Alfa_Beta(this.Piezas);
            //donde quiero mover la pieza
            var ubicacion = agenteAyuda.Cell_location;
            //pieza me sirve para su ubicacion
            Pieza pasar = agenteAyuda.Pieza_a_mover;

            //Point modificado = new Point((pasar.Ubicacion.X+1), (8-pasar.Ubicacion.Y));
            //Point target = new Point((ubicacion.X+1),(8-ubicacion.Y));

            string desde = numero_a_coordenada((pasar.Ubicacion.X + 1), 8 - pasar.Ubicacion.Y);
            string hacia = numero_a_coordenada((ubicacion.X + 1), 8 - ubicacion.Y);

            label_recomendacion.Text = ("Se recomienda mover pieza " + pasar.GetType().Name + "\r\n Desde " + desde + " Hacia" + hacia).ToString();
           
            Jugador_jugando.Color = UnColor.Blanco;
            Jugador_jugando.Numero = 1;
            Jugador_jugando.Tipo_jugador = Tipo_de_jugador.Humano;
        }


        /// <summary>
        /// Metodo que donde se escribe la logica del juego
        /// </summary>
        /// <param name="gameTime">informacion de tiempo</param>
        private void Update(Tiempo gameTime)
        {
            this.Piezas.ForEach(x => x.Posicion = val.Obtener_ubicacion_pieza(x.Ubicacion));
            // Actualiza la posicion en pantalla de cada pieza segun su coordenada en el tablero
            label_jugador.Text = $"Player { (Jugador_jugando.Equals(Player1) ? "Humano" : "Aipa") }";
        }

        /// <summary>
        /// Dibuja todos los sprites en pantalla
        /// </summary>
        /// <param name="drawHandler">controlador de dibujado</param>
        public void Draw(DrawHandler drawHandler)
        {
            this.Board.Draw(drawHandler);
            this.Piezas.ForEach(x => x.Draw(drawHandler));
        }
        #endregion
        
    }
}
