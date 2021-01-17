using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aipa.Modelo;

namespace Aipa.Controlador
{
    public class Historial_acciones
    {
        public Historial_acciones()
        {
            movimientos = new List<Historial_movimiento>();
            Pieza_removida = new List<Pieza>();
        }

        /// <summary>
        /// Lista de movimientos realizados en el turno
        /// </summary>
        public List<Historial_movimiento> movimientos { get; set; }
        /// <summary>
        /// Pieza eliminada en el turno
        /// </summary>
        public List<Pieza> Pieza_removida { get; set; }
        /// <summary>
        /// Pieza agregada en el turno (Coronacion de peon)
        /// </summary>
        public Pieza Pieza_añadida { get; set; }
    }
}
