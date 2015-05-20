using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlServerCe;
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

    }

    /// <summary>
    /// Меняет пароль.
    /// </summary>
    /// <param name="newPassword">Пароль, который следует установить.</param>
    public void SetPassword(string newPassword)
    {
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

      // 2. Создаем новую базу данных
      string connectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      SqlDbCeClient.CreateDatabase(connectionString);

      // 3. Подключаемся к базе
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
        var testResult = client.ExecuteScalar("SELECT COUNT(*) FROM [accounts]");
        // 4. Наполнение базы данными по умолчанию
      }

      throw new NotImplementedException("Не весь код есть");

      return new User(path, username, password);
    }
    
  }

}