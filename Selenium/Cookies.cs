using OpenQA.Selenium;

namespace Selenium;

public class Cookies
{
    public static void DeleteAllCookies(IWebDriver driver)
    {
        driver.Manage().Cookies.DeleteAllCookies();
    }
}
