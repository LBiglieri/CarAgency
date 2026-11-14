using System;
using System.Drawing;
using System.Windows.Forms;
using BE;
using Security.Session;

namespace UI.Ayuda
{
    /// <summary>
    /// Ventana de ayuda de una pantalla. No es modal: asi se puede cambiar el idioma
    /// con la ayuda abierta y, como es observer, se traduce en el momento.
    /// </summary>
    public partial class AyudaForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        private const int ContentLeft = 20;
        private const int ContentTop = 60;
        private const int ContentWidth = 380;
        private const int Gap = 12;
        private const int BottomMargin = 20;

        private readonly string helpTag;
        private readonly Control button;

        public AyudaForm(Form host, Control button)
        {
            InitializeComponent();
            helpTag = "Help_" + host.GetType().Name;
            this.button = button;
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        // El layout se vuelve a armar al cargar, antes de que la ventana se vea.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ArrangeContent();
            PlaceBelowButton();
        }

        private void AyudaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LanguageService.Detach(this);
        }

        public void UpdateLanguage(string language)
        {
            Text = LanguageService.GetTagText("HelpButton");
            lblBody.Text = LanguageService.GetTagText(helpTag);
            string next = LanguageService.GetTagText(helpTag + "_Next");
            lblNext.Text = next == "" ? "" : LanguageService.GetTagText("HelpNextStep") + ": " + next;
            lblNext.Visible = next != "";
            ArrangeContent();
            Invalidate();
        }

        // Los labels son AutoSize con ancho maximo (MaximumSize en el Designer): calculan
        // su alto con la misma logica con la que dibujan, asi el texto largo baja de
        // renglon sin pisarse. El alto de la ventana sale de ahi y cambia con el idioma.
        private void ArrangeContent()
        {
            lblBody.Location = new Point(ContentLeft, ContentTop);
            int bottom = lblBody.Bottom;
            if (lblNext.Visible)
            {
                lblNext.Location = new Point(ContentLeft, bottom + Gap);
                bottom = lblNext.Bottom;
            }
            ClientSize = new Size(ContentLeft * 2 + ContentWidth, bottom + BottomMargin);
        }

        // Debajo del boton "?", alineada a su derecha y sin salirse de la pantalla.
        private void PlaceBelowButton()
        {
            Point corner = button.PointToScreen(new Point(button.Width, button.Height + 4));
            Rectangle area = Screen.FromControl(button).WorkingArea;
            int x = Math.Max(area.Left, Math.Min(corner.X - Width, area.Right - Width));
            int y = Math.Max(area.Top, Math.Min(corner.Y, area.Bottom - Height));
            Location = new Point(x, y);
        }
    }
}
