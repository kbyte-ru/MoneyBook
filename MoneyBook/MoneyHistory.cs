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

    public EntryType ItemsType { get; set; }

    public MoneyHistory()
    {
      InitializeComponent();

      ToolStrip2.Items.Insert(1, new ToolStripControlHost(this.DateFrom));
      ToolStrip2.Items.Insert(3, new ToolStripControlHost(this.DateTo));
    }

    private void MoneyHistory_Load(object sender, EventArgs e)
    {
      this.Reload();
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

    public void Reload()
    {
      this.DataGridView1.Rows.Clear();

      var u = Program.CurrentUser;

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
          iconCell.Value = Properties.Resources.cross; // TODO: Поменять
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