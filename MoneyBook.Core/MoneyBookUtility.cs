using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

    /// <summary>
    /// Возвращает объект указанного типа.
    /// </summary>
    /// <param name="value">Объект</param>
    /// <param name="conversionType">Какой тип нужен</param>
    public static object ChangeType(object value, Type conversionType)
    {
      Type t = value.GetType();
      if (conversionType == typeof(Guid) && t == typeof(Guid))
      {
        return (Guid)value;
      }
      else if (conversionType == typeof(Guid) && t == typeof(string))
      {
        return new Guid(value.ToString());
      }
      else if (conversionType == typeof(Guid) && t == typeof(byte[]))
      {
        return new Guid((byte[])value);
      }
      else if (conversionType == typeof(double) && t == typeof(string))
      {
        return double.Parse(Regex.Replace(value.ToString().Trim(), @",|\.", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture);
      }
      else if (conversionType == typeof(float) && t == typeof(string))
      {
        return float.Parse(Regex.Replace(value.ToString().Trim(), @",|\.", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture);
      }
      else if (conversionType == typeof(decimal) && t == typeof(string))
      {
        return decimal.Parse(Regex.Replace(value.ToString().Trim(), @",|\.", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture);
      }
      return Convert.ChangeType(value, conversionType);
    }

    /// <summary>
    /// Возвращает <see cref="System.Guid"/>, представляющий результат расчета хеш-суммы указанного потока.
    /// </summary>
    /// <param name="value">Поток, для которого следует расчитать хеш-сумму.</param>
    public static Guid GetMD5Hash(Stream value)
    {
      return new Guid(new MD5CryptoServiceProvider().ComputeHash(value));
    }

    /// <summary>
    /// Возвращает <see cref="System.Guid"/>, представляющий результат расчета хеш-суммы указанного массива байт.
    /// </summary>
    /// <param name="value">Массив байт, для которого следует расчитать хеш-сумму.</param>
    public static Guid GetMD5Hash(byte[] value)
    {
      return new Guid(new MD5CryptoServiceProvider().ComputeHash(value));
    }

  }

}