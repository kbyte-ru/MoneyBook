using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoneyBook.Core.Data
{

  /// <summary>
  /// Базовый класс для сущностей БД.
  /// </summary>
  public abstract class Entity : IEntity
  {

    /// <summary>
    /// Статус текущего экземпляра объекта.
    /// </summary>
    public EntityStatus Status { get; internal set; }

    private bool _PrimaryKeyChecked = false;
    private PropertyInfo _PrimaryKey = null;

    /// <summary>
    /// Возвращает значение ключевого поля.
    /// </summary>
    public object PrimaryKeyValue
    {
      get
      {
        if (!_PrimaryKeyChecked)
        {
          _PrimaryKey = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);
          _PrimaryKeyChecked = true;
        } 
        
        if (_PrimaryKey == null)
        {
          throw new NullReferenceException("Primary key not found.");
        }

        return _PrimaryKey.GetValue(this, null);
      }
    }

    /// <summary>
    /// Проверяет эквивалентность значения первичного ключа с указанным объектом.
    /// </summary>
    /// <param name="entity">Экземпляр объекта, с которым следует выполнить проверку.</param>
    public bool PrimaryKeyEquals(IEntity entity)
    {
      if (entity == null) { return false; }
      // смотрим, если entity произведен от Entity, то псравниваем значения ключей
      if (entity.GetType().IsSubclassOf(typeof(Entity)))
      {
        /*if (this.PrimaryKeyValue == null && ((Entity)entity).PrimaryKeyValue == null)
        {
          return true;
        }
        else if (this.PrimaryKeyValue == null || ((Entity)entity).PrimaryKeyValue == null)
        {
          return false;
        }*/

        return this.PrimaryKeyValue.Equals(((Entity)entity).PrimaryKeyValue);
      }
      else
      {
        // если нет, то ищем ключевое поле
        var perimaryKey = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);
        if (perimaryKey == null)
        {
          throw new NullReferenceException("Primary key not found.");
        }

        return this.PrimaryKeyValue.Equals(perimaryKey.GetValue(entity, null));
      }
    }

  }

}