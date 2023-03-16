using OpenQA.Selenium;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace Selenium;

public class SignInPage
{
    private readonly By _loginInputFild = By.XPath("//input[@name='identifier']");
    private readonly By _loginInputButton = By.XPath("//span[text()='Далее']");
    private readonly By _passInputFild = By.XPath("//input[@name='Passwd']");
    private readonly By _passInputButton = By.XPath("//span[text()='Далее']");

    private const string url = "https://accounts.google.com/v3/signin/identifier?dsh=S319654989%3A1676542123495862&authuser=0&continue=https%3A%2F%2Fmyaccount.google.com%2F&ec=GAlAwAE&hl=ru&service=accountsettings&flowName=GlifWebSignIn&flowEntry=AddSession";

    private IWebDriver _driver;

    public SignInPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(url);
        _driver.Manage().Window.Maximize();
    }

    public void LogIn(Credentials credentials)
    {
        InputLogin(credentials.UserName);

        InputPassword(credentials.Password);

        WaitUntil.WaitSomeInterval(2);
        _driver.Navigate().GoToUrl("https://mail.google.com/mail");
    }

    public void InputLogin(string log)
    {
        var login = _driver.FindElement(_loginInputFild);
        login.Clear();
        login.SendKeys(log);

        _driver.FindElement(_loginInputButton).Click();
    }

    public void InputPassword(string pass)
    {
        WaitUntil.WaitElement(_driver, _passInputFild);
        _driver.FindElement(_passInputFild).SendKeys(pass);

        WaitUntil.WaitElement(_driver, _passInputButton);
        _driver.FindElement(_passInputButton).Click();
    }
}
