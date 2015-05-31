using System;

namespace MoneyBook.Core
{

  public partial struct InfoId
  {

    /// <summary>
    /// Информация об инициализации и последнем использовании файла (0-99).
    /// </summary>
    public static class Initial
    {

      /// <summary>
      /// Тип приложения, для которого был создан файл: desktop, mobile или web.
      /// </summary>
      public const short AppType = 0;

      /// <summary>
      /// Идентификатор операционной системы, в которой был создан файл.
      /// </summary>
      public const short SystemID = 1;

      /// <summary>
      /// Версия операционной системы, в которой был создан файл.
      /// </summary>
      public const short SystemVersion = 2;

      /// <summary>
      /// Версия .NET Framework.
      /// </summary>
      public const short NetVersion = 3;

      /// <summary>
      /// Название программы, в которой был создан файл профиля пользователя.
      /// </summary>
      public const short ProgramName = 4;

      /// <summary>
      /// Версия программы, в которой был создан файл профиля пользователя.
      /// </summary>
      public const short ProgramVersion = 5;

      /// <summary>
      /// Имя ядра, которое использовалось для создания профиля пользователя.
      /// </summary>
      public const short CoreName = 6;

      /// <summary>
      /// Версия ядра, которое использовалось для создания профиля пользователя.
      /// </summary>
      public const short CoreVersion = 7;

      /// <summary>
      /// Номер версии структуры базы данных. На сегодняшний день: 1.0
      /// </summary>
      public const short ProfileVersion = 8;

      /// <summary>
      /// Дата и время инициализации.
      /// </summary>
      public const short DateTime = 10;

      /// <summary>
      /// Часовой пояс <see cref="InitialDateTime"/>.
      /// </summary>
      public const short TimeZone = 11;

      /// <summary>
      /// Код культуры, в которой был создан файл.
      /// </summary>
      public const short Culture = 12;

      /// <summary>
      /// Разрешение экрана.
      /// </summary>
      public const short ScreenResolution = 20;

      /// <summary>
      /// Имя машины, на которой был создан файл.
      /// </summary>
      public const short MachineName = 30;

      /// <summary>
      /// Имя пользователь, который создал файл.
      /// </summary>
      public const short UserName = 31;

      /// <summary>
      /// Доменнное имя веб-сайта, на котором был создан файл профиля.
      /// </summary>
      public const short DomainName = 32;

      /// <summary>
      /// IP-адрес пользователя.
      /// </summary>
      public const short UserHostAddress = 33;

      /// <summary>
      /// DNS-имя удаленного клиента.
      /// </summary>
      public const short UserHostName = 34;

      /// <summary>
      /// IP-адрес сервера.
      /// </summary>
      public const short ServerHostAddress = 35;

      /// <summary>
      /// Имя узла локального компьютера.
      /// </summary>
      public const short ServerHostName = 36;

      // TODO:

      /*
      public const short LastPlatform = 50;
      public const short LastProgramName = 51;
      public const short LastProgramVersion = 52;
      public const short LastCoreVersion = 53;
      public const short LastCulture = 54;
      public const short LastDateTime = 60;
      public const short LastTimeZone = 61;
      */

    }

  }

}