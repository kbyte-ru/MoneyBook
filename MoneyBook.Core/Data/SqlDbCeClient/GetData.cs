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

  }

}