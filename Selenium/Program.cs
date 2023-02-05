using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium;
internal class Program
{
    static void Main(string[] args)
    {

        WebDriver driver = new ChromeDriver();

        Authorization login = new Authorization (driver);

        login.InputLoginAndPassword();

        driver.Close();            
    }
}