using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Указывает на то, что значение поля уникально и его можно использовать при проверке дубликатов записей.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class UniqueValueAttribute : Attribute
  {
  }

}