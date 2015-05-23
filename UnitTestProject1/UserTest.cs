using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBook.Core;

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
  }

}