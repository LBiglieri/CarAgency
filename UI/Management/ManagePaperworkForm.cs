using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
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
using UI.Clients.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace UI.Vehicles
{

    public partial class ManagePaperworkForm : MetroFramework.Forms.MetroForm
    {
        PaperworkBLL _PaperworkBLL;
        ClientsBLL _ClientsBLL;
        List<Paperwork> paperworks = new List<Paperwork>();
        Guid Client_Id = Guid.Empty;

        public ManagePaperworkForm()
        {
            InitializeComponent();
            _PaperworkBLL = new PaperworkBLL();
            _ClientsBLL = new ClientsBLL();

            clientView1.clientViewMode = ClientViewMode.WithNoRegistration;
            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);

            PerformUpdatePaperworkView();
        }
        #region "Performs"
        void PerformUpdatePaperworkView()
        {
            try
            {
                paperworks = _PaperworkBLL.GetAllActiveByClient(Client_Id);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = paperworks;
                if (paperworks != null && paperworks.Count > 0 ) 
                {
                    metroGrid1.Columns["Id"].Visible = false;
                    metroGrid1.Columns["Vehicle_Id"].Visible = false;
                    metroGrid1.Columns["Client_Id"].Visible = false;
                    metroGrid1.Columns["Invoice_Id"].Visible = false;
                    metroGrid1.Columns["Transfer_Date"].Visible = false;
                    metroGrid1.Columns["Paperwork_Precharge_Code"].Visible = false;
                    metroGrid1.Columns["IsFinished"].Visible = false;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
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
                    Paperwork paperwork = (Paperwork)row.DataBoundItem;
                    PaperworkForm frm;
                    if (clientView1.client!=null)
                        frm = new PaperworkForm(clientView1.client, paperwork, PaperworkFormMode.Update);
                    else
                    {
                        Client client = _ClientsBLL.GetById(paperwork.Client_Id);
                        frm = new PaperworkForm(client, paperwork, PaperworkFormMode.Update);
                    }
                    frm.ShowDialog();
                    PerformUpdatePaperworkView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddPaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                PaperworkForm frm = new PaperworkForm(clientView1.client, null, PaperworkFormMode.Add);
                frm.ShowDialog();
                PerformUpdatePaperworkView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clientView1_ClientFound(object sender, EventArgs e)
        {
            if (clientView1.client != null)
            {
                Client_Id = clientView1.client.Id;
                PerformUpdatePaperworkView();
            }
        }
    }
}
