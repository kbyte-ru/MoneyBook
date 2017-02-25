using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{

  public partial class Registration : Form
  {

    /// <summary>
    /// Список сохраненных в профиль созданного пользователя иконок. 
    /// </summary>
    /// <remarks>
    /// Список доступен после вызова <see cref="MakeIcons(User)"/>.
    /// </remarks>
    private Dictionary<string, int> Icons = new Dictionary<string, int>();

    public Registration()
    {
      InitializeComponent();
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
      string userName = UserNameTextBox.Text;

      if (string.IsNullOrEmpty(userName))
      {
        MessageBox.Show("Необходимо указать имя пользователя!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else
      {
        if (User.Exists(Program.ProfileBasePath, UserNameTextBox.Text))
        {
          MessageBox.Show(String.Format("Пользователь {0} уже существует.\r\nВведите другое имя пользователя.", userName), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
          string password = (string.IsNullOrEmpty(PasswordTextBox.Text) ? null : PasswordTextBox.Text);

          // создаем пользователя
          var user = User.Create(Core.ApplicationType.Desktop, Program.ProfileBasePath, userName, password);

          // формируем данные по умолчанию
          this.MakeIcons(user); // NOTE: должен быть первым
          this.MakeCurrencies(user);
          this.MakeAccountTypes(user);
          this.MakeDefaultAccount(user);
          this.MakeDefaultExpenses(user);
          this.MakeDefaultIncomes(user);

          var loginForm = this.Owner as Login;

          if (loginForm != null)
          {
            loginForm.UserNameComboBox.DataSource = Core.User.GetUsers(Program.ProfileBasePath);
          }

          this.Close();
        }
      }
    }

    private void MakeIcons(User user)
    {
      var rs = DefaultValues.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

      foreach (DictionaryEntry entry in rs)
      {
        // берем только иконки
        if (!entry.Key.ToString().StartsWith("icon_"))
        {
          continue;
        }

        var icon = user.AddIcon((Image)entry.Value);

        this.Icons.Add(entry.Key.ToString(), icon.Id);
      }
    }

    private void MakeCurrencies(User user)
    {
      // код|полное название|сокращенное название
      var defaultCurrencies = DefaultValues.Currencies.Split('\n');

      foreach (var item in defaultCurrencies)
      {
        var fields = item.Trim().Split('|');

        var currency = new Currency();
        currency.Code = fields[0].Trim();
        currency.LongName = fields[1].Trim();
        currency.ShortName = fields[2].Trim();

        user.Save(currency);
      }
    }

    private void MakeAccountTypes(User user)
    {
      // название|имя ресурса иконки
      var defaultAccountTypes = DefaultValues.AccountTypes.Split('\n');

      foreach (var item in defaultAccountTypes)
      {
        var fields = item.Trim().Split('|');

        var accountType = new AccountType();
        accountType.Name = fields[0].Trim();
        accountType.IconId = this.GetIconIdOrDefault(fields[1].Trim());
        
        user.Save(accountType);
      }
    }

    private void MakeDefaultAccount(User user)
    {
      var account = new Account();
      account.Name = DefaultValues.DefaultAccount;
      account.CurrencyCode = DefaultValues.DefaultCurrency;
      account.AccountTypeId = 1;
      // TODO: иконка

      user.Save(account);
    }

    private void MakeDefaultExpenses(User user)
    {
      // название|цвет шрифта|цвет фона|имя ресурса иконки
      //DefaultValues.Incomes

      var defaultExpenses = DefaultValues.Expenses.Split('\n');

      foreach (var item in defaultExpenses)
      {
        var fields = item.Trim().Split('|');

        var category = new Category();
        category.CategoryType = EntryType.Expense;
        category.Name = fields[0].Trim();
        category.ForeColor = ColorTranslator.FromHtml(fields[1].Trim());
        category.BackColor = ColorTranslator.FromHtml(fields[2].Trim());
        category.IconId = this.GetIconIdOrDefault(fields[3].Trim());

        user.Save(category);
      }
    }

    private void MakeDefaultIncomes(User user)
    {
      // название|цвет шрифта|цвет фона|имя ресурса иконки
      var defaultIncomes = DefaultValues.Incomes.Split('\n');

      foreach (var item in defaultIncomes)
      {
        var fields = item.Trim().Split('|');

        var category = new Category();
        category.CategoryType = EntryType.Income;
        category.Name = fields[0].Trim();
        category.ForeColor = ColorTranslator.FromHtml(fields[1].Trim());
        category.BackColor = ColorTranslator.FromHtml(fields[2].Trim());
        category.IconId = this.GetIconIdOrDefault(fields[3].Trim());

        user.Save(category);
      }
    }

    private int GetIconIdOrDefault(string key)
    {
      if (this.Icons.ContainsKey(key))
      {
        return this.Icons[key];
      }
      else
      {
        return this.Icons.Values.First();
      }
    }

    private void CancellButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      PasswordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\0' : '*';
    }

  }

}