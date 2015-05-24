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
  /// Представляет запись о расходах/доходах.
  /// </summary>
  [Serializable]
  [Table("items")]
  public class MoneyItem : UserMoneyObject
  {

    /// <summary>
    /// Уникальный идентификатор записи.
    /// </summary>
    [Column("id_items", SqlDbType.Int, ColumnFlags.PrimaryKey | ColumnFlags.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    [Column("id_categories", SqlDbType.Int)]
    public int CategoryId { get; set; }

    /// <summary>
    /// Идентификатор счета.
    /// </summary>
    [Column("id_accounts", SqlDbType.Int)]
    public int AccountId { get; set; }

    /// <summary>
    /// Идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }

    /// <summary>
    /// Заголовок записи.
    /// </summary>
    [Column("title", SqlDbType.NVarChar, Size = 100)]
    public string Title { get; set; }

    /// <summary>
    /// Описание записи.
    /// </summary>
    [Column("description", SqlDbType.NVarChar, Size = 4000)]
    public string Description { get; set; }
    
    /// <summary>
    /// Сумма.
    /// </summary>
    [Column("amount", SqlDbType.Money)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Дата записи. По этому полю осуществляется фильтрация записей по датам.
    /// </summary>
    [Column("date_entry", SqlDbType.DateTime, Default = DefaultValues.Now)]
    public DateTime DateEntry { get; set; }

    /// <summary>
    /// Дата и время последнего обновления записи.
    /// </summary>
    [Column("date_updated", SqlDbType.DateTime, ColumnFlags.AllowNull)]
    public DateTime? DateUpdated { get; set; }

    /// <summary>
    /// Дата и время фактического создания записи.
    /// </summary>
    [Column("date_created", SqlDbType.DateTime, Default = DefaultValues.Now)]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Тип записи.
    /// </summary>
    [Column("entry_type", SqlDbType.TinyInt)]
    public EntryType EntryType { get; set; }

  }

}