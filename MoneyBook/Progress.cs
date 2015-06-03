using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{

  public partial class Progress : Form
  {

    /// <summary>
    /// Заголовок окна.
    /// </summary>
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        this.SetTitlte(value);
      }
    }

    /// <summary>
    /// Название выполняемой операции.
    /// </summary>
    public string ActionName
    {
      get
      {
        return lblAction.Text;
      }
      set
      {
        this.SetLabel(lblAction, value);
      }
    }

    /// <summary>
    /// Подробная информация выполняемой операции.
    /// </summary>
    public string DetailedInfo
    {
      get
      {
        return lblDetails.Text;
      }
      set
      {
        this.SetLabel(lblDetails, value);
      }
    }

    /// <summary>
    /// Задает или получает максимальное значение ProgressBar.
    /// </summary>
    public int ProgressMaximum
    {
      get
      {
        return progressBar1.Maximum;
      }
      set
      {
        this.SetProgress("max", value);
      }
    }

    /// <summary>
    /// Задает или получает минимальное значение ProgressBar.
    /// </summary>
    public int ProgressMinimum
    {
      get
      {
        return progressBar1.Minimum;
      }
      set
      {
        this.SetProgress("min", value);
      }
    }

    /// <summary>
    /// Задает или получает текущее значение ProgressBar.
    /// </summary>
    public int ProgressValue
    {
      get
      {
        return progressBar1.Value;
      }
      set
      {
        this.SetProgress("value", value);
      }
    }

    /// <summary>
    /// Задает или получает значение, указывающее на возможность отмены выполняемой операции.
    /// </summary>
    public bool AllowCancel
    {
      get
      {
        return flowLayoutPanel1.Visible;
      }
      set
      {
        flowLayoutPanel1.Visible = value;
      }
    }

    /// <summary>
    /// Метод, который будет вызван при отмене операции пользователем.
    /// </summary>
    public Action CancelCallback { get; set; }

    public Progress()
    {
      InitializeComponent();

      this.AllowCancel = false;
    }

    private void SetTitlte(string value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<string>(this.SetTitlte), value);
        return;
      }

      base.Text = value;
    }

    private void SetLabel(Label lbl, string value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<Label, string>(this.SetLabel), lbl, value);
        return;
      }

      lbl.Text = value;
    }

    private void SetProgress(string key, int value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<string, int>(this.SetProgress), key, value);
        return;
      }

      if (key == "max")
      {
        progressBar1.Maximum = value;
      }
      else if (key == "min")
      {
        progressBar1.Minimum = value;
      }
      else if (key == "value")
      {
        progressBar1.Value = value;
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Вы действительно хотите прервать операцию?", "Отмена операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
      {
        return;
      }

      this.ActionName = "Отмена операции...";
      
      if (this.CancelCallback != null)
      {
        this.CancelCallback();
      }

      this.Close();
    }

  }

}
