using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Перечень типов приложений.
  /// </summary>
  public enum ApplicationType
  {
    /// <summary>
    /// Нет данных.
    /// </summary>
    None,
    /// <summary>
    /// Приложение рабочего стола.
    /// </summary>
    Desktop,
    /// <summary>
    /// Мобильное приложение.
    /// </summary>
    Mobile,
    /// <summary>
    /// Веб-приложение.
    /// </summary>
    Web
  }

}