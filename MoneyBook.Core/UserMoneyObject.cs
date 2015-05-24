using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;

namespace MoneyBook.Core
{

  /// <summary>
  /// Базовый класс для объектов базы данных пользователя.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Все объекты пользователя должны наследоваться от этого класса, 
  /// чтобы иметь возможность работать с ними через методы <see cref="User.Save(UserMoneyObject)"/> и <see cref="User.Delete(UserMoneyObject)"/>.
  /// </para>
  /// </remarks>
  public abstract class UserMoneyObject : Entity
  {
  }

}