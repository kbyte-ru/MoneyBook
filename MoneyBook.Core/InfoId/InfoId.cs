using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyBook.Core
{

  /// <summary>
  /// Представляет идентификатор элемента информации.
  /// </summary>
  /// <remarks><para>Значение от -2^15 (-32 768) до 2^15-1 (32 767).</para></remarks>
  public partial struct InfoId
  {

    // 1000-9999 - пользовательские параметры

    #region ..свойства..

    private short _Value;

    /// <summary>
    /// Значение.
    /// </summary>
    public short Value
    {
      get
      {
        return _Value;
      }
      set
      {
        _Value = value;
      }
    }
    
    #endregion
    #region ..конструктор..

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="InfoId"/> с указанным значением.
    /// </summary>
    /// <param name="value">Значение.</param>
    internal InfoId(short value)
    {
      _Value = value;
    }
    
    #endregion
    #region ..методы..

    /// <summary>
    /// Возвращает значение текущего экземпляра.
    /// </summary>
    public override string ToString()
    {
      return this.Value.ToString();
    }

    /// <summary>
    /// Возвращает хэш-код текущего экземпляра.
    /// </summary>
    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }

    /// <summary>
    /// Проверяет, является ли текущий экземпляр эквивалентом указанному объекту или нет.
    /// </summary>
    /// <param name="obj">Объект, с которым следует провести сравнение.</param>
    public override bool Equals(object obj)
    {
      if (obj != null && obj.GetType() == typeof(InfoId))
      {
        return this.Equals(((InfoId)obj).Value);
      }

      return this.Value.Equals(obj);
    }

    /// <summary>
    /// Проверяет, является ли текущий экземпляр эквивалентом указанному экземпляру <see cref="InfoId"/>.
    /// </summary>
    /// <param name="value">Экземпляр <see cref="InfoId"/>, с которым следует провести сравнение.</param>
    public bool Equals(InfoId value)
    {
      return this.Value.Equals(value.Value);
    }

    #endregion
    #region ..операторы..

    public static implicit operator short(InfoId value)
    {
      return value.Value;
    }

    public static implicit operator InfoId(short value)
    {
      return new InfoId(value);
    }

    public static bool operator !=(InfoId x, InfoId y)
    {
      return !x.Equals(y.Value);
    }
    public static bool operator ==(InfoId x, InfoId y)
    {
      return x.Equals(y.Value);
    }

    public static bool operator !=(InfoId x, short y)
    {
      return !x.Equals(new InfoId(y));
    }
    public static bool operator ==(InfoId x, short y)
    {
      return x.Equals(new InfoId(y));
    }

    public static bool operator !=(short x, InfoId y)
    {
      return !new InfoId(x).Equals(y.Value);
    }
    public static bool operator ==(short x, InfoId y)
    {
      return new InfoId(x).Equals(y.Value);
    }

    #endregion

  }

}