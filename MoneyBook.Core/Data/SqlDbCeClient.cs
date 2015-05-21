// ----------------------------------------------------------------------------
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.SqlServerCe;
using System.Runtime.Serialization;
using MoneyBook.Core.Data.Enums;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Вспомогательный класс для работы с базами данных SQL Server Compact Edition.
  /// </summary>
  /// <remarks>
  /// <para>Это упрощенная версия класса для полноценного SQL Server из проекта Nemiro.Data v2.0</para>
  public class SqlDbCeClient : IDisposable
  {

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
    /// Выполняет запрос и возвращает <see cref="System.Data.DataSet"/>.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает <see cref="System.Data.DataSet"/>, содержащий результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустой <see cref="System.Data.DataSet"/>.</para>
    /// </returns>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   c.CommandText = "SELECT * FROM messages; SELECT * FROM users;";
    ///   // выполняем запрос
    ///   DataSet data = c.GetData();
    ///   if(data.Tables.Count > 0)
    ///   {
    ///     // есть данные, выводим в консоль
    ///     foreach(DataTable t in data.Tables)
    ///     {
    ///       foreach(DataRow r in table.Rows)
    ///       {
    ///         Console.WriteLine("id = {0}", r["id"]);
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   c.CommandText = "SELECT * FROM messages; SELECT * FROM users;"
    ///   ' выполняем запрос
    ///   Dim data As DataSet = c.GetData()
    ///   If data.Tables.Count > 0 Then
    ///     ' есть данные, выводим в консоль
    ///     For Each t As DataTable In data.Tables
    ///       For Each r As DataRow In table.Rows
    ///         Console.WriteLine("id = {0}", r("id"))
    ///       Next
    ///     Next
    ///   End If
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataSet GetData()
    {
      return this.GetData(_Cmd);
    }
    /// <summary>
    /// Выполняет указанную SQL-инструкцию и возвращает <see cref="System.Data.DataSet"/>.
    /// Не рекомендуется использовать этот метод, чтобы избежать возникновение SQL Injection при неправильном построении запроса.
    /// Лучше используйте одну из перегрузок этого метода: <see cref="GetData()"/> или <see cref="GetData(SqlCeCommand)"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает <see cref="System.Data.DataSet"/>, содержащий результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустой <see cref="System.Data.DataSet"/>.</para>
    /// </returns>
    /// <example>
    /// <para>
    /// Следующий пример демонстрирует выполнение двух инструкций <c>SELECT FROM</c>, для получения данных из двух таблиц: <c>messages</c> и <c>users</c>. 
    /// В случае успешного выполнения запроса, в <see cref="System.Data.DataSet"/> в свойстве <see cref="System.Data.DataSet.Tables"/> будет две таблицы, содержащие результат выполнения запроса.</para>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос
    ///   DataSet data = c.GetData("SELECT * FROM messages; SELECT * FROM users;");
    ///   if(data.Tables.Count > 0)
    ///   {
    ///     // есть данные, выводим в консоль
    ///     foreach(DataTable t in data.Tables)
    ///     {
    ///       foreach(DataRow r in table.Rows)
    ///       {
    ///         Console.WriteLine("id = {0}", r["id"]);
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim data As DataSet = c.GetData("SELECT * FROM messages; SELECT * FROM users;")
    ///   If data.Tables.Count > 0 Then
    ///     ' есть данные, выводим в консоль
    ///     For Each t As DataTable In data.Tables
    ///       For Each r As DataRow In table.Rows
    ///         Console.WriteLine("id = {0}", r("id"))
    ///       Next
    ///     Next
    ///   End If
    /// End Using
    /// </code>
    /// <para>
    /// Следующий пример демонстрирует динамическое построение SQL-запроса в веб-проекте. В запрос передается значение из параметра <c>search</c> адресной строки.
    /// Например, адрес страницы может быть следующим: http://example.org/users?search=pupkin.
    /// </para>
    /// <para>
    /// Этот код содержит уязвимость типа SQL Injection. Поскольку любой пользователь имеет доступ к своей адресной строке и может указать туда все, что угодно, 
    /// в том числе любую SQL-инструкцию. Например, чтобы удалить все данные из таблицы, достаточно указать следующий адрес страницы:
    /// http://example.org/users?search='; DELETE FROM users; --
    /// </para>
    /// <para>Именно поэтому не рекомендуется использовать подобные методы построения SQL-запросов, лучше используйте методы <see cref="GetData()"/> или <see cref="GetData(SqlCeCommand)"/>.</para>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос
    ///   DataSet data = c.GetData("SELECT * FROM users WHERE first_name LIKE '%" + Request["search"] + "%'");
    ///   if(data.Tables.Count > 0)
    ///   {
    ///     // есть данные, выводим в консоль
    ///     foreach(DataTable t in data.Tables)
    ///     {
    ///       foreach(DataRow r in table.Rows)
    ///       {
    ///         Console.WriteLine("id = {0}", r["id"]);
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim data As DataSet = c.GetData("SELECT * FROM users WHERE first_name LIKE '%" &amp; Request("search") &amp; "%'")
    ///   If data.Tables.Count > 0 Then
    ///     ' есть данные, выводим в консоль
    ///     For Each t As DataTable In data.Tables
    ///       For Each r As DataRow In table.Rows
    ///         Console.WriteLine("id = {0}", r("id"))
    ///       Next
    ///     Next
    ///   End If
    /// End Using
    /// </code>
    /// <para>
    /// Избежать инъекции, в данном примере, можно сделав проверку на наличие в параметре символа одинарной кавычки или экранировать кавычки: <c>Request["search"].Replace("'", "''")</c>. 
    /// Но это не все проблемы, с которыми можно столкнуться при подобном способе построения запросов.
    /// Именно поэтому не рекомендуется использовать подобные методы построения SQL-запросов, лучше используйте методы <see cref="GetData()"/> или <see cref="GetData(SqlCeCommand)"/>. ;-)
    /// </para>
    /// </example>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataSet GetData(string sql)
    {
      return this.GetData(new SqlCeCommand(sql));
    }
    /// <summary>
    /// Выполняет запрос к базе данных с указанными в экземпляре класса <see cref="System.Data.SqlServerCe.SqlCeCommand"/> параметрами и возвращает <see cref="System.Data.DataSet"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает <see cref="System.Data.DataSet"/>, содержащий результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустой <see cref="System.Data.DataSet"/>.</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// В большинстве случаев, рекомендуется использовать метод <see cref="GetData()"/> без параметров, совместно со свойствами 
    /// <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// Суть будет примерно такой же, как и с <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, но работать удобней.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM messages; SELECT * FROM users;");
    ///   // выполняем запрос
    ///   DataSet data = c.GetData(cmd);
    ///   if(data.Tables.Count > 0)
    ///   {
    ///     // есть данные, выводим в консоль
    ///     foreach(DataTable t in data.Tables)
    ///     {
    ///       foreach(DataRow r in table.Rows)
    ///       {
    ///         Console.WriteLine("id = {0}", r["id"]);
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   Dim cmd As New SqlCeCommand("SELECT * FROM messages; SELECT * FROM users")
    ///   ' выполняем запрос
    ///   Dim data As DataSet = c.GetData(cmd)
    ///   If data.Tables.Count > 0 Then
    ///     ' есть данные, выводим в консоль
    ///     For Each t As DataTable In data.Tables
    ///       For Each r As DataRow In table.Rows
    ///         Console.WriteLine("id = {0}", row("id"))
    ///       Next
    ///     Next
    ///   End If
    /// End Using
    /// </code>
    /// </example>
    /// <param name="cmd">Экземпляр <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, содержащий параметры запроса, который необходимо выполнить.</param>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataSet GetData(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);
      
      DataSet result = this.GetData2(cmd);

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }
   
    /// <summary>
    /// Выполняет запрос и возвращает <see cref="System.Data.DataTable"/>.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает таблицу, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустую таблицу.</para>
    /// </returns>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   c.CommandText = "SELECT * FROM users;";
    ///   // выполняем запрос
    ///   DataTable table = c.GetTable();
    ///   // выводим результат в консоль
    ///   foreach(DataRow row in table.Rows)
    ///   {
    ///     Console.WriteLine("login = {0}", row["login"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   c.CommandText = "SELECT * FROM users;"
    ///   ' выполняем запрос
    ///   Dim table As DataTable = c.GetTable()
    ///   ' выводим результат в консоль
    ///   For Each row As DataRow In table.Rows
    ///     Console.WriteLine("login = {0}", row("login"))
    ///   Next
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataTable GetTable()
    {
      return this.GetTable(_Cmd);
    }
    /// <summary>
    /// Выполняет запрос и возвращает <see cref="System.Data.DataTable"/>.
    /// Не рекомендуется использовать этот метод, чтобы избежать возникновение SQL Injection при неправильном построении запроса.
    /// Лучше используйте одну из перегрузок этого метода: <see cref="GetTable()"/> или <see cref="GetTable(SqlCeCommand)"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает таблицу, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустую таблицу.</para>
    /// </returns>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос
    ///   DataTable table = c.GetTable("SELECT * FROM users;");
    ///   // выводим результат в консоль
    ///   foreach(DataRow row in table.Rows)
    ///   {
    ///     Console.WriteLine("login = {0}", row["login"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim table As DataTable = c.GetTable("SELECT * FROM users;")
    ///   ' выводим результат в консоль
    ///   For Each row As DataRow In table.Rows
    ///     Console.WriteLine("login = {0}", row("login"))
    ///   Next
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataTable GetTable(string sql)
    {
      return this.GetTable(new SqlCeCommand(sql));
    }
    /// <summary>
    /// Выполняет запрос к базе данных с указанными в экземпляре класса <see cref="System.Data.SqlServerCe.SqlCeCommand"/> параметрами и возвращает <see cref="System.Data.DataTable"/>.
    /// </summary>
    /// <param name="cmd">Объект типа <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, который должен содержать текст SQL-запроса, а также параметры запроса.</param>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает таблицу, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает пустую таблицу.</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// В большинстве случаев, рекомендуется использовать метод <see cref="GetTable()"/> без параметров, совместно со свойствами 
    /// <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// Суть будет примерно такой же, как и с <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, но работать удобней.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM users;");
    ///   // выполняем запрос
    ///   DataTable table = c.GetTable(cmd);
    ///   // выводим результат в консоль
    ///   foreach(DataRow row in table.Rows)
    ///   {
    ///     Console.WriteLine("login = {0}", row["login"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   Dim cmd As New SqlCeCommand("SELECT * FROM users;")
    ///   ' выполняем запрос
    ///   Dim table As DataTable = c.GetTable(cmd)
    ///   ' выводим результат в консоль
    ///   For Each row As DataRow In table.Rows
    ///     Console.WriteLine("login = {0}", row("login"))
    ///   Next
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataTable GetTable(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);

      DataSet DS = this.GetData2(cmd);

      DataTable result = null;
      if (DS.Tables.Count <= 0)
      {
        result = new DataTable("Empty");
      }
      else
      {
        result = DS.Tables[0];
      }

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }

    /// <summary>
    /// Выполняет запрос и возвращает строку данных.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает строку, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// </returns>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   c.CommandText = "SELECT * FROM users WHERE login = 'anylogin';";
    ///   // выполняем запрос
    ///   DataRow row = c.GetRow();
    ///   // выводим результат в консоль, если есть
    ///   if(row != null)
    ///   {
    ///     Console.WriteLine("nickname = {0}", row["nickname"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   c.CommandText = "SELECT * FROM users WHERE login = 'anylogin';"
    ///   ' выполняем запрос
    ///   Dim row As DataRow = c.GetRow()
    ///   ' выводим результат в консоль, если есть
    ///   If row IsNot Nothing Then
    ///     Console.WriteLine("nickname = {0}", row("nickname"))
    ///   End If
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataRow GetRow()
    {
      return this.GetRow(_Cmd);
    }
    /// <summary>
    /// Выполняет указанный SQL-запрос и возвращает строку данных.
    /// Не рекомендуется использовать этот метод, чтобы избежать возникновение SQL Injection при неправильном построении запроса.
    /// Лучше используйте одну из перегрузок этого метода: <see cref="GetRow()"/> или <see cref="GetRow(SqlCeCommand)"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает строку, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// </returns>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос
    ///   DataRow row = c.GetRow("SELECT * FROM users WHERE login = 'anylogin';");
    ///   // выводим результат в консоль, если есть
    ///   if(row != null)
    ///   {
    ///     Console.WriteLine("nickname = {0}", row["nickname"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim row As DataRow = c.GetRow("SELECT * FROM users WHERE login = 'anylogin';")
    ///   ' выводим результат в консоль, если есть
    ///   If row IsNot Nothing Then
    ///     Console.WriteLine("nickname = {0}", row("nickname"))
    ///   End If
    /// End Using
    /// </code>
    /// </example>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataRow GetRow(string sql)
    {
      return this.GetRow(new SqlCeCommand(sql)); 
    }
    /// <summary>
    /// Выполняет запрос к базе данных с указанными в экземпляре класса <see cref="System.Data.SqlServerCe.SqlCeCommand"/> параметрами и возвращает строку данных.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает строку, содержащую результат выполнения запроса.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// В большинстве случаев, рекомендуется использовать метод <see cref="GetRow()"/> без параметров, совместно со свойствами 
    /// <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// Суть будет примерно такой же, как и с <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, но работать удобней.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var c = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос 
    ///   SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM users WHERE login = 'anylogin';");
    ///   // выполняем запрос
    ///   DataRow row = c.GetRow(cmd);
    ///   // выводим результат в консоль, если есть
    ///   if(row != null)
    ///   {
    ///     Console.WriteLine("nickname = {0}", row["nickname"]);
    ///   }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using c As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   Dim cmd As New SqlCeCommand("SELECT * FROM users WHERE login = 'anylogin';")
    ///   ' выполняем запрос
    ///   Dim row As DataRow = c.GetRow(cmd)
    ///   ' выводим результат в консоль, если есть
    ///   If row IsNot Nothing Then
    ///     Console.WriteLine("nickname = {0}", row("nickname"))
    ///   End If
    /// End Using
    /// </code>
    /// </example>
    /// <param name="cmd">Объект типа <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, который должен содержать текст SQL-запроса, а также параметры запроса.</param>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="ExecuteScalar()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public DataRow GetRow(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);

      DataSet DS = this.GetData2(cmd);

      DataRow result = null;
      if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
      {
        result = DS.Tables[0].Rows[0];
      }

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }

    /// <summary>
    /// Выполняет запрос и возвращает количество задействованных в инструкции строк данных.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Возвращает число, содержащее количество задействованных строк данных в результате выполнения запроса.</para>
    /// <para>Если запрос не коснулся ни одной строки данных, возвращает ноль.</para>
    /// </returns>
    /// <remarks>
    /// <para>Параметры кэширования при работе с этим методом игнорируются.</para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   client.CommandText = "UPDATE messages SET hits = hits + 1 WHERE id = 42";
    ///   // выполняем запрос
    ///   int used = client.ExecuteNonQuery();
    ///   // выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used);
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   client.CommandText = "UPDATE messages SET hits = hits + 1 WHERE id = 42"
    ///   ' выполняем запрос
    ///   Dim used As Integer = client.ExecuteNonQuery()
    ///   ' выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used)
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    public int ExecuteNonQuery()
    {
      return this.ExecuteNonQuery(_Cmd);
    }
    /// <summary>
    /// Выполняет указанный SQL-запрос и возвращает количество задействованных в инструкции строк.
    /// Не рекомендуется использовать этот метод, чтобы избежать возникновение SQL Injection при неправильном построении запроса.
    /// Лучше используйте одну из перегрузок этого метода: <see cref="ExecuteNonQuery()"/> или <see cref="ExecuteNonQuery(SqlCeCommand)"/>.
    /// </summary>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <returns>
    /// <para>Возвращает число, содержащее количество задействованных строк данных в результате выполнения запроса.</para>
    /// <para>Если запрос не коснулся ни одной строки данных, возвращает ноль.</para>
    /// </returns>
    /// <remarks>
    /// <para>Параметры кэширования при работе с этим методом игнорируются.</para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос
    ///   int used = client.ExecuteNonQuery("UPDATE messages SET hits = hits + 1 WHERE id = 42");
    ///   // выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used);
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim used As Integer = client.ExecuteNonQuery("UPDATE messages SET hits = hits + 1 WHERE id = 42")
    ///   ' выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used)
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    public int ExecuteNonQuery(string sql)
    {
      return this.ExecuteNonQuery(new SqlCeCommand(sql));
    }
    /// <summary>
    /// Выполняет запрос к базе данных с указанными в экземпляре класса <see cref="System.Data.SqlServerCe.SqlCeCommand"/> параметрами и возвращает количество задействованных в инструкции строк.
    /// </summary>
    /// <returns>
    /// <para>Возвращает число, содержащее количество задействованных строк данных в результате выполнения запроса.</para>
    /// <para>Если запрос не коснулся ни одной строки данных, возвращает ноль.</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// В большинстве случаев, рекомендуется использовать метод <see cref="ExecuteNonQuery()"/> без параметров, совместно со свойствами 
    /// <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// Суть будет примерно такой же, как и с <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, но работать удобней.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   SqlCeCommand cmd = new SqlCeCommand("UPDATE messages SET hits = hits + 1 WHERE id = @id");
    ///   cmd.Parameters.Add("@id", SqlDbType.Int).Value = 42;
    ///   // выполняем запрос
    ///   int used = client.ExecuteNonQuery(cmd);
    ///   // выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used);
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   Dim cmd As New SqlCeCommand("UPDATE messages SET hits = hits + 1 WHERE id = @id")
    ///   cmd.Parameters.Add("@id", SqlDbType.Int).Value = 42
    ///   ' выполняем запрос
    ///   Dim used As Integer = client.ExecuteNonQuery(cmd)
    ///   ' выводим результат в консоль
    ///   Console.WriteLine("Обновлено {0} строк", used)
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteScalar()"/>
    public int ExecuteNonQuery(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);

      int result = 0;
      Exception ex2 = null;

      this.Connect();

      try
      {
        cmd.Connection = _Conn;
        result = cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        ex2 = ex;
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

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }

    /// <summary>
    /// Выполняет запрос и возвращает первый столбец первой строки из полученного набора данных.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает первый столбец первой строки из полученного набора данных.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// <para>Может вернуть <see cref="System.DBNull.Value"/>, если запрос был успешно выполнен и полученное поле имеет значение <c>NULL</c>.</para>
    /// </returns>
    /// <remarks>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   client.CommandText = "SELECT nickname FROM users WHERE id_users = @id_users";
    ///   client.Parameters.Add("@id_users", SqlDbType.Int).Value = 1024;
    ///   // выполняем запрос и выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", client.ExecuteScalar());
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   client.CommandText = "SELECT nickname FROM users WHERE id_users = @id_users"
    ///   client.Parameters.Add("@id_users", SqlDbType.Int).Value = 1024
    ///   ' выполняем запрос и выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", client.ExecuteScalar())
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public object ExecuteScalar()
    {
      return this.ExecuteScalar(_Cmd);
    }
    /// <summary>
    /// Выполняет указанный SQL-запрос и возвращает первый столбец первой строки из полученного набора данных.
    /// Не рекомендуется использовать этот метод, чтобы избежать возникновение SQL Injection при неправильном построении запроса.
    /// Лучше используйте одну из перегрузок этого метода: <see cref="ExecuteScalar()"/> или <see cref="ExecuteScalar(SqlCeCommand)"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает первый столбец первой строки из полученного набора данных.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// <para>Может вернуть <see cref="System.DBNull.Value"/>, если запрос был успешно выполнен и полученное поле имеет значение <c>NULL</c>.</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// Результат выполнения запроса может кэшироваться, если свойство <see cref="CacheDuration"/> 
    /// больше нуля и <see cref="CacheType"/> имеет отличное от <see cref="Nemiro.Data.CachingType.None"/> значение.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // выполняем запрос 
    ///   object result = client.ExecuteScalar("SELECT nickname FROM users WHERE id_users = 1");
    ///   // выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", result);
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' выполняем запрос
    ///   Dim result As Object 
    ///   result = client.ExecuteScalar("SELECT nickname FROM users WHERE id_users = 1")
    ///   ' выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", result)
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public object ExecuteScalar(string sql)
    {
      return this.ExecuteScalar(new SqlCeCommand(sql));
    }
    /// <summary>
    /// Выполняет запрос к базе данных с указанными в экземпляре класса <see cref="System.Data.SqlServerCe.SqlCeCommand"/> параметрами и возвращает первый столбец первой строки из полученного набора данных.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает первый столбец первой строки из полученного набора данных.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// <para>Может вернуть <see cref="System.DBNull.Value"/>, если запрос был успешно выполнен и полученное поле имеет значение <c>NULL</c>.</para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// В большинстве случаев, рекомендуется использовать метод <see cref="ExecuteScalar()"/> без параметров, совместно со свойствами 
    /// <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// Суть будет примерно такой же, как и с <see cref="System.Data.SqlServerCe.SqlCeCommand"/>, но работать удобней.
    /// </para>
    /// <para>
    /// Результат выполнения запроса может кэшироваться, если свойство <see cref="CacheDuration"/> 
    /// больше нуля и <see cref="CacheType"/> имеет отличное от <see cref="Nemiro.Data.CachingType.None"/> значение.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// using (var client = new SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;"))
    /// {
    ///   // формируем запрос
    ///   SqlCeCommand cmd = new SqlCeCommand("SELECT nickname FROM users WHERE id_users = @id_users");
    ///   cmd.Parameters.Add("@id_users", SqlDbType.Int).Value = 1024;
    ///   // выполняем запрос и выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", client.ExecuteScalar(cmd));
    /// }
    /// </code>
    /// <code lang="VB">
    /// Using client As New SqlDbCeClient("Data Source=MyData.sdf;Persist Security Info=False;")
    ///   ' формируем запрос
    ///   Dim cmd As New SqlCeCommand("SELECT nickname FROM users WHERE id_users = @id_users")
    ///   cmd.Parameters.Add("@id_users", SqlDbType.Int).Value = 1024
    ///   ' выполняем запрос и выводим результат в консоль
    ///   Console.WriteLine("Псевдоним пользователя: {0}", client.ExecuteScalar(cmd))
    /// End Using
    /// </code>
    /// </example>
    /// <seealso cref="GetData()"/>
    /// <seealso cref="GetTable()"/>
    /// <seealso cref="GetRow()"/>
    /// <seealso cref="ExecuteNonQuery()"/>
    public object ExecuteScalar(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);

      DataSet DS = this.GetData2(cmd);
      
      object result = null;
      if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
      {
        result = DS.Tables[0].Rows[0][0];
      }

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
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
    /// Открывает соединение с базой данных, если нет открытого соединения.
    /// </summary>
    /// <remarks>
    /// <para>Нет необходимости вызывать этот метод отдельно, поскольку соединение с базой данных и так будет открываться при первой необходимости.</para>
    /// </remarks>
    /// <seealso cref="Disconnect"/>
    public void Connect()
    {
      if (_Conn == null)
      {
        _Conn = new SqlCeConnection(this.ConnectionString);
      }
      if (_Conn.State == ConnectionState.Closed || _Conn.State == ConnectionState.Broken)
      {
        _Conn.Open();
      }
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
      if (_Conn == null) return;
      if (_Conn.State == ConnectionState.Open || _Conn.State == ConnectionState.Connecting || _Conn.State == ConnectionState.Executing || _Conn.State == ConnectionState.Fetching)
      {
        _Conn.Close();
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
        cmd.Connection = _Conn;
        var DA = new SqlCeDataAdapter(cmd);
        DA.Fill(DS);
      }
      catch (Exception ex)
      {
        ex2 = ex;
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

    #endregion

  }
}
