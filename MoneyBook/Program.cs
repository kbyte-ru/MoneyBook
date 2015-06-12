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
      // параметры приложения
      Program.ProfileBasePath = Path.Combine(Application.StartupPath, "Users");

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.ApplicationExit += Program.OnApplicationExit;

      // форма входа
      new Login().Show();

      // запускаем приложение
      Application.Run();
    }

    static void OnApplicationExit(object sender, EventArgs e)
    {
      if (Program.CurrentUser != null)
      {
        Program.CurrentUser.Flush();
        //Program.CurrentUser = null;
      }
    }

  }
}
