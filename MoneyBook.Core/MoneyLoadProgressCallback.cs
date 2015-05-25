using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public delegate void MoneyLoadProgressCallback(object sender, MoneyLoadEventArgs e);

  /// <summary>
  /// </summary>
  public class MoneyLoadEventArgs : EventArgs
  {

    //public int TotalRecords

  }

}