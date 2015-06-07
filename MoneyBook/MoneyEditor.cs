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

    public MoneyItem MoneyItem { get; protected set; }

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
      
      // Accounts_SelectedIndexChanged(this.Accounts, null);

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

      //Categories_SelectedIndexChanged(this.Categories, null);

      this.Title.Text = this.MoneyItem.Title;
      this.Description.Text = this.MoneyItem.Description;
      this.Amount.Text = this.MoneyItem.Amount.ToString();
      this.DateEntry.Value = this.MoneyItem.DateEntry;
    }

    private void Accounts_SelectedIndexChanged(object sender, EventArgs e)
    {
      var c = Program.CurrentUser.Currencies[((Account)this.Accounts.SelectedItem).CurrencyCode];
      CurrancyName.Text = c.ShortName;
    }

    private void Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Subcategories.Items.Clear();
      this.Subcategories.Items.Add(new Category { Id = 0, Name = "<Без категории>" });

      var categoryId = ((Category)this.Categories.SelectedItem).Id;

      foreach (var item in Program.CurrentUser.Categories.Values.Where(c => c.ParentId == categoryId && c.CategoryType == this.MoneyItem.EntryType))
      {
        this.Subcategories.Items.Add(item);

        if (this.MoneyItem.CategoryId == item.Id)
        {
          this.Subcategories.SelectedIndex = this.Subcategories.Items.Count - 1;
        }
      }

      if (this.Subcategories.SelectedItem == null && this.Subcategories.Items.Count > 0)
      {
        this.Subcategories.SelectedIndex = 0;
      }
    }

  }

}