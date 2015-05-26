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
    public void Set(InfoId id, DateTime value)
    {
      this.Set(id, value.ToString());
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    public void Set(InfoId id, long value)
    {
      this.Set(id, value.ToString());
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    public void Set(InfoId id, int value)
    {
      this.Set(id, value.ToString());
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 20 символов.</param>
    public void Set(InfoId id, Version value)
    {
      this.Set(id, value.ToString());
    }

    /// <summary>
    /// Добавляет, либо обновляет информацию.
    /// </summary>
    /// <param name="id">Идентификатор записи (см. список констант).</param>
    /// <param name="value">Значение, не более 30 символов.</param>
    public void Set(InfoId id, string value)
    {
      if (!String.IsNullOrEmpty(value) && value.Length > 30)
      {
        //throw new ArgumentOutOfRangeException(value);
        value = value.Substring(30);
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
          // и в текущий экземпляр
          if (!this.Items.ContainsKey(id))
          {
            this.Items.Add(id, value);
          }
          else
          {
            this.Items[id] = value;
          }
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
    /// Возвращает все записи.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public List<InfoItem> GetAllInfo()
    {
      using (var client = new SqlDbCeClient(this.CurrentUser.ConnectionString))
      {
        client.CommandText = "SELECT * FROM [info]";
        return client.GetEntities<InfoItem>();
      }
    }

    #endregion

  }

}