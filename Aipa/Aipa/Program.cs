using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aipa.Vista;

namespace Aipa
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
 
            //Ventana_bienvenida ventana_bienvenida = new Ventana_bienvenida();
            //ventana_bienvenida.ShowDialog();
            Application.Run(new Ventana_juego());
        }
    }
}
