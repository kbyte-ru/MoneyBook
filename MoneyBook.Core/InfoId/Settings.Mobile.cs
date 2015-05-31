using System;

namespace MoneyBook.Core
{

  public partial struct InfoId
  {

    /// <summary>
    /// Hастройки приложений (500-999).
    /// </summary>
    public static partial class Settings
    {

      /// <summary>
      /// Мобильные приложения (600-699).
      /// </summary>
      public static class Mobile
      {

        /// <summary>
        /// Параметры доходов (600-649).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 601;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 602;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 603;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 610;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 611;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 612;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 613;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 620;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 621;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 622;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 623;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 630;

        }
        
        /// <summary>
        /// Параметры расходов (650-699).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 651;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 652;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 653;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 660;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 661;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 662;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 663;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 670;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 671;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 672;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 673;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 680;

        }

      }

    }

  }

}