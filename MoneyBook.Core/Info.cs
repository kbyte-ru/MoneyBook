using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;

namespace MoneyBook.Core
{
  
  /// <summary>
  /// Предствляет техническую информацию о файле профиля пользователя.
  /// </summary>
  public class Info
  {

    #region ..константы..

    // 0-99 - информация об инициализации и последнем использовании файла

    /// <summary>
    /// Платформа, на который был создан файл: desktop, mobile, web.
    /// </summary>
    public const int InitialPlatform = 0;

    /// <summary>
    /// Название операционной системы, в которой был создан файл.
    /// </summary>
    public const int InitialSystemName = 1;

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
    /// Версия ядра, которое использовалось для создания профиля пользователя.
    /// </summary>
    public const int InitialCoreVersion = 6;

    /// <summary>
    /// Код культуры, в которой был создан файл.
    /// </summary>
    public const int InitialCulture = 7;

    /// <summary>
    /// Пользователь, которым был создан файл.
    /// </summary>
    public const int InitialUser = 8;

    /// <summary>
    /// Разрешение экрана.
    /// </summary>
    public const int InitialScreenResolution = 9;

    /// <summary>
    /// Дата и время инициализации.
    /// </summary>
    public const int InitialDateTime = 10;

    /// <summary>
    /// Часовой пояс <see cref="InitialDateTime"/>.
    /// </summary>
    public const int InitialTimeZone = 11;

    // TODO:

    public const int LastPlatform = 50;
    public const int LastProgramName = 51;
    public const int LastProgramVersion = 52;
    public const int LastCoreVersion = 53;
    public const int LastCulture = 54;
    public const int LastDateTime = 60;
    public const int LastTimeZone = 61;

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

    /// <summary>
    /// Экземпляр пользователя, к которому относится информация.
    /// </summary>
    private User CurrentUser = null;

    /// <summary>
    /// Коллекция записей.
    /// </summary>
    private Dictionary<int, string> Items = null;

    /// <summary>
    /// Возвращает значение указанного параметра.
    /// </summary>
    /// <param name="id">Идентификатор параметра, значение которого следует получить.</param>
    public string this[int id]
    {
      get
      {
        if (!this.Items.ContainsKey(id))
        {
          // нет в памяти данных для этого ключа, получаем из базы
          this.Items.Add(id, this.GetValue(id));
        }
        return this.Items[id];
      }
    }

    #endregion
    #region ..конструктор..

    public Info(User u)
    {
      this.Items = new Dictionary<int, string>();
      this.CurrentUser = u;
    }

    #endregion
    #region ..методы..

    /// <summary>
    /// Получает из базы и возвращает значение по указанному ключу.
    /// </summary>
    /// <param name="key">Ключ, значение для которого следует получить.</param>
    private string GetValue(int id)
    {
      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT [value] FROM [info] WHERE [id_info] = @id";
        client.Parameters.Add("@id", SqlDbType.Int).Value = id;
        object result = client.ExecuteScalar();
        if (result == DBNull.Value) { return null; }
        return Convert.ToString(result);
      }
    }

    #endregion

  }

}