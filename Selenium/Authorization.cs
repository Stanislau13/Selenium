using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium;

internal class Authorization
{
    private IWebDriver _driver;

    private readonly By _loginInputButton = By.XPath("//input[@name='user-name']");
    private readonly By _passInputButton = By.XPath("//input[@name='password']");
    private readonly By _enterButton = By.XPath("//input[@name='login-button']");

    private const string _login = "standard_user";
    private const string _password = "secret_sauce";

    public Authorization(IWebDriver driver)
    {
        _driver = driver;
        _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        _driver.Manage().Window.Maximize();
    }

    public void InputLoginAndPassword()
    {
        var login = _driver.FindElement(_loginInputButton);
        login.SendKeys(_login);

        var pass = _driver.FindElement(_passInputButton);
        pass.SendKeys(_password);

        var enter = _driver.FindElement(_enterButton);
        enter.Click();
    }
}
