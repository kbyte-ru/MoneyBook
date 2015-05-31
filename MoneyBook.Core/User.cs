using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using MoneyBook.Core.Data;
using MoneyBook.Core.Extensions;

namespace MoneyBook.Core
{

  [Serializable]
  public class User : IDisposable
  {

    #region ..свойства..

    /// <summary>
    /// Расширение файлов БД, включая точку.
    /// </summary>
    private const string DatabaseFileExtension = ".mbk";

    /// <summary>
    /// Признак того, что ресурсы были освобождены.
    /// </summary>
    private bool IsDisposed = false;

    /// <summary>
    /// Перечень свойств, в которых следует отслеживать изменения.
    /// </summary>
    private IEnumerable<PropertyInfo> MonitoringPropeties = null;

    /// <summary>
    /// Строка соединения с базой данных текущего экземпляра пользователя.
    /// </summary>
    internal string ConnectionString = "";

    /// <summary>
    /// Дата и время начала сессии.
    /// </summary>
    private DateTime SessionDate;

    /// <summary>
    /// Базовый каталог, в котором располагается файл профиля пользователя.
    /// </summary>
    private string BasePath = "";

    /// <summary>
    /// Имя текущего пользователя.
    /// </summary>
    public string UserName = "";

    /// <summary>
    /// Пароль текущего пользователя.
    /// </summary>
    private string Password = "";

    private Dictionary<int, AccountType> _AccountTypes = null;

    /// <summary>
    /// Список типов счетов пользователя.
    /// </summary>
    [ChangesMonitor]
    public Dictionary<int, AccountType> AccountTypes
    {
      get
      {
        if (_AccountTypes == null)
        {
          _AccountTypes = this.GetAccountTypes();
        }
        return _AccountTypes;
      }
    }

    private Dictionary<int, Account> _Accounts = null;

    /// <summary>
    /// Список счетов пользователя.
    /// </summary>
    [ChangesMonitor]
    public Dictionary<int, Account> Accounts
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

    private Dictionary<int, Category> _Categories = null;

    /// <summary>
    /// Список категорий.
    /// </summary>
    [ChangesMonitor]
    public Dictionary<int, Category> Categories
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

    private Dictionary<string, Currency> _Currencies = null;

    /// <summary>
    /// Список валют.
    /// </summary>
    [ChangesMonitor]
    public Dictionary<string, Currency> Currencies
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

