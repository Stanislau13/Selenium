using OpenQA.Selenium;

namespace Selenium;

public class NewLetterWindow : Page
{
    const string TO_WRITE_A_LETTER_BUTTON = "//div[@class='T-I T-I-KE L3']";
    const string WHO_THE_LETTER_FILD = "//input[@peoplekit-id='BbVjBd']";
    const string TITLE_INPUT_FILD = "//input[@name='subjectbox']";
    const string TEXT_OF_THE_LETTER_INPUT_FILD = "//div[@class='Am Al editable LW-avf tS-tW']";
    const string SEND_LETTER_ENTER_BUTTON = "//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']";

    private NewLetterWindow(IWebDriver driver) : base(driver) 
    {
    }

    public static NewLetterWindow Navigate(IWebDriver driver) 
    {
        NewLetterWindow newLetterWindow = new NewLetterWindow(driver);
        newLetterWindow.ClickElement(TO_WRITE_A_LETTER_BUTTON);
        newLetterWindow.Log("Clik to the new letter button");
        return newLetterWindow;
    }

    public void FillLetterFields(LetterInfo letterinfo)
    {
        FillField(WHO_THE_LETTER_FILD, letterinfo.Recipient);
        FillField(TITLE_INPUT_FILD, letterinfo.Title);
        FillField(TEXT_OF_THE_LETTER_INPUT_FILD, letterinfo.Body);
    }

    public void Send()
    {
        ClickElement(SEND_LETTER_ENTER_BUTTON);
        Log();
    }
    
}
