using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyBook.Core;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;

namespace MoneyBook.WinApp
{

  public partial class MoneyHistory : UserControl
  {

    #region ..поля и свойства, поля и свойства..

    /// <summary>
    /// Менеджер задач.
    /// </summary>
    private TaskManager TaskManager = new TaskManager();

    /// <summary>
    /// Необходимо отмена текущего процесса.
    /// </summary>
    private bool Cancellation = false;

    private Regex PeriodParser = new Regex(@"^(?<interval>(d|w|m|q|y|all))(?<value>[0-9\-]*)$", RegexOptions.IgnoreCase);

    private User _User = null;

    /// <summary>
    /// Текущий пользователь.
    /// </summary>
    public User User
    {
      get
      {
        return _User;
      }
      set
      {
        var reloadNeed = (_User != value);
        _User = value;
        if (reloadNeed)
        {
          this.Init();
        }
      }
    }

    private EntryType _ItemsType = EntryType.None;

    /// <summary>
    /// Типы записей.
    /// </summary>
    public EntryType ItemsType
    {
      get
      {
        return _ItemsType;
      }
      set
      {
        var reloadNeed = (_ItemsType != value);
        _ItemsType = value;
        if (reloadNeed)
        {
          this.Init();
        }
      }
    }

    /// <summary>
    /// Общая сумма в текущем списке.
    /// </summary>
    protected Dictionary<string, decimal> TotalAmountByCurrencies { get; set; }

    #endregion
    #region ..конструктор..

    public MoneyHistory()
    {
      InitializeComponent();

      ToolStrip2.Items.Insert(1, new ToolStripControlHost(this.DateFrom));
      ToolStrip2.Items.Insert(3, new ToolStripControlHost(this.DateTo));

      this.Accounts.ComboBox.ValueMember = "Id";
      this.Accounts.ComboBox.DisplayMember = "Name";

      this.Categories.ComboBox.ValueMember = "Id";
      this.Categories.ComboBox.DisplayMember = "Name";

      this.Subcategories.ComboBox.ValueMember = "Id";
      this.Subcategories.ComboBox.DisplayMember = "Name";

      this.TotalAmountByCurrencies = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

      this.ProgressBar1.CancelCallback = () =>
      {
        this.Cancellation = true;
      };
    }
    
    #endregion
    #region ..обработчики..

    private void MoneyHistory_Load(object sender, EventArgs e)
    {
      // список месяцев до текущего
      var baseDate = new DateTime(DateTime.Now.Year, 1, 1);
      for (int i = 1; i <= DateTime.Now.Month; i++)
      {
        // добавляем
        this.mnuPeriodMonth.DropDownItems.Add
        (
          new ToolStripMenuItem(baseDate.AddMonths(i - 1).ToString("MMMM")) 
          { 
            Tag = String.Format("m-{0}", DateTime.Now.Month - i) 
          }
        );
        // обработчик клика
        this.mnuPeriodMonth.DropDownItems[this.mnuPeriodMonth.DropDownItems.Count - 1].Click += Period_Click;
      }

      // восстанавливаем ранее выбранные параметры
      //this.LoadSettings();

      // загружаем данные
      //this.ReloadItems();
    }
    
    private void btnAdd_Click(object sender, EventArgs e)
    {

    }

    private void btnReport_Click(object sender, EventArgs e)
    {

    }

