using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{

  class MDataGridView : DataGridView
  {

    public MDataGridView()
    {
      this.AllowUserToAddRows = false;
      this.AllowUserToDeleteRows = false;
      this.AllowUserToResizeRows = false;
      this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.BackgroundColor = SystemColors.Window;
      this.BorderStyle = BorderStyle.None;
      this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      // this.Dock =DockStyle.Fill;
      this.EnableHeadersVisualStyles = false;
      this.MultiSelect = false;
      this.ReadOnly = true;
      this.RowHeadersWidth = 12;
      this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

  }

}