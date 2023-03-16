using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium;
public class Program
{
    private const string _login1 = "taskqaautotest1@gmail.com";
    private const string _password1 = "123123123test";

    private const string _login2 = "taskqaautotest3@gmail.com";
    private const string _password2 = "123123123test";

    static void Main(string[] args)
    {
        WebDriver driver = new ChromeDriver();

        Credentials credentials1 = new Credentials(_login1, _password1);
        Credentials credentials2 = new Credentials(_login2, _password2);        
        string title = "New title(test)";
        string sentMessage = RandomValueGenerator.RandomString(10);
        string responseMessage = RandomValueGenerator.RandomString(10);
        LetterInfo sentLetterInfo = new LetterInfo(title, sentMessage, credentials2.UserName);

        Gmail mail = new Gmail(driver);
        mail.LogIn(credentials1);
        mail.SendLetter(sentLetterInfo);
        mail.LogOut();

        mail.LogIn(credentials2);
        mail.NavigateToLetter(sentMessage);
        mail.ReplyToLetter(responseMessage);
        mail.LogOut();

        mail.LogIn(credentials1);
        mail.NavigateToLetter(responseMessage);

        driver.Close();            
    }
}