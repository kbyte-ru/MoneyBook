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
  static class TaskManager
  {

    /// <summary>
    /// Представляет очередь задач.
    /// </summary>
    private static ConcurrentQueue<Action> Items = new ConcurrentQueue<Action>(); // volatile
    
    /// <summary>
    /// Признак выполнения заданий, находящихся в очереди.
    /// </summary>
    private static bool Executing = false; // volatile

    /// <summary>
    /// Добавляет задачу в очередь.
    /// </summary>
    /// <param name="task"></param>
    public static void Add(Action task)
    {
      TaskManager.Items.Enqueue(task);
    }

    /// <summary>
    /// Выполняет все задачи, находящиеся в очереди.
    /// </summary>
    public static void ExecuteAll()
    {
      if (TaskManager.Executing) { return; }

      TaskManager.Executing = true;

      Task.Factory.StartNew(() =>
      {
        Console.WriteLine("ExecuteAll start");

        Action action = null;
        while (TaskManager.Items.TryDequeue(out action))
        {
          action();
        }

        TaskManager.Executing = false;
        Console.WriteLine("ExecuteAll complete");
      });
    }
    
  }

}