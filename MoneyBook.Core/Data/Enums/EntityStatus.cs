using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Перечень статусов сущностей.
  /// </summary>
  public enum EntityStatus
  {
    /// <summary>
    /// Текущий экземпляр еще не использовался для работы с базой.
    /// </summary>
    None,
    /// <summary>
    /// Информация в базе данных не была найдена.
    /// </summary>
    NotFound,
    /// <summary>
    /// Загружены в экземпляр объекта данные из базы.
    /// </summary>
    Loaded,
    /// <summary>
    /// Добавлена запись в базу.
    /// </summary>
    Created,
    /// <summary>
    /// Обновлена существующая запись.
    /// </summary>
    Updated,
    /// <summary>
    /// Удалена запись из базы.
    /// </summary>
    Deleted
  }

}