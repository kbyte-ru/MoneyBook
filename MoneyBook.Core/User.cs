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

    /// <summary>
    /// Строка соединения с базой данных текущего экземпляра пользователя.
    /// </summary>
    private string ConnectionString = "";

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
    public List<Account> GetAccounts()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [accounts] ORDER BY [account_name], [id_accounts]";
        return client.GetEntities<Account>();
      }
    }

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

        // test
        
        /*
        var a = new Account();
        a.CurrencyCode = "RUB";
        a.AccountTypeId = 1;
        a.Details = "test";
        a.IconId = 123;
        a.Id = 0;
        a.Name = "проверка";
        var e = new List<Account>();
        e.Add(a);
        client.SaveEntities<Account>(e);
        
        var aaa = client.GetEntities<Account>("SELECT * FROM accounts");
        */

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
    
  }

}