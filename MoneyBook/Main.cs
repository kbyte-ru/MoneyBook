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
    /// Последяя нормальная ширина формы, которую можно запомнить.
    /// </summary>
    private int LastWidth = 0;

    /// <summary>
    /// Последяя нормальная высота формы, которую можно запомнить.
    /// </summary>
    private int LastHeight = 0;

    /// <summary>
    /// Последие координаты расположения формы по X, которые можно запомнить.
    /// </summary>
    private int LastLeft = 0;

    /// <summary>
    /// Последие координаты расположения формы по Y, которые можно запомнить.
    /// </summary>
    private int LastTop = 0;

    public Main(string username, string password)
    {
      Program.CurrentUser = new User(Program.ProfileBasePath, username, password);
      
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {      
      this.Expenses.User = Program.CurrentUser;
      this.Incomes.User = Program.CurrentUser;
      // параметры окна
      var screen = Screen.FromPoint(this.Location);

      // порядок имеет значение!!!
      // размер
      if 
      (
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowWidth]) >= 0 && 
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowWidth]) <= screen.WorkingArea.Width && 
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowHeight]) >= 0 &&
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowHeight]) <= screen.WorkingArea.Height
      )
      {
        this.Width = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowWidth]);
        this.Height = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowHeight]);
      }
      // позиция
      if
      (
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowLeft]) >= 0 &&
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowLeft]) <= (screen.WorkingArea.Width - this.Width) &&
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowTop]) >= 0 &&
        Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowTop]) <= (screen.WorkingArea.Height - this.Height)
      )
      {
        this.Left = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowLeft]);
        this.Top = Convertion.ToInt32(Program.CurrentUser.Info[InfoId.Settings.Desktop.WindowTop]);
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
      if (Convert.ToInt32(Program.CurrentUser.Info[Program.InfoIdCustomDetailsSize]) > 0)
      {
        this.Expenses.DetailsSize = this.Incomes.DetailsSize = Convert.ToInt32(Program.CurrentUser.Info[Program.InfoIdCustomDetailsSize]);
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
      Program.CurrentUser.Info.Set(Program.InfoIdCustomDetailsSize, this.Incomes.DetailsSize, false);
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

  }

}