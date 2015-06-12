using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            string userName = UserNameTextBox.Text;
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Необходимо указать имя пользователя!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MoneyBook.Core.User.Exists(Program.ProfileBasePath, UserNameTextBox.Text))
                {
                    MessageBox.Show("Пользователь " + userName + " уже существует. \r\nВведите другое имя пользователя.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string password = string.IsNullOrEmpty(PasswordTextBox.Text) ? null : PasswordTextBox.Text;
                    MoneyBook.Core.User.Create(Core.ApplicationType.Desktop, Program.ProfileBasePath, userName, password);


                    Login loginForm = this.Owner as Login;
                    if (loginForm != null)
                    {
                        loginForm.UserNameComboBox.DataSource = Core.User.GetUsers(Program.ProfileBasePath);
                    }
                    this.Close();
                }
            }
        }

        private void CancellButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\0' : '*';
        }
    }
}
