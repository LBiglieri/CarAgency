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
using CarAgency.Utilities.Session;

namespace CarAgency.UI
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {
        UserBLL _userBLL;
        public LoginForm()
        {
            _userBLL = new UserBLL();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                _userBLL.Login(this.tbUser.Text, this.tbPassword.Text);
                MessageBox.Show("Login successful!");
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
#region  Form Events 
        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbPassword.Select();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Select();
                btnLogin.PerformClick();
            }
        }

        #endregion

    }
}
