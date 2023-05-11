using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium;

public class Gmail
{
    private WebDriver _driver;
    private LoggingOptions _loggingOption;

    public Gmail() : this(LoggingOptions.Txt)
    {
    }

    public Gmail(LoggingOptions loggingOption)
    {
        _driver = new ChromeDriver();
        _loggingOption = loggingOption;
    }

    public SignInPage LogIn(Credentials credentials)
    {
        SignInPage signInPage = SignInPage.Navigate(_driver, _loggingOption);
        signInPage.LogIn(credentials);
        return signInPage;
    }

    public NewLetterWindow SendLetter(LetterInfo letterinfo)
    {
        NewLetterWindow newLetterWindow = NewLetterWindow.Navigate(_driver, _loggingOption);
        newLetterWindow.FillLetterFields(letterinfo);
        newLetterWindow.Send();
        return newLetterWindow;
    }

    public InboxPage NavigateToInboxLetter(string receivedMessage)
    {
        InboxPage inboxPage = InboxPage.Navigate(_driver, _loggingOption);
        inboxPage.OpenLetter(receivedMessage);
        return inboxPage;
    }

    public SentPage NavigateToSentPage()
    {
        return SentPage.Navigate(_driver, _loggingOption);
    }

    public InboxPage ReplyToLetter(string receivedMessage, string responseMessage)
    {
        InboxPage inboxPage = NavigateToInboxLetter(receivedMessage);
        inboxPage.ClickReplyButton();
        inboxPage.FillResponseMessage(responseMessage);
        inboxPage.ClickSendButton();
        return inboxPage;
    }

    public AccountFrame LogOut()
    {
        AccountFrame accountFrame = AccountFrame.Navigate(_driver);
        accountFrame.ClickLogOutButton();
        return accountFrame;
    }

    public void Close()
    {
        _driver.Close();
    }
}