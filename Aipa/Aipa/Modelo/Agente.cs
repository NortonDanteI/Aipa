using Aipa.Modelo;
using Aipa.Controlador;
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
        #region Constructor
        /// <summary>
        /// Agente
        /// </summary>
        /// <param name="color">Color de las piezas del agente</param>
        /// <param name="tipo_jugador">Tipo de jugador</param>
        /// <param name="numero">Numero del jugador</param>
        public Agente(Estado estado, List<Historial_acciones> ActionLog_, List<Pieza> Piezas, int profundidad_limite_, UnColor color, Tipo_de_jugador tipo_jugador, int numero) : base(color, tipo_jugador, numero)
        {
            if (color == UnColor.Negro) {
                this.color_humano = UnColor.Blanco;
            }
            else {
                this.color_humano = UnColor.Negro;
            }

            this.Las_piezas = Piezas;
            this.profundidad_limite = profundidad_limite_;
            this.ActionLog = ActionLog_;
            this.GameState = estado;
        }
        #endregion

        #region Propiedades
        private UnColor color_humano { get; set; }
        private List<Pieza> Las_piezas { get; set; }
        private int profundidad_limite { get; set; }
        private List<Historial_acciones> ActionLog { get; set; }
        private Estado GameState { get; set; }
        private Point punto { get; set; }
        #endregion

        #region Metodos
        public Point Obtener_movimiento_optimo() {
            Point punto = new Point(-1, -1);
            //Imprimir_variables_que_llegan();
            Imprimir_tablero();
            punto = Funcion_min_max();
            return punto;
        }
        #endregion

        #region Funcion minimax
        private Point Funcion_min_max() {
            Point puntito = new Point(0, 0);
            Calcular_movimientos(Las_piezas);

            //Calcular los movimientos posibles blancas.
            //primero movimientos comunes.
            //luego movimientos especiales.
            //Calcular los movimientos posibles negras.
            //primero movimientos comunes.
            //luego movimientos especiales.
            return punto;
        }
        #endregion

        #region Funcion de evaluacion
        private float Funcion_eval() {
            float puntaje_jugador = 0;
            float puntaje_Aipa = 0;

            List<Pieza> Piezas_jugador = Las_piezas.Where(x => x.Color == color_humano).ToList();
            List<Pieza> Piezas_enemigo = Las_piezas.Where(x => x.Color == color_humano).ToList();
            foreach (var a in Piezas_jugador) {
                puntaje_jugador += (a.valor_pieza);
            }

            foreach (var b in Piezas_enemigo)
            {
                puntaje_Aipa += (b.valor_pieza);
            }

            return puntaje_Aipa - puntaje_jugador;
        }
        #endregion

        #region metodos para actualizar movimientos posibles
        private void Calcular_movimientos(List<Pieza> piezas_actuales)
        {
            List<Pieza> piezas_blancas = piezas_actuales.Where(x => x.Color == UnColor.Blanco).ToList();
            List<Pieza> piezas_negras = piezas_actuales.Where(x => x.Color == UnColor.Negro).ToList();
            var rey_blancas = piezas_blancas.FirstOrDefault(x => x.GetType() == typeof(Rey));
            var rey_negras = piezas_negras.FirstOrDefault(x => x.GetType() == typeof(Rey));
            
            List<Pieza> nuevas_piezas_blancas = new List<Pieza>();
            List<Pieza> nuevas_piezas_negras = new List<Pieza>();
            List<Pieza> nuevas_piezas = new List<Pieza>();

            //Obteniendo nueva ubicacion de piezas blancas
            foreach (var a in piezas_blancas) {
                a.Seleccionada = false;
                if (a.GetType().Name == "Peon") {
                    nuevas_piezas_blancas.Add(obtener_peon_modificado_blancas(piezas_actuales,piezas_negras, a, rey_negras));
                }
                if (a.GetType().Name == "Torre") {
                    nuevas_piezas_blancas.Add(obtener_torre_modificado_blancas(piezas_actuales, a,rey_negras));
                }
                if (a.GetType().Name == "Caballo") {
                    nuevas_piezas_blancas.Add(obtener_caballo_modificado_blancas(piezas_actuales, a, rey_negras));
                }
                if (a.GetType().Name == "Alfil") { 
                
                }
                if (a.GetType().Name == "Reina") { }
                if (a.GetType().Name == "Rey") { }
            }

            //imprimir tablero y movimientos de las piezas

            //Obteniendo nueva ubicacion de piezas negras
            foreach (var b in piezas_negras)
            {
                b.Seleccionada = false;
                if (b.GetType().Name == "Peon")
                {
                    nuevas_piezas_negras.Add(obtener_peon_modificado_negras(piezas_actuales, piezas_blancas, b, rey_blancas));
                }
                if (b.GetType().Name == "Torre") {
                    nuevas_piezas_negras.Add(obtener_torre_modificado_negras(piezas_actuales, b, rey_blancas));
                }
                if (b.GetType().Name == "Caballo") {
                    //nuevas_piezas_negras.Add(obtener_caballo_modificado_negras(piezas_actuales, b, rey_negras));
                }
                if (b.GetType().Name == "Alfil") { }
                if (b.GetType().Name == "Reina") { }
                if (b.GetType().Name == "Rey") { }
            }

        }
        #endregion

        #region Metodos para actualizar movimientos
        private Pieza obtener_peon_modificado_blancas(List<Pieza> piezas_actuales, List<Pieza> piezas_negras, Pieza peon_blancas, Pieza rey_negras) {
            int total_movs = peon_blancas.Movimientos.Length;
            List<Point> lista_de_ubicaciones = new List<Point>();

            for (int d = 0; d < total_movs; d++)
            {
                Point nueva_ubicacion = new Point(peon_blancas.Ubicacion.X + peon_blancas.Movimientos[d].Direccion.X, peon_blancas.Ubicacion.Y + peon_blancas.Movimientos[d].Direccion.Y);

                //Ninguna pieza puede moverse encima del rey enemigo
                if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                {
                    ///hacer nada
                }
                else {
                    //Primer mov del peon (0,-1) mueve un espacio linea recta
                    if (d == 0) {
                        //si alguna de las piezas del tablero tiene esta ubicacion entonces
                        if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion))) {
                            //hacer nada
                        }
                        else {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }

                    //Segundo mov del peon (0,-2) mueve dos espacios linea recta
                    if (d == 1) {
                        //si alguna de las piezas del tablero tiene esta ubicacion entonces
                        if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion))) {
                            //hacer nada
                        }
                        else {
                            //si la ubicacion es Y=6 significa que inicial para las blancas
                            if (peon_blancas.Ubicacion.Y == 6) {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                    }

                    //Tercer mov del peon (-1,-1) come hacia la izquierda
                    if (d == 2) {
                        //Solo si hay alguna pieza rival en esa posicion
                        if (piezas_negras.Any(x => x.Ubicacion == nueva_ubicacion)) {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }

                    //cuarto mov del peon (1,-1) come hacia la derecha
                    if (d == 3) {
                        //Solo si hay alguna pieza rival en esa posicion
                        if (piezas_negras.Any(x => x.Ubicacion == nueva_ubicacion)) {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }
                }
            }

            Point[] movimientos_permitidos = new Point[lista_de_ubicaciones.Count()];

            for (int f = 0; f < lista_de_ubicaciones.Count; f++) {
                movimientos_permitidos[f] = lista_de_ubicaciones[f];
            }

            peon_blancas.Movimientos_permitidos = movimientos_permitidos;
            return peon_blancas;

        }
        private Pieza obtener_torre_modificado_blancas(List<Pieza> piezas_actuales, Pieza torre_blancas, Pieza rey_negras)
        {
            int total_movs = torre_blancas.Movimientos.Length;
            List<Point> lista_de_ubicaciones = new List<Point>();

            for (int d = 0; d < total_movs; d++){
                //Primer mov de la torre (0,1) mueve hacia abajo cada ves hasta que encuentra pieza
                if (d == 0)
                {
                    Point nueva_ubicacion = new Point(torre_blancas.Ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, torre_blancas.Ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion (encontro pieza) entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, nueva_ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    }
                }

                //segundo mov de la torre (0,-1) mueve hacia arriba cada ves hasta que encuentra una pieza
                if (d == 1)
                {
                    Point nueva_ubicacion = new Point(torre_blancas.Ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, torre_blancas.Ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, nueva_ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    }
                }

                //tercer mov de la torre (1,0) mueve hacia derecha cada ves hasta que encuentra una pieza
                if (d == 2){
                    Point nueva_ubicacion = new Point(torre_blancas.Ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, torre_blancas.Ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, nueva_ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    }
                }

                //cuarto mov de la torre (-1,0) mueve hacia izquierda cada ves hasta que encuentra una pieza
                if (d == 3)
                {
                    Point nueva_ubicacion = new Point(torre_blancas.Ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, torre_blancas.Ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_blancas.Movimientos[d].Direccion.X, nueva_ubicacion.Y + torre_blancas.Movimientos[d].Direccion.Y);
                    }
                }
            }

            Point[] movimientos_permitidos = new Point[lista_de_ubicaciones.Count()];

            for (int f = 0; f < lista_de_ubicaciones.Count; f++)
            {
                movimientos_permitidos[f] = lista_de_ubicaciones[f];
            }

            torre_blancas.Movimientos_permitidos = movimientos_permitidos;
            return torre_blancas;

        }
        private Pieza obtener_caballo_modificado_blancas(List<Pieza> piezas_actuales,Pieza caballo_blancas,Pieza rey_negras){

            int total_movs = caballo_blancas.Movimientos.Length;
            List<Point> lista_de_ubicaciones = new List<Point>();

            for (int d = 0; d < total_movs; d++)
            {
                Point nueva_ubicacion = new Point(caballo_blancas.Ubicacion.X + caballo_blancas.Movimientos[d].Direccion.X, caballo_blancas.Ubicacion.Y + caballo_blancas.Movimientos[d].Direccion.Y);
                //Si en esa ubicacion esta un rey negra o sale del tablero           
                if (rey_negras.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                {
                    //hacer nada
                }
                else
                {
                    //Si alguna de las piezas blancas esta alli no se puede mover
                    if (piezas_actuales.Any(x => x.Color == UnColor.Blanco && x.Ubicacion == nueva_ubicacion))
                    {
                        //hacer nada
                    }
                    else
                    {
                        lista_de_ubicaciones.Add(nueva_ubicacion);
                    }
                }
            }

            Point[] movimientos_permitidos = new Point[lista_de_ubicaciones.Count()];

            for (int f = 0; f < lista_de_ubicaciones.Count; f++)
            {
                movimientos_permitidos[f] = lista_de_ubicaciones[f];
            }

            caballo_blancas.Movimientos_permitidos = movimientos_permitidos;
            return caballo_blancas;
        }
        private Pieza obtener_peon_modificado_negras(List<Pieza> piezas_actuales, List<Pieza> piezas_blancas, Pieza peon_negras, Pieza rey_blancas)
        {
            int total_movs = peon_negras.Movimientos.Length;
            List<Point> lista_de_ubicaciones = new List<Point>();

            for (int d = 0; d < total_movs; d++)
            {
                Point nueva_ubicacion = new Point(peon_negras.Ubicacion.X + (peon_negras.Movimientos[d].Direccion.X), peon_negras.Ubicacion.Y + (peon_negras.Movimientos[d].Direccion.Y*-1));

                //Ninguna pieza puede moverse encima del rey enemigo ni salirse del tablero
                if (rey_blancas.Ubicacion == nueva_ubicacion && nueva_ubicacion.X>=0 && nueva_ubicacion.X<=7 && nueva_ubicacion.Y>=0 && nueva_ubicacion.Y<=7 )
                {
                    ///hacer nada
                }
                else
                {
                    //Primer mov del peon (0,-1) mueve un espacio linea recta
                    if (d == 0)
                    {
                        //si alguna de las piezas del tablero tiene esta ubicacion entonces
                        if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                        {
                            //hacer nada
                        }
                        else
                        {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }

                    //Segundo mov del peon (0,-2) mueve dos espacios linea recta
                    if (d == 1)
                    {
                        //si alguna de las piezas del tablero tiene esta ubicacion entonces
                        if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                        {
                            //hacer nada
                        }
                        else
                        {
                            //si la ubicacion es Y=6 significa que inicial para las blancas
                            if (peon_negras.Ubicacion.Y == 6)
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                    }

                    //Tercer mov del peon (-1,-1) come hacia la izquierda
                    if (d == 2)
                    {
                        //Solo si hay alguna pieza rival en esa posicion
                        if (piezas_blancas.Any(x => x.Ubicacion == nueva_ubicacion))
                        {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }

                    //cuarto mov del peon (1,-1) come hacia la derecha
                    if (d == 3)
                    {
                        //Solo si hay alguna pieza rival en esa posicion
                        if (piezas_blancas.Any(x => x.Ubicacion == nueva_ubicacion))
                        {
                            lista_de_ubicaciones.Add(nueva_ubicacion);
                        }
                    }
                }
            }

            Point[] movimientos_permitidos = new Point[lista_de_ubicaciones.Count()];

            for (int f = 0; f < lista_de_ubicaciones.Count; f++)
            {
                movimientos_permitidos[f] = lista_de_ubicaciones[f];
            }

            peon_negras.Movimientos_permitidos = movimientos_permitidos;
            return peon_negras;

        }
        private Pieza obtener_torre_modificado_negras(List<Pieza> piezas_actuales, Pieza torre_negras, Pieza rey_blancas)
        {
            int total_movs = torre_negras.Movimientos.Length;
            List<Point> lista_de_ubicaciones = new List<Point>();

            for (int d = 0; d < total_movs; d++)
            {
                //Primer mov de la torre (0,1) mueve hacia abajo cada ves hasta que encuentra pieza
                if (d == 0)
                {
                    Point nueva_ubicacion = new Point(torre_negras.Ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, torre_negras.Ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_blancas.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion (encontro pieza) entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, nueva_ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    }
                }

                //segundo mov de la torre (0,-1) mueve hacia arriba cada ves hasta que encuentra una pieza
                if (d == 1)
                {
                    Point nueva_ubicacion = new Point(torre_negras.Ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, torre_negras.Ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_blancas.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, nueva_ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    }
                }

                //tercer mov de la torre (1,0) mueve hacia derecha cada ves hasta que encuentra una pieza
                if (d == 2)
                {
                    Point nueva_ubicacion = new Point(torre_negras.Ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, torre_negras.Ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_blancas.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, nueva_ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    }
                }

                //cuarto mov de la torre (-1,0) mueve hacia izquierda cada ves hasta que encuentra una pieza
                if (d == 3)
                {
                    Point nueva_ubicacion = new Point(torre_negras.Ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, torre_negras.Ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    Boolean continua = true;
                    //mientras no encuentre pieza y mientras no salga del tablero
                    while (continua)
                    {
                        if (rey_blancas.Ubicacion == nueva_ubicacion && nueva_ubicacion.X >= 0 && nueva_ubicacion.X <= 7 && nueva_ubicacion.Y >= 0 && nueva_ubicacion.Y <= 7)
                        {
                            continua = false;
                        }
                        else
                        {
                            // si alguna de las piezas del tablero tiene esta ubicacion entonces 
                            if (piezas_actuales.Any(x => x.Ubicacion == (nueva_ubicacion)))
                            {
                                Pieza pieza = (Pieza)piezas_actuales.Where(x => x.Ubicacion == nueva_ubicacion);
                                if (pieza.Color == UnColor.Blanco)
                                {
                                    //hacer nada
                                    continua = false;
                                }
                                else
                                { //si la pieza es de color negro se la puede comer
                                    lista_de_ubicaciones.Add(nueva_ubicacion);
                                    continua = false;
                                }
                            }
                            else
                            {
                                lista_de_ubicaciones.Add(nueva_ubicacion);
                            }
                        }
                        nueva_ubicacion = new Point(nueva_ubicacion.X + torre_negras.Movimientos[d].Direccion.X*-1, nueva_ubicacion.Y + torre_negras.Movimientos[d].Direccion.Y*-1);
                    }
                }
            }

            Point[] movimientos_permitidos = new Point[lista_de_ubicaciones.Count()];

            for (int f = 0; f < lista_de_ubicaciones.Count; f++)
            {
                movimientos_permitidos[f] = lista_de_ubicaciones[f];
            }

            torre_negras.Movimientos_permitidos = movimientos_permitidos;
            return torre_negras;

        }
        #endregion

        #region Metodos de impresion
        private void Imprimir_tablero()
        {
            Pieza[,] tablero = transformar_lista_a_matriz();
            Console.WriteLine("Imprimiendo tablero");
            Console.WriteLine("  0|1|2|3|4|5|6|7");
            for (int y = 0; y < 8; y++)
            {
                Console.Write(y + "|");
                for (int x = 0; x < 8; x++)
                {
                    if (tablero[x, y] != null)
                    {
                        if (tablero[x, y].GetType().Name == "Torre") { Console.Write("T|"); }
                        if (tablero[x, y].GetType().Name == "Caballo") { Console.Write("C|"); }
                        if (tablero[x, y].GetType().Name == "Alfil") { Console.Write("A|"); }
                        if (tablero[x, y].GetType().Name == "Reina") { Console.Write("Q|"); }
                        if (tablero[x, y].GetType().Name == "Rey") { Console.Write("R|"); }
                        if (tablero[x, y].GetType().Name == "Peon") { Console.Write("P|"); }
                    }
                    else { Console.Write(" |"); }
                }
                Console.WriteLine("");
            }
        }

        private Pieza[,] transformar_lista_a_matriz()
        {
            Pieza[,] tablero = new Pieza[8, 8];

            //inicializo arreglo vacio
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    tablero[a, b] = null;
                }
            }

            foreach (var piezita in Las_piezas)
            {
                tablero[piezita.Ubicacion.X, piezita.Ubicacion.Y] = piezita;
            }

            return tablero;
        }
        private void Imprimir_variables_que_llegan()
        {
            Console.WriteLine("Color de las piezas del humano: " + color_humano);
            foreach (var pieza in Las_piezas)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Tipo de pieza: " + pieza.GetType().Name);
                Console.WriteLine("Color: " + pieza.Color);
                Imprimir_movimientos(pieza.Movimientos);
                Console.WriteLine("----------Imprimiendo Ubicación-----------: ");
                Console.WriteLine(pieza.Ubicacion);
                Imprimir_movimientos_permitidos(pieza.Movimientos_permitidos);
            }
            /*
            Console.WriteLine("Profundidad limite: " + profundidad_limite);

            foreach (var histo in ActionLog) {
                Console.WriteLine("--------Imprimiendo historial de acciones------");
                Imprimir_historial_movimiento(histo.movimientos);
                Imprimir_historial_removidas(histo.Pieza_removida);
                Console.WriteLine(histo.Pieza_añadida);
            }
            */
        }
        private void Imprimir_movimientos(Movimiento[] movimiento)
        {
            foreach (var m in movimiento)
            {
                Console.WriteLine("-------Imprimiendo movimiento que puede hacer la pieza------:");
                Console.WriteLine("Dirección: " + m.Direccion);
                Console.WriteLine("Tipo de movimiento: " + m.Tipo_de_mov);
                Console.WriteLine("Lineal: " + m.IsLinear);
            }
        }
        private void Imprimir_movimientos_permitidos(Point[] punto)
        {
            foreach (var p in punto)
            {
                Console.WriteLine("-------Imprimiendo movimientos permitidos-------:");
                Console.Write("(" + p.X + ",");
                Console.Write(p.Y + ")\n");
            }
        }
        private void Imprimir_historial_movimiento(List<Historial_movimiento> hist)
        {
            foreach (var a in hist)
            {
                Console.WriteLine("Imprimiendo historial");
                Imprimir_historia_mov(a);
            }
        }
        private void Imprimir_historia_mov(Historial_movimiento a)
        {
            Console.WriteLine(a.Pieza_);
            Console.WriteLine(a.Ubicacion_nueva);
            Console.WriteLine(a.Ubicacion_original);
        }
        private void Imprimir_historial_removidas(List<Pieza> Pieza_removida)
        {
            foreach (var x in Pieza_removida)
            {
                Console.WriteLine("-------Imprimiendo piezas removidas---------");
                Console.WriteLine(x.GetType().Name);
            }
        }
        #endregion
    }
}