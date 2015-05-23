﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Представляет коллекцию записей расходов/доходов.
  /// </summary>
  [Serializable]
  public class Entries : List<Entry>
  {

    /// <summary>
    /// Текущая страница.
    /// </summary>
    public int CurrentPage { get; internal set; }

    /// <summary>
    /// Всего записей в базе.
    /// </summary>
    public int TotalRecords { get; internal set; }

    /// <summary>
    /// Максимальное число записей на одной странице.
    /// </summary>
    public int MaxDataPerPage { get; internal set; }

  }

}