using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyBook.Core;

namespace MoneyBook.WinApp
{

  public partial class Main : Form
  {

    public Main()
    {
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
    }

    private void Main_Shown(object sender, EventArgs e)
    {
      this.Test();

      this.Expenses.User = Program.CurrentUser;
      this.Incomes.User = Program.CurrentUser;
    }

    private void Test()
    {
      if (User.Exists(Program.ProfileBasePath, "test")) 
      {
        Program.CurrentUser = new User(Program.ProfileBasePath, "test", "");
        return; 
      }

      Program.CurrentUser = User.Create(ApplicationType.Desktop, Program.ProfileBasePath, "test", "");
      var currency = new Currency();
      currency.LongName = "Российский рубль";
      currency.ShortName = "₽";
      currency.Code = "RUB";
      Program.CurrentUser.Save(currency);

      var accountType = new AccountType();
      accountType.Name = "Сундук";
      Program.CurrentUser.Save(accountType);

      var account = new Account();
      account.Name = "Денежный счет";
      account.AccountTypeId = accountType.Id;
      account.CurrencyCode = currency.Code;
      Program.CurrentUser.Save(account);

      var cat = new Category();
      cat.Name = "Легальный доход";
      cat.CategoryType = EntryType.Income;
      cat.FontStyle = FontStyle.Bold;
      Program.CurrentUser.Save(cat);

      var cat2 = new Category();
      cat2.Name = "Откаты";
      cat2.CategoryType = EntryType.Income;
      cat2.FontStyle = FontStyle.Italic;
      cat2.ForeColor = Color.Red;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Перекаты";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.Green;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Зубокаты";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.Violet;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Самокаты";
      cat2.CategoryType = EntryType.Income;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Цукаты";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.OrangeRed;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Вкаты";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.Pink;
      cat2.ParentId = cat.Id;
      cat2.FontStyle = FontStyle.Bold;
      Program.CurrentUser.Save(cat2);

      var rnd = new Random(DateTime.Now.Millisecond);
      var date = DateTime.Now;

      var items = new List<MoneyItem>();

      for (int i = 1; i <= 100; i++)
      {
        var item = new MoneyItem();
        item.AccountId = account.Id;
        item.Amount = rnd.Next(1000, 100000);
        item.EntryType = cat.CategoryType;
        item.CategoryId = rnd.Next(2, cat2.Id);
        item.DateEntry = date.AddDays(rnd.Next(-30, 30));
        item.DateCreated = date;
        item.Description = "Вот такой вот доход!";
        item.Title = String.Format("Запись #{0}", i);
        items.Add(item);
      }

      Program.CurrentUser.Save(items);

      cat = new Category();
      cat.Name = "Расходительные расходы";
      cat.CategoryType = EntryType.Expense;
      Program.CurrentUser.Save(cat);

      cat2 = new Category();
      cat2.Name = "Казино";
      cat2.CategoryType = EntryType.Income;
      cat2.FontStyle = FontStyle.Italic;
      cat2.ForeColor = Color.Red;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Рояль";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.Green;
      cat2.ParentId = cat.Id;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Пианино";
      cat2.CategoryType = EntryType.Income;
      cat2.ForeColor = Color.Violet;
      cat2.ParentId = cat.Id;
      cat2.FontStyle = FontStyle.Bold;
      Program.CurrentUser.Save(cat2);

      cat2 = new Category();
      cat2.Name = "Баян";
      cat2.CategoryType = EntryType.Income;
      cat2.ParentId = cat.Id;
      cat2.FontStyle = FontStyle.Strikeout;
      Program.CurrentUser.Save(cat2);

      items = new List<MoneyItem>();

      for (int i = 1; i <= 100; i++)
      {
        var item = new MoneyItem();
        item.AccountId = account.Id;
        item.Amount = rnd.Next(1000, 10000);
        item.EntryType = cat.CategoryType;
        item.CategoryId = rnd.Next(cat.Id, cat2.Id);
        item.DateEntry = date.AddDays(rnd.Next(-30, 30));
        item.DateCreated = date;
        item.Description = "Вот такой вот расход!";
        item.Title = String.Format("Запись #{0}", i);
        items.Add(item);
      }

      Program.CurrentUser.Save(items);
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void Main_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (Program.CurrentUser != null)
      {
        Program.CurrentUser.Dispose();
        Program.CurrentUser = null;
      }
    }


  }

}