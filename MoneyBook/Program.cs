using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{
  static class Program
  {

    /// <summary>
    /// Экземпляр профиля текущего пользователя.
    /// </summary>
    internal static MoneyBook.Core.User CurrentUser = null;

    /// <summary>
    /// Базовый каталог, в котором располагаются файлы профилей.
    /// </summary>
    internal static string ProfileBasePath = "";

    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      Program.ProfileBasePath = Path.Combine(Application.StartupPath, "Users");
      
      Application.Run(new Main());
    }
  }
}
