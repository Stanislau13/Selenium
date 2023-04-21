using OpenQA.Selenium;


namespace Selenium;

public class SentPage : Page
{
    const string URL = "https://mail.google.com/mail/u/0/#sent";

    private SentPage(IWebDriver driver, string url) : base(driver, url)
    {
    }

    public static SentPage Navigate(IWebDriver driver)
    {
        return new SentPage(driver, URL);
    }
}
