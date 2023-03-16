
using OpenQA.Selenium;

namespace Selenium;

public class InboxPage
{
    private readonly By _replyButton = By.XPath("//span[@class='ams bkH']");
    private readonly By _replyTextOfTheLetterInputFild = By.XPath("//div[@class='Am aO9 Al editable LW-avf tS-tW']");
    private readonly By _replyLetterEnterButton = By.XPath("//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']");

    private IWebDriver _driver;

    public InboxPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl("https://mail.google.com/mail/u/1/?ogbl#inbox");
    }

    public void OpenLetter(string letterBody)
    {
        By letterXPath = GetLetterxPath(letterBody);
        IWebElement letter = WaitUntil.WaitElementWithRefresh(_driver, letterXPath);
        Console.WriteLine($"Found letter is {letter.Text}");
        letter.Click();
        WaitUntil.WaitSomeInterval(2);
    }

    public void ClickReplyButton()
    {
        WaitUntil.WaitElement(_driver, _replyButton);
        _driver.FindElement(_replyButton).Click();
    }

    public void FillResponseMessage(string responseMessage)
    {
        WaitUntil.WaitSomeInterval(2);
        WaitUntil.WaitElement(_driver, _replyTextOfTheLetterInputFild);
        _driver.FindElement(_replyTextOfTheLetterInputFild).SendKeys(responseMessage);
    }

    public void ClickSendButton()
    {
        _driver.FindElement(_replyLetterEnterButton).Click();
        Console.WriteLine($"Reply to letter");
        WaitUntil.WaitSomeInterval(2);
    }

    public static By GetLetterxPath(string body)
    {
        return By.XPath($"//*[contains(text(), '{body}')]");
    }

}
