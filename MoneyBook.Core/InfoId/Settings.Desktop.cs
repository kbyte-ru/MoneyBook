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
      /// Приложения рабочего стола (500-599).
      /// </summary>
      public static class Desktop
      {

        /// <summary>
        /// Параметры доходов (500-549).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 501;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 502;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 503;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 510;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 511;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 512;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 513;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 520;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 521;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 522;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 523;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 530;

        }
        
        /// <summary>
        /// Параметры расходов (550-599).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 551;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 552;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 553;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 560;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 561;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 562;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 563;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 570;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 571;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 572;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 573;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 580;

        }

      }

    }

  }

}