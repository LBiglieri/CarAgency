using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Vehiculos;
using Security.Session;
using MetroFramework;
using MetroFramework.Controls;

namespace UI.Personas.Controls
{
    public enum PersonaViewMode
    {
        WithNoRegistration,
        WithRegistration
    }
    public partial class PersonaView : UserControl, ILanguageObserver
    {
        public Persona person;
        PersonaBLL _peoplebll;
        public PersonaViewMode personViewMode = PersonaViewMode.WithNoRegistration;

        public event EventHandler PersonFound;

        public PersonaView()
        {
            InitializeComponent();
            _peoplebll = new PersonaBLL();
            UpdateLanguage("");
        }

        public void InitWithPerson(Persona _person)
        {
            person = _person;
            UpdateControlView();
        }

        // Vuelve a pedir el DNI para buscar otra persona (PayAcquisitionForm).
        public void Clear()
        {
            person = null;
            lblDNI.Visible = false;
            lblNameSurname.Visible = false;
            lblPhoneHome.Visible = false;
            lblPhonePersonal.Visible = false;
            lblEmail.Visible = false;
            lblPlease.Visible = true;
            tbDni.Text = "";
            tbDni.Visible = true;
            tbDni.Enabled = true;
            UpdateLanguage("");
        }

        private void UpdateControlView()
        {
            if (person != null)
            {
                lblDNI.Visible = true;
                lblNameSurname.Visible = true;
                lblPhoneHome.Visible = true;
                lblPhonePersonal.Visible = true;
                lblEmail.Visible = true;
                lblPlease.Visible = false;
                tbDni.Visible = false;
                tbDni.Enabled = false;
                UpdateLanguage("");

                this.PersonFound(this, new EventArgs());
            }
        }

        private void tbDni_Leave(object sender, EventArgs e)
        {
            SearchPerson();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool handled = base.ProcessDialogKey(keyData);
            // Si tbDni es el único control habilitado del form (GenerateInvoiceForm), Tab no le saca el foco
            // y Leave nunca se dispara: con Enter o Tab se busca igual.
            if ((keyData == Keys.Enter || keyData == Keys.Tab) && tbDni.ContainsFocus)
                SearchPerson();
            return handled;
        }

        private void SearchPerson()
        {
            try
            {
                int dni;
                if (person == null && tbDni.Text != "" && int.TryParse(tbDni.Text, out dni))
                {

                    person = _peoplebll.GetByDni(dni);
                    if (person != null)
                    {
                        UpdateControlView();
                    }
                    else
                    {
                        if (personViewMode == PersonaViewMode.WithRegistration && MessageBox.Show(LanguageService.GetTagText("AttentionDNINotFound"), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            CreatePersonaForm frm = new CreatePersonaForm(dni);
                            frm.ShowDialog();
                            if (frm._savedPerson != null)
                            {
                                person = _peoplebll.GetByDni(dni);
                                UpdateControlView();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        public void UpdateLanguage(string language)
        {
            if (person == null)
            {
                lblPlease.Text = LanguageService.GetTagText(lblPlease.Tag.ToString());
            }
            else
            {
                // El nombre va solo, como título de la ficha; el resto lleva su etiqueta traducida.
                SetNameText((person.Nombre + " " + person.Apellido).Trim());
                SetFieldText(lblDNI, person.DNI.ToString());
                SetFieldText(lblPhonePersonal, person.TelefonoPersonal);
                SetFieldText(lblPhoneHome, person.TelefonoCasa);
                SetFieldText(lblEmail, person.Email);

                lblDNI.Visible = true;
                lblNameSurname.Visible = true;
                lblPhoneHome.Visible = true;
                lblPhonePersonal.Visible = true;
                lblEmail.Visible = true;
                lblPlease.Visible = false;
                tbDni.Visible = false;
                tbDni.Enabled = false;
            }
        }

        // Las traducciones no siempre traen el espacio final ("DNI:" en es.xml), así que se normaliza acá.
        // Si el texto no entra, MetroLabel lo corta con "..." y el tooltip muestra el valor completo.
        private void SetFieldText(MetroLabel label, string value)
        {
            string text = LanguageService.GetTagText(label.Tag.ToString()).TrimEnd() + " " + (value ?? "");
            label.Text = text;
            toolTip.SetToolTip(label, text);
        }

        // Un nombre largo baja de Tall a Medium antes de recortarse, para que se lea entero en la mayoría de los casos.
        private void SetNameText(string fullName)
        {
            lblNameSurname.FontSize = MetroLabelSize.Tall;
            if (MeasureWidth(lblNameSurname, fullName) > lblNameSurname.Width)
                lblNameSurname.FontSize = MetroLabelSize.Medium;
            lblNameSurname.Text = fullName;
            toolTip.SetToolTip(lblNameSurname, fullName);
        }

        private static int MeasureWidth(MetroLabel label, string text)
        {
            Font font = MetroFonts.Label(label.FontSize, label.FontWeight);
            return TextRenderer.MeasureText(text, font).Width;
        }
    }
}
