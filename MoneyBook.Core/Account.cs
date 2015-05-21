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
  public class Account
  {

    [Column("id_accounts",  SqlDbType.Int, ColumnFlags.PrimaryKey | ColumnFlags.Identity)]
    public int Id { get; set; }

    [Column("id_account_types", SqlDbType.Int)]
    public int AccountTypeId { get; set; }

    [Column("id_currencies", SqlDbType.NVarChar, Size = 3)]
    public string CurrencyCode { get; set; }

    [Column("id_icons", SqlDbType.Int)]
    public int IconId { get; set; }
    
    [Column("account_name", SqlDbType.NVarChar, Size = 100)]
    public string Name { get; set; }

    [Column("account_details", SqlDbType.NVarChar, Size = 4000)]
    public string Details { get; set; }

    #region todo: подумать об именах
    
    // TODO: https://github.com/alekseynemiro/MoneyBook/issues/9

    [Column("total_income_entries", SqlDbType.Int)]
    public int TotalIncomeEntries { get; set; }

    [Column("total_expense_entries", SqlDbType.Int)]
    public int TotalExpenseEntries { get; set; }

    #endregion

    [Column("last_operation", SqlDbType.DateTime)]
    public DateTime? LastOperation { get; set; }

    [Column("date_created", SqlDbType.DateTime)]
    public DateTime DateCreated { get; set; }

  }

}