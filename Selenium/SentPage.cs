using OpenQA.Selenium;


namespace Selenium;

public class SentPage : Page
{
    const string URL = "https://mail.google.com/mail/u/0/#sent";

    private SentPage(IWebDriver driver, string url, LoggingOptions loggingOption) : base(driver, url, loggingOption)
    {
    }

    public static SentPage Navigate(IWebDriver driver, LoggingOptions loggingOption)
    {
        return new SentPage(driver, URL, loggingOption);
    }
}
