using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;
using MetroFramework.Controls;
using Security.Session;
using UI.Comun;

namespace CarAgency.UI
{
    public enum UsuarioManagementFormAction
    {
        NoAction,
        Add,
        Update,
        Delete,
        Unblock
    }
    public partial class UsuarioManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        UsuarioBLL _userBLL;
        List<Usuario> users = new List<Usuario>();
        Usuario selected_user;
        PermisoBLL _permissionBLL;

        UsuarioManagementFormAction Form_Action;

        public UsuarioManagementForm()
        {
            InitializeComponent();
            _userBLL = new UsuarioBLL();
            _permissionBLL = new PermisoBLL();

            PerformFillFamiliesCombo();
            btnCancelOpp_Click(null, null);
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void UserManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }

        #region  Perform
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("UsuarioManagementForm");
            this.Refresh();
            tbDniUser.WaterMark = LanguageService.GetTagText(tbDniUser.Tag.ToString());
            tbUsername.WaterMark = LanguageService.GetTagText(tbUsername.Tag.ToString());
            tbNameU.WaterMark = LanguageService.GetTagText(tbNameU.Tag.ToString());
            tbSurnameU.WaterMark = LanguageService.GetTagText(tbSurnameU.Tag.ToString());
            lblActiveUSers.Text = LanguageService.GetTagText(lblActiveUSers.Tag.ToString());
            btnAddUser.Text = LanguageService.GetTagText(btnAddUser.Tag.ToString());
            btnUpdateUser.Text = LanguageService.GetTagText(btnUpdateUser.Tag.ToString());
            btnDeleteUser.Text = LanguageService.GetTagText(btnDeleteUser.Tag.ToString());
            btnUnblockUser.Text = LanguageService.GetTagText(btnUnblockUser.Tag.ToString());
            btnApplyOpp.Text = LanguageService.GetTagText(btnApplyOpp.Tag.ToString());
            btnCancelOpp.Text = LanguageService.GetTagText(btnCancelOpp.Tag.ToString());
            chkBlocked.Text = LanguageService.GetTagText(chkBlocked.Tag.ToString());
            chkActive.Text = LanguageService.GetTagText(chkActive.Tag.ToString());
            GridColumns.Translate(metroGrid1);
        }
        void PerformUpdateUsersView()
        {
            try
            {
                users = _userBLL.GetAllByState(toggleActivos.Checked);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = users;
                GridColumns.Show(metroGrid1, "NombreUsuario", "Nombre", "Apellido", "DNI", "Activo", "Bloqueado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        void PerformFillFamiliesCombo()
        {
            try
            {
                this.comboFamily.DataSource = _permissionBLL.GetAllFamilies();
                this.comboFamily.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        void PerformEnableDisableTextBoxes(Boolean flag)
        {
            tbDniUser.Enabled = flag;
            tbUsername.Enabled = flag;
            tbNameU.Enabled = flag;
            tbSurnameU.Enabled = flag;
            comboFamily.Enabled = flag;
        }
        void PerformClearTextBoxData()
        {
            tbDniUser.Text = "";
            tbUsername.Text = "";
            tbNameU.Text = "";
            tbSurnameU.Text = "";
            comboFamily.SelectedIndex = -1;
            chkBlocked.Checked = false;
            chkActive.Checked = false;
        }
        void PerformPopulateTextBoxData()
        {
            tbDniUser.Text = selected_user.DNI.ToString();
            tbUsername.Text = selected_user.NombreUsuario;
            tbNameU.Text = selected_user.Nombre;
            tbSurnameU.Text = selected_user.Apellido;
            foreach (Familia family in comboFamily.Items)
            {
                if (family.Permiso_Id.Equals(selected_user.Rol_Id))
                {
                    comboFamily.SelectedIndex = comboFamily.FindStringExact(family.Nombre); 
                }
            }
            chkBlocked.Checked = selected_user.Bloqueado;
            chkActive.Checked = selected_user.Activo;
        }
        void PerformCleanFormAction()
        {
            selected_user = null;
            metroGrid1.Enabled = false;
            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = UsuarioManagementFormAction.NoAction;
            PerformUpdateUsersView();
        }
        Boolean PerformValidateTextBoxData()
        {
            if (tbDniUser.Text == "" || tbUsername.Text == "" || tbNameU.Text == "" || tbSurnameU.Text == "" || comboFamily.SelectedIndex== -1)
            {
                MessageBox.Show(LanguageService.GetTagText("FillAllFieldsUser"));
                return false;
            }
            if (!int.TryParse(tbDniUser.Text, out _))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseOnlyDigitsUser"));
                return false;
            }

            return true;
        }

        private void MapTextboxesToUser()
        {
            selected_user.DNI = int.Parse(tbDniUser.Text);
            selected_user.NombreUsuario = tbUsername.Text;
            selected_user.Nombre = tbNameU.Text;
            selected_user.Apellido = tbSurnameU.Text;
            selected_user.Rol_Id = ((Familia)comboFamily.SelectedItem).Permiso_Id;
        }

        private void ToggleActionButtonsStates(bool state)
        {
            btnAddUser.Enabled = state;
            btnUpdateUser.Enabled = state;
            btnDeleteUser.Enabled = state;
            btnUnblockUser.Enabled = state;
            btnApplyOpp.Enabled = !state;
            btnCancelOpp.Enabled = !state;
        }
        #endregion

        #region  Form Events 
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = false;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UsuarioManagementFormAction.Add;
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UsuarioManagementFormAction.Update;

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UsuarioManagementFormAction.Delete;
        }
        private void btnUnblockUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UsuarioManagementFormAction.Unblock;
        }

        private void toggleActivos_CheckedChanged(object sender, EventArgs e)
        {
            PerformUpdateUsersView();
        }

        private void btnApplyOpp_Click(object sender, EventArgs e)
        {
            var result = default(SQLUpdateResult);
            if (Form_Action != UsuarioManagementFormAction.Add && selected_user == null)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectUser"));
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
                return;
            }
            try
            {
                switch (Form_Action)
                {
                    case UsuarioManagementFormAction.Add:
                        if (PerformValidateTextBoxData())
                        {
                            selected_user = new Usuario();
                            MapTextboxesToUser();
                            result = _userBLL.AddUser(selected_user);
                        }
                        break;
                    case UsuarioManagementFormAction.Update:
                        if (PerformValidateTextBoxData())
                        {
                            MapTextboxesToUser(); 
                            if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUpdate") + selected_user.NombreUsuario + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                result = _userBLL.UpdateUser(selected_user);
                            }
                        }
                        break;
                    case UsuarioManagementFormAction.Delete:
                        if (MessageBox.Show(LanguageService.GetTagText("AreYouSureDelete") + selected_user.NombreUsuario + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _userBLL.DeleteUser(selected_user);
                        }
                        break;
                    case UsuarioManagementFormAction.Unblock:
                        if (!selected_user.Bloqueado)
                            MessageBox.Show(LanguageService.GetTagText("IsNotBlocked"));
                        else if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUnblock") + selected_user.NombreUsuario + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _userBLL.AlterBlockedState(selected_user,false);
                        }
                        break;
                }

                if (result != null)
                {
                    MessageBox.Show(LanguageService.GetTagText("OperationSuccesfull"));
                }
                PerformCleanFormAction();
            }
            catch (Exception ee)
            {
                MessageBox.Show(LanguageService.GetErrorText(ee));
            }
            finally
            {
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
            }
        }

        private void btnCancelOpp_Click(object sender, EventArgs e)
        {
            PerformCleanFormAction();
            ToggleActionButtonsStates(true);
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (Form_Action == UsuarioManagementFormAction.Update || Form_Action == UsuarioManagementFormAction.Delete || Form_Action == UsuarioManagementFormAction.Unblock) 
            {
                selected_user = (Usuario)metroGrid1.CurrentRow.DataBoundItem;
                PerformPopulateTextBoxData();
            }
        }
        #endregion

    }
}
