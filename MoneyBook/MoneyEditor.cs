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

  public partial class MoneyEditor : Form
  {

    private bool AmountKeyIsClipboard = false;
    private bool AmountIsKeyPress = false;

    public MoneyItem MoneyItem { get; protected set; }

    public decimal PrevAmount { get; protected set; }
    public string PrevCurrencyCode { get; protected set; }

    public MoneyEditor(MoneyItem moneyItem)
    {
      if (moneyItem == null)
      {
        throw new ArgumentNullException("moneyItem");
      }

      if (moneyItem.EntryType == EntryType.None)
      {
        throw new ArgumentException("moneyItem.EntryType");
      }

      InitializeComponent();

      this.MoneyItem = moneyItem;

      this.Accounts.ValueMember = "Id";
      this.Accounts.DisplayMember = "Name";

      this.Categories.ValueMember = "Id";
      this.Categories.DisplayMember = "Name";

      this.Subcategories.ValueMember = "Id";
      this.Subcategories.DisplayMember = "Name";
    }

    private void MoneyEditor_Load(object sender, EventArgs e)
    {
      try
      {
        foreach (var item in Program.CurrentUser.Accounts.Values)
        {
          this.Accounts.Items.Add(item);

          if (this.MoneyItem.AccountId == item.Id)
          {
            this.Accounts.SelectedIndex = this.Accounts.Items.Count - 1;
          }
        }

        if (this.Accounts.SelectedItem == null && this.Accounts.Items.Count > 0)
        {
          this.Accounts.SelectedIndex = 0;
        }
        else
        {
          Accounts_SelectedIndexChanged(this.Accounts, null);
        }

        var selectedCategory = Program.CurrentUser.Categories.Values.FirstOrDefault(c => c.Id == this.MoneyItem.CategoryId);

        foreach (var item in Program.CurrentUser.Categories.Values.Where(c => c.ParentId == 0 && c.CategoryType == this.MoneyItem.EntryType))
        {
          this.Categories.Items.Add(item);

          if (selectedCategory != null && (selectedCategory.Id == item.Id || selectedCategory.ParentId == item.Id))
          {
            this.Categories.SelectedIndex = this.Categories.Items.Count - 1;
          }
        }

        if (this.Categories.SelectedItem == null && this.Categories.Items.Count > 0)
        {
          this.Categories.SelectedIndex = 0;
        }
        else
        {
          Categories_SelectedIndexChanged(this.Categories, null);
        }

        this.Title.Text = this.MoneyItem.Title;
        this.Description.Text = this.MoneyItem.Description;
        this.Amount.Text = this.MoneyItem.Amount.ToString();

        if (this.MoneyItem.Id > 0)
        {
          this.DateEntry.Value = this.MoneyItem.DateEntry;
        }

        this.btnDelete.Visible = this.MoneyItem.Id > 0;

        if (this.Accounts.SelectedItem != null)
        {
          this.PrevAmount = this.MoneyItem.Amount;
          this.PrevCurrencyCode = ((Account)this.Accounts.SelectedItem).CurrencyCode;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Accounts_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.Accounts.SelectedItem != null)
      {
        var c = Program.CurrentUser.Currencies[((Account)this.Accounts.SelectedItem).CurrencyCode];
        CurrancyName.Text = c.ShortName;
      }
      else
      {
        CurrancyName.Text = "";
      }
    }

    private void Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Subcategories.Items.Clear();
      this.Subcategories.Items.Add(new Category { Id = 0, Name = "<Без категории>" });

      if (this.Categories.SelectedItem != null)
      {
        var categoryId = ((Category)this.Categories.SelectedItem).Id;

        foreach (var item in Program.CurrentUser.Categories.Values.Where(c => c.ParentId == categoryId && c.CategoryType == this.MoneyItem.EntryType))
        {
          this.Subcategories.Items.Add(item);

          if (this.MoneyItem.CategoryId == item.Id)
          {
            this.Subcategories.SelectedIndex = this.Subcategories.Items.Count - 1;
          }
        }
      }

      if (this.Subcategories.SelectedItem == null && this.Subcategories.Items.Count > 0)
      {
        this.Subcategories.SelectedIndex = 0;
      }
    }

    private void Amount_TextChanged(object sender, EventArgs e)
    {
      if (this.AmountIsKeyPress && !this.AmountKeyIsClipboard)
      {
        return;
      }

      // защита от вставок неправильных данных из буфера обмана
      var value = Convertion.ToDecimal(this.Amount.Text, null);
      if (!value.HasValue)
      {
        this.Amount.Text = "";
      }
      else
      {
        this.Amount.TextChanged -= this.Amount_TextChanged;
        this.Amount.Text = value.Value.ToString();
        this.Amount.TextChanged += this.Amount_TextChanged;
      }
    }

    private void Amount_Leave(object sender, EventArgs e)
    {
      if (String.IsNullOrWhiteSpace(this.Amount.Text)) { return; }

      var value = Convertion.ToDecimal(this.Amount.Text, null);
      if (!value.HasValue)
      {
        this.Amount.Text = "";
      }
      else
      {
        this.Amount.TextChanged -= this.Amount_TextChanged;
        this.Amount.Text = value.Value.ToString();
        this.Amount.TextChanged += this.Amount_TextChanged;
      }
    }

    private void Amount_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && (e.KeyData.HasFlag(Keys.C) || e.KeyData.HasFlag(Keys.X) || e.KeyData.HasFlag(Keys.V) || e.KeyData.HasFlag(Keys.A) || e.KeyData.HasFlag(Keys.Z)))
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

      // минус может быть только в начале строки и только один
      if (e.KeyChar == '-' && this.Amount.SelectionStart != 0 && this.Amount.Text.IndexOf("-") == -1)
      {
        e.Handled = true;
        return;
      }

      // проверяем, получится в итоге число или нет
      if (e.KeyChar != '\b')
      {
        var value = Convertion.ToDecimal(this.Amount.Text + e.KeyChar, null);

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
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        // проверяем заполнение формы
        var errors = new List<string>();
        if (this.Accounts.SelectedItem == null)
        {
          errors.Add("- Необходимо выбрать счет;");
        }
        if (this.Categories.SelectedItem == null)
        {
          errors.Add(String.Format("- Необходимо выбрать статью {0};", this.MoneyItem.EntryType == EntryType.Income ? "доходов" : "расходов"));
        }
        if (String.IsNullOrWhiteSpace(this.Title.Text))
        {
          errors.Add(String.Format("- Необходимо указать название записи {0};", this.MoneyItem.EntryType == EntryType.Income ? "дохода" : "расхода"));
        }
        if (String.IsNullOrWhiteSpace(this.Amount.Text))
        {
          errors.Add("- Необходимо указать сумму;");
        }

        if (errors.Count > 0)
        {
          MessageBox.Show(String.Format("Не все поля формы заполнены правильно:\r\n\r\n{0}", String.Join("\r\n", errors)), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          this.DialogResult = System.Windows.Forms.DialogResult.None;
          return;
        }

        this.Cursor = Cursors.WaitCursor;
        //panel1.Enabled = tableLayoutPanel3.Enabled = false;
        this.Enabled = false;
        
        // если все ok, сохраняем
        this.MoneyItem.AccountId = ((Account)this.Accounts.SelectedItem).Id;
        this.MoneyItem.Amount = Convertion.ToDecimal(this.Amount.Text);
        if (this.Subcategories.SelectedItem == null || ((Category)this.Subcategories.SelectedItem).Id == 0)
        {
          this.MoneyItem.CategoryId = ((Category)this.Categories.SelectedItem).Id;
        }
        else
        {
          this.MoneyItem.CategoryId = ((Category)this.Subcategories.SelectedItem).Id;
        }
        this.MoneyItem.DateEntry = this.DateEntry.Value;
        if (this.MoneyItem.Id > 0)
        {
          this.MoneyItem.DateUpdated = DateTime.Now;
        }
        this.MoneyItem.Title = this.Title.Text;
        this.MoneyItem.Description = this.Description.Text;

        Program.CurrentUser.Save(this.MoneyItem);
       
        // закрываем окно
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.DialogResult = System.Windows.Forms.DialogResult.None;
        //panel1.Enabled = tableLayoutPanel3.Enabled = true;
        this.Cursor = Cursors.Default;
        this.Enabled = true;
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      var currencyCode = Program.CurrentUser.Accounts[this.MoneyItem.AccountId].CurrencyCode;

      // запрос на удаление
      if (MessageBox.Show(String.Format("Вы действительно хотите удалить запись «{0}» от {3} на сумму {1:##,###,##0.00} {2}?\r\n\r\nВосстановить данные после удаления будет невозможно.\r\n\r\nНажмите «Да», чтобы удалить запись.", this.MoneyItem.Title, this.MoneyItem.Amount, currencyCode, this.MoneyItem.DateEntry.ToShortDateString()), "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      {
        this.DialogResult = System.Windows.Forms.DialogResult.None;
        return;
      }

      // удаляем из базы
      Program.CurrentUser.Delete(this.MoneyItem);

      // закрываем окно
      this.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.Close();
    }

    private void Description_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.A)
      {
        this.Description.SelectAll();
        e.SuppressKeyPress = true; // чтобы не было beep
      }
    }

  }

}