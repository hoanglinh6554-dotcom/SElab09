using BankAccountApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccountTests;

[TestClass]
public class BankAccountTestsClass
{
    public static IEnumerable<object[]> AccountRows
    {
        get
        {
            var path = Path.Combine(AppContext.BaseDirectory, "TestData", "BankAccountTestData.csv");
            foreach (var line in File.ReadLines(path).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var p = line.Split(',');
                yield return new object[] { p[0], decimal.Parse(p[1]), decimal.Parse(p[2]), decimal.Parse(p[3]) };
            }
        }
    }

    [DataTestMethod, DynamicData(nameof(AccountRows))]
    public void Debit_CsvRows(string name, decimal initial, decimal debit, decimal expected)
    {
        var account = new BankAccount(name, initial);
        account.Debit(debit);
        Assert.AreEqual(expected, account.Balance);
    }

    [TestMethod]
    public void Debit_InsufficientFunds_ThrowsAndKeepsBalance()
    {
        var account = new BankAccount("Ky Trung Pham", 100);
        Assert.ThrowsException<InvalidOperationException>(() => account.Debit(150));
        Assert.AreEqual(100m, account.Balance);
    }

    [TestMethod]
    public void Credit_IncreasesBalance()
    {
        var account = new BankAccount("Test", 100m);
        account.Credit(50m);
        Assert.AreEqual(150m, account.Balance);
    }

    [TestMethod]
    public void Withdraw_DecreasesBalance()
    {
        var account = new BankAccount("Test", 100m);
        account.Withdraw(40m);
        Assert.AreEqual(60m, account.Balance);
    }

    [TestMethod]
    public void NegativeCredit_IsRejected() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BankAccount("Test", 10m).Credit(-1m));
}
