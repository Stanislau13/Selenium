using OpenQA.Selenium;

namespace Selenium;

public class Gmail
{
    private IWebDriver _driver;

    private readonly By _loginInputFild = By.XPath("//input[@name='identifier']");
    private readonly By _loginInputButton = By.XPath("//span[text()='Далее']");
    private readonly By _passInputFild = By.XPath("//input[@name='Passwd']");
    private readonly By _passInputButton = By.XPath("//span[text()='Далее']");
    private readonly By _toWriteALetterButton = By.XPath("//div[@class='T-I T-I-KE L3']");
    private readonly By _whoTheLetterFild = By.XPath("//input[@peoplekit-id='BbVjBd']");
    private readonly By _titleInputFild = By.XPath("//input[@name='subjectbox']");
    private readonly By _textOfTheLetterInputFild = By.XPath("//div[@class='Am Al editable LW-avf tS-tW']");
    private readonly By _sendLetterEnterButton = By.XPath("//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']");
    private readonly By _accountManagementButton = By.XPath("//a[@class='gb_e gb_0a gb_r']");
    private readonly By _accountLogOuttButton = By.XPath("//div[text()='Выйти']");
    private readonly By _replyButton = By.XPath("//span[@class='ams bkH']");
    private readonly By _replyTextOfTheLetterInputFild = By.XPath("//div[@class='Am aO9 Al editable LW-avf tS-tW']");
    private readonly By _replyLetterEnterButton = By.XPath("//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']");

    public Gmail(IWebDriver driver)
    {
        _driver = driver;
    }

    public void NavigateToGmail() 
    {
        _driver.Navigate().GoToUrl("https://accounts.google.com/v3/signin/identifier?dsh=S319654989%3A1676542123495862&authuser=0&continue=https%3A%2F%2Fmyaccount.google.com%2F&ec=GAlAwAE&hl=ru&service=accountsettings&flowName=GlifWebSignIn&flowEntry=AddSession");
        _driver.Manage().Window.Maximize();
    }
    

    public void LogIn(Credentials credentials)
    {
        InputLogin(credentials.UserName);

        InputPassword(credentials.Password);

        WaitUntil.WaitSomeIntervl(2);
        _driver.Navigate().GoToUrl("https://mail.google.com/mail");
    }

    public void SendLetter(string recipient, string title, string summary)
    {
        WaitUntil.WaitElement(_driver, _toWriteALetterButton);
        _driver.FindElement(_toWriteALetterButton).Click();

        WaitUntil.WaitElement(_driver, _whoTheLetterFild);
        _driver.FindElement(_whoTheLetterFild).SendKeys(recipient);

        _driver.FindElement(_titleInputFild).SendKeys(title); ;

        WaitUntil.WaitElement(_driver, _textOfTheLetterInputFild);
        _driver.FindElement(_textOfTheLetterInputFild).SendKeys(summary);

        _driver.FindElement(_sendLetterEnterButton).Click();

        Console.WriteLine($"Sent a letter {summary}");

        WaitUntil.WaitSomeIntervl(2);
    }

    public void LetterCheck(string text)
    {
        WaitUntil.WaitSomeIntervl(5);
        _driver.Navigate().Refresh();

        By xPath = By.XPath($"//*[contains(text(), '{text}')]");
        WaitUntil.WaitElement(_driver, xPath, 600);

        Console.WriteLine($"Found a letter {text}");
    }

    public void ReplyToLetter(string text, string text2)
    {
        By xPath = By.XPath($"//*[contains(text(), '{text}')]");
        IWebElement letter = WaitUntil.WaitElementWithRefresh(_driver, xPath);
        Console.WriteLine($"Find letter {letter.Text}");
        letter.Click();

        WaitUntil.WaitElement(_driver, _replyButton);
        _driver.FindElement(_replyButton).Click();

        WaitUntil.WaitSomeIntervl(1);
        WaitUntil.WaitElement(_driver, _replyTextOfTheLetterInputFild);
        _driver.FindElement(_replyTextOfTheLetterInputFild).SendKeys(text2);

        _driver.FindElement(_replyLetterEnterButton).Click();

        Console.WriteLine($"Reply to letter {text2}");

        WaitUntil.WaitSomeIntervl(2);
    }

    public void LogOut()
    {
        _driver.FindElement(_accountManagementButton).Click();
        
        WaitUntil.WaitSomeIntervl(2);
        _driver.SwitchTo().Frame("account");
        WaitUntil.WaitElement( _driver, _accountLogOuttButton);
        _driver.FindElement(_accountLogOuttButton).Click();
    }

    public void DeleteAllCookies() 
    {
        _driver.Manage().Cookies.DeleteAllCookies();
    }

    private void InputLogin(string log)
    {
        var login = _driver.FindElement(_loginInputFild);
        login.Clear();
        login.SendKeys(log);

        _driver.FindElement(_loginInputButton).Click();
    }

    private void InputPassword(string pass)
    {
        WaitUntil.WaitElement(_driver, _passInputFild);
        _driver.FindElement(_passInputFild).SendKeys(pass);

        WaitUntil.WaitElement(_driver, _passInputButton);
        _driver.FindElement(_passInputButton).Click();
    }
}