﻿// ----------------------------------------------------------------------------
// Copyright (c) Aleksey Nemiro, 2015. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------
using System;
using System.Linq;
using System.Data;
using System.Data.SqlServerCe;
using MoneyBook.Core.Data.Enums;
using System.Reflection;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Вспомогательный класс для работы с базами данных SQL Server Compact Edition.
  /// </summary>
  /// <remarks>
  /// <para>Это упрощенная версия класса для полноценного SQL Server из проекта Nemiro.Data v2.0</para>
  public partial class SqlDbCeClient : IDisposable
  {

    #region ..события..

    /// <summary>
    /// Возникает при изменении процесса выполнения запроса.
    /// </summary>
    public event EventHandler QueryProcessing;

    protected virtual void OnQueryProcessing(QueryProcessingEventArgs e)
    {
      if (this.QueryProcessing == null) { return; }
      this.QueryProcessing(this, e);
    }

    #endregion
    #region ..свойства..

    private SqlCeConnection _Conn = null;
    private SqlCeCommand _Cmd = new SqlCeCommand();

    private string _ConnectionString = String.Empty;
    /// <summary>
    /// Строка соединения с базой данных.
    /// </summary>
    public string ConnectionString
    {
      get
      {
        return _ConnectionString;
      }
      set
      {
        this.Disconnect();
        _Conn = null;
        _ConnectionString = value;
      }
    }

    private ConnectionMode _ConnectionMode = ConnectionMode.Auto;
    /// <summary>
    /// Режим соединения с базой данных.
    /// </summary>
    public ConnectionMode ConnectionMode
    {
      get
      {
        return _ConnectionMode;
      }
      set
      {
        _ConnectionMode = value;
      }
    }

    /// <summary>
    /// Тип команды, указанной в свойстве <see cref="CommandText"/>.
    /// </summary>
    public CommandType CommandType
    {
      get
      {
        return _Cmd.CommandType;
      }
      set
      {
        _Cmd.CommandType = value;
      }
    }

    /// <summary>
    /// Текст SQL-запроса, либо имя хранимой процедуры, которую необходимо выполнить.
    /// </summary>
    public string CommandText
    {
      get
      {
        return _Cmd.CommandText;
      }
      set
      {
        _Cmd.CommandText = value;
      }
    }

    /// <summary>
    /// Коллекция параметров запроса.
    /// </summary>
    /// <remarks>
    public SqlCeParameterCollection Parameters
    {
      get
      {
        return _Cmd.Parameters;
      }
    }

    /// <summary>
    /// Время ожидания выполнения команды (в секундах). По умолчанию используется значение <c>30 секунд</c>.
    /// </summary>
    public int CommandTimeout
    {
      get
      {
        return _Cmd.CommandTimeout;
      }
      set
      {
        _Cmd.CommandTimeout = value;
      }
    }

    private TimeSpan _LastQueryTime = TimeSpan.Zero;

    /// <summary>
    /// Время, затраченное на выполнение последнего запроса к базе данных.
    /// </summary>
    /// <value>Значение по умолчанию <see cref="System.TimeSpan.Zero"/>.</value>
    public TimeSpan LastQueryTime
    {
      get
      {
        return _LastQueryTime;
      }
    }

    #endregion
    #region ..конструктор..

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SqlDbCeClient"/> с указанием строки соединения с базой данных.
    /// </summary>
    /// <param name="connectionString">Строка соединения с базой данных. Например: <c>Data Source=MyData.sdf;Max Database Size=256;Persist Security Info=False;</c>.</param>
    /// <example>
    /// <para>В следующем примере создается экземпляр класса <see cref="SqlDbCeClient"/> с указанием строкb соединения с базой SQL Server CE.</para>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Max Database Size=256;Persist Security Info=False;"))
    /// {
    ///   // ...
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Max Database Size=256;Persist Security Info=False;")
    ///   '...
    /// End Using
    /// </code>
    /// </example>
    public SqlDbCeClient(string connectionString)
    {
      this.ConnectionString = connectionString;
    }

    /// <summary>
    /// Освобождает все ресурсы, занятые объектом.
    /// Если необходимо, закрывает все открытые объектом соединения с базой данных.
    /// </summary>
    public void Dispose()
    {
      try
      {
        this.Disconnect();
        if (_Cmd != null) { _Cmd.Dispose(); }
        if (_Conn != null) { _Conn.Dispose(); }
      }
      catch
      {
      }
    }

    #endregion
    #region ..функции и методы..

    /// <summary>
    /// Открывает соединение с базой данных, если нет открытого соединения.
    /// </summary>
    /// <remarks>
    /// <para>Нет необходимости вызывать этот метод отдельно, поскольку соединение с базой данных и так будет открываться при первой необходимости.</para>
    /// </remarks>
    /// <seealso cref="Disconnect"/>
    public void Connect()
    {
      this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Connecting));

      if (_Conn == null)
      {
        _Conn = new SqlCeConnection(this.ConnectionString);
      }
      if (_Conn.State == ConnectionState.Closed || _Conn.State == ConnectionState.Broken)
      {
        _Conn.Open();
      }
      
      this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Connected));
    }

    /// <summary>
    /// Закрывает соединение с базой данных, если оно открыто.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Если свойство <see cref="ConnectionMode"/> имеет значение <see cref="Nemiro.Data.ConnectionMode.Auto"/> (по умолчанию),
    /// то соединения с базой данных будут закрываться автоматически и вызывать отдельно метод <see cref="Disconnect"/> нет необходимости.
    /// </para>
    /// <para>При удалении экземпляра класса <see cref="SqlClient"/>, все открытые соединения автоматически закрываются, независимо от значения свойства <see cref="ConnectionMode"/>.</para>
    /// </remarks>
    /// <seealso cref="ConnectionMode"/>
    /// <seealso cref="Connect"/>
    public void Disconnect()
    {
      if (_Conn != null && (_Conn.State == ConnectionState.Open || _Conn.State == ConnectionState.Connecting || _Conn.State == ConnectionState.Executing || _Conn.State == ConnectionState.Fetching))
      {
        _Conn.Close();

        this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Disconnected));
      }
    }
    
    /// <summary>
    /// Создает экземпляр сущности указанного типа и наполняет указанными данными.
    /// </summary>
    private T CreateEntityInstance<T>(DataRow row) where T : IEntity
    {
      object primaryKeyValue = null;
      return this.CreateEntityInstance<T>(row, ref primaryKeyValue);
    }

    /// <summary>
    /// Создает экземпляр сущности указанного типа и наполняет указанными данными.
    /// </summary>
    private T CreateEntityInstance<T>(DataRow row, ref object primaryKeyValue) where T : IEntity
    {
      primaryKeyValue = null;

      var entity = (T)Activator.CreateInstance(typeof(T));

      if (row == null)
      {
        if (entity.GetType().IsSubclassOf(typeof(Entity)))
        {
          entity.GetType().GetProperty("Status").SetValue(entity, EntityStatus.NotFound, null);
        }
        return entity;
      }

      var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      foreach (PropertyInfo p in properties)
      {
        ColumnAttribute column = this.GetColumnAttribute(p);
        if (column == null) { continue; }

        // найдено соответсвие свойства и поля бд
        // передаем ссылку на целевой экземпляр класса
        column.SetOwner(entity, p);

        // проверяем наличие поля в результатах выборки и наличие в нем значения
        if (this.ContainsColumn(row, column.ColumnName) && row[column.ColumnName] != DBNull.Value)
        {
          // есть значение, передаем его в свойство
          if (p.PropertyType == typeof(Int16))
          {
            p.SetValue(entity, Convert.ToInt16(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(Int32))
          {
            p.SetValue(entity, Convert.ToInt32(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(Int64))
          {
            p.SetValue(entity, Convert.ToInt64(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(UInt16))
          {
            p.SetValue(entity, Convert.ToUInt16(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(UInt32))
          {
            p.SetValue(entity, Convert.ToUInt32(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(UInt64))
          {
            p.SetValue(entity, Convert.ToUInt64(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(decimal))
          {
            p.SetValue(entity, Convert.ToDecimal(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(double))
          {
            p.SetValue(entity, Convert.ToDouble(row[column.ColumnName]), null);
          }
          else if (p.PropertyType == typeof(float))
          {
            p.SetValue(entity, Convert.ToSingle(row[column.ColumnName]), null);
          }
          else if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
          {
            object v = row[column.ColumnName];
            var t = p.PropertyType.GetGenericArguments().FirstOrDefault();
            if (t != null && (t == typeof(Enum) || t.BaseType == typeof(Enum)))
            {
              v = Enum.Parse(t, Convert.ToString(row[column.ColumnName]), true);
            }
            p.SetValue(entity, v, null);
          }
          else
          {
            // другой тип данных
            p.SetValue(entity, row[column.ColumnName], null);
          }

          if (column.IsPrimaryKey)
          {
            primaryKeyValue = row[column.ColumnName];
          }
        }
        else
        {
          // аннулируем значение
          p.SetValue(entity, null, null);
        }
      }

      if (entity.GetType().IsSubclassOf(typeof(Entity)))
      {
        entity.GetType().GetProperty("Status").SetValue(entity, EntityStatus.Loaded, null);
      }

      return entity;
    }

    /// <summary>
    /// Сохраняет сущность в базу и возвращает сохранный экземпляр сущности.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности.</param>
    private T SaveEntityInstanceToDatabase<T>(T entity) where T : IEntity
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      this.Parameters.Clear();

      var t = entity.GetType();

      // имя таблицы 
      string tableName = "";
      var table = (TableAttribute)t.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
      if (table != null)
      {
        tableName = table.TableName;
      }
      else
      {
        tableName = t.Name;
      }

      // извлекаем свойства
      var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      // ищем ключ
      var primaryKey = properties.FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);
      var primaryKeyColumn = (ColumnAttribute)primaryKey.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault();
      if (primaryKeyColumn != null)
      {
        primaryKeyColumn.SetOwner(entity, primaryKey);
      }

      // формируем универсальный запрос сохранения данных
      string queryWhere = "";
      string queryInsertNames = "", queryInsertValues = "";
      string queryUpdateValues = "";
      var newId = Guid.NewGuid();

      // перебираем все свойства
      foreach (PropertyInfo p in properties)
      {
        var column = this.GetColumnAttribute(p);
        if (column == null) { continue; }
        // устанавливаем родителя
        column.SetOwner(entity, p);
        // берем значение свойства
        object value = column.GetValidParameterValue(p);
        // проверяем
        if (primaryKey == null && column.IsUnique)
        { // нет ключевого поля, формируем строку параметров для проверки данных по уникальным полям
          // запрос проверки
          if (!String.IsNullOrEmpty(queryWhere)) queryWhere += " AND ";
          queryWhere += String.Format("{0} = @{1}", this.EscapeSqlObject(column.ColumnName), column.ColumnName);
          // запрос добавления
          if (!String.IsNullOrEmpty(queryInsertNames)) queryInsertNames += ", ";
          if (!String.IsNullOrEmpty(queryInsertValues)) queryInsertValues += ", ";
          queryInsertNames += this.EscapeSqlObject(column.ColumnName);
          queryInsertValues += String.Format("@{0}", column.ColumnName);
        }
        else if (primaryKey != null && primaryKey.Name == p.Name)
        { // есть ключевое поле и это оно
          // запрос проверки
          queryWhere += String.Format("{0} = @{1}", this.EscapeSqlObject(column.ColumnName), column.ColumnName);
          // запрос добавления
          if (!column.IsIdentity)
          { // ключевое поле не является счетчиком, проверяем тип
            if (SqlType.IsGuidType(column.SqlDbType))
            { // это PrimaryKey, он имеет тип Guid
              // он может быть указан, либо не указан
              if (!String.IsNullOrEmpty(queryInsertNames)) queryInsertNames += ", ";
              if (!String.IsNullOrEmpty(queryInsertValues)) queryInsertValues += ", ";
              queryInsertNames += this.EscapeSqlObject(column.ColumnName);
              
              value = newId;

              /*object val = p.GetValue(this, null);
              if (val == null || (val.GetType() == typeof(Guid) && (Guid)val == Guid.Empty) || (val.GetType() == typeof(string) && (String.IsNullOrEmpty(val.ToString()) || Guid.Parse(val.ToString()) == Guid.Empty)))
              { // пустое значение, нужно сгенерировать ключ
                queryInsertValues += newIdName;
              }
              else
              { // указанное значение
              }*/

              queryInsertValues += String.Format("@{0}", column.ColumnName);
            }
            else
            { // другой тип данных, пользователь сам указывает ключ
              if (!String.IsNullOrEmpty(queryInsertNames)) queryInsertNames += ", ";
              if (!String.IsNullOrEmpty(queryInsertValues)) queryInsertValues += ", ";
              queryInsertNames += this.EscapeSqlObject(column.ColumnName);
              queryInsertValues += String.Format("@{0}", column.ColumnName);
            }
          }
        }
        else
        { // ключевого поля нет, либо это не уникальное поле
          // запрос добавления
          if (!String.IsNullOrEmpty(queryInsertNames)) queryInsertNames += ", ";
          if (!String.IsNullOrEmpty(queryInsertValues)) queryInsertValues += ", ";
          queryInsertNames += this.EscapeSqlObject(column.ColumnName);
          queryInsertValues += String.Format("@{0}", column.ColumnName);
          // запрос обновления
          if (!String.IsNullOrEmpty(queryUpdateValues)) queryUpdateValues += ", ";
          queryUpdateValues += String.Format("{0} = @{1}", this.EscapeSqlObject(column.ColumnName), column.ColumnName);
        }

        // добавляем параметр в коллекцию
        this.Parameters.Add(column.GetSqlParameter()).Value = value;
      }

      if (String.IsNullOrEmpty(queryWhere))
      {
        throw new Exception("NoPrimaryKeyOrUniqueFieldsException()"); // TODO
      }

      // проверка существования записи
      string firstColumnName = "";
      if (primaryKeyColumn != null)
      {
        firstColumnName = primaryKeyColumn.ColumnName;
      }
      else
      {
        firstColumnName = this.Parameters[0].ParameterName.Substring(1, this.Parameters[0].ParameterName.Length - 1);
      }

      this.CommandText = String.Format("SELECT CAST((CASE WHEN EXISTS(SELECT {1} FROM {0} WHERE {2}) THEN 1 ELSE 0 END) as bit)", this.EscapeSqlObject(tableName), this.EscapeSqlObject(firstColumnName), queryWhere);

      bool isExists = Convert.ToBoolean(this.ExecuteScalar());

      if (isExists)
      {
        // есть запись, выполняем обновление
        this.CommandText = String.Format("UPDATE {0} SET {1} WHERE {2}", this.EscapeSqlObject(tableName), queryUpdateValues, queryWhere);
        this.ExecuteNonQuery();

        if (t.IsSubclassOf(typeof(Entity)))
        {
          t.GetProperty("Status").SetValue(entity, EntityStatus.Updated, null);
        }
      }
      else
      {
        // нет записи, создаем новую
        // запоминаем режим
        var mode = this.ConnectionMode;
        // меняем на ручной (для получения id)
        this.ConnectionMode = ConnectionMode.Manual;
        // формируем и выполняем запрос
        this.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", this.EscapeSqlObject(tableName), queryInsertNames, queryInsertValues);
        this.ExecuteNonQuery();

        // получаем ключ и передаем в экземпляр сохраняемого класса
        if (primaryKeyColumn != null && SqlType.IsNumericType(primaryKeyColumn.SqlDbType) && primaryKeyColumn.IsIdentity)
        { // основной ключ является числовым счетчиком
          this.CommandText = "SELECT @@IDENTITY";
          this.Parameters.Clear();
          primaryKey.SetValue(entity, MoneyBookUtility.ChangeType(this.ExecuteScalar(), primaryKey.PropertyType), null);
        }
        else if (primaryKeyColumn != null && SqlType.IsGuidType(primaryKeyColumn.SqlDbType) && !isExists)
        { // guid
          primaryKey.SetValue(entity, MoneyBookUtility.ChangeType(newId, primaryKey.PropertyType), null);
        }

        // возвращаем режим соединения
        this.ConnectionMode = mode;
        // закрываем соединение, если режим Auto
        if (this.ConnectionMode == ConnectionMode.Auto)
        {
          this.Disconnect();
        }

        if (t.IsSubclassOf(typeof(Entity)))
        {
          t.GetProperty("Status").SetValue(entity, EntityStatus.Created, null);
        }
      }

      return entity;
    }
    
    /// <summary>
    /// Удаляет сущность из базы и возвращает число удаленных записей.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности.</param>
    private int DeleteEntityFromDatabase(IEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }

      this.Parameters.Clear();

      var t = entity.GetType();

      // имя таблицы 
      string tableName = "";
      var table = (TableAttribute)t.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
      if (table != null)
      {
        tableName = table.TableName;
      }
      else
      {
        tableName = t.Name;
      }

      // извлекаем свойства
      var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      // ищем ключ
      var primaryKey = properties.FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);
      var primaryKeyColumn = (ColumnAttribute)primaryKey.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault();

      int result = 0;

      if (primaryKeyColumn == null)
      { 
        //нет ключевого поля, удаляем по уникальным
        this.SetSqlParametersFromUniqueProperties(entity, this);

        // если есть параметры, значит можно выполнять запрос
        if (this.Parameters.Count > 0)
        {
          this.CommandText = String.Format("DELETE FROM {0} WHERE " + this.CommandText, this.EscapeSqlObject(tableName));

          result = Convert.ToInt32(this.ExecuteNonQuery());
        }
        else
        { 
          // параметров нет, генерируем исключение
          throw new Exception("NoPrimaryKeyOrUniqueFieldsException()"); // TODO
        }
      }
      else
      { 
        // есть ключевое поле
        primaryKeyColumn.SetOwner(entity, primaryKey);
        this.CommandText = String.Format("DELETE FROM {0} WHERE {1} = @{2}", this.EscapeSqlObject(tableName), this.EscapeSqlObject(primaryKeyColumn.ColumnName), primaryKeyColumn.ColumnName);
        this.Parameters.Add(primaryKeyColumn.GetSqlParameter()).Value = primaryKey.GetValue(entity, null);
        
        result = Convert.ToInt32(this.ExecuteNonQuery());
      }

      if (entity.GetType().IsSubclassOf(typeof(Entity)))
      {
        ((Entity)entity).Status = EntityStatus.Deleted;
      }

      return result;
    }

    /// <summary>
    /// Ищет и возвращает ColumnAttribute указанного свойства.
    /// </summary>
    /// <param name="property">Свойство, их которого нужно получить ColumnAttribute.</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private ColumnAttribute GetColumnAttribute(PropertyInfo property)
    {
      if (property == null) return null;
      var result = property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
      if (result == null) return null;
      return (ColumnAttribute)result;
    }

    /// <summary>
    /// Устанавливает клиенту параметры SQL на основе свойств класса с флагом Unique.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private void SetSqlParametersFromUniqueProperties(IEntity parent, SqlDbCeClient client)
    {
      if (parent == null || client == null) return;
      client.CommandText = "";
      client.Parameters.Clear();
      var properties = parent.GetType().GetProperties();
      foreach (PropertyInfo p in properties)
      {
        ColumnAttribute column = this.GetColumnAttribute(p);
        if (column == null) { continue; }
        column.SetOwner(parent, p);
        if (column.IsUnique)
        {
          if (!String.IsNullOrEmpty(client.CommandText)) client.CommandText += " AND ";
          client.CommandText += String.Format("{0} = @{1}", this.EscapeSqlObject(column.ColumnName), column.ColumnName);
          client.Parameters.Add(column.GetSqlParameter()).Value = p.GetValue(parent, null);
        }
      }
    }

    /// <summary>
    /// Возвращает true, если в строке есть колонка с указанным именем.
    /// </summary>
    /// <param name="row">Строка, в которой нужно выполнить поиск колонки.</param>
    /// <param name="columnName">Имя колонки.</param>
    private bool ContainsColumn(DataRow row, string columnName)
    {
      return row != null && row.Table.Columns.Contains(columnName);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private string EscapeString(string value)
    {
      return value.Replace("'", "''");
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private string EscapeSqlObject(string value)
    {
      if (String.IsNullOrEmpty(value)) return "";
      value = value.Trim();
      if (value.StartsWith("[")) return value;
      return String.Format("[{0}]", value);
    }

    /// <summary>
    /// Проверяет и, если необходимо, корректирует запрос.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private void ValidateCommand(ref SqlCeCommand cmd)
    {
      if (cmd == null)
      {
        throw new ArgumentNullException("cmd");  
      }

      // проверка и нормализация текста запроса
      if (!String.IsNullOrEmpty(cmd.CommandText))
      {
        var trimChars = "\r\n ".ToCharArray();
        string[] lines = cmd.CommandText.Split('\n');
        string normalizedCommand = "";
        foreach (string line in lines)
        {
          if (line.Trim(trimChars).Equals("GO", StringComparison.OrdinalIgnoreCase))
          {
            throw new NotSupportedException("GO is not supported.");
          }
          if (normalizedCommand.Length > 0) { normalizedCommand += " "; }
          normalizedCommand += line.Trim(trimChars);
        }
        cmd.CommandText = normalizedCommand;
      }
      // нормализация параметров
      if (cmd.Parameters != null && cmd.Parameters.Count > 0)
      {
        foreach (SqlCeParameter p in cmd.Parameters)
        {
          if (p.Value == null)
          {
            p.Value = DBNull.Value;
          }
        }
      }
    }

    /// <summary>
    /// Выполняет запрос и возвращает DataSet, без использования кэша.
    /// </summary>
    /// <param name="cmd">Команда, которую нужно выполнить.</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    private DataSet GetData2(SqlCeCommand cmd)
    {
      Exception ex2 = null;
      DataSet DS = new DataSet();

      this.Connect();

      try
      {
        this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Executing));

        cmd.Connection = _Conn;
        var DA = new SqlCeDataAdapter(cmd);
        DA.Fill(DS);

        this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Executed));
      }
      catch (Exception ex)
      {
        ex2 = ex;
        this.OnQueryProcessing(new QueryProcessingEventArgs(ex));
      }
      finally
      {
        if (_ConnectionMode == ConnectionMode.Auto)
        {
          this.Disconnect();
        }
      }

      if (ex2 != null)
      {
        throw ex2;
      }

      return DS;
    }

    #endregion
    #region ..статичные функции..
    
    /// <summary>
    /// Создает новую базу данных.
    /// </summary>
    /// <param name="connectionString">Строка соединения.</param>
    public static void CreateDatabase(string connectionString)
    {
      new SqlCeEngine(connectionString).CreateDatabase();
    }

    /// <summary>
    /// Освобождает место на диске, занимаемое базой данных SQL Server Compact, перемещая пустые страницы в конец файла, а затем производя его усечение.
    /// </summary>
    /// <param name="connectionString">Строка соединения.</param>
    public static void ShrinkDatabase(string connectionString)
    {
      new SqlCeEngine(connectionString).Shrink();
    }

    /// <summary>
    /// Освобождает место на диске, занимаемое базой данных SQL Server Compact, создавая новый файл базы данных на основе уже существующего. 
    /// </summary>
    /// <param name="connectionString">Строка соединения.</param>
    public static void CompactDatabase(string connectionString)
    {
      new SqlCeEngine(connectionString).Compact(null);
    }

    /// <summary>
    /// Меняет пароль для указанной базы данных.
    /// </summary>
    /// <param name="databasePath">Путь расположения файла базы данных.</param>
    /// <param name="newPassword">Новый пароль, который следует установить.</param>
    /// <param name="oldPassword">Текущий пароль.</param>
    public static void ChangeDatabasePassword(string databasePath, string oldPassword, string newPassword)
    {
      string connectionString = String.Format("Data Source={0}; password={1}", databasePath, oldPassword);
      var engine = new SqlCeEngine(connectionString);
      engine.Compact(String.Format("Data Source=; password={0}", newPassword));
    }

    #endregion

  }
}
