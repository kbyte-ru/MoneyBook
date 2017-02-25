using System;
using System.Drawing;
using System.IO;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{

  /// <summary>
  /// Представляет форму просмотра и выбора иконок из коллекции пользователя.
  /// </summary>
  public partial class IconsViewer : MoneyBook.WinApp.MForm
  {

    private int _SelectedIconId;

    /// <summary>
    /// Идентификатор выбранной иконки.
    /// </summary>
    public int SelectedIconId
    {
      get
      {
        return _SelectedIconId;
      }
      private set
      {
        _SelectedIconId = value;
        btnDelete.Enabled = btnSelect.Enabled = value > 0;
      }
    }

    public IconsViewer(User user) : base(user)
    {
      InitializeComponent();
    }

    private void IconsViewer_Load(object sender, System.EventArgs e)
    {
      this.LoadIcons();

      if (this.listView1.Items.Count > 0)
      {
        this.SelectedIconId = Convert.ToInt32(this.listView1.Items[0].ImageKey);
      }
      else
      {
        this.SelectedIconId = 0;
      }
    }

    private void LoadIcons()
    {
      this.listView1.Items.Clear();
      this.imageList1.Images.Clear();

      var icons = this.User.GetIcons();

      foreach (var icon in icons)
      {
        using (var m = new MemoryStream(icon.Data))
        {
          string key = icon.Id.ToString();

          try
          {
            this.imageList1.Images.Add(key, Image.FromStream(m));
          }
          catch
          {
            this.imageList1.Images.Add(key, Properties.Resources.cross);
          }

          this.listView1.Items.Add(null, key);
        }
      }
    }

    private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if (this.listView1.FocusedItem != null)
      {
        this.SelectedIconId = Convert.ToInt32(this.listView1.FocusedItem.ImageKey);
      }
      else
      {
        this.SelectedIconId = 0;
      }
    }

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.Close();
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.Close();
    }

  }

}
