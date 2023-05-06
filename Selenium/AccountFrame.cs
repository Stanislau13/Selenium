using OpenQA.Selenium;

namespace Selenium;

public class AccountFrame : Page
{
    const string ACCOUNT_MANAGEMENT_BUTTON = "//a[@class='gb_f gb_3a gb_v']";
    const string ACCOUNT_LOG_OUT_BUTTON = "//div[text()='Выйти']";
    const string FRAME_NAME = "account";

    private AccountFrame(IWebDriver driver) : base(driver)
    {
    }

    public static AccountFrame Navigate(IWebDriver driver)
    {
        AccountFrame accountFrame = new AccountFrame(driver);
        accountFrame.ClickElement(ACCOUNT_MANAGEMENT_BUTTON);
        accountFrame.SwitchToFrame(FRAME_NAME);
        accountFrame.WaitSomeInterval(2);
        return accountFrame;
    }

    public void ClickLogOutButton()
    {
        ClickElement(ACCOUNT_LOG_OUT_BUTTON);
        Log();
    }
}
