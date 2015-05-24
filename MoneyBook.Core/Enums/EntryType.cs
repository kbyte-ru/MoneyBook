using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Перечень типов записей.
  /// </summary>
  public enum EntryType : byte
  {
    /// <summary>
    /// Непонятно.
    /// </summary>
    None = 0,
    /// <summary>
    /// Доход.
    /// </summary>
    Income = 1,
    /// <summary>
    /// Расход.
    /// </summary>
    Expense = 2
  }

}