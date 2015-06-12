using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            UserNameComboBox.DataSource = Core.User.GetUsers(Program.ProfileBasePath);
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\0' : '*';
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
          if (string.IsNullOrEmpty(UserNameComboBox.Text))
          {
            MessageBox.Show("Выбирите имя пользователя!\r\n\r\nЧтобы создать нового пользователя, нажмите кнопку «Создать»", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
          }
          else
          {
            // Загружаем пользователя
            try
            {
              var f = new Main(this.UserNameComboBox.Text, this.PasswordTextBox.Text);
              f.Show();
              this.Close();
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Form registration = new Registration();
            registration.Owner = this;
            registration.ShowDialog();
        }
    }
}
