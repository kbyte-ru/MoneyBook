using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{
  
  /// <summary>
  /// Предствляет техническую информацию о файле профиля пользователя.
  /// </summary>
  public class Info
  {

    #region ..константы..

    // TODO: Рожать

    /// <summary>
    /// Название программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const string ProgramName = "ProgramName";

    /// <summary>
    /// Версия программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const string ProgramVersion = "ProgramVersion";

    /// <summary>
    /// Версия ядра, которое использовалось для создания профиля пользователя.
    /// </summary>
    public const string CoreVersion = "CoreVersion";

    public const string Culture = "Culture";

    public const string LastCulture = "LastCulture";

    #endregion
    #region ..свойства..

    /// <summary>
    /// Коллекция записей.
    /// </summary>
    public NameValueCollection Items { get; set; }

    #endregion
    #region ..конструктор..

    public Info()
    {
      this.Items = new NameValueCollection();
    }

    #endregion
    #region ..методы..



    #endregion

  }

}