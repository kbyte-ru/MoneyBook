// ----------------------------------------------------------------------------
// Copyright (c) Aleksey Nemiro, 2008-2014. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

// https://github.com/alekseynemiro/Nemiro.Convertion

namespace MoneyBook.WinApp
{

  public static class Convert
  {

    /// <summary>
    /// Converts the value of the specified object to an equivalent decimal number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="default">The default value is to be returned in the case of conversion errors.</param>
    public static decimal? ToDecimal(object value, decimal? @default)
    {
      if (!Convert.HasValue(value)) { return @default; }
      decimal result = 0;
      if (!decimal.TryParse(Convert.GetNumber(value), NumberStyles.Any, Thread.CurrentThread.CurrentCulture, out result))
      {
        return @default;
      }
      return result;
    }

    /// <summary>
    /// Converts the value of the specified object to an equivalent decimal number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="default">The default value is to be returned in the case of conversion errors.</param>
    public static decimal ToDecimal(object value, decimal @default)
    {
      return Convert.ToDecimal(value, (decimal?)@default).GetValueOrDefault(@default);
    }

    /// <summary>
    /// Converts the value of the specified object to an equivalent decimal number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static decimal ToDecimal(object value)
    {
      return Convert.ToDecimal(value, null).GetValueOrDefault();
    }
    
    /// <summary>
    /// Checks in the specified value has data or is empty.
    /// </summary>
    /// <param name="value">The value for checking.</param>
    private static bool HasValue(object value)
    {
      if (value == DBNull.Value || value == null) { return false; }
      if (value.GetType() == typeof(string) && String.IsNullOrEmpty(value.ToString())) { return false; }
      return true;
    }

    /// <summary>
    /// Returns a string containing a number.
    /// </summary>
    /// <param name="value">The value for processing.</param>
    private static string GetNumber(object value)
    {
      if (!Convert.HasValue(value)) { return "0"; }
      return Regex.Replace(Regex.Replace(value.ToString(), @",|\.", NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator), @"\s+", "");
    }

  }

}