    private void btnEdit_Click(object sender, EventArgs e)
    {

    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      // такого быть не должно, но лучше перестраховаться
      if (DataGridView1.CurrentRow == null || DataGridView1.CurrentRow.Tag == null)
      {
        this.UpdateButtons();
        return;
      }

      var item = (MoneyItem)DataGridView1.CurrentRow.Tag;
      var currencyCode = this.User.Accounts[item.AccountId].CurrencyCode;

      // запрос на удаление
      if (MessageBox.Show(String.Format("Вы действительно хотите удалить запись «{0}» от {3} на сумму {1:##,###,##0.00} {2}?\r\n\r\nВосстановить данные после удаления будет невозможно.\r\n\r\nНажмите «Да», чтобы удалить запись.", item.Title, item.Amount, currencyCode, item.DateEntry.ToShortDateString()), "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      {
        return;
      }

      // удаляем из базы
      this.User.Delete(item);

      // удаляем из списка
      DataGridView1.Rows.Remove(DataGridView1.CurrentRow);
      this.TotalAmountByCurrencies[currencyCode] -= item.Amount;
      this.UpdateStatus();
    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
      // загружаем данные
      TaskManager.Add(this.ReloadItems);
      TaskManager.ExecuteAll();

      // запоминаем выбранные параметры
      // this.SaveSettings();
    }

    private void Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selectedCategoryId = 0;

      if (this.Subcategories.SelectedItem != null)
      {
        selectedCategoryId = ((Category)this.Subcategories.SelectedItem).Id;
      }

      this.Subcategories.Items.Clear();
      this.Subcategories.Items.Add(new Category { Id = 0, Name = "<Все>" });
      this.Subcategories.SelectedIndex = 0;

      var u = this.User;

      if (u == null) { return; }

      IEnumerable<Category> list = null;
      var categoryId = ((Category)this.Categories.SelectedItem).Id;
      if (categoryId > 0)
      {
        // если выбрана статья, то показываем только подкатегории этой статьи
        list = u.Categories.Values.Where(c => c.ParentId == categoryId && c.CategoryType == this.ItemsType);
      }
      else
      {
        // в противном случае, все подкатегории
        list = u.Categories.Values.Where(c => c.ParentId > 0 && c.CategoryType == this.ItemsType);
      }

      foreach (var item in list)
      {
        this.Subcategories.Items.Add(item);

        if (selectedCategoryId == item.Id)
        {
          this.Subcategories.SelectedIndex = this.Subcategories.Items.Count - 1;
        }
      }
    }

    private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      btnEdit_Click(sender, null);
    }

