﻿using OpenQA.Selenium;

namespace Selenium;

public class InboxPage : Page
{
    const string REPLY_BUTTON = "//span[@class='ams bkH']";
    const string REPLY_TEXT_OF_THE_LETTER_INPUT_FILD = "//div[@class='Am aO9 Al editable LW-avf tS-tW']";
    const string REPLY_LETTER_ENTER_BUTTON = "//div[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']";
    const string LETTER_BODY_XPATH = "//div[@class='a3s aiL '][1]";
    const string URL = "https://mail.google.com/mail/u/1/?ogbl#inbox";

    private InboxPage(IWebDriver driver, string url) : base(driver, url)
    {        
    }

    public static InboxPage Navigate(IWebDriver driver)
    {
        return new InboxPage(driver, URL);
    }    

    public void ClickReplyButton()
    {
        ClickElement(REPLY_BUTTON);
    }

    public void FillResponseMessage(string responseMessage)
    {
        FillField(REPLY_TEXT_OF_THE_LETTER_INPUT_FILD, responseMessage);
    }

    public void ClickSendButton()
    {
        ClickElement(REPLY_LETTER_ENTER_BUTTON);
    }

    public void OpenLetter(string letterBody)
    {
        IWebElement letter = FindeElementByXPathWithRefresh(GetLetterXPath(letterBody));
        Console.WriteLine($"Found letter is {letter.Text}");
        ClickElement(letter);
    }

    public string GetLetterBody()
    {
        return GetElementText(LETTER_BODY_XPATH);
    }

}
