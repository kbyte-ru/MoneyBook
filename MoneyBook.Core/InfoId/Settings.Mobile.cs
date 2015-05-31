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

        // общие параметры 600-619
        
        /// <summary>
        /// Параметры доходов (620-659).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 621;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 622;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 623;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 630;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 631;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 632;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 633;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 640;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 641;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 642;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 643;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 650;

        }
        
        /// <summary>
        /// Параметры расходов (660-699).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 661;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 662;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 663;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 670;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 671;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 672;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 673;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 680;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 681;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 682;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 683;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 690;

        }

      }

    }

  }

}