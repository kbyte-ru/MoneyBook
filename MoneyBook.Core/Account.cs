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
  /// Представляет счет.
  /// </summary>
  [Serializable]
  [Table("accounts")]
  public class Account: UserMoneyObject
  {

    /// <summary>
    /// Уникальный идентификатор счета.
    /// </summary>
    [PrimaryKey]
    [Column("id_accounts",  SqlDbType.Int)]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор типа счета.
    /// </summary>
    [Column("id_account_types", SqlDbType.Int)]
    public int AccountTypeId { get; set; }

    /// <summary>
    /// Трехзначный код валюты.
    /// </summary>
    [Column("id_currencies", SqlDbType.NVarChar, Size = 3)]
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }
    
    /// <summary>
    /// Название счета.
    /// </summary>
    [Column("account_name", SqlDbType.NVarChar, Size = 100)]
    public string Name { get; set; }

    /// <summary>
    /// Детальная информация о счете.
    /// </summary>
    [Column("account_details", SqlDbType.NVarChar, Size = 4000)]
    public string Details { get; set; }

    #region todo: подумать об именах
    
    // TODO: https://github.com/alekseynemiro/MoneyBook/issues/9

    [Column("total_income_entries", SqlDbType.Int)]
    public int TotalIncomeEntries { get; set; }

    [Column("total_expense_entries", SqlDbType.Int)]
    public int TotalExpenseEntries { get; set; }

    #endregion

    /// <summary>
    /// Дата и время последней операции по данному счету.
    /// </summary>
    [AllowNull]
    [Column("last_operation", SqlDbType.DateTime)]
    public DateTime? LastOperation { get; set; }

    /// <summary>
    /// Дата и время создания счета.
    /// </summary>
    [Column("date_created", SqlDbType.DateTime, Default = DefaultValues.Now)]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Возвращает название и идентификатор текущего экземпляра счета.
    /// </summary>
    public override string ToString()
    {
      return String.Format("{0} (id: {1})", this.Name, this.Id);
    }
   
  }

}