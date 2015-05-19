using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlServerCe;

namespace MoneyBook.Core
{

  public class User
  {

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

      // 2. Извлечение образца базы из ресурсов и создание файла профиля
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = String.Format("{0}.pattern.sdf", assembly.GetName().Name);
      using (var reader = assembly.GetManifestResourceStream(resourceName))
      {
        using (var writer = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Inheritable))
        {
          byte[] buffer=new byte[(int)reader.Length];
          writer.Write(buffer, 0, buffer.Length);
        }
      }
      
      // 3. Подключаемся к базе
      using (var client = new Data.SqlDbCeClient(String.Format("Data Source={0}", filePath)))
      {
        // test
        client.ExecuteScalar("SELECT 1");
        // 4. Наполнение базы данными по умолчанию
      }
      

      throw new NotImplementedException("Не весь код есть");

      return new User(path, username, password);
    }
    
  }

}