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
        this.SetText(value);
      }
    }

    /// <summary>
    /// Название выполняемой операции.
    /// </summary>
    public string ActionName
    {
      get
      {
        return ProgressBar1.ActionName;
      }
      set
      {
        ProgressBar1.ActionName = value;
      }
    }

    /// <summary>
    /// Подробная информация выполняемой операции.
    /// </summary>
    public string DetailedInfo
    {
      get
      {
        return ProgressBar1.DetailedInfo;
      }
      set
      {
        ProgressBar1.DetailedInfo = value;
      }
    }

    /// <summary>
    /// Задает или получает максимальное значение ProgressBar.
    /// </summary>
    public int ProgressMaximum
    {
      get
      {
        return ProgressBar1.ProgressMaximum;
      }
      set
      {
        ProgressBar1.ProgressMaximum = value;
      }
    }

    /// <summary>
    /// Задает или получает минимальное значение ProgressBar.
    /// </summary>
    public int ProgressMinimum
    {
      get
      {
        return ProgressBar1.ProgressMinimum;
      }
      set
      {
        ProgressBar1.ProgressMinimum = value;
      }
    }

    /// <summary>
    /// Задает или получает текущее значение ProgressBar.
    /// </summary>
    public int ProgressValue
    {
      get
      {
        return ProgressBar1.ProgressValue;
      }
      set
      {
        ProgressBar1.ProgressValue = value;
      }
    }

    /// <summary>
    /// Метод, который будет вызван при отмене операции пользователем.
    /// </summary>
    public Action CancelCallback
    {
      get
      {
        return ProgressBar1.CancelCallback;
      }
      set
      {
        ProgressBar1.CancelCallback = value;
      }
    }

    public Progress()
    {
      InitializeComponent();
    }

    public Progress(Form owner) : this()
    {
      this.Owner = owner;
      this.StartPosition = FormStartPosition.CenterParent;
    }

    private void SetText(string value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action<string>(this.SetText), value);
        return;
      }

      base.Text = value;
    }

  }

}
