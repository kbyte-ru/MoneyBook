using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Перечень типов категорий.
  /// </summary>
  public enum CategoryType : byte
  {
    None = 0,
    /// <summary>
    /// Доходы.
    /// </summary>
    Incomes = 1,
    /// <summary>
    /// Расходы.
    /// </summary>
    Expenses = 2
  }

}