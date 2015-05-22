using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MoneyBook.Core.Data;
using MoneyBook.Core.Data.Enums;

namespace MoneyBook.Core
{

  /// <summary>
  /// Представляет иконку (размер 16x16px).
  /// </summary>
  [Table("icons")]
  public class Icon
  {

    /// <summary>
    /// Уникальный идентификатор иконки.
    /// </summary>
    [Column("id_icons", SqlDbType.Int, ColumnFlags.PrimaryKey | ColumnFlags.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Уникальная хеш-сумма файла иконки в виде GUID.
    /// </summary>
    [Column("hash", SqlDbType.UniqueIdentifier)]
    public Guid Hash { get; set; }

    /// <summary>
    /// Содержимое файла иконки.
    /// </summary>
    [Column("data", SqlDbType.VarBinary, Size = 2048)]
    public byte[] Data { get; set; }

    /// <summary>
    /// Дата и время создания иконки.
    /// </summary>
    [Column("date_created", SqlDbType.DateTime, Default = DefaultValues.Now)]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Возвращает поток представляющий данные файла иконки текущего экземпляра класса.
    /// </summary>
    public Stream ToStream()
    {
      return new MemoryStream(this.Data);
    }

  }

}