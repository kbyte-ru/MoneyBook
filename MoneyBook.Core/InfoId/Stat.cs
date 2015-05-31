using System;

namespace MoneyBook.Core
{

  public partial struct InfoId
  {

    /// <summary>
    /// Статистика (100-199).
    /// </summary>
    public static class Stat
    {

      /// <summary>
      /// Суммарное число сессий.
      /// </summary>
      public const short TotalSessions = 100;

      /// <summary>
      /// Число сессий в desktop-приложениях.
      /// </summary>
      public const short DesktopSessions = 101;

      /// <summary>
      /// Число сессий на мобильных платформах.
      /// </summary>
      public const short MobileSessions = 102;

      /// <summary>
      /// Число сессий в web-приложениях.
      /// </summary>
      public const short WebSessions = 103;

      /// <summary>
      /// Суммарное время работы с файлом (секунд).
      /// </summary>
      public const short TotalTime = 110;

    }

  }

}