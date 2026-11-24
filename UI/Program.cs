using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarAgency.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var gui = Guid.NewGuid();
            Application.AddMessageFilter(new global::UI.Ayuda.AyudaF1());  // F1 en cualquier pantalla abre la guia
            Application.Run(new MainForm());
        }
    }
}
