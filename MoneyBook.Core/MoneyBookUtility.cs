using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoneyBook.Core
{
    
  /// <summary>
  /// Вспомогашки-полезняшки.
  /// </summary>
  internal static class MoneyBookUtility
  {

    /// <summary>
    /// Извлекает указанный внедренный ресурс в виде строки.
    /// </summary>
    /// <param name="name">Имя файла.</param>
    public static string GetEmbeddedResourceString(string name)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = String.Format("{0}.{1}", assembly.GetName().Name, name);
      using (var stream = assembly.GetManifestResourceStream(resourceName))
      {
        using (var reader = new StreamReader(stream))
        {
          return reader.ReadToEnd();
        }
      }
    }

  }

}