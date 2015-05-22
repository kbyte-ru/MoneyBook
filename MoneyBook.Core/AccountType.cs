using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;
using MoneyBook.Core.Data.Enums;

namespace MoneyBook.Core
{
 
  /// <summary>
  /// Представляет тип счета.
  /// </summary>
  [Table("account_types")]
  public class AccountType
  {

    /// <summary>
    /// Уникальный идентификатор типа счета.
    /// </summary>
    [Column("id_account_types", SqlDbType.Int, ColumnFlags.PrimaryKey | ColumnFlags.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    /// <summary>
    /// Название типа счета.
    /// </summary>
    [Column("account_type_name", SqlDbType.NVarChar, Size = 50)]
    public string Name { get; set; }


    /// <summary>
    /// Возвращает название и идентификатор текущего экземпляра типа счета.
    /// </summary>
    public override string ToString()
    {
      return String.Format("{0} (id: {1})", this.Name, this.Id);
    }

  }

}