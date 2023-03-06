using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.VisualBasic.FileIO;

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

        Gmail mail = new Gmail(driver);
        mail.NavigateToGmail();

        string title = "New title(test)";
        string letterText = RandomValueGenerator.RandomString(10);
        string letterText2 = RandomValueGenerator.RandomString(10);

        mail.LogIn(credentials1);
        mail.SendLetter(credentials2.UserName, title, letterText);
        mail.LogOut();

        mail.NavigateToGmail();
        mail.LogIn(credentials2);
        mail.LetterCheck(letterText);
        mail.ReplyToLetter(letterText, letterText2);
        mail.LogOut();

        mail.NavigateToGmail();
        mail.LogIn(credentials1);
        mail.LetterCheck(letterText2);

        driver.Close();            
    }
}