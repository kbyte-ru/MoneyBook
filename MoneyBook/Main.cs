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

    /// <summary>
    /// Последнее состояние формы, которое можно запомнить.
    /// </summary>
    private FormWindowState LastWindowState = FormWindowState.Normal;

    /// <summary>
    /// Последняя нормальная ширина формы, которую можно запомнить.
    /// </summary>
    private int LastWidth = 0;

    /// <summary>
    /// Последняя нормальная высота формы, которую можно запомнить.
    /// </summary>
    private int LastHeight = 0;

    /// <summary>
    /// Последние координаты расположения формы по X, которые можно запомнить.
    /// </summary>
    private int LastLeft = 0;

    /// <summary>
    /// Последние координаты расположения формы по Y, которые можно запомнить.
    /// </summary>
    private int LastTop = 0;

    public Main(string username, string password)
    {
      Program.CurrentUser = new User(Program.ProfileBasePath, username, password);

      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      this.Text = String.Format("{0} - The Money Book v{1}", Program.CurrentUser.UserName, Application.ProductVersion);
      this.Expenses.User = Program.CurrentUser;
      this.Incomes.User = Program.CurrentUser;
      this.ReloadAccounts();

      // параметры окна
      var screen = Screen.FromPoint(this.Location);

      // порядок имеет значение!!!
      // размер

      int windowsWidth = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowWidth]);
      int windowHeight = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowHeight]);
      if
      (
        windowsWidth > 0 &&
        windowsWidth <= screen.WorkingArea.Width &&
        windowHeight > 0 &&
        windowHeight <= screen.WorkingArea.Height
      )
      {
        this.Width = windowsWidth;
        this.Height = windowHeight;
      }
      // позиция
      int windowLeft = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowLeft]);
      int windowTop = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowTop]);
      if
      (
        windowLeft > 0 &&
        windowLeft <= (screen.WorkingArea.Width - this.Width) &&
        windowTop > 0 &&
        windowTop <= (screen.WorkingArea.Height - this.Height)
      )
      {
        this.Left = windowLeft;
        this.Top = windowTop;
      }
      // состояние
      if (Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowState]) != (int)FormWindowState.Minimized)
      {
        var windowState = FormWindowState.Normal;
        if (!Enum.TryParse<FormWindowState>(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowState], out windowState))
        {
          windowState = FormWindowState.Normal;
        }
        this.WindowState = windowState;
      }
      // панель комментария
      this.Incomes.ShowDetails = Convertion.ToBoolean(Program.CurrentUser.Info[Program.InfoIdCustomShowDetails]);
      int customDetails = 0;
      if (Int32.TryParse(Program.CurrentUser.Info[Program.InfoIdCustomDetailsSize], out customDetails) && customDetails > 0)
      {
        this.Expenses.DetailsSize = this.Incomes.DetailsSize = customDetails;
      }
    }

    private void Main_Shown(object sender, EventArgs e)
    {
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
      // фиксируем параметры расходов/доходов
      this.Incomes.SaveSettings();
      this.Expenses.SaveSettings();

      // обновляем информацию о размере окна
      Main_SizeChanged(sender, null);

      // фиксируем общие параметры и параметры окна
      Program.CurrentUser.Info.Set(Program.InfoIdCustomShowDetails, this.Incomes.ShowDetails, false);
      if (this.Incomes.ShowDetails)
      {
        Program.CurrentUser.Info.Set(Program.InfoIdCustomDetailsSize, this.Incomes.DetailsSize, false);
      }

      Program.CurrentUser.Info.Set(InfoId.Settings.Desktop.WindowState, (int)this.LastWindowState, false);
      Program.CurrentUser.Info.Set(InfoId.Settings.Desktop.WindowWidth, this.LastWidth, false);
      Program.CurrentUser.Info.Set(InfoId.Settings.Desktop.WindowHeight, this.LastHeight, false);
      Program.CurrentUser.Info.Set(InfoId.Settings.Desktop.WindowLeft, this.LastLeft, false);
      Program.CurrentUser.Info.Set(InfoId.Settings.Desktop.WindowTop, this.LastTop, false);

      // выходим из приложения
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

    private void Main_SizeChanged(object sender, EventArgs e)
    {
      if (this.WindowState != FormWindowState.Minimized)
      {
        this.LastWindowState = this.WindowState;
        if (this.WindowState != FormWindowState.Maximized)
        {
          this.LastWidth = this.Width;
          this.LastHeight = this.Height;
          this.LastLeft = this.Left;
          this.LastTop = this.Top;
        }
      }
    }

    private void btnAccountTypeNew_Click(object sender, EventArgs e)
    {
      var accountType = new AccountTypeEditor();
      accountType.ShowDialog();
    }

    private void btnAccountTypeAdd_Click(object sender, EventArgs e)
    {
      var accountTypeAdd = new AccountTypeEditor();
      accountTypeAdd.ShowDialog();
    }

    private void ReloadAccounts()
    {
      this.Accounts.SuspendLayout();

      this.Accounts.Enabled = false;

      this.Accounts.Rows.Clear();

      foreach (var account in Program.CurrentUser.Accounts.Values)
      {
        var row = new DataGridViewRow();

        var iconCell = new DataGridViewImageCell();


        if (account.IconId > 0)
        {
          iconCell.Value = Program.CurrentUser.GetIcon(account.IconId);
        }
        else
        {
          iconCell.Value = Properties.Resources.item;
        }

        row.Cells.Add(iconCell);
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.Name });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = Program.CurrentUser.AccountTypes[account.AccountTypeId].Name });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.CurrencyCode });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = 0 }); // TODO: либо добавить счетчик (скорее всего), либо считать в режиме реального времени
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.TotalExpenseEntries });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.TotalIncomeEntries });

        row.Tag = account;

        this.Accounts.Rows.Add(row);
      }

      this.Accounts.ResumeLayout();
    }

  }

}