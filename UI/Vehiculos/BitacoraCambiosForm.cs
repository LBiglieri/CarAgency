using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BE;
using BLL;
using CarAgency.BE;
using Security.Session;
using UI.Comun;

namespace UI.Vehiculos
{
    // Historial de cada vehiculo: altas, modificaciones campo por campo y ventas.
    // Las filas las escriben los triggers de la base; esta pantalla solo las consulta.
    public partial class BitacoraCambiosForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        BitacoraCambiosBLL _changeLogBLL;
        VehiculoBLL _vehicleBLL;
        List<BitacoraCambio> changes = new List<BitacoraCambio>();
        // Vehiculo con el que se abrio la pantalla desde el ABM; null si se abrio desde el menu.
        readonly Guid? initialVehicleId;
        int printRowIndex;

        public BitacoraCambiosForm() : this(null) { }

        public BitacoraCambiosForm(Guid? vehicleId)
        {
            BitacoraCambiosBLL.RequireAccess();
            InitializeComponent();
            // El diseñador no persiste estas dos propiedades: si estuvieran en el Designer se perderían al guardarlo.
            metroGrid1.AutoGenerateColumns = false;
            printDocument1.DefaultPageSettings.Landscape = true;
            _changeLogBLL = new BitacoraCambiosBLL();
            _vehicleBLL = new VehiculoBLL();
            initialVehicleId = vehicleId;

            PerformFillOperationsCombo();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        #region Perform
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("BitacoraCambiosForm");
            lblVehicle.Text = LanguageService.GetTagText(lblVehicle.Tag.ToString());
            lblOperation.Text = LanguageService.GetTagText(lblOperation.Tag.ToString());
            lblFrom.Text = LanguageService.GetTagText(lblFrom.Tag.ToString());
            lblTo.Text = LanguageService.GetTagText(lblTo.Tag.ToString());
            PerformUpdateCount();
            btnClear.Text = LanguageService.GetTagText(btnClear.Tag.ToString());
            btnApply.Text = LanguageService.GetTagText(btnApply.Tag.ToString());
            btnPrint.Text = LanguageService.GetTagText(btnPrint.Tag.ToString());
            btnExit.Text = LanguageService.GetTagText(btnExit.Tag.ToString());
            colDate.HeaderText = LanguageService.GetTagText("AuditDate");
            colTime.HeaderText = LanguageService.GetTagText("AuditTime");
            colLicensePlate.HeaderText = LanguageService.GetTagText("ChangeLogLicensePlate");
            colOperation.HeaderText = LanguageService.GetTagText("ChangeLogOperation");
            colField.HeaderText = LanguageService.GetTagText("ChangeLogField");
            colOldValue.HeaderText = LanguageService.GetTagText("ChangeLogOldValue");
            colNewValue.HeaderText = LanguageService.GetTagText("ChangeLogNewValue");
            colUser.HeaderText = LanguageService.GetTagText("ChangeLogUser");
            this.Refresh();
        }

        void PerformFillVehiclesCombo()
        {
            comboVehicle.Items.Clear();
            comboVehicle.Items.Add("");
            foreach (Vehiculo vehicle in _vehicleBLL.GetAll())
                comboVehicle.Items.Add(vehicle);
            comboVehicle.SelectedIndex = 0;
        }

        void PerformFillOperationsCombo()
        {
            comboOperation.Items.Add("");
            foreach (BitacoraCambiosOperation operation in Enum.GetValues(typeof(BitacoraCambiosOperation)))
                comboOperation.Items.Add(operation);
            comboOperation.SelectedIndex = 0;
        }

        void PerformSelectVehicle(Guid? vehicleId)
        {
            comboVehicle.SelectedIndex = 0;
            if (!vehicleId.HasValue)
                return;
            for (int i = 1; i < comboVehicle.Items.Count; i++)
            {
                if (((Vehiculo)comboVehicle.Items[i]).Vehiculo_Id == vehicleId.Value)
                {
                    comboVehicle.SelectedIndex = i;
                    return;
                }
            }
        }

