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

  }

}