    private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      this.UpdateButtons();
      //btnEdit.Enabled = btnDelete.Enabled = 
      //mnuEdit.Enabled = mnuDelete.Enabled =
      //(e.RowIndex >= 0 && DataGridView1.Rows[e.RowIndex].Tag != null);
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      this.UpdateButtons();
    }

    private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
    {
      /*if (DataGridView1.CurrentRow != null && DataGridView1.CurrentRow.Tag != null)
      {
        if (e.KeyCode == Keys.Delete)
        {
          btnDelete_Click(sender, null);
        }
        else if (e.KeyCode == Keys.F2)
        {
          btnEdit_Click(sender, null);
        }
      }*/

      if (e.KeyCode == Keys.F5)
      {
        TaskManager.Add(this.ReloadItems);
        TaskManager.ExecuteAll();
      }
    }

    private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == System.Windows.Forms.MouseButtons.Right)
      {
        var hit = DataGridView1.HitTest(e.X, e.Y);
        if (hit.RowIndex != -1)
        {
          DataGridView1.ClearSelection();
          DataGridView1.Rows[hit.RowIndex].Selected = true;
          DataGridView1.CurrentCell = DataGridView1.Rows[hit.RowIndex].Cells[0];
        }
      }
    }

    private bool AmountKeyIsClipboard = false;
    private bool AmountIsKeyPress = false;

    private void Amount_KeyDown(object sender, KeyEventArgs e)
    {
      //Console.WriteLine("Amount_KeyDown");

      if (e.Control && (e.KeyData.HasFlag(Keys.C) || e.KeyData.HasFlag(Keys.X) || e.KeyData.HasFlag(Keys.V)))
      {
        this.AmountKeyIsClipboard = true;
        return;
      }

      this.AmountKeyIsClipboard = false;

      // началась обработка нажатий
      this.AmountIsKeyPress = true;
    }

    private void Amount_KeyPress(object sender, KeyPressEventArgs e)
    {
      //Console.WriteLine("Amount_KeyPress");

      // проверяем, может проверять не нужно
      if (this.AmountKeyIsClipboard)
      {
        return;
      }

      // если не backspace, разделитель или число, то
      if (!(e.KeyChar == '\b' || e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-') && (e.KeyChar < 48 || e.KeyChar > 57))
      {
        // кина не будет
        e.Handled = true;
        return;
      }

      var textBox = (ToolStripTextBox)sender;
      // минус может быть только в начале строки и только один
      if (e.KeyChar == '-' && textBox.SelectionStart != 0 && textBox.Text.IndexOf("-") == -1)
      {
        e.Handled = true;
        return;
      }

      // проверяем, получится в итоге число или нет
      if (e.KeyChar != '\b')
      {
        var value = Convertion.ToDecimal(textBox.Text + e.KeyChar, null);

        if (!value.HasValue)
        {
          e.Handled = true;
          return;
        }
      }
    }

    private void Amount_KeyUp(object sender, KeyEventArgs e)
    {
      // обработка клавы завершена
      this.AmountIsKeyPress = false;
      //Console.WriteLine("Amount_KeyUp");
    }

    private void Amount_TextChanged(object sender, EventArgs e)
    {
      //Console.WriteLine("Amount_TextChanged");

      if (this.AmountIsKeyPress && !this.AmountKeyIsClipboard)
      {
        //Console.WriteLine("false");
        return;
      }

      //Console.WriteLine("true");

      // защита от вставок неправильных данных из буфера обмана
      var textBox = ((ToolStripTextBox)sender);
      var value = Convertion.ToDecimal(textBox.Text, null);
      if (!value.HasValue)
      {
        textBox.Text = "";
      }
      else
      {
        textBox.TextChanged -= this.Amount_TextChanged;
        textBox.Text = value.Value.ToString();
        textBox.TextChanged += this.Amount_TextChanged;
      }
    }

    private void Amount_Leave(object sender, EventArgs e)
    {
      var textBox = ((ToolStripTextBox)sender);
      if (String.IsNullOrWhiteSpace(textBox.Text)) { return; }

      var value = Convertion.ToDecimal(textBox.Text, null);
      if (!value.HasValue)
      {
        textBox.Text = "";
      }
      else
      {
        textBox.TextChanged -= this.Amount_TextChanged;
        textBox.Text = value.Value.ToString();
        textBox.TextChanged += this.Amount_TextChanged;
      }
    }

    private void Period_Click(object sender, EventArgs e)
    {
      var item = (ToolStripMenuItem)sender;
      var period = this.PeriodParser.Match(item.Tag.ToString());
      var value = Convertion.ToInt32(period.Groups["value"].Value, 0);

      DateTime d = DateTime.Now;

      switch (period.Groups["interval"].Value.ToLower())
      {
        case "d": // день
          this.DateFrom.Value = this.DateTo.Value = DateTime.Now.AddDays(value);
          break;

        case "w": // неделя
          // телепортируемся в нужную неделю
          d = DateTime.Now.AddDays(value * 7);
          // определяем первый день недели
          DateTime firstDay = d.AddDays(-((d.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7));
          // устанавливаем фильтры
          this.DateFrom.Value = firstDay;
          this.DateTo.Value = firstDay.AddDays(6);
          break;

        case "m": // месяц
          d = DateTime.Now.AddMonths(value);
          this.DateFrom.Value = new DateTime(d.Year, d.Month, 1);
          this.DateTo.Value = this.DateFrom.Value.AddMonths(1).AddDays(-1);
          break;

        case "q": // квартал
          // текущий квартал
          int quarter = (DateTime.Now.Month - 1) / 3 + 1;  
          // перый день текущего квартала
          d = new DateTime(DateTime.Now.Year, (quarter - 1) * 3 + 1, 1);
          // смещаемся на нужное число кварталов
          d = d.AddMonths(value * 3);
          // квартал новой даты
          quarter = (d.Month - 1) / 3 + 1;
          // передаем в фильтры
          this.DateFrom.Value = new DateTime(d.Year, (quarter - 1) * 3 + 1, 1);
          this.DateTo.Value = this.DateFrom.Value.AddMonths(3).AddDays(-1);
          break;

        case "y": // год
          d = DateTime.Now.AddYears(value);
          this.DateFrom.Value = new DateTime(d.Year, 1, 1);
          this.DateTo.Value = this.DateFrom.Value.AddYears(1).AddDays(-1); // мало ли чего :)
          break;

        case "all":
          this.DateFrom.Value = new DateTime(1900, 1, 1);
          this.DateTo.Value = DateTime.Now;
          break;
      }

      // todo: придумать и написать внятный комментарий к этому блоку кода
      // здесь все работает так, как задумано :)
      if (value == 0)
      {
        foreach (var m in ddbPeriod.DropDownItems)
        {
          if (m.GetType() == typeof(ToolStripMenuItem))
          {
            ((ToolStripMenuItem)m).Checked = false;
          }
        }
        item.Checked = true;
        ddbPeriod.Tag = item.Tag.ToString();
      }
    }

    private void MoneyHistory_Resize(object sender, EventArgs e)
    {
      ProgressBar1.Left = (this.Width - ProgressBar1.Width) / 2;
      ProgressBar1.Top = (this.Height - ProgressBar1.Height) / 2;
    }

    #endregion
    #region ..методы..

    private void Init()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      TaskManager.Add(this.UpdateLabels);
      TaskManager.Add(this.ReloadDictionaries);
      TaskManager.Add(this.LoadSettings);
      //TaskManager.Add(this.ReloadItems);

      TaskManager.ExecuteAll();
    }

    /// <summary>
    /// Загружает и применяет настройки пользователя.
    /// </summary>
    private void LoadSettings()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      Console.WriteLine("{0} > LoadSettings", this.ItemsType);

      this.SafeInvoke(() =>
      {
        //this.Accounts.Enabled = this.Categories.Enabled = this.Subcategories.Enabled = false;
        ToolStrip1.Enabled = ToolStrip2.Enabled = ToolStrip3.Enabled = false;
      });

      int selectedAccountId = 0;
      int selectedCategoryId = 0;
      int selectedSubcategoryId = 0;
      DateTime dateForm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
      DateTime dateTo = dateForm.AddMonths(1).AddDays(-1);
      decimal? amountFrom = null;
      decimal? amountTo = null;
      string period = "m";

      if (this.ItemsType == EntryType.Expense)
      {
        selectedAccountId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.AccountId]);
        selectedCategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.CategoryId]);
        selectedSubcategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.SubcategoryId]);
        dateForm = Convertion.ToDateTime(this.User.Info[InfoId.Settings.Desktop.Expenses.DateForm], dateForm);
        dateTo = Convertion.ToDateTime(this.User.Info[InfoId.Settings.Desktop.Expenses.DateTo], dateTo);
        amountFrom = Convertion.ToDecimal(this.User.Info[InfoId.Settings.Desktop.Expenses.AmountFrom], null);
        amountTo = Convertion.ToDecimal(this.User.Info[InfoId.Settings.Desktop.Expenses.AmountTo], null);
        period = this.User.Info[InfoId.Settings.Desktop.Expenses.Period];
      }
      else if (this.ItemsType == EntryType.Income)
      {
        selectedAccountId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.AccountId]);
        selectedCategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.CategoryId]);
        selectedSubcategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.SubcategoryId]);
        dateForm = Convertion.ToDateTime(this.User.Info[InfoId.Settings.Desktop.Incomes.DateForm], dateForm);
        dateTo = Convertion.ToDateTime(this.User.Info[InfoId.Settings.Desktop.Incomes.DateTo], dateTo);
        amountFrom = Convertion.ToDecimal(this.User.Info[InfoId.Settings.Desktop.Incomes.AmountFrom], null);
        amountTo = Convertion.ToDecimal(this.User.Info[InfoId.Settings.Desktop.Incomes.AmountTo], null);
        period = this.User.Info[InfoId.Settings.Desktop.Incomes.Period];
      }

      this.SafeInvoke(() =>
      {
        for (int i = 0; i < this.Accounts.Items.Count; i++)
        {
          if (((Account)this.Accounts.Items[i]).Id == selectedAccountId)
          {
            this.Accounts.SelectedIndex = i;
            break;
          }
        }

        for (int i = 0; i < this.Categories.Items.Count; i++)
        {
          if (((Category)this.Categories.Items[i]).Id == selectedCategoryId)
          {
            this.Categories.SelectedIndex = i;
            break;
          }
        }

        for (int i = 0; i < this.Subcategories.Items.Count; i++)
        {
          if (((Category)this.Subcategories.Items[i]).Id == selectedSubcategoryId)
          {
            this.Subcategories.SelectedIndex = i;
            break;
          }
        }

        this.DateFrom.Value = dateForm;
        this.DateTo.Value = dateTo;
        this.AmountFrom.Text = amountFrom.HasValue ? amountFrom.Value.ToString() : "";
        this.AmountTo.Text = amountTo.HasValue ? amountTo.Value.ToString() : "";

        if (!String.IsNullOrEmpty(period))
        {
          foreach (var m in ddbPeriod.DropDownItems)
          {
            if (m.GetType() == typeof(ToolStripMenuItem) && ((ToolStripMenuItem)m).Tag != null && ((ToolStripMenuItem)m).Tag.ToString().Equals(period, StringComparison.OrdinalIgnoreCase))
            {
              Period_Click(m, null);
              break;
            }
          }
        }

        //this.Accounts.Enabled = this.Categories.Enabled = this.Subcategories.Enabled = true;
        ToolStrip1.Enabled = ToolStrip2.Enabled = ToolStrip3.Enabled = true;
      });

      // загружаем записи
      this.ReloadItems();
    }

    /// <summary>
    /// Сохраняет настройки.
    /// </summary>
    public void SaveSettings()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      //DateTime s = DateTime.Now;

      if (this.ItemsType == EntryType.Expense)
      {
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AccountId, ((Account)this.Accounts.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.CategoryId, ((Category)this.Categories.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.SubcategoryId, ((Category)this.Subcategories.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.Period, this.ddbPeriod.Tag.ToString(), true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateForm, this.DateFrom.Value.Ticks, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateTo, this.DateTo.Value.Ticks, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountFrom, this.AmountFrom.Text, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountTo, this.AmountTo.Text, true);
      }
      else if (this.ItemsType == EntryType.Income)
      {
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AccountId, ((Account)this.Accounts.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.CategoryId, ((Category)this.Categories.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.SubcategoryId, ((Category)this.Subcategories.SelectedItem).Id, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.Period, this.ddbPeriod.Tag.ToString(), true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateForm, this.DateFrom.Value.Ticks, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateTo, this.DateTo.Value.Ticks, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountFrom, this.AmountFrom.Text, true);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountTo, this.AmountTo.Text, true);
      }

      //Console.WriteLine(DateTime.Now.Subtract(s));
    }

    /// <summary>
    /// Устанавливает надписи в зависимости от параметров списка.
    /// </summary>
    private void UpdateLabels()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(UpdateLabels));
        return;
      }

      Console.WriteLine("{0} > UpdateLabels", this.ItemsType);

      if (this.ItemsType == EntryType.Expense)
      {
        this.btnAdd.Text = this.mnuAdd.Text = "Добавить расход";
      }
      else if (this.ItemsType == EntryType.Income)
      {
        this.btnAdd.Text = this.mnuAdd.Text = "Добавить доход";
      }
    }

    /// <summary>
    /// Обновляет строку статуса статус.
    /// </summary>
    private void UpdateStatus()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(UpdateStatus));
        return;
      }

      if (this.ItemsType == EntryType.Expense)
      {
        this.StatusTitle.Text = String.Format("Расходы с {0} по {1}", this.DateFrom.Value.ToShortDateString(), this.DateTo.Value.ToShortDateString());
      }
      else if (this.ItemsType == EntryType.Income)
      {
        this.StatusTitle.Text = String.Format("Доходы с {0} по {1}", this.DateFrom.Value.ToShortDateString(), this.DateTo.Value.ToShortDateString());
      }

      this.TotalItems.Text = String.Format("Всего: {0}", this.DataGridView1.Rows.Count);

      if (this.TotalAmountByCurrencies.Count <= 0)
      {
        this.TotalAmount.Text = "-";
      }
      else
      {
        this.TotalAmount.Text = "";
        foreach (var key in this.TotalAmountByCurrencies.Keys)
        {
          if (!String.IsNullOrEmpty(this.TotalAmount.Text))
          {
            this.TotalAmount.Text += ", ";
          }
          this.TotalAmount.Text += this.TotalAmountByCurrencies[key].ToString("##,###,##0.00");
          this.TotalAmount.Text += " " + this.User.Currencies[key].ShortName;
        }
      }
    }

    /// <summary>
    /// Обновляет статус доступности управляющих элементов.
    /// </summary>
    private void UpdateButtons()
    {
      btnEdit.Enabled = btnDelete.Enabled =
      mnuEdit.Enabled = mnuDelete.Enabled =
      (DataGridView1.CurrentRow != null && DataGridView1.CurrentRow.Tag != null);
    }

    /// <summary>
    /// Загружает и выводит в списки справочники.
    /// </summary>
    public void ReloadDictionaries()
    {
      Console.WriteLine("{0} > ReloadDictionaries", this.ItemsType);

      if (this.InvokeRequired)
      {
        //System.Threading.Thread.Sleep(10000);
        this.Invoke(new Action(ReloadDictionaries));
        return;
      }

      Console.WriteLine("{0} > ReloadDictionaries2", this.ItemsType);

      var selectedAccountId = 0;
      var selectedMoneyItemId = 0;
     
      if (this.Accounts.SelectedItem != null)
      {
        selectedAccountId = ((Account)this.Accounts.SelectedItem).Id;
      }

      if (this.Categories.SelectedItem != null)
      {
        selectedMoneyItemId = ((Category)this.Categories.SelectedItem).Id;
      }

      this.Accounts.Items.Clear();
      this.Categories.Items.Clear();

      this.Accounts.Items.Add(new Account { Id = 0, Name = "<Все>" });
      this.Categories.Items.Add(new Category { Id = 0, Name = "<Все>" });

      this.Accounts.SelectedIndex = this.Categories.SelectedIndex = 0;

      var u = this.User;

      if (u == null) { return; }

      foreach (var item in u.Accounts.Values)
      {
        this.Accounts.Items.Add(item);

        if (selectedAccountId == item.Id)
        {
          this.Accounts.SelectedIndex = this.Accounts.Items.Count - 1;
        }
      }

      foreach (var item in u.Categories.Values.Where(c => c.ParentId == 0 && c.CategoryType == this.ItemsType))
      {
        this.Categories.Items.Add(item);

        if (selectedMoneyItemId == item.Id)
        {
          this.Categories.SelectedIndex = this.Categories.Items.Count - 1;
        }
      }

      // список подкатегорий
      Categories_SelectedIndexChanged(this.Categories, null);
    }

    /// <summary>
    /// Загружает из базы и выводит список записей в соответствии с параметрами фильтра.
    /// </summary>
    public void ReloadItems()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      Console.WriteLine("{0} > ReloadItems", this.ItemsType);

      int accountId = 0, categoryId = 0;
      DateTime dateFrom = DateTime.Now, dateTo = DateTime.Now;
      decimal? amountFrom = null, amountTo = null;

      this.SafeInvoke(() => { 
        accountId = ((Account)this.Accounts.SelectedItem).Id;
        categoryId = ((Category)this.Subcategories.SelectedItem).Id;
        if (categoryId <= 0)
        {
          categoryId = ((Category)this.Categories.SelectedItem).Id;
        }
        dateFrom = this.DateFrom.Value;
        dateTo = this.DateTo.Value;
        amountFrom = Convertion.ToDecimal(this.AmountFrom.Text, null);
        amountTo = Convertion.ToDecimal(this.AmountTo.Text, null);
      });

      this.ReloadItems_Task(accountId, categoryId, dateFrom, dateTo, amountFrom, amountTo);
    }

    private void ReloadItems_Task(int accountId, int categoryId, DateTime dateFrom, DateTime dateTo, decimal? amountFrom, decimal? amountTo)
    {
      int selectedItemId = 0;

      this.SafeInvoke(() =>
      {
        this.DataGridView1.Enabled = this.StatusStrip1.Enabled = 
        this.ToolStrip1.Enabled = this.ToolStrip2.Enabled = this.ToolStrip3.Enabled = false;

        //this.Cursor = Cursors.WaitCursor;
        this.DataGridView1.SuspendLayout();

        if (this.DataGridView1.CurrentRow != null)
        {
          selectedItemId = ((MoneyItem)this.DataGridView1.CurrentRow.Tag).Id;
        }
        this.DataGridView1.Rows.Clear();
        this.TotalAmountByCurrencies.Clear();
        this.UpdateStatus();

        this.ProgressBar1.Run("Загрузка данных...", "", 0);

        this.Cancellation = false;
      });

      var u = this.User;

      var items = u.GetMoneyItems
      (
        type: this.ItemsType,
        accountId: accountId,
        categoryId: categoryId,
        dateFrom: dateFrom,
        dateTo: dateTo,
        amountFrom: amountFrom,
        amountTo: amountTo
      );

      this.ProgressBar1.ProgressMaximum = items.Count;

      int detailStep = 0;

      if (this.ProgressBar1.ProgressMaximum >= 500 && this.ProgressBar1.ProgressMaximum < 1000)
      {
        detailStep = 2;
      }
      else if (this.ProgressBar1.ProgressMaximum >= 1000 && this.ProgressBar1.ProgressMaximum < 2000)
      {
        detailStep = 5;
      }
      else if (this.ProgressBar1.ProgressMaximum >= 2000 && this.ProgressBar1.ProgressMaximum < 5000)
      {
        detailStep = 10;
      }
      else if (this.ProgressBar1.ProgressMaximum >= 5000)
      {
        detailStep = 100;
      }

      foreach (var item in items)
      {
        if (this.Cancellation)
        {
          break;
        }

        var row = new DataGridViewRow();

        int iconId = 0;
        Account account = u.Accounts[item.AccountId];
        Category category = u.Categories[item.CategoryId];
        Category parentCategory = null;

        if (category.ParentId > 0)
        {
          parentCategory = u.Categories[category.ParentId];
        }

        if (item.IconId > 0)
        {
          // иконка записи
          iconId = item.IconId;
        }
        else
        {
          if (category.IconId > 0)
          {
            // иконка категории
            iconId = category.IconId;
          }
          else
          {
            // иконка статьи
            if (parentCategory != null)
            {
              iconId = parentCategory.IconId;
            }
          }
        }
        
        var iconCell = new DataGridViewImageCell();

        if (iconId > 0)
        {
          iconCell.Value = u.GetIcon(iconId);
        }
        else
        {
          if (item.EntryType == EntryType.Expense)
          {
            iconCell.Value = Properties.Resources.item_еxpense;
          }
          else if (item.EntryType == EntryType.Income)
          {
            iconCell.Value = Properties.Resources.item_income;
          }
          else
          {
            iconCell.Value = Properties.Resources.item;
          }
        }
        
        row.Cells.Add(iconCell);
        row.Cells.Add(new DataGridViewTextBoxCell { Value = (parentCategory != null ? parentCategory.Name : category.Name) });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = (parentCategory != null ? category.Name : "") });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Title });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.Name });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = item.DateEntry });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Amount });
        row.Cells.Add(new DataGridViewTextBoxCell { Value = account.CurrencyCode }); //u.Currencies[account.CurrencyCode].ShortName

        row.Tag = item;

        // стили
        row.DefaultCellStyle.BackColor = category.BackColor;
        row.DefaultCellStyle.ForeColor = category.ForeColor;
        row.DefaultCellStyle.Font = new Font(this.DataGridView1.Font, category.FontStyle);

        // добавляем строку
        this.SafeInvoke(() => this.DataGridView1.Rows.Add(row));

        // если эта запись была выбрана до обновления, выбираем её снова
        /*if (selectedItemId == item.Id)
        {
          this.SafeInvoke(() => this.DataGridView1.CurrentCell = this.DataGridView1.Rows[this.DataGridView1.RowCount - 1].Cells[0]);
        }*/

        // System.Threading.Thread.Sleep(100); // note: проверка работы потоков

        // общая сумма
        if (!this.TotalAmountByCurrencies.ContainsKey(account.CurrencyCode))
        {
          this.TotalAmountByCurrencies.Add(account.CurrencyCode, 0);
        }

        this.TotalAmountByCurrencies[account.CurrencyCode] += item.Amount;

        this.ProgressBar1.ProgressValue++;

        if (this.ProgressBar1.ProgressValue > 0 && (this.ProgressBar1.ProgressValue % 100) == 0)
        {
          this.UpdateStatus();
        }

        if (detailStep == 0 || (this.ProgressBar1.ProgressValue % detailStep) == 0)
        {
          this.ProgressBar1.DetailedInfo = String.Format("Получено {0:##,###,##0} из {1:##,###,##0} записей.", this.ProgressBar1.ProgressValue, this.ProgressBar1.ProgressMaximum);
        }
      }

      // this.ProgressBar1.ActionName = "Пересчет...";

      /*if (this.ProgressBar1.Visible)
      {
        System.Threading.Thread.Sleep(1000);
      }*/

      this.SafeInvoke(() =>
      {
        this.UpdateStatus();
        this.UpdateButtons();

        this.DataGridView1.Enabled = this.StatusStrip1.Enabled =
        this.ToolStrip1.Enabled = this.ToolStrip2.Enabled = this.ToolStrip3.Enabled = true;

        this.ProgressBar1.End();

        this.DataGridView1.ResumeLayout();
      });

      //this.Cursor = Cursors.Default;
      //btnEdit.Enabled = btnDelete.Enabled =
      //mnuEdit.Enabled = mnuDelete.Enabled = (DataGridView1.Rows.Count > 0);
    }

    private void SafeInvoke(Action action)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<Action>(SafeInvoke), action);
        return;
      }

      if (this.IsDisposed)
      {
        return;
      }

      action();
    }

    #endregion
        
  }

}