        void PerformClearFilters()
        {
            BitacoraCambiosFilter defaults = initialVehicleId.HasValue
                ? BitacoraCambiosFilter.ForVehicle(initialVehicleId.Value)
                : BitacoraCambiosFilter.LastMonth();
            dtFrom.Value = defaults.From;
            dtTo.Value = defaults.To;
            PerformSelectVehicle(defaults.Vehiculo_Id);
            comboOperation.SelectedIndex = 0;
        }

        void PerformUpdateChangesView()
        {
            BitacoraCambiosFilter filter = new BitacoraCambiosFilter();
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            if (comboVehicle.SelectedIndex > 0)
                filter.Vehiculo_Id = ((Vehiculo)comboVehicle.SelectedItem).Vehiculo_Id;
            if (comboOperation.SelectedIndex > 0)
                filter.Operacion = (BitacoraCambiosOperation)comboOperation.SelectedItem;

            PerformShowChanges(_changeLogBLL.Query(filter));
        }

        void PerformShowChanges(List<BitacoraCambio> list)
        {
            changes = list;
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = changes;
            PerformUpdateCount();
            btnPrint.Enabled = changes.Count > 0;
        }

        void PerformUpdateCount()
        {
            lblCount.Text = string.Format(LanguageService.GetTagText("ChangeLogCount"), changes.Count);
        }

        // Precio y kilometraje se guardan con formato invariante; se muestran con el de la máquina.
        static string PerformFormatValue(string field, string value)
        {
            // Altas y ventas no tienen valores. Con FormattingApplied = true la grilla exige un string:
            // un null tira FormatException en cada repintado.
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            decimal number;
            if ((field == "Precio" || field == "Kilometraje")
                && decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out number))
                return number.ToString(field == "Precio" ? "N2" : "N0", CultureInfo.CurrentCulture);
            // El trigger guarda la descripcion del color tal como esta en la base, en ingles.
            if (field == "Color")
                return LookupText.Colour(value);
            return value;
        }
        #endregion

        #region Form Events
        private void BitacoraCambiosForm_Shown(object sender, EventArgs e)
        {
            try
            {
                PerformFillVehiclesCombo();
                PerformClearFilters();
                PerformUpdateChangesView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void BitacoraCambiosForm_Disposed(object sender, EventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdateChangesView();
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
                PerformFillVehiclesCombo();
                PerformClearFilters();
                PerformUpdateChangesView();
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

        private void comboFilters_Format(object sender, ListControlConvertEventArgs e)
        {
            Vehiculo vehicle = e.ListItem as Vehiculo;
            if (vehicle != null)
                e.Value = vehicle.Patente + " - " + vehicle.Marca_Descripcion + " " + vehicle.Modelo_Descripcion + " "
                    + vehicle.Version_Descripcion + " " + vehicle.Anio;
            else if (e.ListItem is BitacoraCambiosOperation)
                e.Value = LanguageService.GetTagText("ChangeLogOperation_" + e.ListItem);
            else if (string.Equals(e.ListItem, ""))
                e.Value = LanguageService.GetTagText("AuditAll");
        }

        private void metroGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= changes.Count)
                return;
            BitacoraCambio change = changes[e.RowIndex];
            if (e.ColumnIndex == colOperation.Index)
            {
                e.Value = LanguageService.GetTagText("ChangeLogOperation_" + change.Operacion);
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colField.Index && change.Campo != null)
            {
                e.Value = LanguageService.GetTagText("ChangeLogField_" + change.Campo, change.Campo);
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colOldValue.Index || e.ColumnIndex == colNewValue.Index)
            {
                e.Value = PerformFormatValue(change.Campo, e.ColumnIndex == colOldValue.Index ? change.ValorAnterior : change.ValorNuevo);
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colUser.Index && !change.Usuario_Id.HasValue)
            {
                // Sin usuario: carga inicial o un cambio hecho directamente en la base.
                e.Value = LanguageService.GetTagText("ChangeLogNoUser");
                e.FormattingApplied = true;
            }
        }
        #endregion

        #region Printing
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                BitacoraCambiosBLL.RequireAccess();
                if (changes.Count == 0)
                    return;
                printDocument1.DocumentName = this.Text;
                if (printDialog1.ShowDialog(this) != DialogResult.OK)
                    return;
                printDocument1.Print();
            }
            catch (TranslatableException ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageService.GetTagText("ChangeLogPrintFailed"));
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
