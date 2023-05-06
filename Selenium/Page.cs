using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace Selenium;

public abstract class Page
{

    protected IWebDriver _driver;
    Actions _actions;
    WebDriverWait _wait;

    LoggingOptions _loggingOption;
    XmlLogger _xmlLogger;

    protected Page(IWebDriver driver, LoggingOptions loggingOption) 
    {
        _driver = driver;
        _loggingOption = loggingOption;
        _actions = new Actions(_driver);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _wait.PollingInterval = TimeSpan.FromSeconds(2);
        _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        _xmlLogger = new XmlLogger();
    }

    protected Page(IWebDriver driver) : this(driver, LoggingOptions.Txt)
    {
    }

    protected Page(IWebDriver driver, string url, LoggingOptions loggingOption) : this(driver, loggingOption)
    {
        GoToUrl(url);
    }

    protected Page(IWebDriver driver, string url) : this(driver, url, LoggingOptions.Txt)
    {
    }

    public string GetCurrentUrl() 
    {
        return _driver.Url;
    }

    public bool IsElementDisplayed(string xPath)
    {
        IWebElement element = FindeElementByXPath(xPath);
        return element.Displayed;
    }

    public string GetElementText(string xPath)
    {
        IWebElement element = FindeElementByXPath(xPath);
        return element.Text;
    }

    public static string GetLetterXPath(string body)
    {
        return $"//*[contains(text(), '{body}')]";
    }

    public void GoToUrl(string url) 
    {
        _driver.Url = url;
        _driver.Manage().Window.Maximize();
        WaitSomeInterval(3);
    }

    protected void ClickElement(string xPath)
    {
        IWebElement webElement = FindeElementByXPath(xPath);
        ClickElement(webElement);
    }

    protected void ClickElement(IWebElement webElement) 
    { 
        _actions.MoveToElement(webElement);
        _actions.Perform();
        webElement.Click();
        WaitSomeInterval(3);
    }

    protected void FillField(string xPath, string value)
    {
        IWebElement webElement = FindeElementByXPath(xPath);
        webElement.Clear();
        webElement.SendKeys(value);
    }

    protected IWebElement FindeElementByXPath(string xPath) 
    {
        Log(xPath);
        return _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xPath)));
    }

    protected IWebElement FindeElementByXPathWithRefresh(string xPath)
    {
        Log(xPath);

        return _wait.Until(drv =>
        {
            drv.Navigate().Refresh();            
            IWebElement element = drv.FindElement(By.XPath(xPath));
            if (!element.Displayed || !element.Enabled)
            {
                throw new NoSuchElementException();
            }
            return element;
        });
    }

    public void WaitSomeInterval(int second = 5)
    {
        Task.Delay(TimeSpan.FromSeconds(second)).Wait();
    }

    protected void SwitchToFrame(string frameLocator)
    {
        _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameLocator));
    }

    protected void UrlContains(string partOfUrl)
    {
        _wait.Until(ExpectedConditions.UrlContains(partOfUrl));
    }

    protected void Log()
    {
        Log(null);
    }

    public void Log(string? message)
    {
        switch (_loggingOption)
        {           
            case LoggingOptions.Xml:                
                WriteToLog(message, _xmlLogger);                
                break;

            default:
                using (var logger = new TxtLogger())
                {
                    WriteToLog(message, logger);
                }
                break;
        }
    }

    private void WriteToLog(string? messsage, ILogger logger) 
    {

        if (messsage is null)
        {
            logger.WriteToLog();
        }
        else
        {
            logger.WriteToLog(messsage);
        }
    }
}
