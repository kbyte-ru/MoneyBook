using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyBook.WinApp
{

  /// <summary>
  /// «Управлятель» задачами.
  /// </summary>
  class TaskManager
  {

    /// <summary>
    /// Представляет очередь задач.
    /// </summary>
    private ConcurrentQueue<Action> Items = new ConcurrentQueue<Action>(); // volatile
    
    /// <summary>
    /// Признак выполнения заданий, находящихся в очереди.
    /// </summary>
    private bool Executing = false; // volatile

    /// <summary>
    /// Добавляет задачу в очередь.
    /// </summary>
    /// <param name="task"></param>
    public void Add(Action task)
    {
      this.Items.Enqueue(task);
    }

    /// <summary>
    /// Выполняет все задачи, находящиеся в очереди.
    /// </summary>
    public void ExecuteAll()
    {
      if (this.Executing) { return; }

      this.Executing = true;

      Task.Factory.StartNew(() =>
      {
        Console.WriteLine("ExecuteAll start");

        Action action = null;
        while (this.Items.TryDequeue(out action))
        {
          action();
        }

        this.Executing = false;
        Console.WriteLine("ExecuteAll complete");
      });
    }
    
  }

}