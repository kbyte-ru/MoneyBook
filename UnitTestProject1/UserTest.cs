using System;
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

  }

}