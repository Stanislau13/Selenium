namespace Selenium;

public class LetterInfo
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string Recipient { get; set; }

    public LetterInfo(string title, string body, string recipient) 
    {
        Title = title;
        Body = body;
        Recipient = recipient;
    }
}
