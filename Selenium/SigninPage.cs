using OpenQA.Selenium;

namespace Selenium;

public class SignInPage : Page
{
    const string LOGIN_INPUT_FILD = "//input[@name='identifier']";
    const string LOGIN_INPUT_BUTTON = "//span[text()='Далее']";
    const string PASS_INPUT_FILD = "//input[@name='Passwd']";
    const string PASS_INPUT_BUTTON = "//span[text()='Далее']";

    const string URL = "https://accounts.google.com/v3/signin/identifier?dsh=S319654989%3A1676542123495862&authuser=0&continue=https%3A%2F%2Fmyaccount.google.com%2F&ec=GAlAwAE&hl=ru&service=accountsettings&flowName=GlifWebSignIn&flowEntry=AddSession";

    private SignInPage(IWebDriver driver, string url, LoggingOptions loggingOption) : base(driver, url, loggingOption)
    {
    }

    public static SignInPage Navigate(IWebDriver driver, LoggingOptions loggingOption)
    {
        return new SignInPage(driver, URL, loggingOption);
    }

    public void LogIn(Credentials credentials)
    {
        Log($"with username {credentials.UserName} and password {credentials.Password}");
        FillFieldAndClickToButton(LOGIN_INPUT_FILD, credentials.UserName, LOGIN_INPUT_BUTTON);
        FillFieldAndClickToButton(PASS_INPUT_FILD, credentials.Password, PASS_INPUT_BUTTON);

        GoToUrl("https://mail.google.com/mail");
    }

    private void FillFieldAndClickToButton(string fielXPath, string value, string buttonXPath)
    {
        FillField(fielXPath, value);
        ClickElement(buttonXPath);
    }
}
