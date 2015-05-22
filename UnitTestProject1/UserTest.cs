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

      var u = User.Create(App.CurrentPath, "user", "123");
      var account = new Account();
      account.Name = "Проверочный счет";

      u.Save(account);

      Assert.AreEqual(account.Id, 2);

      var deleted = u.Delete(account);
      Assert.AreEqual(deleted, 1);

      deleted = u.Delete(account);
      Assert.AreEqual(deleted, 0);
    }

  }

}