using OpenQA.Selenium;

namespace Selenium;

public class Gmail
{
    private SignInPage _signInPage;
    private InboxPage _inboxPage;
    private NewLetterWindow _newLetterWindow;
    private AccountFrame _accountFrame;

    public Gmail(IWebDriver driver)
    {
        _signInPage = new SignInPage(driver);
        _inboxPage = new InboxPage(driver);
        _newLetterWindow = new NewLetterWindow(driver);
        _accountFrame = new AccountFrame(driver);
    }

    public void LogIn(Credentials credentials)
    {
        _signInPage.Navigate();
        _signInPage.LogIn(credentials);
    }

    public void SendLetter(LetterInfo letterinfo)
    {
        _newLetterWindow.Navigate();
        _newLetterWindow.FillLetterFields(letterinfo);
        _newLetterWindow.Send();
    }

    public void NavigateToLetter(string receivedMessage)
    {
        _inboxPage.Navigate();
        _inboxPage.OpenLetter(receivedMessage);
    }

    public void ReplyToLetter(string responseMessage)
    {        
        _inboxPage.ClickReplyButton();
        _inboxPage.FillResponseMessage(responseMessage);
        _inboxPage.ClickSendButton();
    }

    public void LogOut()
    {
        _accountFrame.Navigate();
        _accountFrame.ClickLogOutButton();
    }
}