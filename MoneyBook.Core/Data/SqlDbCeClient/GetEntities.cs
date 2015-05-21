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
    /// Выполняет запрос и возвращает список сущностей указанного типа.
    /// </summary>
    /// <typeparam name="T">Тип, список значений которого следует вернуть.</typeparam>
    public List<T> GetEntities<T>()
    {
      return this.GetEntities<T>(_Cmd);
    }

    /// <summary>
    /// Выполняет запрос и возвращает список сущностей указанного типа.
    /// </summary>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <typeparam name="T">Тип, список значений которого следует вернуть.</typeparam>
    public List<T> GetEntities<T>(string sql)
    {
      return this.GetEntities<T>(new SqlCeCommand(sql));
    }

    /// <summary>
    /// Выполняет запрос и возвращает список сущностей указанного типа.
    /// </summary>
    /// <typeparam name="T">Тип, список значений которого следует вернуть.</typeparam>
    public List<T> GetEntities<T>(SqlCeCommand cmd)
    {
      DateTime timePoint = DateTime.Now;

      // проверяем параметры
      this.ValidateCommand(ref cmd);

      var data = this.GetData2(cmd);

      // формируем результат
      var result = new List<T>();

      if (data.Tables.Count > 0)
      {
        foreach (DataRow row in data.Tables[0].Rows)
        {
          result.Add(this.CreateEntityInstance<T>(row));
        }
      }

      // время
      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return result;
    }

  }

}