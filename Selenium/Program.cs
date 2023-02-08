using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium;
internal class Program
{
    static void Main(string[] args)
    {
        WebDriver driver = new ChromeDriver();
        Devbyio devBy = new Devbyio(driver);

        string homePageJavaVacancyRateString = devBy.ExtractVacancies();
        Console.WriteLine($"Java vacancies on the home page {homePageJavaVacancyRateString}");

        devBy.LinkToTheJavaJobList();
        int vacancyPagejavaVacancyRate = devBy.OutputDescriptionsOfAllJavaVacancies();
        Console.WriteLine($"Java vacancies on the jobs page {vacancyPagejavaVacancyRate}");

        Console.WriteLine($"!!!!!!!!!!! {homePageJavaVacancyRateString.Equals(vacancyPagejavaVacancyRate.ToString())}");
        if (homePageJavaVacancyRateString.Equals(vacancyPagejavaVacancyRate.ToString()))
        {
            Console.WriteLine($"Number of vacancies on the home page({homePageJavaVacancyRateString}) is equal to the number of vacancies on the Vacancies tab({vacancyPagejavaVacancyRate})");
        }

        driver.Close();            
    }
}