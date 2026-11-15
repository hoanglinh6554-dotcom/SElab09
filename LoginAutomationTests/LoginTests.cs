using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginAutomationTests;

[TestClass]
public class LoginTests
{
    public static IEnumerable<object[]> LoginRows
    {
        get
        {
            var path = Path.Combine(AppContext.BaseDirectory, "TestData", "LoginTestData.csv");
            foreach (var line in File.ReadLines(path).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var p = line.Split(',');
                yield return new object[] { p[0], p[1], p[2] };
            }
        }
    }

    [DataTestMethod, DynamicData(nameof(LoginRows))]
    public void Login_ShowsExpectedMessage(string username, string password, string expected)
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        using var driver = new ChromeDriver(options);
        var page = new Uri(Path.Combine(AppContext.BaseDirectory, "login.html")).AbsoluteUri;
        driver.Navigate().GoToUrl(page);
        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("loginButton")).Click();
        var actual = driver.FindElement(By.Id("message")).Text;
        Assert.AreEqual(expected, actual);
    }
}
