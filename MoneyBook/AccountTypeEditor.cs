using MoneyBook.Core;
using System;
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
      accountType.IconId = Convertion.ToInt32(this.picIcon.Tag);

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

    private void btnSelectIcon_Click(object sender, EventArgs e)
    {
      var f = new IconsViewer(this.User);
      f.Owner = this;

      if (f.ShowDialog() == DialogResult.OK)
      {
        picIcon.Image = this.User.GetIcon(f.SelectedIconId);
        picIcon.Tag = f.SelectedIconId;
      }
    }

  }

}