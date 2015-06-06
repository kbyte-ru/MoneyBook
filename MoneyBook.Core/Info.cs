using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;

namespace MoneyBook.Core
{
  
  /// <summary>
  /// Предствляет техническую информацию о файле профиля пользователя.
  /// </summary>
  [Serializable]
  public class Info
  {

    #region ..свойства..

    /// <summary>
    /// Экземпляр пользователя, к которому относится информация.
    /// </summary>
    private User CurrentUser = null;

    /// <summary>
    /// Идентификаторы записей, которые следует сохранить в базу при вызове метода <see cref="Flush"/>.
    /// </summary>
    private List<InfoId> ToSave = null;

    /// <summary>
    /// Коллекция записей.
    /// </summary>
    private Dictionary<InfoId, string> Items = null;

    /// <summary>
    /// Возвращает значение указанного параметра.
    /// </summary>
    /// <param name="id">Идентификатор параметра, значение которого следует получить.</param>
    public string this[InfoId id]
    {
      get
      {
        if (!this.Items.ContainsKey(id))
        {
          // нет в памяти данных для этого ключа, получаем из базы
          this.Items.Add(id, this.GetValue(id));
        }
        return this.Items[id];
      }
    }

    #endregion
    #region ..конструктор..

    /// <summary>
    /// Инициализирует новый экземпляр класса информации для указанного пользователя.
    /// </summary>
    /// <param name="u">Экземпляр пользователя, для которого необходима работа с информацией о базе.</param>
    internal Info(User u)
    {
      this.ToSave = new List<InfoId>();
      this.Items = new Dictionary<InfoId, string>();
      this.CurrentUser = u;
    }

    #endregion
    #region ..методы..

