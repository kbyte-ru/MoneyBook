using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  internal class QueryProcessingEventArgs : EventArgs
  {

    /// <summary>
    /// Всего элементов.
    /// </summary>
    public int TotalItems { get; protected set; }

    /// <summary>
    /// Позизия обработанного элемента.
    /// </summary>
    public int ItemPosition { get; internal set; }

    /// <summary>
    /// Экземпляр обработанного элемента.
    /// </summary>
    public IEntity Item { get; internal set; }

    /// <summary>
    /// Текущее состояние.
    /// </summary>
    public QueryProcessingState Status { get; protected set; }

    /// <summary>
    /// Экземпляр исключения, если <see cref="Status"/> является <see cref="QueryProcessingState.Error"/>.
    /// </summary>
    public Exception Exception { get; protected set; }

    internal QueryProcessingEventArgs(QueryProcessingState status)
    {
      this.Status = status;
    }

    internal QueryProcessingEventArgs(Exception ex)
    {
      if (ex == null)
      {
        throw new ArgumentNullException("ex");
      }
      this.Status = QueryProcessingState.Error;
      this.Exception = ex;
    }

    internal QueryProcessingEventArgs(QueryProcessingState status, int totalItems)
    {
      this.Status = QueryProcessingState.ItemProcessed;
      this.TotalItems = totalItems;
    }

  }

}