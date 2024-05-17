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
using CarAgency.Entities;
using CarAgency.Utilities.Permissions;

namespace CarAgency.UI
{
    public partial class UserManagementForm : MetroFramework.Forms.MetroForm
    {
        UserBLL _userBLL;
        List<User> users = new List<User>();
        public UserManagementForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();

            PerformUpdateUsersView();
        }

        #region  Perform
        void PerformUpdateUsersView()
        {
            users = _userBLL.GetAllByState(toggleActivos.Checked);
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = users;

            metroGrid1.Columns["Password"].Visible = false;
            metroGrid1.Columns["Permissions"].Visible = false;
            metroGrid1.Columns["Id"].Visible = false;
        }
        #endregion

        #region  Form Events 
        private void btnConfigureUser_Click(object sender, EventArgs e)
        {

        }

        private void toggleActivos_CheckedChanged(object sender, EventArgs e)
        {
            PerformUpdateUsersView();
        }
        #endregion

    }
}
