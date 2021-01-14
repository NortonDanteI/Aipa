using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aipa.Controlador
{
    public class Tiempo
    {
        public Tiempo()
        {
            Fecha_inicial =
            Frame_fecha_actual = DateTime.Now;
        }

        /// <summary>
        /// Fecha y hora de inicio del juego
        /// </summary>
        public DateTime Fecha_inicial { get; set; }
        /// <summary>
        /// Fecha y hora de ejecucion del frame actual
        /// </summary>
        public DateTime Frame_fecha_actual { get; set; }
        /// <summary>
        /// Tiempo total de ejecucion del juego
        /// </summary>
        public TimeSpan Tiempo_total { get { return Frame_fecha_actual - Fecha_inicial; } }
        /// <summary>
        /// Milisegundos transcurridos entre el cuadro actual y el anterior
        /// </summary>
        public int FrameMillisegundos { get; set; }
    }
}
