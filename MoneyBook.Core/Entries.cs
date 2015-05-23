using System;
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
  }

}