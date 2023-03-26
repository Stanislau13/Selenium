using Selenium;

namespace GmailUnitTests;

[TestClass]
public class GmailUnitTest
{
    private Gmail _gmail;

    private const string LOGIN_1 = "taskqaautotest1@gmail.com";
    private const string PASSWORD_1 = "123123123test";
    private const string LOGIN_2 = "taskqaautotest3@gmail.com";
    private const string PASSWORD_2 = "123123123test";

    private readonly string LETTER_BODY = RandomValueGenerator.RandomString(10);
    private readonly string LETTER_BODY_2 = RandomValueGenerator.RandomString(10);


    [TestInitialize]
    public void Inicialization()
    {
        _gmail = new Gmail();        
    }

    [TestCleanup]
    public void CleanUp()
    {
        _gmail.Close();
    }

    [TestMethod]
    [DataRow(LOGIN_1, PASSWORD_1)]
    [DataRow(LOGIN_2, PASSWORD_2)]
    public void CheckLogIn(string username, string password)
    {
        SignInPage page = LogIn(username, password);

        CheckString(url => !url.Contains("challenge"), page.GetCurrentUrl());
    }

    [TestMethod]
    public void CheckSendLetter()
    {
        SendLetter();

        CheckLetterInSentFolder(LETTER_BODY);
    }

    [TestMethod]
    public void NavigateToInboxLetter()
    {
        SendLetter();
        _gmail.LogOut();

        LogIn(LOGIN_2, PASSWORD_2);
        InboxPage page = _gmail.NavigateToInboxLetter(LETTER_BODY);

        CheckString(text => text.Contains(LETTER_BODY), page.GetLetterBody());
    }

    [TestMethod]
    public void CheckReplyToLetter()
    {
        SendLetter();
        _gmail.LogOut();

        LogIn(LOGIN_2, PASSWORD_2);
        _gmail.ReplyToLetter(LETTER_BODY, LETTER_BODY_2);

        CheckLetterInSentFolder(LETTER_BODY_2);
    }

    [TestMethod]
    [DataRow(LOGIN_1, PASSWORD_1)]
    [DataRow(LOGIN_2, PASSWORD_2)]
    public void CheckClickLogOutButton(string username, string password)
    {
        LogIn(username, password);
        AccountFrame page = _gmail.LogOut();

        CheckString(url => !url.Contains("inbox"), page.GetCurrentUrl());
    }

    private void CheckString(Func<string, bool> trueCondition, string currentUrl)
    {
        Assert.IsNotNull(currentUrl);
        Assert.IsTrue(trueCondition(currentUrl));
    }

    private void CheckLetterInSentFolder(string letterBody)
    {
        SentPage page = _gmail.NavigateToSentPage();
        Assert.IsTrue(page.IsElementDisplayed(InboxPage.GetLetterXPath(letterBody)));
    }

    private SignInPage LogIn(string username, string password)
    {
        Credentials credentials = new Credentials(username, password);
        return _gmail.LogIn(credentials);
    }

    private NewLetterWindow SendLetter()
    {
        LogIn(LOGIN_1, PASSWORD_1);
        LetterInfo letterInfo = CreateLetterInfo(LOGIN_2);
        return _gmail.SendLetter(letterInfo);
    }

    private LetterInfo CreateLetterInfo(string recipient)
    {
        string title = RandomValueGenerator.RandomString(10);
        return new LetterInfo(title, LETTER_BODY, recipient);
    }
}