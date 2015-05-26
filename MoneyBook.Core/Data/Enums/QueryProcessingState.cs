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
    /// <summary>
    /// Обработка еще не проводилась.
    /// </summary>
    Empty,
    /// <summary>
    /// Подключение к базе.
    /// </summary>
    Connecting,
    /// <summary>
    /// Соединение с базой данных установлено.
    /// </summary>
    Connected,
    /// <summary>
    /// Запрос обрабатывается.
    /// </summary>
    Executing,
    /// <summary>
    /// Запрос обработан.
    /// </summary>
    Executed,
    /// <summary>
    /// Обработка отдельного элемента.
    /// </summary>
    ItemProcessing,
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