using OpenQA.Selenium;
using Selenium;

namespace GmailUnitTests;

[TestClass]
public class SigninPageUnitTests : BaseUnitTest
{
    private readonly By _wrongPasswordError = By.XPath("//div[@class='EjBTad']");

    [TestInitialize]
    public void Inicialization()
    {
        BaseInicialization();
    }

    [TestMethod]
    public void CheckNavigate() 
    {
        _signInPage.Navigate();

        CheckCurrentUrl("accounts.google.com/v3/signin", true);
    }

    [TestMethod]
    [DataRow(_login1, _password1)]
    [DataRow(_login2, _password2)]
    public void CheckLogIn(string username, string password) 
    {
        CheckNavigate();
        Credentials credentials = new Credentials(username, password);
        _signInPage.LogIn(credentials);

        CheckCurrentUrl("#inbox", true);
    }

    [TestMethod]
    [DataRow(_login1)]
    [DataRow(_login2)]
    public void CheckInputLogin(string username)
    {
        CheckNavigate();
        _signInPage.InputLogin(username);
        WaitUntil.WaitSomeInterval(5);

        CheckCurrentUrl("/signin/challenge", true);
    }

    [TestMethod]
    [DataRow(_login1, _password1)]
    [DataRow(_login2, _password2)]
    public void CheckInputPassword(string username, string password)
    {
        CheckInputLogin(username);
        _signInPage.InputPassword(password);
        WaitUntil.WaitSomeInterval(2);

        CheckCurrentUrl("myaccount.google.com", true);
    }

    [TestMethod]
    public void CheckInputLoginWithWrongUsername()
    {
        CheckNavigate();
        _signInPage.InputLogin("wrongUsername13@gmail.com");
        WaitUntil.WaitSomeInterval(2);

        CheckCurrentUrl("/signin/challenge", false);
    }

    [TestMethod]
    [DataRow(_login1, "ertertert")]
    public void CheckInputPasswordWithWrongPassword(string username, string password)
    {
        CheckInputLogin(username);
        _signInPage.InputPassword(password);
        WaitUntil.WaitSomeInterval(2);

        CheckIfElementDisplayed(_wrongPasswordError);
    }

}
