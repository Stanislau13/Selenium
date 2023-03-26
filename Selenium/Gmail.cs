using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium;

public class Gmail
{
    private WebDriver _driver;

    public Gmail()
    {
        _driver = new ChromeDriver();
    }

    public SignInPage LogIn(Credentials credentials)
    {
        SignInPage signInPage = SignInPage.Navigate(_driver);
        signInPage.LogIn(credentials);
        return signInPage;
    }

    public NewLetterWindow SendLetter(LetterInfo letterinfo)
    {
        NewLetterWindow newLetterWindow = NewLetterWindow.Navigate(_driver);
        newLetterWindow.FillLetterFields(letterinfo);
        newLetterWindow.Send();
        return newLetterWindow;
    }

    public InboxPage NavigateToInboxLetter(string receivedMessage)
    {
        InboxPage inboxPage = InboxPage.Navigate(_driver);
        inboxPage.OpenLetter(receivedMessage);
        return inboxPage;
    }

    public SentPage NavigateToSentPage()
    {
        return SentPage.Navigate(_driver);
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