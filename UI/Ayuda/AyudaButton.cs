using System;
using System.Drawing;
using System.Windows.Forms;
using BE;
using Security.Session;

namespace UI.Ayuda
{
    /// <summary>
    /// Boton "?" que abre la ayuda de la pantalla que lo contiene.
    /// Los textos salen de los tags Help_{NombreDelForm} y Help_{NombreDelForm}_Next.
    /// </summary>
    public class AyudaButton : MetroFramework.Controls.MetroButton, ILanguageObserver
    {
        private const int ButtonSize = 25;
        private const int MarginRight = 20;
        private const int ButtonTop = 30;

        private readonly Form host;
        private readonly ToolTip toolTip = new ToolTip();
        private AyudaForm help;

        /// <summary>
        /// Agrega el boton de ayuda arriba a la derecha de la pantalla.
        /// Se llama despues de InitializeComponent() y ANTES de LanguageService.Attach(this),
        /// asi el Attach de la pantalla registra el boton como observer hijo.
        /// </summary>
        public static void Add(Form host)
        {
            host.Controls.Add(new AyudaButton(host));
        }

        private AyudaButton(Form host)
        {
            this.host = host;
            Name = "btnAyuda";
            Tag = "HelpButton";
            Text = "?";
            TabStop = false;
            Size = new Size(ButtonSize, ButtonSize);
            Location = new Point(host.ClientSize.Width - MarginRight - ButtonSize, ButtonTop);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Click += AyudaButton_Click;
            host.FormClosed += Host_FormClosed;
            UpdateLanguage("");
        }

        public void UpdateLanguage(string language)
        {
            toolTip.SetToolTip(this, LanguageService.GetTagText(Tag.ToString()));
        }

        private void AyudaButton_Click(object sender, EventArgs e)
        {
            // Una sola ayuda abierta por pantalla.
            if (help != null && !help.IsDisposed)
            {
                help.Activate();
                return;
            }
            // El duenio tiene que ser un form de nivel superior: si la pantalla es hija MDI, es el MainForm.
            help = new AyudaForm(host, this);
            help.Show(host.IsMdiChild ? host.MdiParent : host);
        }

        private void Host_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (help != null && !help.IsDisposed)
                help.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                toolTip.Dispose();
            base.Dispose(disposing);
        }
    }
}
