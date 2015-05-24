using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;
using MoneyBook.Core.Data.Enums;

namespace MoneyBook.Core
{
  
  /// <summary>
  /// Представляет категорию статьи расходов/доходов.
  /// </summary>
  [Serializable]
  [Table("categories")]
  public class Category : UserMoneyObject
  {

    /// <summary>
    /// Уникальный идентификатор категории.
    /// </summary>
    [PrimaryKey]
    [Column("id_categories", SqlDbType.Int)]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    /// <summary>
    /// Идентификатор родительской категории. Ноль - категория не имеет родителя.
    /// </summary>
    [Column("parent_id", SqlDbType.Int)]
    public int ParentId { get; set; }

    /// <summary>
    /// Название категории.
    /// </summary>
    [Column("category_name", SqlDbType.NVarChar, Size = 100)]
    public string Name { get; set; }

    /// <summary>
    /// Цвета шрифта.
    /// </summary>
    [Column("fore_color", SqlDbType.Int)]
    internal int ForeColorArgb { get; set; }

    /// <summary>
    /// Цвет шрифта.
    /// </summary>
    public Color ForeColor
    {
      get
      {
        return Color.FromArgb(this.ForeColorArgb);
      }
      set
      {
        this.ForeColorArgb = value.ToArgb();
      }
    }

    /// <summary>
    /// Цвет фона.
    /// </summary>
    [Column("back_color", SqlDbType.Int)]
    internal int BackColorArgb { get; set; }

    /// <summary>
    /// Цвет фона.
    /// </summary>
    public Color BackColor
    {
      get
      {
        return Color.FromArgb(this.BackColorArgb);
      }
      set
      {
        this.BackColorArgb = value.ToArgb();
      }
    }

    /// <summary>
    /// Стиль шрифта.
    /// </summary>
    [Column("font_style", SqlDbType.Int)]
    public FontStyle FontStyle { get; set; }

    /// <summary>
    /// Тип категории.
    /// </summary>
    [Column("category_type", SqlDbType.TinyInt)]
    public EntryType CategoryType { get; set; }

    /// <summary>
    /// Общее число записей в категории.
    /// </summary>
    [Column("total_entries", SqlDbType.Int)]
    public int TotalEntries { get; set; }

    /// <summary>
    /// Дата и время последней операции в данной категории.
    /// </summary>
    [AllowNull]
    [Column("last_operation", SqlDbType.DateTime)]
    public DateTime? LastOperation { get; set; }

    /// <summary>
    /// Дата и время создания категории.
    /// </summary>
    [Column("date_created", SqlDbType.DateTime, Default = DefaultValues.Now)]
    public DateTime DateCreated { get; set; }

  }

}