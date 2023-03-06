using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace Selenium;

public static class WaitUntil
{
    public static void WaitSomeIntervl(int second = 5) 
    {
        Task.Delay(TimeSpan.FromSeconds(second)).Wait();
    }

    public static void WaitElement(IWebDriver webDriver, By locator, int second = 20) 
    {
        new WebDriverWait(webDriver, TimeSpan.FromSeconds(second)).Until(ExpectedConditions.ElementIsVisible(locator));
        new WebDriverWait(webDriver, TimeSpan.FromSeconds(second)).Until(ExpectedConditions.ElementToBeClickable(locator));
    }

    public static IWebElement WaitElementWithRefresh(IWebDriver webDriver, By locator) {
        WebDriverWait wait = new WebDriverWait(webDriver, timeout: TimeSpan.FromSeconds(600))
        {
            PollingInterval = TimeSpan.FromSeconds(3),
        };
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        return wait.Until(drv =>
        {
            drv.Navigate().Refresh();
            return drv.FindElement(locator);
        });
    }
 
} 
