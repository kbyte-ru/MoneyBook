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
    public Main()
    {
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      var table = new DataTable();
      table.Columns.Add("123");
      table.Columns.Add("test");
      table.Columns.Add("test-test");
      table.Rows.Add("123", "123", "123");
      table.Rows.Add("123", "123", "123");
      DataGridView1.DataSource = table;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      // test
      User.Create(ApplicationType.Desktop, Application.StartupPath, "test", "");
    }
  }
}
