using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Представляет идентификатор элемента информации.
  /// </summary>
  public struct InfoId
  {

    #region ..константы..

    // 0-99 - информация об инициализации и последнем использовании файла

    /// <summary>
    /// Тип приложения, для которого был создан файл: desktop, mobile или web.
    /// </summary>
    public const int InitialAppType = 0;

    /// <summary>
    /// Идентификатор операционной системы, в которой был создан файл.
    /// </summary>
    public const int InitialSystemID = 1;

    /// <summary>
    /// Версия операционной системы, в которой был создан файл.
    /// </summary>
    public const int InitialSystemVersion = 2;

    /// <summary>
    /// Версия .NET Framework.
    /// </summary>
    public const int InitialNetVersion = 3;

    /// <summary>
    /// Название программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const int InitialProgramName = 4;

    /// <summary>
    /// Версия программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const int InitialProgramVersion = 5;

    /// <summary>
    /// Имя ядра, которое использовалось для создания профиля пользователя.
    /// </summary>
    public const int InitialCoreName = 6;

    /// <summary>
    /// Версия ядра, которое использовалось для создания профиля пользователя.
    /// </summary>
    public const int InitialCoreVersion = 7;

    /// <summary>
    /// Дата и время инициализации.
    /// </summary>
    public const int InitialDateTime = 10;

    /// <summary>
    /// Часовой пояс <see cref="InitialDateTime"/>.
    /// </summary>
    public const int InitialTimeZone = 11;

    /// <summary>
    /// Код культуры, в которой был создан файл.
    /// </summary>
    public const int InitialCulture = 12;

    /// <summary>
    /// Разрешение экрана.
    /// </summary>
    public const int InitialScreenResolution = 20;

    /// <summary>
    /// Имя машины, на которой был создан файл.
    /// </summary>
    public const int InitialMachineName = 30;

    /// <summary>
    /// Имя пользователь, который создал файл.
    /// </summary>
    public const int InitialUserName = 31;

    /// <summary>
    /// Доменнное имя веб-сайта, на котором был создан файл профиля.
    /// </summary>
    public const int InitialDomainName = 32;

    /// <summary>
    /// IP-адрес пользователя.
    /// </summary>
    public const int InitialUserHostAddress = 33;

    /// <summary>
    /// DNS-имя удаленного клиента.
    /// </summary>
    public const int InitialUserHostName = 34;

    /// <summary>
    /// IP-адрес сервера.
    /// </summary>
    public const int InitialServerHostAddress = 35;

    /// <summary>
    /// Имя узла локального компьютера.
    /// </summary>
    public const int InitialServerHostName = 36;

    // TODO:

    /*
    public const int LastPlatform = 50;
    public const int LastProgramName = 51;
    public const int LastProgramVersion = 52;
    public const int LastCoreVersion = 53;
    public const int LastCulture = 54;
    public const int LastDateTime = 60;
    public const int LastTimeZone = 61;
    */

    // 100-199 - статистика

    /// <summary>
    /// Суммарное число сессий.
    /// </summary>
    public const int TotalSessions = 100;

    /// <summary>
    /// Число сессий в desktop-приложениях.
    /// </summary>
    public const int DesktopSessions = 101;

    /// <summary>
    /// Число сессий на мобильных платформах.
    /// </summary>
    public const int MobileSessions = 102;

    /// <summary>
    /// Число сессий в web-приложениях.
    /// </summary>
    public const int WebSessions = 103;

    /// <summary>
    /// Суммарное время работы с файлом (секунд).
    /// </summary>
    public const int TotalTime = 110;

    // 1000-9999 - пользовательские параметры

    #endregion
    #region ..свойства..

    private int _Value;

    /// <summary>
    /// Значение.
    /// </summary>
    public int Value
    {
      get
      {
        return _Value;
      }
      set
      {
        _Value = value;
      }
    }
    
    #endregion
    #region ..конструктор..

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="InfoId"/> с указанным значением.
    /// </summary>
    /// <param name="value">Значение.</param>
    internal InfoId(int value)
    {
      _Value = value;
    }
    
    #endregion
    #region ..методы..

    /// <summary>
    /// Возвращает значение текущего экземпляра.
    /// </summary>
    public override string ToString()
    {
      return this.Value.ToString();
    }

    /// <summary>
    /// Возвращает хэш-код текущего экземпляра.
    /// </summary>
    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }

    /// <summary>
    /// Проверяет, является ли текущий экземпляр эквивалентом указанному объекту или нет.
    /// </summary>
    /// <param name="obj">Объект, с которым следует провести сравнение.</param>
    public override bool Equals(object obj)
    {
      if (obj != null && obj.GetType() == typeof(InfoId))
      {
        return this.Equals(((InfoId)obj).Value);
      }

      return this.Value.Equals(obj);
    }

    /// <summary>
    /// Проверяет, является ли текущий экземпляр эквивалентом указанному экземпляру <see cref="InfoId"/>.
    /// </summary>
    /// <param name="value">Экземпляр <see cref="InfoId"/>, с которым следует провести сравнение.</param>
    public bool Equals(InfoId value)
    {
      return this.Value.Equals(value.Value);
    }

    #endregion
    #region ..операторы..

    public static implicit operator int(InfoId value)
    {
      return value.Value;
    }

    public static implicit operator InfoId(int value)
    {
      return new InfoId(value);
    }

    public static bool operator !=(InfoId x, InfoId y)
    {
      return !x.Equals(y.Value);
    }
    public static bool operator ==(InfoId x, InfoId y)
    {
      return x.Equals(y.Value);
    }

    public static bool operator !=(InfoId x, int y)
    {
      return !x.Equals(new InfoId(y));
    }
    public static bool operator ==(InfoId x, int y)
    {
      return x.Equals(new InfoId(y));
    }

    public static bool operator !=(int x, InfoId y)
    {
      return !new InfoId(x).Equals(y.Value);
    }
    public static bool operator ==(int x, InfoId y)
    {
      return new InfoId(x).Equals(y.Value);
    }

    #endregion

  }

}