using OpenQA.Selenium;

namespace Selenium;

public class NewLetterWindow
{
    private readonly By _toWriteALetterButton = By.XPath("//div[@class='T-I T-I-KE L3']");
    private readonly By _whoTheLetterFild = By.XPath("//input[@peoplekit-id='BbVjBd']");
    private readonly By _titleInputFild = By.XPath("//input[@name='subjectbox']");
    private readonly By _textOfTheLetterInputFild = By.XPath("//div[@class='Am Al editable LW-avf tS-tW']");
    private readonly By _sendLetterEnterButton = By.XPath("//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']");

    private IWebDriver _driver;

    public NewLetterWindow(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate() 
    {
        WaitUntil.WaitElement(_driver, _toWriteALetterButton);
        _driver.FindElement(_toWriteALetterButton).Click();
        WaitUntil.WaitSomeInterval(2);
    }

    public void FillLetterFields(LetterInfo letterinfo)
    {
        FillRecipient(letterinfo.Recipient);
        FillTitle(letterinfo.Title);
        FillBody(letterinfo.Body);
    }

    public void Send()
    {
        _driver.FindElement(_sendLetterEnterButton).Click();

        Console.WriteLine($"Sent a letter");

        WaitUntil.WaitSomeInterval(2);
    }

    private void FillRecipient(string recipient) 
    {
        FillField(_whoTheLetterFild, recipient);
    }

    private void FillTitle(string title)
    {
        FillField(_titleInputFild, title);
    }

    private void FillBody(string body)
    {
        FillField(_textOfTheLetterInputFild, body);
    }

    private void FillField(By xPath, string text)
    {
        _driver.FindElement(xPath).SendKeys(text);
    }
}
