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
using System.Web;
using System.Data.SqlServerCe;
using MoneyBook.Core.Data.Enums;

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
        this.OnQueryProcessing(new QueryProcessingEventArgs(QueryProcessingState.Executing));

        cmd.Connection = _Conn;
        result = cmd.ExecuteNonQuery();

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

      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }

  }

}