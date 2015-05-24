using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Указывает на то, что свойство является первичным ключом.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class PrimaryKeyAttribute : Attribute
  {

    /// <summary>
    /// Указывает является ключ числовым счетчиком или нет.
    /// </summary>
    public bool Identity { get; protected set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PrimaryKeyAttribute"/> со значение свойства <see cref="Identity"/> ранвым <b>true</b>.
    /// </summary>
    public PrimaryKeyAttribute()
    {
      this.Identity = true;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PrimaryKeyAttribute"/> с указанным значение свойства <see cref="Identity"/>.
    /// </summary>
    public PrimaryKeyAttribute(bool identity)
    {
      this.Identity = identity;
    }

  }

}