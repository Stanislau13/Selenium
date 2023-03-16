using OpenQA.Selenium;
using Selenium;

namespace GmailUnitTests;

[TestClass]
public class AccountFrameUnitTests : BaseUnitTest
{
    private readonly By _accountLogOuttButton = By.XPath("//div[text()='Выйти']");

    [TestInitialize]
    public void Inicialization()
    {
        BaseInicialization();

        LogIn(_login1, _password1);
    }

    [TestMethod]
    public void CheckNavigate() 
    {
        _accountFrame.Navigate();
        WaitUntil.WaitElement(_driver, _accountLogOuttButton);

        CheckIfElementDisplayed(_accountLogOuttButton);
    }

    [TestMethod]
    public void CheckClickLogOutButton()
    {
        CheckNavigate();
        _accountFrame.ClickLogOutButton();
        WaitUntil.WaitSomeInterval(3);

        CheckCurrentUrl("www.google.com/gmail/about", true);
    }
}
