using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BE;
using CarAgency.BE.Audit;
using CarAgency.Security.Audit;
using Security.Session;

namespace CarAgency.UI
{
    public partial class EventLogForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        AuditBLL _auditBLL;
        List<AuditEvent> events = new List<AuditEvent>();
        int printRowIndex;

        public EventLogForm()
        {
            AuditBLL.RequireAccess();
            InitializeComponent();
            _auditBLL = new AuditBLL();

            PerformFillModulesCombo();
            PerformClearFilters();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        #region Perform
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("EventLogForm");
            lblLogin.Text = LanguageService.GetTagText(lblLogin.Tag.ToString());
            lblFrom.Text = LanguageService.GetTagText(lblFrom.Tag.ToString());
            lblTo.Text = LanguageService.GetTagText(lblTo.Tag.ToString());
            lblModule.Text = LanguageService.GetTagText(lblModule.Tag.ToString());
            lblEvent.Text = LanguageService.GetTagText(lblEvent.Tag.ToString());
            lblCriticality.Text = LanguageService.GetTagText(lblCriticality.Tag.ToString());
            lblName.Text = LanguageService.GetTagText(lblName.Tag.ToString());
            lblSurname.Text = LanguageService.GetTagText(lblSurname.Tag.ToString());
            lblCount.Text = string.Format(LanguageService.GetTagText("AuditCount"), events.Count);
            btnClear.Text = LanguageService.GetTagText(btnClear.Tag.ToString());
            btnApply.Text = LanguageService.GetTagText(btnApply.Tag.ToString());
            btnPrint.Text = LanguageService.GetTagText(btnPrint.Tag.ToString());
            btnExit.Text = LanguageService.GetTagText(btnExit.Tag.ToString());
            colLogin.HeaderText = LanguageService.GetTagText("AuditLogin");
            colDate.HeaderText = LanguageService.GetTagText("AuditDate");
            colTime.HeaderText = LanguageService.GetTagText("AuditTime");
            colModule.HeaderText = LanguageService.GetTagText("AuditModule");
            colEvent.HeaderText = LanguageService.GetTagText("AuditEvent");
            colCriticality.HeaderText = LanguageService.GetTagText("AuditCriticality");
            this.Refresh();
        }

        void PerformFillLoginsCombo()
        {
            List<string> logins = _auditBLL.GetLogins();
            logins.Insert(0, "");
            comboLogin.DataSource = logins;
            comboLogin.SelectedIndex = 0;
        }

        void PerformFillModulesCombo()
        {
            comboModule.Items.Add("");
            foreach (AuditModule module in Enum.GetValues(typeof(AuditModule)))
            {
                comboModule.Items.Add(module);
            }
            comboModule.SelectedIndex = 0;
        }

        void PerformFillEventsCombo()
        {
            object selectedEvent = comboEvent.SelectedItem;
            comboEvent.Items.Clear();
            comboEvent.Items.Add("");
            foreach (AuditDefinition definition in AuditCatalog.All)
            {
                if (comboModule.SelectedIndex == 0 || definition.Module == (AuditModule)comboModule.SelectedItem)
                    comboEvent.Items.Add(definition.Type);
            }
            comboEvent.SelectedItem = selectedEvent;
            if (comboEvent.SelectedIndex == -1)
                comboEvent.SelectedIndex = 0;
        }

        void PerformClearFilters()
        {
            AuditFilter defaults = AuditFilter.LastThreeDays();
            dtFrom.Value = defaults.From;
            dtTo.Value = defaults.To;
            if (comboLogin.Items.Count > 0)
                comboLogin.SelectedIndex = 0;
            comboModule.SelectedIndex = 0;
            comboEvent.SelectedIndex = 0;
            comboCriticality.SelectedIndex = 0;
        }

