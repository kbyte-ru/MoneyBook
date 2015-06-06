using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{
  public partial class MProgressBar : UserControl
  {

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
        this.SetText(lblAction, value);
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
        this.SetText(lblDetails, value);
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

    private int _CancelDelay = 0;

    /// <summary>
    /// Время в миллисекундах, через которое следует показать кнопку "Отмена".
    /// </summary>
    public int CancelDelay
    {
      get
      {
        return _CancelDelay;
      }
      set
      {
        _CancelDelay = value;
      }
    }

    /// <summary>
    /// Метод, который будет вызван при отмене операции пользователем.
    /// </summary>
    public Action CancelCallback { get; set; }
    
    public MProgressBar()
    {
      InitializeComponent();

      this.AllowCancel = false;
      lblAction.Text = lblDetails.Text = "";
    }

    private void SetText(Control ctrl, string value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<Control, string>(this.SetText), ctrl, value);
        return;
      }

      ctrl.Text = value;
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

    private void MProgressBar_Load(object sender, EventArgs e)
    {
      if (this.CancelCallback != null && this.CancelDelay > 0)
      {
        timer1.Interval = this.CancelDelay;
        timer1.Enabled = true;
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
      else
      {
        MessageBox.Show("Невозможно отменить операцию, т.к. программист не сделал для этого абсолютно ничего. Ничего, Карл!", "Ошибка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.CancelCallback != null)
      {
        flowLayoutPanel1.Visible = true;
      }
      timer1.Enabled = false;
    }


  }
}
