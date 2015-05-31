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

        // общие параметры 500-519

        /// <summary>
        /// Ширина окна.
        /// </summary>
        public const short WindowWidth = 501;

        /// <summary>
        /// Высота окна.
        /// </summary>
        public const short WindowHeight = 502;

        /// <summary>
        /// Состояние окна.
        /// </summary>
        public const short WindowState = 503;

        /// <summary>
        /// Позиция по X.
        /// </summary>
        public const short WindowLeft = 504;

        /// <summary>
        /// Позиция по Y.
        /// </summary>
        public const short WindowTop = 505;

        /// <summary>
        /// Параметры доходов (520-559).
        /// </summary>
        public static class Incomes
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 521;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 522;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 523;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 530;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 531;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 532;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 533;
          
          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 540;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 541;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 542;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 543;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 550;

        }
        
        /// <summary>
        /// Параметры расходов (560-599).
        /// </summary>
        public static class Expenses
        {

          /// <summary>
          /// Идентификатор последнего счета.
          /// </summary>
          public const short AccountId = 561;

          /// <summary>
          /// Идентификатор последней категории.
          /// </summary>
          public const short CategoryId = 562;

          /// <summary>
          /// Идентификатор последней подкатегории.
          /// </summary>
          public const short SubcategoryId = 563;

          /// <summary>
          /// Период.
          /// </summary>
          public const short Period = 570;

          /// <summary>
          /// Значение периода.
          /// </summary>
          public const short PeriodValue = 571;

          /// <summary>
          /// Дата от...
          /// </summary>
          public const short DateForm = 572;

          /// <summary>
          /// Дата до...
          /// </summary>
          public const short DateTo = 573;

          /// <summary>
          /// Сумма.
          /// </summary>
          public const short Amount = 580;

          /// <summary>
          /// Код валюты.
          /// </summary>
          public const short CurrencyCode = 581;

          /// <summary>
          /// Сумма от...
          /// </summary>
          public const short AmountFrom = 582;

          /// <summary>
          /// Сумма до...
          /// </summary>
          public const short AmountTo = 583;

          /// <summary>
          /// Поиск.
          /// </summary>
          public const short Search = 590;

        }

      }

    }

  }

}