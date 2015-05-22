using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MoneyBook.Core.Data;
using System.Text.RegularExpressions;

namespace MoneyBook.Core
{

  public class User
  {

    #region ..свойства..

    /// <summary>
    /// Строка соединения с базой данных текущего экземпляра пользователя.
    /// </summary>
    private string ConnectionString = "";


    private List<Account> _Accounts = null;

    /// <summary>
    /// Список счетов пользователя.
    /// </summary>
    public List<Account> Accounts
    {
      get
      {
        if (_Accounts == null)
        {
          _Accounts = this.GetAccounts();
        }
        return _Accounts;
      }
    }

    private List<Category> _Categories = null;

    /// <summary>
    /// Список категорий.
    /// </summary>
    public List<Category> Categories
    {
      get
      {
        if (_Categories == null)
        {
          _Categories = this.GetCategories();
        }
        return _Categories;
      }
    }

    private List<Currency> _Currencies = null;

    /// <summary>
    /// Список валют.
    /// </summary>
    public List<Currency> Currencies
    {
      get
      {
        if (_Currencies == null)
        {
          _Currencies = this.GetCurrencies();
        }
        return _Currencies;
      }
    }

    #endregion
    #region ..конструктор и деструктор..

    public User(string path, string username, string password)
    {
      // проверка существования файла базы
      string filePath = Path.Combine(path, String.Format("{0}.mbk", username));
      if (!File.Exists(filePath))
      {
        throw new Exception("Пользователь не найден.");
      }
      // загрузка данных
      this.ConnectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        // счета
        // справочники
        // расходы
        // доходы
      }
    }

    #endregion
    #region ..методы..

    /// <summary>
    /// Возвращает список записей.
    /// </summary>
    /// <param name="accountId">Идентификатор счета. По умолчанию, ноль - любой счет.</param>
    /// <param name="categoryId">Идентификатор категории. По умолчанию, ноль - все категории.</param>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <param name="amountFrom">Сумма от.</param>
    /// <param name="amountTo">Сумма до.</param>
    /// <param name="search">Строка поиска.</param>
    public Entries GetEntries(int accountId = 0, int categoryId = 0, DateTime? dateFrom = null, DateTime? dateTo = null, decimal? amountFrom = null, decimal? amountTo = null, string search = null)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Меняет пароль.
    /// </summary>
    /// <param name="newPassword">Пароль, который следует установить.</param>
    public void SetPassword(string newPassword)
    {
    }

    /// <summary>
    /// Возвращает список счетов.
    /// </summary>
    private List<Account> GetAccounts()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [accounts] ORDER BY [account_name], [id_accounts]";
        return client.GetEntities<Account>();
      }
    }

    /// <summary>
    /// Возвращает список категорий расходов/доходов.
    /// </summary>
    private List<Category> GetCategories()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [categories]";
        return client.GetEntities<Category>();
      }
    }

    /// <summary>
    /// Возвращает список валют.
    /// </summary>
    private List<Currency> GetCurrencies()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [currencies] ORDER BY [priority]";
        return client.GetEntities<Currency>();
      }
    }

    #endregion
    #region ..статичные методы..

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="path">Путь к каталогу, в который следует создать профиль пользователя.</param>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль к файлу базы.</param>
    public static User Create(string path, string username, string password)
    {
      // 0. Проверка имени пользователя
      if (username.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
      {
        throw new Exception("Имя пользователя содержит недопустимые символы."); //InvalidUserNameException();
      }

      // 1. Проверка файлов
      string filePath = Path.Combine(path, String.Format("{0}.mbk", username));
      // проверка каталога
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      // проверка уникальности имени файла
      if (File.Exists(filePath))
      {
        throw new Exception("Пользователь с таким именем уже существует.");
      }

      // 2. Создает новую базу данных
      string connectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      SqlDbCeClient.CreateDatabase(connectionString);

      // 3. Подключение к базе
      using (var client = new SqlDbCeClient(connectionString))
      {
        // создаем необходимые таблицы
        string[] queries = Regex.Split(MoneyBookUtility.GetEmbeddedResourceString("DbInit.sql"), "^GO(;|)\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        foreach (string query in queries)
        {
          if (String.IsNullOrEmpty(query)) { continue; }
          client.ExecuteNonQuery(query);
        }

        // наполнение базы данными по умолчанию
        // список валют

        // типы счетов
        foreach (var line in Resources.DefaultData.AccountTypes.Split('\n'))
        {
          var item = line.Split('|');
          // item[0]
          if (item.Length >= 2)
          {
            // иконка 
            // item[1]
          }
        }

        // счет по умолчанию

        // статьи доходов
        // статьи расходов
        // категории
      }

      return new User(path, username, password);
    }

    #endregion

  }

}