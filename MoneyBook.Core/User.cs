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
      // 1. Проверка пути
      // 2. Извлечение образца базы из ресурсов
      // 3. Сохранение
      throw new NotImplementedException();
    }
    
  }

}