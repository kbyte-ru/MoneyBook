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
  /// Представляет информацию о валюте.
  /// </summary>
  [Table("currencies")]
  public class Currency : IUserObject
  {

    /// <summary>
    /// Уникальный буквенный код валюты. В соответствии со стандартами ISO 4217.
    /// </summary>
    [Column("id_currencies", SqlDbType.NVarChar, ColumnFlags.PrimaryKey, Size = 3)]
    public string Code { get; set; }

    /// <summary>
    /// Идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    /// <summary>
    /// Полное название валюты.
    /// </summary>
    [Column("long_name", SqlDbType.NVarChar, Size = 50)]
    public string LongName { get; set; }

    /// <summary>
    /// Сокращённое название валюты.
    /// </summary>
    [Column("short_name", SqlDbType.NVarChar, Size = 10)]
    public string ShortName { get; set; }

    /// <summary>
    /// Приоритет TODO: понять, как и зачем
    /// </summary>
    [Column("priority", SqlDbType.Int)]
    public int Priority { get; set; }

    /// <summary>
    /// Возвращает строковое представление информации о валюте, расположенной в текущем экземпляре класса.
    /// </summary>
    public override string ToString()
    {
      if (!String.IsNullOrEmpty(this.LongName) && !String.IsNullOrEmpty(this.ShortName) && !String.IsNullOrEmpty(this.Code))
      {
        return String.Format("{0}, {1} ({2})", this.LongName, this.ShortName, this.Code);
      }
      else if (!String.IsNullOrEmpty(this.LongName) && String.IsNullOrEmpty(this.ShortName) && !String.IsNullOrEmpty(this.Code))
      {
        return String.Format("{0} ({1})", this.LongName, this.Code);
      }
      else if (String.IsNullOrEmpty(this.LongName) && !String.IsNullOrEmpty(this.ShortName) && !String.IsNullOrEmpty(this.Code))
      {
        return String.Format("{0} ({1})", this.ShortName, this.Code);
      }
      else if (String.IsNullOrEmpty(this.LongName) && String.IsNullOrEmpty(this.ShortName) && !String.IsNullOrEmpty(this.Code))
      {
        return this.Code;
      }
      else
      {
        return base.ToString();
      }
    }

  }

}