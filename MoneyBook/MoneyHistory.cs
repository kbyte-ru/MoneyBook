﻿using System;
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
          this.ReloadItems();
        }
      }
    }

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
          this.ReloadItems();
        }
      }
    }

    public MoneyHistory()
    {
      InitializeComponent();

      ToolStrip2.Items.Insert(1, new ToolStripControlHost(this.DateFrom));
      ToolStrip2.Items.Insert(3, new ToolStripControlHost(this.DateTo));

      this.Accounts.ComboBox.ValueMember = "Id";
      this.Accounts.ComboBox.DisplayMember = "Name";

      this.MoneyItems.ComboBox.ValueMember = "Id";
      this.MoneyItems.ComboBox.DisplayMember = "Name";

      this.Categories.ComboBox.ValueMember = "Id";
      this.Categories.ComboBox.DisplayMember = "Name";
    }

    private void MoneyHistory_Load(object sender, EventArgs e)
    {
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

    }

    public void ReloadDictionaries()
    {
      var selectedAccountId = 0;
      var selectedMoneyItemId = 0;
      var selectedCategoryId = 0;

      if (this.Accounts.SelectedItem != null)
      {
        selectedAccountId = ((Account)this.Accounts.SelectedItem).Id;
      }

      if (this.MoneyItems.SelectedItem != null)
      {
        selectedMoneyItemId = ((Category)this.MoneyItems.SelectedItem).Id;
      }

      if (this.Categories.SelectedItem != null)
      {
        selectedCategoryId = ((Category)this.Categories.SelectedItem).Id;
      }

      this.Accounts.Items.Clear();
      this.MoneyItems.Items.Clear();
      this.Categories.Items.Clear();

      this.Accounts.Items.Add(new Account { Id = 0, Name = "<Все>" });
      this.MoneyItems.Items.Add(new Category { Id = 0, Name = "<Все>" });
      this.Categories.Items.Add(new Category { Id = 0, Name = "<Все>" });

      this.Accounts.SelectedIndex = this.MoneyItems.SelectedIndex = this.Categories.SelectedIndex = 0;

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
        this.MoneyItems.Items.Add(item);

        if (selectedMoneyItemId == item.Id)
        {
          this.MoneyItems.SelectedIndex = this.MoneyItems.Items.Count - 1;
        }
      }

      foreach (var item in u.Categories.Values.Where(c => c.ParentId > 0 && c.CategoryType == this.ItemsType))
      {
        this.Categories.Items.Add(item);

        if (selectedCategoryId == item.Id)
        {
          this.Categories.SelectedIndex = this.Categories.Items.Count - 1;
        }
      }
    }

    public void ReloadItems()
    {
      this.DataGridView1.Rows.Clear();

      var u = this.User;

      if (this.ItemsType == EntryType.None || u == null) { return; }

      var items = u.GetMoneyItems(this.ItemsType);
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

        row.Tag = item.Id; // TODO: или весь элемент

        // стили
        row.DefaultCellStyle.BackColor = category.BackColor;
        row.DefaultCellStyle.ForeColor = category.ForeColor;
        row.DefaultCellStyle.Font = new Font(this.DataGridView1.Font, category.FontStyle);

        // добавляем строку
        this.DataGridView1.Rows.Add(row);
      }
    }

    

    private void DataGridView1_VisibleChanged(object sender, EventArgs e)
    {
    }

  }

}