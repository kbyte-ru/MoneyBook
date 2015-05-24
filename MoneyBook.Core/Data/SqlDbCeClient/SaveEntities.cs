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
    /// Выполняет запись указанных сущностей в базу данных.
    /// </summary>
    /// <param name="entities">Список экземпляров сущностей, которые следует сохранить в базу.</param>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public List<T> SaveEntities<T>(List<T> entities) where T : IEntity
    {
      if (entities == null)
      {
        throw new ArgumentNullException("entities");
      }

      DateTime timePoint = DateTime.Now;

      // выполняем сохранение каждой записи
      foreach (var item in entities)
      {
        this.SaveEntityInstanceToDatabase<T>(item);
      }

      // время
      _LastQueryTime = DateTime.Now.Subtract(timePoint);

      return entities;
    }

    /// <summary>
    /// Выполняет запись указанной сущности в базу данных.
    /// </summary>
    /// <param name="entity">Экземпляр сущности, которую следует сохранить в базу.</param>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public T SaveEntities<T>(T entity) where T : IEntity
    {
      return this.SaveEntityInstanceToDatabase<T>(entity);
    }

  }

}