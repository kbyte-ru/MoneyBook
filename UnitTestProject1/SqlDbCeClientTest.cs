using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBook.Core;
using MoneyBook.Core.Data;

namespace UnitTestProject1
{

  [TestClass]
  public class SqlDbCeClientTest
  {

    [TestMethod]
    public void EntitiesTest()
    {
      string password = "";
      string filePath = Path.Combine(App.CurrentPath, "test.sdf");
      if (File.Exists(filePath))
      {
        File.Delete(filePath);
      }
      string connectionString = String.Format("Data Source={0}; password={1}", filePath, password);
      SqlDbCeClient.CreateDatabase(connectionString);

      using (var client = new SqlDbCeClient(connectionString))
      {
        client.CommandText = @"CREATE TABLE [accounts] (
  [id_accounts] int IDENTITY (1,1) NOT NULL, 
	[id_account_types] int NOT NULL, 
	[id_currencies] nvarchar(3) NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL, 
	[account_name] nvarchar(100) NOT NULL, 
	[account_details] nvarchar(4000) NOT NULL, 
	[total_income_entries] int DEFAULT (0) NOT NULL, 
	[total_expense_entries] int DEFAULT (0) NOT NULL, 
	[last_operation] datetime NULL, 
	[date_created] datetime DEFAULT (GETDATE()) NOT NULL
);";
        client.ExecuteNonQuery();
        client.CommandText = "ALTER TABLE [accounts] ADD CONSTRAINT [PK_accounts] PRIMARY KEY ([id_accounts]);";
        client.ExecuteNonQuery();
      }

      using (var client = new SqlDbCeClient(connectionString))
      {
        client.QueryProcessing += (sender, e) =>
        {
          var args = (QueryProcessingEventArgs)e;
          Console.WriteLine(args.Status);
        };

        var a = new Account();
        a.CurrencyCode = "RUB";
        a.AccountTypeId = 1;
        a.Details = "test";
        a.IconId = 123;
        a.Id = 0;
        a.Name = "проверка";
        for (int i = 1; i <= 10; i++)
        {
          client.SaveEntities<Account>(a);
          Console.WriteLine("Id: {0} => {1}", a.Id, a.Status);
          Assert.AreEqual(a.Id, i);
          Assert.IsTrue(a.Status == EntityStatus.Created);
          a.Id = 0;
        }

        var aaa = client.GetEntities<Account>("SELECT * FROM accounts");
        Console.WriteLine("Count: {0}", aaa.Count);
        Assert.IsTrue(aaa.Count == 10);

        var a2 = client.GetEntity<Account>("SELECT * FROM accounts WHERE id_accounts = 3");
        Assert.IsTrue(a2.Status == EntityStatus.Loaded);
        a2.Name = "Тест123";
        a2.LastOperation = DateTime.Now;
        client.SaveEntities<Account>(a2);
        Assert.AreEqual(a2.Name, "Тест123");
        Assert.AreEqual(a2.Id, 3);
        Assert.IsTrue(a2.Status == EntityStatus.Updated);

        a2 = client.GetEntity<Account>("SELECT * FROM accounts WHERE id_accounts = 3");
        Assert.AreEqual(a2.Name, "Тест123");
        Assert.AreEqual(a2.Id, 3);

        a2 = client.GetEntity<Account>("SELECT * FROM accounts WHERE id_accounts = 4");
        Assert.AreNotEqual(a2.Name, "Тест123");
        Assert.AreNotEqual(a2.Id, 3);

        a2 = client.GetEntity<Account>("SELECT * FROM accounts WHERE id_accounts = 123");
        Assert.IsTrue(a2 == null);
      }
    }

  }

}