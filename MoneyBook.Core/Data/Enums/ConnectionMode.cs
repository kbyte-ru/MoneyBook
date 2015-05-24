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
  /// Режим подключения к базе данных.
  /// </summary>
  internal enum ConnectionMode
  {
    /// <summary>
    /// Автоматически открывать и закрывать соединение с базой (рекомендуется). Используется по умолчанию.
    /// </summary>
    Auto = 0,
    /// <summary>
    /// Вручную закрывать соединение с базой.
    /// </summary>
    /// <remarks>
    /// <para>Открытие соединений с базой данных при ручном режиме производится автоматически, при первой необходимости.</para>
    /// </remarks>
    Manual = 1
  }

}