        void PerformUpdateEventsView()
        {
            AuditFilter filter = new AuditFilter();
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            if (comboLogin.SelectedIndex > 0)
                filter.Login = (string)comboLogin.SelectedItem;
            if (comboModule.SelectedIndex > 0)
                filter.Module = (AuditModule)comboModule.SelectedItem;
            if (comboEvent.SelectedIndex > 0)
                filter.EventType = (AuditEventType)comboEvent.SelectedItem;
            if (comboCriticality.SelectedIndex > 0)
                filter.Criticality = (int)comboCriticality.SelectedItem;

            events = _auditBLL.Query(filter);
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = events;
            lblCount.Text = string.Format(LanguageService.GetTagText("AuditCount"), events.Count);
            btnPrint.Enabled = events.Count > 0;
            PerformPopulateUserData();
        }

        void PerformPopulateUserData()
        {
            if (metroGrid1.CurrentRow == null)
            {
                tbName.Text = "";
                tbSurname.Text = "";
                return;
            }
            AuditEvent selectedEvent = (AuditEvent)metroGrid1.CurrentRow.DataBoundItem;
            tbName.Text = selectedEvent.Name;
            tbSurname.Text = selectedEvent.Surname;
        }
        #endregion

        #region Form Events
        private void EventLogForm_Shown(object sender, EventArgs e)
        {
            try
            {
                AuditBLL.Record(AuditEventType.AuditViewed);
                PerformFillLoginsCombo();
                PerformUpdateEventsView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void EventLogForm_Disposed(object sender, EventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdateEventsView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                PerformClearFilters();
                PerformFillLoginsCombo();
                PerformUpdateEventsView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboModule.SelectedIndex != -1)
                PerformFillEventsCombo();
        }

        private void comboFilters_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is AuditModule)
                e.Value = LanguageService.GetTagText("AuditModule_" + e.ListItem);
            else if (e.ListItem is AuditEventType)
                e.Value = LanguageService.GetTagText("AuditEvent_" + e.ListItem);
            else if (string.Equals(e.ListItem, ""))
                e.Value = LanguageService.GetTagText("AuditAll");
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            PerformPopulateUserData();
        }

        private void metroGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.ColumnIndex == colModule.Index)
            {
                e.Value = LanguageService.GetTagText("AuditModule_" + e.Value);
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colEvent.Index)
            {
                e.Value = LanguageService.GetTagText("AuditEvent_" + e.Value);
                e.FormattingApplied = true;
            }
        }
        #endregion

        #region Printing
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                AuditBLL.RequireAccess();
                if (events.Count == 0)
                    return;
                printDocument1.DocumentName = this.Text;
                if (printDialog1.ShowDialog(this) != DialogResult.OK)
                    return;
                AuditBLL.Record(AuditEventType.AuditPrintRequested);
                printDocument1.Print();
            }
            catch (TranslatableException ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageService.GetTagText("AuditPrintFailed"));
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printRowIndex = 0;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
            using (Font rowFont = new Font("Segoe UI", 9))
            {
                float y = e.MarginBounds.Top;
                e.Graphics.DrawString(this.Text, titleFont, Brushes.Black, e.MarginBounds.Left, y);
                y += 36;
                PerformPrintRow(e, null, y, 42, rowFont);
                y += 42;
                while (printRowIndex < metroGrid1.Rows.Count && y + 52 <= e.MarginBounds.Bottom)
                {
                    PerformPrintRow(e, metroGrid1.Rows[printRowIndex], y, 52, rowFont);
                    printRowIndex++;
                    y += 52;
                }
                e.HasMorePages = printRowIndex < metroGrid1.Rows.Count;
            }
        }

        void PerformPrintRow(PrintPageEventArgs e, DataGridViewRow row, float y, float height, Font font)
        {
            float x = e.MarginBounds.Left;
            int totalWidth = metroGrid1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            using (StringFormat format = new StringFormat())
            {
                format.Trimming = StringTrimming.EllipsisCharacter;
                foreach (DataGridViewColumn column in metroGrid1.Columns)
                {
                    float width = (float)e.MarginBounds.Width * column.Width / totalWidth;
                    string text = column.HeaderText;
                    if (row != null)
                        text = Convert.ToString(row.Cells[column.Index].FormattedValue);
                    e.Graphics.DrawRectangle(Pens.LightGray, x, y, width, height);
                    e.Graphics.DrawString(text, font, Brushes.Black, new RectangleF(x + 4, y + 4, width - 8, height - 8), format);
                    x += width;
                }
            }
        }
        #endregion
    }
}
