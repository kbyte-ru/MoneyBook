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
  public class Currency
  {

    [Column("id_currencies", SqlDbType.NVarChar, ColumnFlags.PrimaryKey, Size = 3)]
    public string Code { get; set; }

    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    [Column("long_name", SqlDbType.NVarChar, Size = 50)]
    public string LongName { get; set; }

    [Column("short_name", SqlDbType.NVarChar, Size = 10)]
    public string ShortName { get; set; }

    [Column("priority", SqlDbType.Int)]
    public int Priority { get; set; }

  }

}