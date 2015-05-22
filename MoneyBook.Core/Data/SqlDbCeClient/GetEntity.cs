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
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

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
    /// Выполняет запрос и возвращает сущность указанного типа.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public T GetEntity<T>()
    {
      return this.GetEntity<T>(_Cmd);
    }

    /// <summary>
    /// Выполняет запрос и возвращает сущность указанного типа.
    /// </summary>
    /// <param name="sql">Запрос SQL, который необходимо выполнить. Будьте очень осторожны при динамическом формировании запроса, особенно при передаче в запрос строковых типов данных.</param>
    /// <typeparam name="T">Тип сущности.</typeparam>   
    public T GetEntity<T>(string sql)
    {
      return this.GetEntity<T>(new SqlCeCommand(sql));
    }

    /// <summary>
    /// Выполняет запрос и возвращает сущность указанного типа.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>   
    public T GetEntity<T>(SqlCeCommand cmd)
    {
      return this.GetEntities<T>(cmd).FirstOrDefault();
    }

  }

}