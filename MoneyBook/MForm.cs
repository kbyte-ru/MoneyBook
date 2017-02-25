using System.Windows.Forms;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{

  /// <summary>
  /// Базовый класс для форм MoneyBook.
  /// </summary>
  public class MForm : Form
  {

    /// <summary>
    /// Текущий пользователь MoneyBook.
    /// </summary>
    public User User { get; private set; }

    public MForm()
    {

    }

    public MForm(User user)
    {
      this.User = user;
    }

  }

}