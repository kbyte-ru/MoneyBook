using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBook.Core;
using System.Linq;
using MoneyBook.Core.Data;

namespace UnitTestProject1
{

  [TestClass]
  public class UserTest
  {

    [TestMethod]
    public void TestMethod1()
    {
      User.Kill(App.CurrentPath, "user");

      var u = User.Create(ApplicationType.Desktop, App.CurrentPath, "user", "123");
      var account = new Account();
      account.Name = "Проверочный счет";

      u.Save(account);
      
      Assert.AreEqual(account.Id, 1);

      var deleted = u.Delete(account);
      Assert.AreEqual(deleted, 1);

      deleted = u.Delete(account);
      Assert.AreEqual(deleted, 0);

      var category = new Category();
      category.Name = "Проверка";
      category.ForeColor = Color.Red;
      category.BackColor = Color.Yellow;
      category.FontStyle = FontStyle.Bold | FontStyle.Italic;

      u.Save(category);

      Assert.AreEqual(u.Categories[0].ForeColor.ToArgb(), Color.Red.ToArgb());
      Assert.AreEqual(u.Categories[0].BackColor.ToArgb(), Color.Yellow.ToArgb());
      Assert.AreEqual(u.Categories[0].FontStyle, FontStyle.Bold | FontStyle.Italic);
    }

    [TestMethod]
    public void InfoTest()
    {
      User.Kill(App.CurrentPath, "info");

      using (var u = User.Create(ApplicationType.Desktop, App.CurrentPath, "info"))
      {
        // пользовательские параметры
        u.Info.Set(1000, "123");
        u.Info.Set(1001, "test");
        u.Info.Set(1000, "456");
        u.Info.Set(1005, "Это проверка");
      }

      using (var u = new User(App.CurrentPath, "info"))
      {
        Assert.AreEqual(u.Info[1005], "Это проверка");
        Assert.AreEqual(u.Info[1000], "456");
        Assert.AreEqual(u.Info[InfoId.TotalSessions], "1");
      }
      
      using (var u = new User(App.CurrentPath, "info"))
      {
        Assert.AreEqual(u.Info[InfoId.TotalSessions], "2");
      }
    }
    
    [TestMethod]
    public void IconTest()
    {
      User.Kill(App.CurrentPath, "icons");

      var u = User.Create(ApplicationType.Mobile, App.CurrentPath, "icons");

      var ico = u.AddIcon(Properties.Resources.add_16);
      Assert.AreEqual(ico.Id, 1);

      var ico2 = u.AddIcon(Properties.Resources.add_16);
      Assert.AreEqual(ico2.Id, 1);

      var ico3 = u.AddIcon(Properties.Resources.calendar);
      Assert.AreEqual(ico3.Id, 2);

      // целостность данных в базе
      var bmp = System.Drawing.Bitmap.FromStream(new MemoryStream(ico3.Data));
      Assert.AreEqual(bmp.Width, 16);

      var bmp2 = u.GetIcon(1);
      Assert.AreEqual(bmp2.Width, 16);

      // большая картинка
      var ico4 = u.AddIcon(Properties.Resources.smile);
      var bmp4 = System.Drawing.Bitmap.FromStream(new MemoryStream(ico4.Data));
      Assert.AreEqual(bmp4.Width, 16);
    }

    [TestMethod]
    public void PwdTest()
    {
      User.Kill(App.CurrentPath, "pwd");

      var u = User.Create(ApplicationType.Desktop, App.CurrentPath, "pwd", "123");

      try
      {
        u = new User(App.CurrentPath, "pwd");
        Assert.Fail("Что-то работает не так, т.к. на базе должен стоять пароль.");
      }
      catch
      {
      }

      u = new User(App.CurrentPath, "pwd", "123");
      u.SetPassword("test");

      var account = new Account();
      account.Name = "Проверочный счет";
      u.Save(account);

      Assert.AreEqual(account.Id, 1);


      try
      {
        u = new User(App.CurrentPath, "pwd", "123");
        Assert.Fail("Что-то работает не так, т.к. пароль был изменен.");
      }
      catch
      {
      }

      u = new User(App.CurrentPath, "pwd", "test");
      u.SetPassword(null);
      
      u = new User(App.CurrentPath, "pwd");
    }

