using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;

namespace MoneyBook.Core
{

  /// <summary>
  /// Представляет элемент информации.
  /// </summary>
  [Serializable]
  [Table("info")]
  [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
  public class InfoItem : IEntity
  {

    /// <summary>
    /// Уникальный идентификатор записи.
    /// </summary>
    [PrimaryKey]
    [Column("id_info", SqlDbType.SmallInt)]
    public short Id { get; set; }

    /// <summary>
    /// Значение.
    /// </summary>
    [Column("value", SqlDbType.NVarChar, Size = 30)]
    public string Value { get; set; }

  }


}