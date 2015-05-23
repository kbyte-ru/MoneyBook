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
    /// Расширение файлов БД, включая точку.
    /// </summary>
    private const string DatabaseFileExtension = ".mbk";

    /// <summary>
    /// Строка соединения с базой данных текущего экземпляра пользователя.
    /// </summary>
    internal string ConnectionString = "";

    /// <summary>
    /// Дата и время начала сессии.
    /// </summary>
    private DateTime SessionDate;

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

    /// <summary>
    /// Служебная информация о файле БД.
    /// </summary>
    public Info Info { get; protected set; }

    #endregion
    #region ..конструктор и деструктор..

    /// <summary>
    /// Инициализрует новый экземпляр пользователя.
    /// </summary>
    /// <param name="path">Путь, по которому следует искать файл профиля пользователя.</param>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль для доступа к файлу профиля.</param>
    public User(string path, string username, string password = null)
    {
      // проверка существования файла базы
      string filePath = Path.Combine(path, String.Format("{0}{1}", username, User.DatabaseFileExtension));
      if (!File.Exists(filePath))
      {
        throw new Exception("Пользователь не найден.");
      }
      // фиксируем время начала текущей сессии
      this.SessionDate = DateTime.Now;
      // строка соединения
      this.ConnectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      // информация о базе
      this.Info = new Info(this);
      // счетчик запусков
      int totalSession = 0;
      int.TryParse(this.Info[InfoId.TotalSessions], out totalSession);
      this.Info.Set(InfoId.TotalSessions, totalSession + 1);
    }
    
    public void Dispose()
    {
      // обновление информации:
      // продолжительность сессии
      long totalTime = 0;
      long.TryParse(this.Info[InfoId.TotalTime], out totalTime);
      this.Info.Set(InfoId.TotalTime, totalTime + Convert.ToInt64(DateTime.Now.Subtract(this.SessionDate).TotalSeconds));
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
    /// Добавляет иконку в профиль текущего пользователя.
    /// </summary>
    /// <param name="image">Экземпляр изображения файла иконки.</param>
    /// <returns>Возвращает экземпляр записи добавленной иконки.</returns>
    public Icon AddIcon(System.Drawing.Bitmap image)
    {
      if (image == null)
      {
        throw new ArgumentNullException("image");
      }

      var m = new MemoryStream();
      image.Save(m, System.Drawing.Imaging.ImageFormat.Png);

      return this.AddIcon(m.ToArray());
    }

    /// <summary>
    /// Добавляет иконку в профиль текущего пользователя.
    /// </summary>
    /// <param name="stream">Поток содержащий файл иконки.</param>
    /// <returns>Возвращает экземпляр записи добавленной иконки.</returns>
    public Icon AddIcon(Stream stream)
    {
      if (stream == null)
      {
        throw new ArgumentNullException("stream");
      }

      if (stream.Position != 0) { stream.Position = 0; }
      byte[] data = new byte[Convert.ToInt32(stream.Length)];
      stream.Read(data, 0, data.Length);

      return this.AddIcon(data);
    }

    /// <summary>
    /// Добавляет иконку в профиль текущего пользователя.
    /// </summary>
    /// <param name="data">Содержимое файла иконки.</param>
    /// <returns>Возвращает экземпляр записи добавленной иконки.</returns>
    public Icon AddIcon(byte[] data)
    {
      if (data == null)
      {
        throw new ArgumentNullException("data");
      }

      // получаем хеш-сумму
      var hash = MoneyBookUtility.GetMD5Hash(data);

      Icon result = null;

      // проверяем существование иконки
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [icons] WHERE [hash] = @hash";
        client.Parameters.Add("@hash", System.Data.SqlDbType.UniqueIdentifier).Value = hash;
        result = client.GetEntity<Icon>();
        if (result == null)
        {
          // нет такой иконки, добавляем
          result = new Icon
          {
            Data = data,
            Hash = hash,
            DateCreated = DateTime.Now
          };

          client.SaveEntity<Icon>(result);
        }
      }

      return result;
    }

    /// <summary>
    /// Извлекает из профиля текущего пользователя иконку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор иконку, которую необходимо извлечь из базы.</param>
    /// <returns>
    /// <para>Возвращает экземпляр <see cref="System.Drawing.Bitmap"/>, представляющий извлеченное изображение иконки.</para>
    /// <para>Если запись не будет найдена в базе данных, возвращает значение <b>null</b>.</para>
    /// </returns>
    public System.Drawing.Bitmap GetIcon(int id)
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [icons] WHERE [id_icons] = @id_icons";
        client.Parameters.Add("@id_icons", System.Data.SqlDbType.Int).Value = id;
        var result = client.GetEntity<Icon>();
        if (result == null)
        {
          return null;
        }
        return result.ToBitmap();
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
    /// <param name="applicationType">Тип приложения, под которым создается профиль.</param>
    public static User Create(ApplicationType applicationType, string path, string username, string password = null) /*, List<Currency> currencies = null, List<AccountType> accountTypes = null, List<Account> accounts = null, List<Category> categories = null*/
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
      string filePath = Path.Combine(path, String.Format("{0}{1}", username, User.DatabaseFileExtension));
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

      User newUser = null;

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

        newUser = new User(path, username, password);
      }

      // добавляем информация об источнике происхождения базы
      newUser.Info.Set(InfoId.InitialAppType, applicationType.ToString());
      newUser.Info.Set(InfoId.InitialSystemID, Environment.OSVersion.Platform.ToString());
      newUser.Info.Set(InfoId.InitialSystemVersion, Environment.OSVersion.Version);
      newUser.Info.Set(InfoId.InitialNetVersion, Environment.Version);

      var program = Assembly.GetEntryAssembly();
      if (program != null)
      {
        var programName = program.GetName();
        newUser.Info.Set(InfoId.InitialProgramName, programName.Name);
        newUser.Info.Set(InfoId.InitialProgramVersion, programName.Version);
      }

      var me = Assembly.GetExecutingAssembly().GetName();
      newUser.Info.Set(InfoId.InitialCoreName, me.Name);
      newUser.Info.Set(InfoId.InitialCoreVersion, me.Version);

      try
      {
        newUser.Info.Set(InfoId.InitialMachineName, Environment.MachineName);
        newUser.Info.Set(InfoId.InitialUserName, Environment.UserName);
      }
      catch { }

      newUser.Info.Set(InfoId.InitialCulture, System.Globalization.CultureInfo.CurrentCulture.Name);
      newUser.Info.Set(InfoId.InitialDateTime, DateTime.Now.Ticks);
      newUser.Info.Set(InfoId.InitialTimeZone, TimeZoneInfo.Local.BaseUtcOffset.Ticks);

      // считаем создание пользователя за сессию
      newUser.Info.Set(InfoId.TotalSessions, 1);
      // --

      // возвращаем созданного пользователя
      return newUser;
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

      string filePath = Path.Combine(path, String.Format("{0}{1}", username, User.DatabaseFileExtension));

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
      
      return File.Exists(Path.Combine(path, String.Format("{0}{1}", username, User.DatabaseFileExtension)));
    }

    /// <summary>
    /// Возвращает список имен профилей пользователей, найденных по указанному пути.
    /// </summary>
    /// <param name="path">Путь, по которому следует искать файлы профилей пользователей.</param>
    public static List<string> GetUsers(string path)
    {
      if (String.IsNullOrEmpty(path))
      {
        throw new ArgumentNullException("path");
      }

      var result = new List<string>();

      if (Directory.Exists(path))
      {
        foreach (var file in Directory.GetFiles(path, String.Format("*{0}", User.DatabaseFileExtension)))
        {
          result.Add(file);
        }
      }

      return result;
    }

    #endregion

  }

}