    /// <summary>
    /// Получает из базы и возвращает значение по указанному ключу.
    /// </summary>
    /// <param name="key">Ключ, значение для которого следует получить.</param>
    private string GetValue(InfoId id)
    {
      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT [value] FROM [info] WHERE [id_info] = @id";
        client.Parameters.Add("@id", SqlDbType.SmallInt).Value = Convert.ToInt16(id);
        object result = client.ExecuteScalar();
        if (result == DBNull.Value) { return null; }
        return Convert.ToString(result);
      }
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    public void Set(InfoId id, DateTime value, bool nodb = false)
    {
      this.Set(id, value.ToString(), nodb);
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    /// <param name="nodb">Позволяет запретить запись данных в базу. По умолчанию значение <c>false</c> - данные в базу записываются сразу.</param>
    public void Set(InfoId id, long value, bool nodb = false)
    {
      this.Set(id, value.ToString(), nodb);
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    /// <param name="nodb">Позволяет запретить запись данных в базу. По умолчанию значение <c>false</c> - данные в базу записываются сразу.</param>
    public void Set(InfoId id, int value, bool nodb = false)
    {
      this.Set(id, value.ToString(), nodb);
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    /// <param name="nodb">Позволяет запретить запись данных в базу. По умолчанию значение <c>false</c> - данные в базу записываются сразу.</param>
    public void Set(InfoId id, Version value, bool nodb = false)
    {
      this.Set(id, value.ToString(), nodb);
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 30 символов.</param>
    /// <param name="nodb">Позволяет запретить запись данных в базу. По умолчанию значение <c>false</c> - данные в базу записываются сразу.</param>
    /// <remarks>
    /// <para>
    /// Если значение параметра <paramref name="nodb"/> равно <c>true</c>, то запись изменений в базу будет произведена только при вызове метода <see cref="Flush"/>.
    /// </para>
    /// </remarks>
    public void Set(InfoId id, string value, bool nodb = false)
    {
      if (!String.IsNullOrEmpty(value) && value.Length > 30)
      {
        //throw new ArgumentOutOfRangeException(value);
        value = value.Substring(30);
      }

      // добавляем или меняем
      if (!this.Items.ContainsKey(id))
      {
        this.Items.Add(id, value);
      }
      else
      {
        this.Items[id] = value;
      }

      // если запрещено сохранение в базу, выходим
      if (nodb)
      {
        if (!this.ToSave.Contains(id))
        {
          this.ToSave.Add(id);
        }
        return;
      }

      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT COUNT([id_info]) FROM [info] WHERE [id_info] = @id";
        client.Parameters.Add("@id", SqlDbType.SmallInt).Value = Convert.ToInt16(id);
        client.Parameters.Add("@value", SqlDbType.NVarChar, 30).Value = value;

        if (Convert.ToInt32(client.ExecuteScalar()) == 0)
        {
          // добавляем
          client.CommandText = "INSERT INTO [info] ([id_info], [value]) VALUES (@id, @value)";
        }
        else
        {
          // обновляем
          client.CommandText = "UPDATE [info] SET [value] = @value WHERE [id_info] = @id";
        }

        client.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="items">Коллекция параметров, которые необходимо сохранить.</param>
    /// <param name="nodb">Позволяет запретить запись данных в базу. По умолчанию значение <c>false</c> - данные в базу записываются сразу.</param>
    /// <remarks>
    /// <para>
    /// Если значение параметра <paramref name="nodb"/> равно <c>true</c>, то запись изменений в базу будет произведена только при вызове метода <see cref="Flush"/>.
    /// </para>
    /// </remarks>
    public void Set(Dictionary<InfoId, object> items, bool nodb = false)
    {
      if (items == null || items.Count <= 0)
      {
        return;
      }

      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.Parameters.Add("@id", SqlDbType.SmallInt);
        client.Parameters.Add("@value", SqlDbType.NVarChar, 30);

        foreach (var id in items.Keys)
        {
          string value = "";

          if (items[id] != null)
          {
            value = items[id].ToString();
          }
          if (value.Length > 30)
          {
            value = value.Substring(30);
          }

          if (!nodb)
          {
            client.CommandText = "SELECT COUNT([id_info]) FROM [info] WHERE [id_info] = @id";

            client.Parameters["@id"].Value = Convert.ToInt16(id);
            client.Parameters["@value"].Value = value;

            if (Convert.ToInt32(client.ExecuteScalar()) == 0)
            {
              // добавляем
              client.CommandText = "INSERT INTO [info] ([id_info], [value]) VALUES (@id, @value)";
            }
            else
            {
              // обновляем
              client.CommandText = "UPDATE [info] SET [value] = @value WHERE [id_info] = @id";
            }

            client.ExecuteNonQuery();
          }
          else
          {
            if (!this.ToSave.Contains(id))
            {
              this.ToSave.Add(id);
            }
          }

          // в текущий экземпляр
          if (!this.Items.ContainsKey(id))
          {
            this.Items.Add(id, value);
          }
          else
          {
            this.Items[id] = value;
          }
        }
      }
    }

    /// <summary>
    /// Возвращает все записи из базы данных.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public List<InfoItem> GetAllInfo()
    {
      var constants = new Dictionary<short, string>();

      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [info] ORDER BY [id_info]";
        var result = client.GetEntities<InfoItem>();

        // добвавляем к результату имена констант
        this.GetAllConstants(null, typeof(InfoId), constants);
        foreach (var item in result)
        {
          if (constants.ContainsKey(item.Id))
          {
            item.Name = constants[item.Id];
          }
        }
        // --

        return result;
      }
    }

    /// <summary>
    /// Выбирает все константы из указанного типа.
    /// </summary>
    private void GetAllConstants(string parentName, Type t, Dictionary<short, string> list)
    {
      foreach (var field in t.GetFields())
      {
        var name = String.Format("{0}.{1}", t.Name, field.Name);
        if (!String.IsNullOrEmpty(parentName) && !parentName.Equals("InfoId"))
        {
          name = String.Format("{0}.{1}", parentName, name);
        }
        list.Add(Convert.ToInt16(field.GetRawConstantValue()), name);
      }

      // вложенные типы
      foreach (var st in t.GetNestedTypes())
      {
        this.GetAllConstants(t.Name, st, list);
      }
    }
    
    /// <summary>
    /// Выполняет запись данных в базу и освобождает ресурсы.
    /// </summary>
    public void Flush()
    {
      if (this.ToSave.Count > 0)
      {
        var itemsToSave = new Dictionary<InfoId, object>();
        foreach (var id in this.Items.Keys)
        {
          if (this.ToSave.Contains(id))
          {
            itemsToSave.Add(id, this.Items[id]);
          }
        }

        this.Set(itemsToSave, false);
        this.ToSave.Clear();
      }

      this.Items.Clear();
    }

    #endregion

  }

}