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
using System.Reflection;
using MoneyBook.Core.Data.Enums;

// фрагмент Nemiro.Data v2.0

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Атрибут указывает, что класс реализует таблицу базы данных.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
  public class TableAttribute : Attribute
  {

    private string _TableName = String.Empty;
    /// <summary>
    /// Имя таблицы, которая реализована в классе.
    /// </summary>
    public string TableName
    {
      get
      {
        return _TableName;
      }
    }

    private string _ConnectionString = String.Empty;
    /// <summary>
    /// Строка соединения с БД (опционально).
    /// </summary>
    public string ConnectionString
    {
      get
      {
        return _ConnectionString;
      }
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TableAttribute"/> с указанием имени таблицы.
    /// </summary>
    /// <param name="tableName">Имя таблицы, которая реализована в классе.</param>
    public TableAttribute(string tableName) : this(tableName, null) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TableAttribute"/> с указанием имени таблицы и строкой соединения с базой данных.
    /// </summary>
    /// <param name="tableName">Имя таблицы, которая реализована в классе.</param>
    /// <param name="connectionString">Строка соединения с базой данных, либо имя параметра строки соединения в файле конфигурации приложения.</param>
    public TableAttribute(string tableName, string connectionString)
    {
      _TableName = tableName;
      _ConnectionString = connectionString;
    }

  }

}
