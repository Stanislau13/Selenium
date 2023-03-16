using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium;
using System.Xml.Linq;

namespace GmailUnitTests;

[TestClass]
public abstract class BaseUnitTest
{
    protected WebDriver _driver;
    protected SignInPage _signInPage;
    protected AccountFrame _accountFrame;

    protected const string _login1 = "taskqaautotest1@gmail.com";
    protected const string _password1 = "123123123test";
    protected const string _login2 = "taskqaautotest3@gmail.com";
    protected const string _password2 = "123123123test";

     public void BaseInicialization()
    {
        _driver = new ChromeDriver();
        _signInPage = new SignInPage(_driver);
        _accountFrame = new AccountFrame(_driver);
    }

    [TestCleanup]
    public void CleanUp()
    {
        _driver.Quit();
    }

    protected void LogIn(string login, string password) {
        _signInPage.Navigate();
        _signInPage.LogIn(new Credentials(login, password));
    }

    protected void LogOut()
    {
        _accountFrame.Navigate();
        _accountFrame.ClickLogOutButton();
    }

    protected void CheckCurrentUrl(string partOfUrl, bool contains)
    {
        string currentUrl = _driver.Url;
        Assert.IsNotNull(currentUrl);
        Assert.IsTrue(contains ? currentUrl.Contains(partOfUrl) : !currentUrl.Contains(partOfUrl));
    }

    protected void CheckCurrentUrlV2(Func<string, bool> trueCondition)
    {
        string currentUrl = _driver.Url;
        Assert.IsNotNull(currentUrl);
        Assert.IsTrue(trueCondition(currentUrl));
    }

    protected void CheckIfElementDisplayed(By xPath)
    {
        IWebElement element = GetElementByxPath(xPath);
        Assert.IsTrue(element.Displayed);
    }

    protected void CheckIfElementTextContains(By xPath, string textPart)
    {
        string elementText = GetElementByxPath(xPath).Text;
        Assert.IsTrue(elementText.Contains(textPart));
    }

    protected IWebElement GetElementByxPath(By xPath) 
    {
        WaitUntil.WaitElement(_driver, xPath, 10);
        IWebElement element = _driver.FindElement(xPath);
        Assert.IsNotNull(element);
        return element;
    }

}
