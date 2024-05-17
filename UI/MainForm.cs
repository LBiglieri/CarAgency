using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarAgency.Utilities;
using CarAgency.Utilities.Session;

namespace CarAgency.UI
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Login();
        }

#region  Form UI Changes
        private void UpdateTitle()
        {
            if (SessionHandler.Logged())
                lblTitle.Text = "Welcome " + SessionHandler.GetUsername();
            else
                lblTitle.Text = "CarAgency";
            lblTitle.Refresh();
        }

#endregion

#region  Form Behavior 
        private void Login()
        {
            if (!SessionHandler.Logged())
            {
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (SessionHandler.Logged())
                    UpdateTitle();
            }
            else
            {
                MessageBox.Show("You are already logged in.");
            }
        }
        private void Logout()
        {
            if (SessionHandler.Logged())
            {
                SessionHandler.Logout();
                if (!SessionHandler.Logged())
                    UpdateTitle();
            }
            else
            {
                MessageBox.Show("You are not logged in.");
            }
        }
        #endregion

#region  Form Events 
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void permissionConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PermissionManagementForm frm = new PermissionManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion

        private void userPermissionConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserPermissionManagementForm frm = new UserPermissionManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagementForm frm = new UserManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
