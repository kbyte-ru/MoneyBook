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

  public class User : IDisposable
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
      // строка соединения
      this.ConnectionString = String.Format("Data Source={0}; password={1}", filePath, password);
    }
    
    public void Dispose()
    {
      // обновление информации
      // TODO
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
    /// Сохраняет указанные объекты (счет, категория, запись) в базе данных.
    /// </summary>
    /// <param name="entities">Список объектов, которые необходимо сохранить.</param>
    public void Save(List<IUserObject> entities)
    {
      if (entities == null)
      {
        throw new ArgumentNullException("entities");
      }

      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.SaveEntities<IUserObject>(entities);
      }
    }

    /// <summary>
    /// Сохраняет указанный объект (счет, категория, запись) в базе данных.
    /// </summary>
    /// <param name="entity">Объект, который необходимо сохранить.</param>
    public void Save(IUserObject entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.SaveEntity<IUserObject>(entity);
      }
    }

    /// <summary>
    /// Удаляет указанные объекты (счет, категория, запись) из базы данных.
    /// </summary>
    /// <param name="entities">Список объектов, которые необходимо удалить.</param>
    public int Delete(List<IUserObject> entities)
    {
      if (entities == null)
      {
        throw new ArgumentNullException("entities");
      }

      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        return client.DeleteEntities<IUserObject>(entities);
      }
    }

    /// <summary>
    /// Удаляет указанный объект (счет, категория, запись) из базы данных.
    /// </summary>
    /// <param name="entity">Объект, который необходимо удалить.</param>
    public int Delete(IUserObject entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        return client.DeleteEntity<IUserObject>(entity);
      }
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
      if (String.IsNullOrEmpty(path))
      {
        throw new ArgumentNullException("path");
      }
      if (String.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException("username");
      }

      // 0. Проверяем имя пользователя
      if (username.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
      {
        throw new ArgumentException("Имя пользователя содержит недопустимые символы.", "username");
      }

      // 1. Проверяем путь
      string filePath = Path.Combine(path, String.Format("{0}.mbk", username));
      // проверяем существование каталога
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      // проверяем уникальность имени файла
      if (File.Exists(filePath))
      {
        throw new Exception("Пользователь с таким именем уже существует.");
      }

      // 2. Создаем новую базу данных
      string connectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      SqlDbCeClient.CreateDatabase(connectionString);

      // 3. Подключяемся к базе
      using (var client = new SqlDbCeClient(connectionString))
      {
        // создаем необходимые таблицы
        string[] queries = Regex.Split(MoneyBookUtility.GetEmbeddedResourceString("DbInit.sql"), "^GO(;|)\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        foreach (string query in queries)
        {
          if (String.IsNullOrEmpty(query)) { continue; }
          client.ExecuteNonQuery(query);
        }

        // наполненяем базу данными по умолчанию
        // список валют
        var currencies = new List<Currency>();
        foreach (var line in Resources.DefaultData.Currencies.Split('\n'))
        {
          var item = line.Trim('\r', ' ').Split('|');
          currencies.Add
          (
            new Currency
            {
              Code = item[0],
              LongName = item[1],
              ShortName = item[2],
              IconId = Convert.ToInt32(item[3])
            }
          );
        }
        client.SaveEntities<Currency>(currencies);

        // типы счетов
        var accountTypes = new List<AccountType>();
        foreach (var line in Resources.DefaultData.AccountTypes.Split('\n'))
        {
          var item = line.Trim('\r', ' ').Split('|');
          accountTypes.Add
          (
            new AccountType
            {
              Name = item[0],
              IconId = Convert.ToInt32(item[1])
            }
          );
        }
        client.SaveEntities<AccountType>(accountTypes);

        // счет по умолчанию
        var defaultAccount = Resources.DefaultData.DefaultAccount.Trim('\r', ' ').Split('|');
        var account = new Account
        {
          CurrencyCode = defaultAccount[0],
          Name = defaultAccount[1],
          AccountTypeId = Convert.ToInt32(defaultAccount[2]),
          IconId = Convert.ToInt32(defaultAccount[3])
        };
        client.SaveEntity<Account>(account);

        // категории

      }

      return new User(path, username, password);
    }

    /// <summary>
    /// Удаляет профиль указанного пользвателя.
    /// </summary>
    /// <param name="path">Путь к каталогу, в котором следует искать файл профиля пользователя.</param>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>
    /// <para><b>true</b> - если профиль был удален.</para>
    /// <para><b>false</b> - если файл профиля не был найден.</para>
    /// </returns>
    public static bool Kill(string path, string username)
    {
      if (String.IsNullOrEmpty(path))
      {
        throw new ArgumentNullException("path");
      }
      if (String.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException("username");
      }

      string filePath = Path.Combine(path, String.Format("{0}.mbk", username));

      if (File.Exists(filePath))
      {
        File.Delete(filePath);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Проверяет существование файла профиля пользователя.
    /// </summary>
    /// <param name="path">Путь к каталогу, в котором следует искать файл профиля пользователя.</param>
    /// <param name="username">Имя пользователя.</param>
    public static bool Exists(string path, string username)
    {
      if (String.IsNullOrEmpty(path))
      {
        throw new ArgumentNullException("path");
      }
      if (String.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException("username");
      }

      return File.Exists(Path.Combine(path, String.Format("{0}.mbk", username)));
    }

    #endregion

  }

}