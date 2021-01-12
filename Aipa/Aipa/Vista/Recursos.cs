using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aipa.Vista
{
    /// <summary>
    /// Clase que carga los recursos del juego
    /// </summary>
    public class Recursos
    {
        /// <summary>s
        /// Imagen del tablero
        /// </summary>
        public Image Imagen_tablero { get; set; }
        /// <summary>
        /// Imagen de la celda de color celeste que se muestra cuando la celda acepta el movimiento de la pieza seleccionada
        /// </summary>
        public Image Imagen_movimiento_resaltado { get; set; }
        /// <summary>
        /// Imagen de la celda de color verde que se muestra cuando se seleecciona la ficha
        /// </summary>
        public Image Imagen_seleccion_resaltado { get; set; }
        /// <summary>
        /// Imagen del peon Blanco
        /// </summary>
        public Image Imagen_peon_blanco { get; set; }
        /// <summary>
        /// Imagen de la Torre Blanca
        /// </summary>
        public Image Imagen_torre_blanca { get; set; }
        /// <summary>
        /// Imagen del Caballo Blanco
        /// </summary>
        public Image Imagen_caballo_blanco { get; set; }
        /// <summary>
        /// Imagen del Alfil Blanco
        /// </summary>
        public Image Imagen_alfil_blanco { get; set; }
        /// <summary>
        /// Imagen de la Reina Blanca
        /// </summary>
        public Image Imagen_reina_blanca { get; set; }
        /// <summary>
        /// Imagen del Rey Blanco
        /// </summary>
        public Image Imagen_rey_blanco { get; set; }
        /// <summary>
        /// Imagen del Peon Negro
        /// </summary>
        public Image Imagen_peon_negro { get; set; }
        /// <summary>
        /// Imagen de la Torre Negra
        /// </summary>
        public Image Imagen_torre_negra { get; set; }
        /// <summary>
        /// Imagen del caballo Negro
        /// </summary>
        public Image Imagen_caballo_negro { get; set; }
        /// <summary>
        /// Imagen del Alfil Negro
        /// </summary>
        public Image Imagen_alfil_negro { get; set; }
        /// <summary>
        /// Imagen de la Reina Negra
        /// </summary>
        public Image Imagen_reina_negra { get; set; }
        /// <summary>
        /// Imagen del Rey Negro
        /// </summary>
        public Image Imagen_rey_negro { get; set; }
    }
}
