using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Personas.Controls;
using Security.Session;
using static System.Net.Mime.MediaTypeNames;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{

    public partial class ManageTramiteForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        TramiteBLL _PaperworkBLL;
        PersonaBLL _PeopleBLL;
        List<Tramite> paperworks = new List<Tramite>();
        Guid Person_Id = Guid.Empty;

        public ManageTramiteForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _PaperworkBLL = new TramiteBLL();
            _PeopleBLL = new PersonaBLL();

            personView1.personViewMode = PersonaViewMode.WithNoRegistration;
            this.personView1.PersonFound += new EventHandler(personView1_PersonFound);

            PerformUpdatePaperworkView();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void ManagePaperworkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("ManageTramiteForm");
            this.Refresh();
            btnAddPaperwork.Text = LanguageService.GetTagText(btnAddPaperwork.Tag.ToString());
            btnEditPaperwork.Text = LanguageService.GetTagText(btnEditPaperwork.Tag.ToString());
            GridColumns.Translate(metroGrid1);
        }
        void PerformUpdatePaperworkView()
        {
            try
            {
                paperworks = _PaperworkBLL.GetAllActiveByPerson(Person_Id);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = paperworks;
                GridColumns.Show(metroGrid1, "Persona_Descripcion", "Vehiculo_Descripcion", "Observaciones");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        #endregion


        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnEditPaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                foreach (DataGridViewRow row in rows)
                {
                    Tramite paperwork = (Tramite)row.DataBoundItem;
                    TramiteForm frm;
                    if (personView1.person!=null)
                        frm = new TramiteForm(personView1.person, paperwork, TramiteFormMode.Update);
                    else
                    {
                        Persona person = _PeopleBLL.GetById(paperwork.Persona_Id);
                        frm = new TramiteForm(person, paperwork, TramiteFormMode.Update);
                    }
                    frm.ShowDialog();
                    PerformUpdatePaperworkView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnAddPaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                TramiteForm frm = new TramiteForm(personView1.person, null, TramiteFormMode.Add);
                frm.ShowDialog();
                PerformUpdatePaperworkView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        public void personView1_PersonFound(object sender, EventArgs e)
        {
            if (personView1.person != null)
            {
                Person_Id = personView1.person.Persona_Id;
                PerformUpdatePaperworkView();
            }
        }
    }
}
