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
      this.Incomes.ShowDetails = Convertion.ToBoolean(Program.CurrentUser.Info[Program.InfoIdCustomShowDetails]);
      if (Convert.ToInt32(Program.CurrentUser.Info[Program.InfoIdCustomDetailsSize]) > 0)
      {
        this.Incomes.DetailsSize = Convert.ToInt32(Program.CurrentUser.Info[Program.InfoIdCustomDetailsSize]);
      }
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
      this.Incomes.SaveSettings();
      this.Expenses.SaveSettings();

      Program.CurrentUser.Info.Set(Program.InfoIdCustomShowDetails, this.Incomes.ShowDetails, false);
      Program.CurrentUser.Info.Set(Program.InfoIdCustomDetailsSize, this.Incomes.DetailsSize, false);

      Application.Exit();
    }

    private void MoneyHistory_DetailsVisibleChanged(object sender, EventArgs e)
    {
      var showDetails = ((MoneyHistory)sender).ShowDetails;
      if (sender == Incomes)
      {
        this.Expenses.ShowDetails = showDetails;
      }
      else if (sender == Expenses)
      {
        this.Incomes.ShowDetails = showDetails;
      }
      else
      {
        MessageBox.Show("Не может этого быть!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void MoneyHistory_DetailsSizeChanged(object sender, EventArgs e)
    {
      var detailsSize = ((MoneyHistory)sender).DetailsSize;
      if (sender == Incomes)
      {
        this.Expenses.DetailsSize = detailsSize;
      }
      else if (sender == Expenses)
      {
        this.Incomes.DetailsSize = detailsSize;
      }
      else
      {
        MessageBox.Show("Не может этого быть!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

  }

}