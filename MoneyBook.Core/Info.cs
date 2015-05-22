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

    // TODO: Рожать
    
    /// <summary>
    /// Название программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const string ProgramName = "program name";

    /// <summary>
    /// Версия программы, в которой был создан файл профиля пользователя.
    /// </summary>
    public const string ProgramVersion = "program version";

    /// <summary>
    /// Версия ядра, которое использовалось для создания профиля пользователя.
    /// </summary>
    public const string CoreVersion = "core version";

    public const string InitialCulture = "initial culture";

    public const string LastCulture = "last culture";

    #endregion
    #region ..свойства..

    /// <summary>
    /// Экземпляр пользователя, к которому относится информация.
    /// </summary>
    private User CurrentUser = null;

    /// <summary>
    /// Коллекция записей.
    /// </summary>
    private NameValueCollection Items = null;

    /// <summary>
    /// Возвращает значение указанного параметра.
    /// </summary>
    /// <param name="key">Имя параметра, значение которого следует получить.</param>
    public string this[string key]
    {
      get
      {
        if (this.Items[key] == null)
        {
          // нет в памяти данных для этого ключа, получаем из базы
          this.Items[key] = this.GetValue(key);
        }
        return this.Items[key];
      }
    }

    #endregion
    #region ..конструктор..

    public Info(User u)
    {
      this.Items = new NameValueCollection();
      this.CurrentUser = u;
    }

    #endregion
    #region ..методы..

    /// <summary>
    /// Получает из базы и возвращает значение по указанному ключу.
    /// </summary>
    /// <param name="key">Ключ, значение для которого следует получить.</param>
    private string GetValue(string key)
    {
      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT TOP 1 [value] FROM [info] WHERE [key] = @key";
        client.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = key;
        object result = client.ExecuteScalar();
        if (result == DBNull.Value) { return ""; }
        return Convert.ToString(result);
      }
    }

    #endregion

  }

}