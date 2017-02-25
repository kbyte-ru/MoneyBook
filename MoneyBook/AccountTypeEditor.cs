using MoneyBook.Core;
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
    public partial class AccountTypeEditor : MForm
    {
        public AccountTypeEditor(User user) : base(user)
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            var accountType = new AccountType();
            accountType.Name = AccountTypeName.Text;
            ///TODO
            ///добавить функционал добавления ид картинки
            ///accountType.IconId            
            this.User.Save(accountType);
            this.Close();
        }

        private void AccountTypeName_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 0)
                AcceptButton.Enabled = true;
            else
                AcceptButton.Enabled = false;
        }       
    }
}