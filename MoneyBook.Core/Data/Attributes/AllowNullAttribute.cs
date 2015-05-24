using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Указывает, что значение поля может иметь значение <see cref="DBNull.Value"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class AllowNullAttribute : Attribute
  {
  }

}