    [TestMethod]
    public void ItemsTest()
    {
      User.Kill(App.CurrentPath, "entries");

      var u = User.Create(ApplicationType.Web, App.CurrentPath, "entries", "123");

      var currency = new Currency();
      currency.LongName = "Российский рубль";
      currency.ShortName = "₽";
      currency.Code = "RUB";
      u.Save(currency);

      var accountType = new AccountType();
      accountType.Name = "Сундук";
      u.Save(accountType);

      var account = new Account();
      account.Name = "Денежный счет";
      account.AccountTypeId = accountType.Id;
      account.CurrencyCode = currency.Code;
      u.Save(account);

      var cat = new Category();
      cat.Name = "Главная статья доходов";
      cat.CategoryType = EntryType.Income;
      cat.FontStyle = FontStyle.Bold;
      u.Save(cat);

      var items = new List<MoneyItem>();
      var rnd = new Random(DateTime.Now.Millisecond);
      var date = DateTime.Now;
      for (int i = 1; i <= 100; i++)
      {
        var item = new MoneyItem();
        item.AccountId = account.Id;
        item.Amount = rnd.Next(1000, Int32.MaxValue);
        item.EntryType = EntryType.Income;
        item.CategoryId = cat.Id;
        item.DateEntry = date;
        item.DateCreated = date;
        item.Description = "Вот такой вот доход!";
        item.Title = String.Format("Запись #{0}", i);
        items.Add(item);
      }

      u.Save(items);

      for (int i = 1; i < 10; i++)
      {
        var list = u.GetMoneyItems(page: i, maxDataPerPage: 10);
        Assert.AreEqual(list[0].Id, 100 - ((i - 1) * 10));
        Assert.AreEqual(list[0].Title, String.Format("Запись #{0}", 100 - ((i - 1) * 10)));
      }

      var list2 = u.GetMoneyItems(search: "Запись #50");
      Assert.IsTrue(list2.Count == 1);

      u.Delete(items);

      // выборка по датам
      items = new List<MoneyItem>();
      for (int i = -5; i <= 5; i++)
      {
        var entry = new MoneyItem();
        entry.AccountId = account.Id;
        entry.Amount = rnd.Next(1000, Int32.MaxValue);
        entry.EntryType = EntryType.Income;
        entry.CategoryId = cat.Id;
        entry.DateEntry = DateTime.Now.AddDays(i);
        entry.Description = "Вот такой вот доход!";
        entry.Title = String.Format("Запись #{0}", i);
        items.Add(entry);
      }
      u.Save(items);

      // выше от текущей даты
      list2 = u.GetMoneyItems(dateFrom: DateTime.Now);
      for (int i = 0; i <= 5; i++)
      {
        Assert.IsTrue(System.DateTime.Now.AddDays(i).Date.Subtract(list2[5 - i].DateEntry.Date).TotalSeconds == 0);
      }

      // до текущей даты
      list2 = u.GetMoneyItems(dateTo: DateTime.Now);
      for (int i = 0; i <= 5; i++)
      {
        Assert.IsTrue(System.DateTime.Now.AddDays(-i).Date.Subtract(list2[i].DateEntry.Date).TotalSeconds == 0);
      }

      // между двух дат
      list2 = u.GetMoneyItems(dateFrom: DateTime.Now.AddDays(-2), dateTo: DateTime.Now.AddDays(2));
      Assert.IsTrue(System.DateTime.Now.Date.Subtract(list2[2].DateEntry.Date).TotalSeconds == 0);
    }

    [TestMethod]
    public void MonitoringTest()
    {
      User.Kill(App.CurrentPath, "monitoring");

      var u = User.Create(ApplicationType.Mobile, App.CurrentPath, "monitoring");

      Assert.IsTrue(u.Currencies.Count == 0);

      var currency = new Currency();
      currency.LongName = "Деньга";
      currency.Code = "MNY";
      u.Save(currency);

      // изменение по ссылке
      u.Currencies[0].LongName = "Другая деньга";
      Assert.IsTrue(currency.LongName == "Другая деньга");

      // новых записей не должно быть создано
      u.Save(currency);
      Assert.IsTrue(u.Currencies.Count == 1);

      // новый экземпляр должен заменить данные существующего
      var currency2 = new Currency();
      currency2.LongName = "Вообще другая деньга";
      currency2.Code = "MNY";
      u.Save(currency2);

      Assert.IsTrue(u.Currencies.Count == 1);
      Assert.IsTrue(u.Currencies[0].LongName == "Вообще другая деньга");

      // первая ссылка должна отвалиться
      Assert.AreNotEqual(currency.LongName, currency2.LongName);

      // удаляем
      u.Delete(currency2);
      Assert.IsTrue(u.Currencies.Count == 0);

      // добавляем вновь
      u.Save(currency2);
      Assert.IsTrue(u.Currencies.Count == 1);

      // удаляем по другому экземпляру
      u.Delete(currency);
      Assert.IsTrue(u.Currencies.Count == 0);

      // добавляем
      u.Save(currency);
      Assert.IsTrue(u.Currencies.Count == 1);

      // проверяем, что удаляется не все подряд
      var currency3 = new Currency { Code = "ANY" };
      u.Delete(currency3);
      Assert.IsTrue(u.Currencies.Count == 1);
    }

  }

}