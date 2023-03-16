using OpenQA.Selenium;

namespace Selenium;


public class AccountFrame
{
    private readonly By _accountManagementButton = By.XPath("//a[@class='gb_e gb_1a gb_s']");
    private readonly By _accountLogOuttButton = By.XPath("//div[text()='Выйти']");

    private IWebDriver _driver;

    public AccountFrame(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate()
    {
        WaitUntil.WaitElement(_driver, _accountManagementButton);
        _driver.FindElement(_accountManagementButton).Click();

        WaitUntil.WaitSomeInterval(2);
        _driver.SwitchTo().Frame("account");
    }

    public void ClickLogOutButton()
    {
        WaitUntil.WaitElement(_driver, _accountLogOuttButton);
        _driver.FindElement(_accountLogOuttButton).Click();
    }
}
