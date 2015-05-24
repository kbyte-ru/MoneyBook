using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Базовый класс для сущностей БД.
  /// </summary>
  public abstract class Entity : IEntity
  {

    /// <summary>
    /// Статус текущего экземпляра объекта.
    /// </summary>
    public EntityStatus Status { get; internal set; }

  }

}