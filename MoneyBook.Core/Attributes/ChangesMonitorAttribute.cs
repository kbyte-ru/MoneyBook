using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Указывает на необходимость отслеживания изменений коллекций при использовании методов <see cref="User.Save(UserMoneyObject)"/> и <see cref="User.Delete(UserMoneyObject)"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  internal class ChangesMonitorAttribute : Attribute
  {
  }

}