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

  public partial class Main : Form
  {

    public Main(string username, string password)
    {
      Program.CurrentUser = new User(Program.ProfileBasePath, username, password);
      
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
    }

    private void Main_Shown(object sender, EventArgs e)
    {
      this.Expenses.User = Program.CurrentUser;
      this.Incomes.User = Program.CurrentUser;
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabControl1.SelectedTab.Name.Equals("tabInfo"))
      {
        dgvInfo.DataSource = Program.CurrentUser.Info.GetAllInfo();
      }
    }

    private void Main_FormClosed(object sender, FormClosedEventArgs e)
    {
      Incomes.SaveSettings();
      Expenses.SaveSettings();
      Application.Exit();
    }

  }

}