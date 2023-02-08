using OpenQA.Selenium;

namespace Selenium;

internal class Devbyio
{
    private IWebDriver _driver;

    private readonly By _numberOfVacancieJava = By.XPath("//h5[@data-vr-contentbox-dynamic-hash='2727258030']/following-sibling::div");
    private readonly By _descriptionVacancyJava = By.XPath("//a[@class='vacancies-list-item__link_block']");

    public Devbyio(IWebDriver driver)
    {
        _driver = driver;
        _driver.Navigate().GoToUrl("https://devby.io/");
        _driver.Manage().Window.Maximize();
    }

    public string ExtractVacancies()
    {
        Thread.Sleep(2000);
        var number = _driver.FindElement(_numberOfVacancieJava).Text;
        
        string numberOfVacancies = "";

        foreach (var item in number)
        {
            if (char.IsDigit(item))
            {
                numberOfVacancies += item;
            }
        }
        return numberOfVacancies;
    }

    public void LinkToTheJavaJobList()
    {
        _driver.Navigate().GoToUrl("https://jobs.devby.io/?filter[specialization_title]=Java");
    }

    public int OutputDescriptionsOfAllJavaVacancies()
    {
        LinkToTheJavaJobList();

        Thread.Sleep(1000);
        var vacancies = _driver.FindElements(_descriptionVacancyJava);

        foreach (var vacancy in vacancies)
        {
            if (vacancy is not null)
            {
                Console.WriteLine(vacancy.Text);
            }
        }
        return vacancies.Count;
    }

}
