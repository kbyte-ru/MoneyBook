using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{

  public partial class MoneyHistory : UserControl
  {

    #region ..свойства..

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
          this.ReloadDictionaries();
          this.LoadSettings();
          this.ReloadItems();
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
          this.LoadSettings();
          this.ReloadItems();
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
    }
    
    #endregion
    #region ..обработчики..

    private void MoneyHistory_Load(object sender, EventArgs e)
    {
      // восстанавливаем ранее выбранные параметры
      this.LoadSettings();

      // загружаем данные
      this.ReloadItems();
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

    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
      // загружаем данные
      this.ReloadItems();

      // запоминаем выбранные параметры
      this.SaveSettings();
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

    #endregion
    #region ..методы..

    /// <summary>
    /// Загружает и применяет настройки пользователя.
    /// </summary>
    private void LoadSettings()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      int selectedAccountId = 0;
      int selectedCategoryId = 0;
      int selectedSubcategoryId = 0;

      if (this.ItemsType == EntryType.Expense)
      {
        selectedAccountId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.AccountId]);
        selectedCategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.CategoryId]);
        selectedSubcategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Expenses.SubcategoryId]);

        //this.User.Info.Set(InfoId.Settings.Desktop.Expenses.Period, 0);
        //this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateForm, DateFrom.Value);
        //this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateTo, DateTo.Value);
        //this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountFrom, AmountFrom.Text);
        //this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountTo, AmountTo.Text);
      }
      else if (this.ItemsType == EntryType.Income)
      {
        selectedAccountId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.AccountId]);
        selectedCategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.CategoryId]);
        selectedSubcategoryId = Convertion.ToInt32(this.User.Info[InfoId.Settings.Desktop.Incomes.SubcategoryId]);

        //this.User.Info.Set(InfoId.Settings.Desktop.Incomes.Period, 0);
        //this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateForm, DateFrom.Value);
        //this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateTo, DateTo.Value);
        //this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountFrom, AmountFrom.Text);
        //this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountTo, AmountTo.Text);
      }

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
    }

    /// <summary>
    /// Сохраняет настройки.
    /// </summary>
    private void SaveSettings()
    {
      if (this.ItemsType == EntryType.None || this.User == null) { return; }

      //DateTime s = DateTime.Now;

      if (this.ItemsType == EntryType.Expense)
      {
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AccountId, ((Account)this.Accounts.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.CategoryId, ((Category)this.Categories.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.SubcategoryId, ((Category)this.Subcategories.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.Period, 0);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateForm, DateFrom.Value);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.DateTo, DateTo.Value);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountFrom, AmountFrom.Text);
        this.User.Info.Set(InfoId.Settings.Desktop.Expenses.AmountTo, AmountTo.Text);
      }
      else if (this.ItemsType == EntryType.Income)
      {
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AccountId, ((Account)this.Accounts.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.CategoryId, ((Category)this.Categories.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.SubcategoryId, ((Category)this.Subcategories.SelectedItem).Id);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.Period, 0);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateForm, DateFrom.Value);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.DateTo, DateTo.Value);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountFrom, AmountFrom.Text);
        this.User.Info.Set(InfoId.Settings.Desktop.Incomes.AmountTo, AmountTo.Text);
      }

      //Console.WriteLine(DateTime.Now.Subtract(s));
    }

    /// <summary>
    /// Устанавливает надписи в соответствии с параметрами текущего списка данных.
    /// </summary>
    private void UpdateLabels()
    {
      if (this.ItemsType == EntryType.Expense)
      {
        this.btnAdd.Text = "Добавить расход";
        this.StatusTitle.Text = String.Format("Расходы с {0} по {1}", this.DateFrom.Value.ToShortDateString(), this.DateTo.Value.ToShortDateString());
      }
      else if (this.ItemsType == EntryType.Income)
      {
        this.btnAdd.Text = "Добавить доход";
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
    /// Загружает и выводит в списки справочники.
    /// </summary>
    public void ReloadDictionaries()
    {
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
      this.DataGridView1.Rows.Clear();
      this.TotalAmountByCurrencies.Clear();

      var u = this.User;

      if (this.ItemsType == EntryType.None || u == null) { return; }

      int categoryId = ((Category)this.Subcategories.SelectedItem).Id;

      if (categoryId <= 0)
      {
        categoryId = ((Category)this.Categories.SelectedItem).Id;
      }

      var items = u.GetMoneyItems
      (
        type: this.ItemsType,
        accountId: ((Account)this.Accounts.SelectedItem).Id,
        categoryId: categoryId,
        dateFrom: this.DateFrom.Value,
        dateTo: this.DateTo.Value,
        amountFrom: Convertion.ToDecimal(this.AmountFrom.Text, null),
        amountTo: Convertion.ToDecimal(this.AmountTo.Text, null)
      );

      foreach (var item in items)
      {
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
        row.Cells.Add(new DataGridViewTextBoxCell { Value = u.Currencies[account.CurrencyCode].ShortName });

        row.Tag = item;

        // стили
        row.DefaultCellStyle.BackColor = category.BackColor;
        row.DefaultCellStyle.ForeColor = category.ForeColor;
        row.DefaultCellStyle.Font = new Font(this.DataGridView1.Font, category.FontStyle);

        // добавляем строку
        this.DataGridView1.Rows.Add(row);

        // общая сумма
        if (!this.TotalAmountByCurrencies.ContainsKey(account.CurrencyCode))
        {
          this.TotalAmountByCurrencies.Add(account.CurrencyCode, 0);
        }

        this.TotalAmountByCurrencies[account.CurrencyCode] += item.Amount;
      }

      this.UpdateLabels();
    }

    #endregion

  }

}