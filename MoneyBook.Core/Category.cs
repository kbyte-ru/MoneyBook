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
  /// Представляет категорию статьи расходов/доходов.
  /// </summary>
  public class Category
  {

    [Column("id_categories", SqlDbType.Int, ColumnFlags.PrimaryKey | ColumnFlags.Identity)]
    public int Id { get; set; }

    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    [Column("parent_id", SqlDbType.Int)]
    public int ParentId { get; set; }

    [Column("category_name", SqlDbType.NVarChar, Size = 100)]
    public string Name { get; set; }

    // TODO: https://github.com/alekseynemiro/MoneyBook/issues/10

    [Column("fore_color", SqlDbType.Int)]
    public int ForeColor { get; set; }
    
    [Column("back_color", SqlDbType.Int)]
    public int BackColor { get; set; }

    // --

    [Column("font_style", SqlDbType.Int)]
    public int FontStyle { get; set; }

    [Column("total_entries", SqlDbType.Int)]
    public int TotalEntries { get; set; }

    [Column("last_operation", SqlDbType.DateTime)]
    public DateTime? LastOperation { get; set; }

    [Column("date_created", SqlDbType.DateTime)]
    public DateTime DateCreated { get; set; }

  }

}