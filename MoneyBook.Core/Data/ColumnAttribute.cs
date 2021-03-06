﻿// ----------------------------------------------------------------------------
// Copyright (c) Aleksey Nemiro, 2015. All rights reserved.
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using MoneyBook.Core.Data.Enums;

// фрагмент Nemiro.Data v2.0

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Атрибут указывает, что свойство реализует поле таблицы.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class ColumnAttribute : Attribute
  {

    #region ..свойства..

    private string _ColumnName = String.Empty;
    /// <summary>
    /// Имя колонки, которая реализована в свойстве.
    /// </summary>
    public string ColumnName
    {
      get
      {
        return _ColumnName;
      }
    }

    private object _DataType = null;
    /// <summary>
    /// Тип данных поля. Допускается использование перечисления <see cref="System.Data.SqlDbType"/>.
    /// </summary>
    public object DataType
    {
      get
      {
        return _DataType;
      }
    }

    /// <summary>
    /// Отображаемое имя (может использоваться при реализации журнала изменений данных).
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Определяет наибольший размер поля (в байтах). Ноль - без ограничений (по умолчанию).
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Значение по умолчанию.
    /// </summary>
    public object Default { get; set; }

    private bool? _IsPrimaryKey = null;

    /// <summary>
    /// Возвращает <c>True</c>, если <see cref="OwnerClass"/> содержит <see cref="PrimaryKeyAttribute"/>.
    /// </summary>
    internal protected bool IsPrimaryKey
    {
      get
      {
        return _IsPrimaryKey.Value;
      }
    }

    private bool? _IsUnique = null;

    /// <summary>
    /// Возвращает <c>True</c>, если <see cref="OwnerClass"/> содержит <see cref="UniqueValueAttribute"/>.
    /// </summary>
    internal protected bool IsUnique
    {
      get
      {
        return _IsUnique.Value;
      }
    }

    private bool? _IsIdentity = null;

    /// <summary>
    /// Возвращает <c>True</c>, если <see cref="OwnerClass"/> содержит <see cref="PrimaryKeyAttribute"/> с флагом <see cref="PrimaryKeyAttribute.Identity"/> в значении <c>True</c>.
    /// </summary>
    internal protected bool IsIdentity
    {
      get
      {
        return _IsIdentity.Value;
      }
    }

    private bool? _AllowNull = null;

    /// <summary>
    /// Возвращает <c>True</c>, если <see cref="OwnerClass"/> содержит <see cref="AllowNullAttribute"/>.
    /// </summary>
    internal protected bool AllowNull
    {
      get
      {
        return _AllowNull.Value;
      }
    }

    /// <summary>
    /// Содержит имя параметра подстановки в запрос. Например: <c>@col_name</c>.
    /// </summary>
    internal protected string ParameterName
    {
      get
      {
        return String.Format("@{0}", this.ColumnName);
      }
    }

    /// <summary>
    /// Содержит тип данных SQL Server, если DataType может быть преобразован в SqlDbType.
    /// </summary>
    /// <returns></returns>
    internal protected SqlDbType SqlDbType
    {
      get
      {
        if (this.DataType.GetType() == typeof(SqlDbType)) return (SqlDbType)this.DataType;
        return (SqlDbType)Enum.Parse(typeof(SqlDbType), this.DataType.ToString(), true);
      }
    }


    /// <summary>
    /// Ссылка на класс.
    /// </summary>
    protected object OwnerClass { get; set; }

    /// <summary>
    /// Тип класса-владельца.
    /// </summary>
    protected Type OwnerClassType
    {
      get
      {
        return OwnerClass.GetType();
      }
    }

    /// <summary>
    /// Ссылка на свойство.
    /// </summary>
    protected PropertyInfo OwnerProperty { get; set; }

    #endregion
    #region ..конструктор..

    // SQL
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ColumnAttribute"/> с указанием имени поля и типа данных SQL Server.
    /// </summary>
    /// <param name="columnName">Имя поля таблицы базы данных SQL Server.</param>
    /// <param name="dataType">Тип данных SQL Server, содержащихся в поле.</param>
    public ColumnAttribute(string columnName, SqlDbType dataType) : this(columnName, (object)dataType, 0, null) { }

    // Универсальный
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ColumnAttribute"/> с указанием имени поля и типа данных.
    /// </summary>
    public ColumnAttribute(string columnName, object dataType) : this(columnName, dataType, 0, null) { }
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ColumnAttribute"/> с указанными параметрами.
    /// </summary>
    /// <param name="columnName">Имя поля таблицы базы данных.</param>
    /// <param name="dataType">Тип данных поля в базе.</param>
    /// <param name="default">Значение по умолчанию.</param>
    /// <param name="size">Определяет наибольший размер поля (в байтах). Ноль - без ограничений (по умолчанию).</param>
    public ColumnAttribute(string columnName, object dataType, int size, object @default)
    {
      _ColumnName = columnName;
      _DataType = dataType;
      this.Size = size;
      this.Default = @default;
    }

    #endregion
    #region ..функции и методы..

    // NOTE: Потенциальная ошибка может быть, если имя поля будет содержать запрещенные символы.

    /// <summary>
    /// Возвращает параметр SQL Server CE.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal protected System.Data.SqlServerCe.SqlCeParameter GetSqlParameter()
    {
      return new System.Data.SqlServerCe.SqlCeParameter(this.ParameterName, this.SqlDbType, this.Size);
    }

    /// <summary>
    /// Возвращает правильное значение параметра для передачи в базу.
    /// Для правильной работы нужно вызвать SetOwner.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal protected object GetValidParameterValue(PropertyInfo p)
    {
      if (this.OwnerClass == null) throw new Exception("Для использования этого метода нужно вызвать метод SetOwner, чтобы свойство OwnerClass имело значение отличное от null.");
      object value = p.GetValue(this.OwnerClass, null);
      Type t = null;
      if (value != null) t = value.GetType();
      // если пустое значение
      if (this.AllowNull && value == null)
      { // разрешено NULL
        return DBNull.Value;
      }
      else if (!this.AllowNull && (value == null || (value != null && value != DBNull.Value && ((t == typeof(DateTime) && (DateTime)value == DateTime.MinValue) || (t == typeof(DateTimeOffset) && (DateTimeOffset)value == DateTimeOffset.MinValue) || (t == typeof(Guid) && (Guid)value == Guid.Empty)))))
      { // NULL не разрешен
        // используем значение по умолчанию, если есть
        if (this.Default != null && this.Default != DBNull.Value)
        {
          if (this.Default.GetType() == typeof(DefaultValues))
          {
            object defaultValue = null;
            switch ((DefaultValues)this.Default)
            {
              case DefaultValues.Now:
                defaultValue = DateTime.Now;
                p.SetValue(this.OwnerClass, defaultValue, null);
                return defaultValue;
              case DefaultValues.NewId:
                defaultValue = Guid.NewGuid();
                p.SetValue(this.OwnerClass, defaultValue, null);
                return defaultValue;
              default:
                return this.GetSqlEmptyValue();
            }
          }
          else
          {
            p.SetValue(this.OwnerClass, this.Default, null);
            return this.Default;
          }
        }
        // пустое значение в зависимости от типа
        return this.GetSqlEmptyValue();
      }
      // значение не пустое
      if (SqlType.IsDateType(this.SqlDbType))
      {
        if (this.SqlDbType == System.Data.SqlDbType.DateTime && ((DateTime)value).Year < 1753)
        {
          return new DateTime(1753, 1, 1, 12, 0, 0);
        }
        else if (this.SqlDbType == System.Data.SqlDbType.SmallDateTime && ((DateTime)value).Year < 1900)
        {
          return new DateTime(1900, 1, 1, 12, 0, 0);
        }
      }
      else if (SqlType.IsStringType(this.SqlDbType) && this.Size > 0 && Convert.ToString(value).Length > this.Size)
      { // усекаем данные в свойстве
        p.SetValue(this.OwnerClass, Convert.ToString(value).Substring(0, this.Size), null);
        //return Convert.ToString(value).Substring(0, this.Size);
      }
      // возвращаем то, что получили
      return value;
    }

    /// <summary>
    /// Возвращает пустое значение для передачи в SQL Server
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal protected object GetSqlEmptyValue()
    {
      if (SqlType.IsGuidType(this.SqlDbType))
      {
        return Guid.Empty;
      }
      else if (SqlType.IsStringType(this.SqlDbType))
      {
        return String.Empty;
      }
      else if (SqlType.IsNumericType(this.SqlDbType))
      {
        return 0;
      }
      else if (SqlType.IsDateType(this.SqlDbType))
      {
        if (this.SqlDbType == System.Data.SqlDbType.DateTime)
        {
          return new DateTime(1753, 1, 1, 12, 0, 0);
        }
        else if (this.SqlDbType == System.Data.SqlDbType.SmallDateTime)
        {
          return new DateTime(1900, 1, 1, 12, 0, 0);
        }
        return new DateTime(1, 1, 1, 0, 0, 0);
      }
      return DBNull.Value;
    }

    /// <summary>
    /// Устанавливает родителя.
    /// </summary>
    /// <param name="class">Ссылка на класс, к которому относится свойство, к которому относится текущий атрибут.</param>
    /// <param name="property">Ссылка на свойство.</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal protected void SetOwner(IEntity @class, PropertyInfo property)
    {
      this.OwnerClass = @class;
      this.OwnerProperty = property;
      _IsUnique = this.OwnerProperty.GetCustomAttributes(typeof(UniqueValueAttribute), true).Length > 0;
      _AllowNull = this.OwnerProperty.GetCustomAttributes(typeof(AllowNullAttribute), true).Length > 0;
      var p = (PrimaryKeyAttribute)this.OwnerProperty.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).FirstOrDefault();
      if (p != null)
      {
        _IsPrimaryKey = true;
        _IsIdentity = p.Identity;
      }
      else
      {
        _IsPrimaryKey = _IsIdentity = false;
      }
    }

    #endregion

  }

}