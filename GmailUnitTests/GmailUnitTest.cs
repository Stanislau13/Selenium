using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Selenium;
using NUnit.Framework.Internal;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GmailUnitTests;

[TestClass]
public class GmailUnitTest
{
    private const string _login1 = "taskqaautotest1@gmail.com";
    private const string _password1 = "123123123test";

    private const string _login2 = "taskqaautotest3@gmail.com";
    private const string _password2 = "123123123test";
        
    private readonly By _userLogin = By.XPath("//div[@class='znj3je NB6Lnc']");
    private readonly By _checkActualAuthorizationButton = By.XPath("//a[@data-action='sign in']");

    private readonly By _accountManagementButton = By.XPath("//a[@class='gb_e gb_0a gb_r']");

    private const string _firstExpectedLogin = "Jubei Kibagami";

    string letterText = RandomValueGenerator.RandomString(10);
    string letterText2 = RandomValueGenerator.RandomString(10);

    Credentials credentials1 = new Credentials(_login1, _password1);
    Credentials credentials2 = new Credentials(_login2, _password2);


    [TestMethod]
    public void CheckLogInPositive()
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();
        mail.LogIn(credentials1);

        WaitUntil.WaitElement(driver, _accountManagementButton);
        driver.FindElement(_accountManagementButton).Click();

        driver.SwitchTo().Frame("account");

        WaitUntil.WaitElement(driver, _userLogin);
        var actualLogin = driver.FindElement(_userLogin).Text;

        Assert.AreEqual(_firstExpectedLogin, actualLogin, "Login incorrect or input was not completed");

        driver.Quit();
    }

    [TestMethod]
    public void CheckLogOutPositive()
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();

        mail.LogIn(credentials1);
        mail.LogOut();

        Assert.IsNotNull(_checkActualAuthorizationButton);

        driver.Quit();
    }

    [TestMethod]
    public void CheckSendLetterPositive()
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();
        mail.LogIn(credentials1);
        mail.SendLetter(_login2, "Hello", letterText);

        driver.Navigate().GoToUrl("https://mail.google.com/mail/u/1/?ogbl#sent");
        
        By xPath = LetterXPath(letterText);
        WaitUntil.WaitElement(driver, xPath, 10);
        var actualSentLetter = driver.FindElement(xPath).Text;

        Assert.IsTrue(actualSentLetter.Contains(letterText));

        driver.Quit();
    }

    [TestMethod]
    public void CheckLetterReceivedPositive()
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();
        mail.LogIn(credentials1);
        mail.SendLetter(_login2, "Hello", letterText);
        mail.LogOut();

        WaitUntil.WaitSomeIntervl(5);

        mail.NavigateToGmail();
        mail.LogIn(credentials2);

        By xPath = LetterXPath(letterText);
        WaitUntil.WaitElement(driver, xPath, 600);
        var actualLetterReceived = driver.FindElement(xPath).Text;

        Assert.IsTrue(actualLetterReceived.Contains(letterText));

        driver.Quit();
    }

    [TestMethod]
    public void CheckReplyToLetterPositive() 
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();
        mail.LogIn(credentials1);
        mail.SendLetter(_login2, "Hello", letterText);
        mail.LogOut();

        mail.NavigateToGmail();
        mail.LogIn(credentials2);
        mail.ReplyToLetter(letterText, letterText2);

        driver.Navigate().GoToUrl("https://mail.google.com/mail/u/1/?ogbl#sent");

        By xPath = LetterXPath(letterText);
        By xPath2 = LetterXPath(letterText2);

        WaitUntil.WaitElement(driver, xPath2, 10);
        var actualSentLetter = driver.FindElement(xPath).Text;

        Assert.IsTrue(actualSentLetter.Contains(letterText2));

        driver.Quit();
    }

    [TestMethod]
    public void CheckForReplyFromTheSecondUserPositive() 
    {
        WebDriver driver = new ChromeDriver();
        Gmail mail = new Gmail(driver);

        mail.NavigateToGmail();
        mail.LogIn(credentials1);
        mail.SendLetter(_login2, "Hello", letterText);
        mail.LogOut();

        mail.NavigateToGmail();
        mail.LogIn(credentials2);

        mail.ReplyToLetter(letterText, letterText2);
        mail.LogOut();

        WaitUntil.WaitSomeIntervl(1);
        mail.NavigateToGmail();
        mail.LogIn(credentials1);

        By xPath = LetterXPath(letterText2);
        var actualLetterReceived = WaitUntil.WaitElementWithRefresh(driver, xPath).Text;

        Assert.IsTrue(actualLetterReceived.Contains(letterText2));

        driver.Quit();
    }

    private By LetterXPath(string letterText) {
        return By.XPath($"//*[contains(text(), '{letterText}')]");
    }
}