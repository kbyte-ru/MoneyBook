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

    #region ..свойства..

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

    private int _CancelDelay = 0;

    /// <summary>
    /// Время в миллисекундах, через которое следует показать кнопку "Отмена". Ноль - не использовать отмену.
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
        this.Init();
      }
    }

    private int _ShowDelay = 0;

    /// <summary>
    /// Время в миллисекундах, через которое должен появиться элемент.
    /// </summary>
    public int ShowDelay
    {
      get
      {
        return _ShowDelay;
      }
      set
      {
        _ShowDelay = value;
        this.Init();
      }
    }

    /// <summary>
    /// Метод, который будет вызван при отмене операции пользователем.
    /// </summary>
    public Action CancelCallback { get; set; }

    /// <summary>
    /// Текст запроса отмены операции.
    /// </summary>
    public string CancelText { get; set; }

    #endregion
    #region ..конструктор..

    public MProgressBar()
    {
      InitializeComponent();

      lblAction.Text = lblDetails.Text = "";
      this.CancelText = "Вы действительно хотите прервать операцию?";
    }

    #endregion
    #region ..методы..

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
      this.Init();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (!String.IsNullOrEmpty(this.CancelText) && MessageBox.Show(this.CancelText, "Отмена", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
      {
        return;
      }

      this.ActionName = "Отмена операции...";
      this.DetailedInfo = "";

      if (this.CancelCallback != null)
      {
        this.CancelCallback();
        btnCancel.Enabled = false;
      }
      else
      {
        MessageBox.Show("Невозможно отменить операцию, т.к. программист не сделал для этого абсолютно ничего. Ничего, Карл!", "Ошибка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.CancelCallback != null && this.ProgressValue < this.ProgressMaximum / 2)
      {
        this.btnCancel.Visible = true;
      }
      timer1.Enabled = false;
    }

    private void timer2_Tick(object sender, EventArgs e)
    {
      if (this.ProgressValue < this.ProgressMaximum / 2)
      {
        this.Visible = true;
      }
      timer2.Enabled = false;
    }

    private void timer3_Tick(object sender, EventArgs e)
    {
      this.Hide();
      timer1.Enabled = false;
    }

    /// <summary>
    /// Инициализирует и отображает progress.
    /// </summary>
    /// <param name="actionName">Название действия.</param>
    /// <param name="detailedInfo">Дополнительная информация.</param>
    /// <param name="max">Максимальное значение progress-а.</param>
    internal void Run(string actionName = null, string detailedInfo = null, int max = 0)
    {
      this.ProgressValue = 0;
      this.ActionName = actionName;
      this.DetailedInfo = detailedInfo;
      this.ProgressMaximum = max;

      this.btnCancel.Visible = false;
      this.btnCancel.Enabled = true;

      this.Init();

      if (this.ShowDelay <= 0)
      {
        this.Visible = true;
      }
    }

    /// <summary>
    /// Увеличивает значение <see cref="ProgressValue"/> на 1.
    /// </summary>
    internal void Next()
    {
      if (this.ProgressValue + 1 > this.ProgressMaximum)
      {
        return;
      }

      this.ProgressValue++;
    }

    internal void End()
    {
      this.ProgressValue = this.ProgressMaximum;
      timer2.Enabled = timer3.Enabled = false;
      timer3.Enabled = true;
    }

    private void Init()
    {
      if (this.CancelCallback != null && this.CancelDelay > 0)
      {
        timer1.Interval = this.CancelDelay;
        timer1.Enabled = true;
      }

      if (this.ShowDelay > 0)
      {
        timer2.Interval = this.ShowDelay;
        timer2.Enabled = true;
      }

      timer3.Enabled = false;
    }

    #endregion    

  }

}