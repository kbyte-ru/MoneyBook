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

// фрагмент Nemiro.Data v2.0

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Вспомогательный класс для работы с типами данных SQL Server.
  /// </summary>
  /// <remarks>
  /// <para>Этот класс используется для внутренних нужд библиотеки, но возможно он будет полезен и для решения каких-нибудь ваших задач.</para>
  /// </remarks>
  internal static class SqlType
  {

    private static SqlDbType[] _StringTypeList = { SqlDbType.NVarChar, SqlDbType.VarChar, SqlDbType.Text, SqlDbType.NText, SqlDbType.Char, SqlDbType.NChar };
    private static SqlDbType[] _NumericTypeList = { SqlDbType.Int, SqlDbType.BigInt, SqlDbType.TinyInt, SqlDbType.Real, SqlDbType.Float, SqlDbType.Money, SqlDbType.SmallInt, SqlDbType.SmallMoney };
    private static SqlDbType[] _DateTypeList = { SqlDbType.Date, SqlDbType.DateTime, SqlDbType.DateTime2, SqlDbType.DateTimeOffset, SqlDbType.SmallDateTime, SqlDbType.Time };

    /// <summary>
    /// Перечь строковых типов данных SQL Server.
    /// </summary>
    /// <remarks>
    /// <para>
    /// В состав списка строковых типов данных входят:
    /// <list type="bullet">
    /// <item><description>SqlDbType.VarChar</description></item>
    /// <item><description>SqlDbType.NVarChar</description></item>
    /// <item><description>SqlDbType.Text</description></item>
    /// <item><description>SqlDbType.NText</description></item>
    /// <item><description>SqlDbType.Char</description></item>
    /// <item><description>SqlDbType.NChar</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static SqlDbType[] StringTypeList
    {
      get
      {
        return _StringTypeList;
      }
    }

    /// <summary>
    /// Перечь числовых типов данных SQL Server.
    /// </summary>
    /// <remarks>
    /// <para>
    /// В состав списка числовых типов данных входят:
    /// <list type="bullet">
    /// <item><description>SqlDbType.Int</description></item>
    /// <item><description>SqlDbType.BigInt</description></item>
    /// <item><description>SqlDbType.Float</description></item>
    /// <item><description>SqlDbType.Money</description></item>
    /// <item><description>SqlDbType.SmallInt</description></item>
    /// <item><description>SqlDbType.SmallMoney</description></item>
    /// <item><description>SqlDbType.TinyInt</description></item>
    /// <item><description>SqlDbType.Real</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static SqlDbType[] NumericTypeList
    {
      get
      {
        return _NumericTypeList;
      }
    }

    /// <summary>
    /// Перечь типов данных дат и времени.
    /// </summary>
    /// <remarks>
    /// <para>
    /// В состав списка типов данных дат и времени входят:
    /// <list type="bullet">
    /// <item><description>SqlDbType.Date</description></item>
    /// <item><description>SqlDbType.DateTime</description></item>
    /// <item><description>SqlDbType.DateTime2</description></item>
    /// <item><description>SqlDbType.DateTimeOffset</description></item>
    /// <item><description>SqlDbType.SmallDateTime</description></item>
    /// <item><description>SqlDbType.Time</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static SqlDbType[] DateTypeList
    {
      get
      {
        return _DateTypeList;
      }
    }

    /// <summary>
    /// Преобразует указанный тип .NET в тип SQL Server.
    /// </summary>
    /// <param name="t">Тип .NET, который нужно конвертировать в тип SQL Server.</param>
    /// <param name="version">Версия SQL Server.</param>
    /// <example>
    /// <code lang="C#">
    /// SqlDbType result = SqlType.ConvertToSqlDbType(typeof(long));
    /// Console.WriteLine("Тип данных SQL Server: {0}", result.ToString());
    /// </code>
    /// <code lang="VB">
    /// Dim result As SqlDbType = SqlType.ConvertToSqlDbType(GetType(Long))
    /// Console.WriteLine("Тип данных SQL Server: {0}", result.ToString())
    /// </code>
    /// </example>
    /// <returns>
    /// <para>Если указанный <paramref name="t"/> невозможно преобразовать в тип SQL Server, функция возвращает <see cref="System.Data.SqlDbType.Variant"/>.</para>
    /// </returns>
    public static SqlDbType ConvertToSqlDbType(Type t, Version version = null)
    {
      if (t == typeof(int))
      {
        return SqlDbType.Int;
      }
      else if (t == typeof(byte[]))
      {
        if (version != null && version.Major < 10)
        {
          return SqlDbType.Image;
        }
        else
        {
          return SqlDbType.VarBinary;
        }
      }
      else if (t == typeof(bool))
      {
        return SqlDbType.Bit;
      }
      else if (t == typeof(long))
      {
        return SqlDbType.BigInt;
      }
      else if (t == typeof(char) || t == typeof(string))
      {
        if (version != null && version.Major <= 8)
        {
          return SqlDbType.NText;
        }
        else
        {
          return SqlDbType.NVarChar;
        }
      }
      else if (t == typeof(DateTime))
      {
        if (version != null && version.Major < 10)
        {
          return SqlDbType.DateTime;
        }
        else
        {
          return SqlDbType.DateTime2;
        }
      }
      else if (t == typeof(DateTimeOffset))
      {
        return SqlDbType.DateTimeOffset;
      }
      else if (t == typeof(decimal))
      {
        return SqlDbType.Money;
      }
      else if (t == typeof(double))
      {
        return SqlDbType.Float;
      }
      else if (t == typeof(float))
      {
        return SqlDbType.Real;
      }
      else if (t == typeof(Int16))
      {
        return SqlDbType.SmallInt;
      }
      else if (t == typeof(byte))
      {
        return SqlDbType.TinyInt;
      }
      else if (t == typeof(Guid))
      {
        return SqlDbType.UniqueIdentifier;
      }
      else if (t == typeof(System.Xml.XmlDocument) || t == typeof(System.Xml.XmlReader))
      {
        return SqlDbType.Xml;
      }
      else
      {
        return SqlDbType.Variant;
      }
    }

    /// <summary>
    /// Проверяет, является ли указанный тип данных SQL Server строковым типом или нет.
    /// </summary>
    /// <param name="t">Тип данных SQL Server.</param>
    /// <returns>Возвращает <c>True</c>, если указанный тип данных SQL Server является строковым. В противном случае функция возвращает <c>False</c>.</returns>
    /// <remarks>
    /// <para>Проверка проводится по списку типов, указанном в свойстве <see cref="StringTypeList"/>.</para>
    /// </remarks>
    public static bool IsStringType(SqlDbType t)
    {
      return Array.IndexOf(SqlType.StringTypeList, t) != -1;
    }

    /// <summary>
    /// Проверяет, является ли указанный тип данных SQL Server числовым типом или нет.
    /// </summary>
    /// <param name="t">Тип данных SQL Server.</param>
    /// <returns>Возвращает <c>True</c>, если указанный тип данных SQL Server является числовым. В противном случае функция возвращает <c>False</c>.</returns>
    /// <remarks>
    /// <para>Проверка проводится по списку типов, указанном в свойстве <see cref="NumericTypeList"/>.</para>
    /// </remarks>
    public static bool IsNumericType(SqlDbType t)
    {
      return Array.IndexOf(SqlType.NumericTypeList, t) != -1;
    }

    /// <summary>
    /// Проверяет, является ли указанный тип данных SQL Server датой или временем.
    /// </summary>
    /// <param name="t">Тип данных SQL Server.</param>
    /// <returns>Возвращает <c>True</c>, если указанный тип данных SQL Server является датой или временем. В противном случае функция возвращает <c>False</c>.</returns>
    /// <remarks>
    /// <para>Проверка проводится по списку типов, указанном в свойстве <see cref="DateTypeList"/>.</para>
    /// </remarks>
    public static bool IsDateType(SqlDbType t)
    {
      return Array.IndexOf(SqlType.DateTypeList, t) != -1;
    }

    /// <summary>
    /// Проверяет, является ли указанный тип данных SQL Server глобальным уникальным идентификатором (<see cref="System.Guid"/>).
    /// </summary>
    /// <param name="t">Тип данных SQL Server.</param>
    /// <returns>Возвращает <c>True</c>, если указанный тип данных SQL Server является <see cref="System.Guid"/>. В противном случае функция возвращает <c>False</c>.</returns>
    public static bool IsGuidType(SqlDbType t)
    {
      return t == SqlDbType.UniqueIdentifier;
    }

  }

}