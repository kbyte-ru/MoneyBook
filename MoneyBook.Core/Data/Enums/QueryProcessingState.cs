using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Перечень состояний обработки запросов.
  /// </summary>
  public enum QueryProcessingState
  {
    Empty,
    /// <summary>
    /// Подключение к базе.
    /// </summary>
    Connecting,
    /// <summary>
    /// Соединение с базой данных установлено.
    /// </summary>
    Connected,
    //QueryValidation,
    //QueryValidated,
    /// <summary>
    /// Запрос обрабатывается.
    /// </summary>
    Executing,
    /// <summary>
    /// Запрос обработан.
    /// </summary>
    Executed,
    /// <summary>
    /// Обработан отдельный элемент.
    /// </summary>
    ItemProcessed,
    //Disconnection,
    /// <summary>
    /// Соединение с базой закрыто.
    /// </summary>
    Disconnected,
    /// <summary>
    /// Ошибка.
    /// </summary>
    Error
  }

}