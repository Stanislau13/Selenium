using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
