using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UI.Ayuda
{
    /// <summary>
    /// F1 en cualquier pantalla abre la guia de usuario (PDF copiado junto al ejecutable).
    /// Es un filtro de mensajes del hilo de UI: cubre el MainForm, las hijas MDI y los dialogos modales sin tocar cada form.
    /// </summary>
    public class AyudaF1 : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private static readonly string Guia = Path.Combine(Application.StartupPath, "Ayuda", "Guia de usuario.pdf");

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != WM_KEYDOWN || (Keys)(int)m.WParam != Keys.F1)
                return false;
            Abrir();
            return true;  // se consume: no dispara ademas el HelpRequested de WinForms
        }

        /// <summary>Abre la guia de usuario; tambien la usa el item "Ayuda" del menu principal.</summary>
        public static void Abrir()
        {
            try
            {
                Process.Start(Guia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Guia, "CarAgency", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
