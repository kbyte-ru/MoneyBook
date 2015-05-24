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
using System.Data;
using System.Data.SqlServerCe;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Вспомогательный класс для работы с базами данных SQL Server Compact Edition.
  /// </summary>
  /// <remarks>
  /// <para>Это упрощенная версия класса для полноценного SQL Server из проекта Nemiro.Data v2.0</para>
  internal partial class SqlDbCeClient : IDisposable
  {

    /// <summary>
    /// Выполняет запрос и возвращает первый столбец первой строки из полученного набора данных.
    /// Данный метод используется совместно со свойствами <see cref="CommandType"/>, <see cref="CommandText"/> и <see cref="Parameters"/>.
    /// </summary>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает первый столбец первой строки из полученного набора данных.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// <para>Может вернуть <see cref="System.DBNull.Value"/>, если запрос был успешно выполнен и полученное поле имеет значение <c>NULL</c>.</para>
    /// </returns>
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
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <returns>
    /// <para>Если запрос успешно выполнен, возвращает первый столбец первой строки из полученного набора данных.</para>
    /// <para>Если запрос не дал результатов, возвращает <c>NULL</c> (в Visual Basic .NET - <c>Nothing</c>).</para>
    /// <para>Может вернуть <see cref="System.DBNull.Value"/>, если запрос был успешно выполнен и полученное поле имеет значение <c>NULL</c>.</para>
    /// </returns>
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

  }

}