      // получаем свойства, в которых следует отслеживать изменения
      this.MonitoringPropeties = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(ChangesMonitorAttribute), false).Length > 0);

      // базовая информация о пользователе
      this.BasePath = path;
      this.UserName = username;
      this.Password = password;

      // фиксируем время начала текущей сессии
      this.SessionDate = DateTime.Now;

      // строка соединения
      this.ConnectionString = String.Format("Data Source={0}; password={1}", filePath, password);

      // информация о базе
      this.Info = new Info(this);

      // счетчик запусков
      int totalSession = 0;
      int.TryParse(this.Info[InfoId.Stat.TotalSessions], out totalSession);
      this.Info.Set(InfoId.Stat.TotalSessions, totalSession + 1);
    }
    
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.IsDisposed) { return; }

      if (disposing)
      {
        // обновление информации
        long totalTime = 0;
        long.TryParse(this.Info[InfoId.Stat.TotalTime], out totalTime);
        this.Info.Set(InfoId.Stat.TotalTime, totalTime + Convert.ToInt64(DateTime.Now.Subtract(this.SessionDate).TotalSeconds));
      }

      this.IsDisposed = true;
    }

    #endregion
    #region ..методы..

    /// <summary>
    /// Возвращает список записей расходов/доходов пользователя.
    /// </summary>
    /// <param name="accountId">Идентификатор счета. По умолчанию, ноль - любой счет.</param>
    /// <param name="categoryId">Идентификатор категории. По умолчанию, ноль - все категории.</param>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <param name="amountFrom">Сумма от.</param>
    /// <param name="amountTo">Сумма до.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="page">Номер страницы, для которой следует получить записи. Начиная с 1.</param>
    /// <param name="maxDataPerPage">Максимальное число записей на одной странице. Минус один (по умолчанию) - все записи, без разбивки на страницы.</param>
    /// <param name="type">Типы записей, которые селдует получить. По умолчанию - записи любого типа.</param>
    public MoneyItems GetMoneyItems(EntryType type = EntryType.None, int accountId = 0, int categoryId = 0, DateTime? dateFrom = null, DateTime? dateTo = null, decimal? amountFrom = null, decimal? amountTo = null, string search = null, int page = 1, int maxDataPerPage = -1)//MoneyLoadProgressCallback callback = null
    {
      if (page <= 0) { page = 1; }
      page--;

      MoneyItems result = new MoneyItems();
      result.CurrentPage = page;
      result.MaxDataPerPage = maxDataPerPage;
      
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        /*if (callback != null)
        {
          var margs = new MoneyLoadEventArgs();
          client.QueryProcessing += (sender, e) =>
          {
            var args = (QueryProcessingEventArgs)e;
            margs.TotalItems = args.TotalItems;
            margs.ItemPosition = args.ItemPosition;
            callback(this, margs);
          };
        }*/

        // формируем условия выборки
        string w = "";

        if (type != EntryType.None)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "entry_type = @entry_type";
          client.Parameters.Add("@entry_type", SqlDbType.TinyInt).Value = (byte)type;
        }

        if (accountId > 0)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "id_accounts = @id_accounts";
          client.Parameters.Add("@id_accounts", SqlDbType.Int).Value = accountId;
        }

        if (categoryId > 0)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "(id_categories = @id_categories OR id_categories IN (SELECT id_categories FROM categories WHERE parent_id = @id_categories))";
          client.Parameters.Add("@id_categories", SqlDbType.Int).Value = categoryId;
        }
       
        /*if (dateFrom.HasValue && dateTo.HasValue)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "date_entry @dateFrom AND @dateTo";
          client.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = dateFrom.Value;
          client.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = dateTo.Value;
        }*/
        if (dateFrom.HasValue)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "DateDiff(Day, @dateFrom, date_entry) >= 0";
          client.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = dateFrom.Value;
        }
        if (dateTo.HasValue)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "DateDiff(Day, @dateTo, date_entry) <= 0";
          client.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = dateTo.Value;
        }

        if (amountFrom.HasValue)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "[amount] >= @amountFrom";
          client.Parameters.Add("@amountFrom", SqlDbType.Money).Value = amountFrom.Value;
        }
        if (amountTo.HasValue)
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "[amount] <= @amountTo";
          client.Parameters.Add("@amountTo", SqlDbType.Money).Value = amountTo.Value;
        }

        if (!String.IsNullOrWhiteSpace(search))
        {
          if (!String.IsNullOrEmpty(w)) { w += " AND "; }
          w += "([title] LIKE @search OR [description] LIKE @search)";
          client.Parameters.Add("@search", SqlDbType.NVarChar, 200).Value = String.Format("%{0}%", search);
        }

        if (!String.IsNullOrEmpty(w))
        {
          w = " WHERE " + w;
        }

        // определяем число записей
        client.CommandText = "SELECT COUNT([id_items]) FROM [items]" + w;
        result.TotalRecords = Convert.ToInt32(client.ExecuteScalar());

        if (result.TotalRecords <= 0)
        {
          return result;
        }

        // получаем записи для текущей страницы
        client.CommandText = String.Format("SELECT * FROM [items] {0} ORDER BY [date_entry] DESC, [id_items] DESC ", w);

        if (maxDataPerPage > 0)
        {
          client.CommandText += String.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY;", page * maxDataPerPage, maxDataPerPage);
        }

        // выполняем запрос и передаем результат в result
        result.AddRange(client.GetEntities<MoneyItem>());
      }

      return result;
    }

    /// <summary>
    /// Устанавливает пароль на файл профиля пользователя.
    /// </summary>
    /// <param name="newPassword">Пароль, который следует установить.</param>
    public void SetPassword(string newPassword)
    {
      // путь к файлу бд
      string filePath = Path.Combine(this.BasePath, String.Format("{0}{1}", this.UserName, User.DatabaseFileExtension));
      // меняем пароль
      SqlDbCeClient.ChangeDatabasePassword(filePath, this.Password, newPassword);
      // обновляем строку соединения
      this.ConnectionString = String.Format("Data Source={0}; password={1}", filePath, newPassword);
    }
    
    /// <summary>
    /// Сохраняет указанные объекты (счет, категория, запись) в базе данных.
    /// </summary>
    /// <param name="entities">Список объектов, которые необходимо сохранить.</param>
    public void Save<T>(List<T> entities) where T : UserMoneyObject
    {
      if (entities == null)
      {
        throw new ArgumentNullException("entities");
      }

      if (entities.Count <= 0)
      {
        return;
      }

      foreach (var entity in entities)
      {
        this.Save(entity);
      }
    }

    /// <summary>
    /// Сохраняет указанный объект (счет, категория, запись) в базе данных.
    /// </summary>
    /// <param name="entity">Объект, который необходимо сохранить.</param>
    public void Save<T>(T entity) where T : UserMoneyObject
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.SaveEntities<UserMoneyObject>(entity);
      }

      var t = entity.GetType();

      // если была создана новая запись
      if (entity.Status == EntityStatus.Created)
      {
        // добавляем запись в текущий экземпляр, если есть куда
        foreach (var p in this.MonitoringPropeties)
        {
          if (p.PropertyType.IsGenericType)
          {
            if (p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && p.PropertyType.GetGenericArguments().First() == t)
            {
              var list = (IList)p.GetValue(this, null);
              list.Add(entity);
              break;
            }
            else if (p.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && p.PropertyType.GetGenericArguments().Any(a => a == t)) // TODO: По идее Last должно быть достаточно
            {
              var list = (IDictionary)p.GetValue(this, null);
              if (!list.Contains(entity.PrimaryKeyValue))
              {
                list.Add(entity.PrimaryKeyValue, entity);
              }
              else
              {
                // TODO: понять, почему так происходит
                bool todo = true;
              }
              break;
            }
          }
        }
      }
      else if (entity.Status == EntityStatus.Updated)
      {
        // если запись была сохранена, то следует проверить, 
        // было ли это сделано по ссылке или же через новый экземпляр
        // если через новый экземпляр, то необходимо найти аналогичный объект в памяти и обновить его
        foreach (var p in this.MonitoringPropeties)
        {
          if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && p.PropertyType.GetGenericArguments().First() == t)
          {
            // нашли нужный тип, перебираем значения
            var list = (IList)p.GetValue(this, null);
            for (int i = 0; i < list.Count; i++)
            {
              if (((UserMoneyObject)list[i]).PrimaryKeyEquals(entity))
              {
                // нашли нужный элемент
                // entity.CopyTo(list[i]); // копируем данные в него
                list[i] = entity; // меняем
                break;
              }
            }
            break;
          }
          else if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && p.PropertyType.GetGenericArguments().Any(a => a == t))
          {
            var list = (IDictionary)p.GetValue(this, null);
            if (list.Contains(entity.PrimaryKeyValue))
            {
              // нашли нужный элемент, меняем на новый
              list[entity.PrimaryKeyValue] = entity;
            }
            break;
          }
        }
      }
    }

    /// <summary>
    /// Удаляет указанные объекты (счет, категория, запись) из базы данных.
    /// </summary>
    /// <param name="entities">Список объектов, которые необходимо удалить.</param>
    public int Delete<T>(List<T> entities) where T : UserMoneyObject
    {
      if (entities == null)
      {
        throw new ArgumentNullException("entities");
      }

      if (entities.Count <= 0)
      {
        return 0;
      }

      int result = 0;

      foreach (var entity in entities)
      {
        result += this.Delete(entity);
      }

      return result;
    }

    /// <summary>
    /// Удаляет указанный объект (счет, категория, запись) из базы данных.
    /// </summary>
    /// <param name="entity">Объект, который необходимо удалить.</param>
    public int Delete<T>(T entity) where T : UserMoneyObject
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      int result = 0;
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        result = client.DeleteEntities(entity);
      }

      var t = entity.GetType();

      // удаляем запись из текущего экземпляра класса, если нужно
      foreach (var p in this.MonitoringPropeties)
      {
        if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && p.PropertyType.GetGenericArguments().First() == t)
        {
          var list = (IList)p.GetValue(this, null);
          for (int i = 0; i < list.Count; i++)
          {
            if (((UserMoneyObject)list[i]).PrimaryKeyEquals(entity))
            {
              // нашли нужный элемент, удаляем
              list.RemoveAt(i);
              break;
            }
          }
          break;
        }
        else if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && p.PropertyType.GetGenericArguments().Any(a => a == t))
        {
          var list = (IDictionary)p.GetValue(this, null);
          if (list.Contains(entity.PrimaryKeyValue))
          {
            // нашли нужный элемент, удаляем
            list.Remove(entity.PrimaryKeyValue);
          }
          break;
        }
      }

      return result;
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

      return this.AddIcon(System.Drawing.Bitmap.FromStream(stream));
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

      return this.AddIcon(System.Drawing.Bitmap.FromStream(new MemoryStream(data)));
    }

    /// <summary>
    /// Добавляет иконку в профиль текущего пользователя (основной метод).
    /// </summary>
    /// <param name="image">Экземпляр изображения файла иконки.</param>
    /// <returns>Возвращает экземпляр записи добавленной иконки.</returns>
    public Icon AddIcon(System.Drawing.Image image)
    {
      if (image == null)
      {
        throw new ArgumentNullException("image");
      }

      var m = new MemoryStream();

      // проверяем размер
      if (image.Width > 16 || image.Height > 16)
      {
        // слишком большой, уменьшаем
        var thumb = image.GetThumbnailImage(16, 16, null, IntPtr.Zero);
        thumb.Save(m, System.Drawing.Imaging.ImageFormat.Png);
      }
      else
      {
        // пойдет, сохраняем как есть
        image.Save(m, System.Drawing.Imaging.ImageFormat.Png);
      }

      var data = m.ToArray();

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

          client.SaveEntities<Icon>(result);
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
    /// Возвращает список всех иконок пользователя.
    /// </summary>
    public List<Icon> GetIcons()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        return client.GetEntities<Icon>("SELECT * FROM [icons]");
      }
    }

    /// <summary>
    /// Возвращает список типов счетов.
    /// </summary>
    private Dictionary<int, AccountType> GetAccountTypes()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [account_types] ORDER BY [account_type_name], [id_account_types]";
        return client.GetEntities<int, AccountType>();
      }
    }

    /// <summary>
    /// Возвращает список счетов.
    /// </summary>
    private Dictionary<int, Account> GetAccounts()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [accounts] ORDER BY [account_name], [id_accounts]";
        return client.GetEntities<int, Account>();
      }
    }

    /// <summary>
    /// Возвращает список категорий расходов/доходов.
    /// </summary>
    private Dictionary<int, Category> GetCategories()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [categories]";
        return client.GetEntities<int, Category>();
      }
    }

    /// <summary>
    /// Возвращает список валют.
    /// </summary>
    private Dictionary<string, Currency> GetCurrencies()
    {
      using (var client = new SqlDbCeClient(this.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [currencies] ORDER BY [priority] ASC";
        return client.GetEntities<string, Currency>();
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
      newUser.Info.Set(InfoId.Initial.AppType, applicationType.ToString());
      newUser.Info.Set(InfoId.Initial.SystemID, Environment.OSVersion.Platform.ToString());
      newUser.Info.Set(InfoId.Initial.SystemVersion, Environment.OSVersion.Version);
      newUser.Info.Set(InfoId.Initial.NetVersion, Environment.Version);

      var program = Assembly.GetEntryAssembly();
      if (program != null)
      {
        var programName = program.GetName();
        newUser.Info.Set(InfoId.Initial.ProgramName, programName.Name);
        newUser.Info.Set(InfoId.Initial.ProgramVersion, programName.Version);
      }

      var me = Assembly.GetExecutingAssembly().GetName();
      newUser.Info.Set(InfoId.Initial.CoreName, me.Name);
      newUser.Info.Set(InfoId.Initial.CoreVersion, me.Version);
      
      try
      {
        newUser.Info.Set(InfoId.Initial.MachineName, Environment.MachineName);
        newUser.Info.Set(InfoId.Initial.UserName, Environment.UserName);
      }
      catch { }

      newUser.Info.Set(InfoId.Initial.Culture, System.Globalization.CultureInfo.CurrentCulture.Name);
      newUser.Info.Set(InfoId.Initial.DateTime, DateTime.Now.Ticks);
      newUser.Info.Set(InfoId.Initial.TimeZone, TimeZoneInfo.Local.BaseUtcOffset.Ticks);

      // считаем создание пользователя за сессию
      newUser.Info.Set(InfoId.Stat.TotalSessions, 1);
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
          result.Add(Path.GetFileNameWithoutExtension(file));
        }
      }

      return result;
    }

    #endregion

  }

}