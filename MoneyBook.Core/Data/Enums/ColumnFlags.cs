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

// фрагмент Nemiro.Data v2.0

namespace MoneyBook.Core.Data.Enums
{

  /// <summary>
  /// Перечень флагов, определяющих интерпретацию свойства при использовании в работаете с источником данных.
  /// </summary>
  public enum ColumnFlags
  {
    /// <summary>
    /// Нет.
    /// </summary>
    None = 0,
    /// <summary>
    /// Ключевое поле.
    /// </summary>
    PrimaryKey = 1,
    /// <summary>
    /// Счетчик, используется совместно с <see cref="ColumnFlags.PrimaryKey"/>.
    /// </summary>
    /// <remarks>Данная опция указывает на то, что значение для поля будет присвоено автоматически.</remarks>
    Identity = 2,
    /// <summary>
    /// Разрешить записывать <c>NULL</c>, при отсутствии значения.
    /// </summary>
    AllowNull = 4,
    /// <summary>
    /// Указывает на то, что значение поля уникально и его можно использовать при проверке дубликатов записей.
    /// </summary>
    Unique = 8
  }

}