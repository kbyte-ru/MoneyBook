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

        /// <summary>
        /// Параметры доходов (700-749).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 701;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 702;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 703;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 710;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 711;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 712;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 713;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 720;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 721;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 722;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 723;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 730;

        }
        
        /// <summary>
        /// Параметры расходов (750-799).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 751;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 752;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 753;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 760;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 761;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 762;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 763;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 770;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 771;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 772;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 773;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 780;

        }

      }

    }

  }

}