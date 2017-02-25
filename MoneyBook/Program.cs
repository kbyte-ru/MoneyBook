using System;
using System.IO;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{
  static class Program
  {

    internal const short InfoIdCustomShowDetails = 1001;
    internal const short InfoIdCustomDetailsSize = 1002;

    /// <summary>
    /// Экземпляр профиля текущего пользователя.
    /// </summary>
    [Obsolete("Лучше передавать ссылку.", true)]
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
    }

  }
}
