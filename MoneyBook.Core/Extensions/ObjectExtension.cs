using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoneyBook.Core.Extensions
{

  /// <summary>
  /// Расширения для объектов.
  /// </summary>
  internal static class ObjectExtension
  {

    /// <summary>
    /// Метод переносит данные из одного типа в другой тип с аналогичной структурой.
    /// </summary>
    /// <param name="source">Откуда нужно перенести данные.</param>
    /// <param name="destination">Куда нужно перенести данные.</param>
    /// <remarks>
    /// <para>-- Aleksey Nemiro, 20.03.2014</para>
    /// </remarks>
    public static void CopyTo(this object source, object destination)
    {
      if (destination == null)
      {
        return;
      }

      foreach (var p in source.GetType().GetProperties())
      {
        var p2 = destination.GetType().GetProperty(p.Name);
        if (p2 != null && p2.CanWrite)
        {
          if (p.PropertyType.IsArray)
          {
            // массив
            Array arr = (Array)p.GetValue(source, null);
            if (arr != null)
            {
              var t = p2.PropertyType.GetElementType();
              var result = Array.CreateInstance(t, arr.Length);
              int i = 0;
              foreach (var itm in arr)
              {
                var newItm = (t == typeof(string) ? new string("".ToCharArray()) : Activator.CreateInstance(t));
                itm.CopyTo(newItm);
                result.SetValue(newItm, i);
                i++;
              }
              p2.SetValue(destination, result, null);
            }
          }
          else
          {
            object value = null;
            var indexParameters = p.GetIndexParameters();
            if (indexParameters.Length > 0)
            {
              // смотрим, сколько элементов в массиве
              int count = 0;
              while (true)
              {
                try
                {
                  p.GetValue(source, new object[] { count });
                  count++;
                }
                catch (TargetInvocationException)
                {
                  break;
                }
              }
              // получаем каждное значение
              //var result = Array.CreateInstance(p2.PropertyType, count);
              //p2.SetValue()

              for (int i = 0; i < count; i++)
              {
                var itm = p.GetValue(source, new object[] { i });
                var n = Activator.CreateInstance(p2.PropertyType);
                itm.CopyTo(n);
                //result.SetValue(n, i);
                ((IList)destination).Add(n);
                // p2.SetValue(destination, n, new object[] { i });
              }
            }
            else
            {
              value = p.GetValue(source, null);
            }

            if (value != null)
            {
              // класс, исключая строки
              //if ((p.PropertyType.IsClass || (p.PropertyType.IsValueType && !p.PropertyType.IsEnum && p.PropertyType.Namespace != "System")) && p.PropertyType != typeof(string))
              var valueType = value.GetType();
              if ((valueType.IsClass || (valueType.IsValueType && !valueType.IsEnum && valueType.Namespace != "System")) && valueType != typeof(string))
              {
                var n = Activator.CreateInstance(p2.PropertyType);
                value.CopyTo(n);
                p2.SetValue(destination, n, null);
              }
              // обычное значение
              else
              {
                p2.SetValue(destination, value, null);
              }
            }
          }
        }
      }
    }

  }

}