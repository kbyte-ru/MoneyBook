using System;

namespace MoneyBook.Core
{

  public partial struct InfoId
  {

    /// <summary>
    /// Hастройки приложений (700-799).
    /// </summary>
    public static partial class Settings
    {

      /// <summary>
      /// Web-приложения (700-799).
      /// </summary>
      public static class Web
      {

        // общие параметры 700-719

        /// <summary>
        /// Параметры доходов (720-759).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 721;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 722;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 723;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 730;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 731;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 732;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 733;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 740;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 741;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 742;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 743;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 750;

        }
        
        /// <summary>
        /// Параметры расходов (760-799).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 761;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 762;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 763;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 770;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 771;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 772;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 773;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 780;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 781;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 782;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 783;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 790;

        }

      }

    }

  }

}