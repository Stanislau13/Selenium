namespace Selenium;
public class Program
{
    const string LOGIN1 = "taskqaautotest1@gmail.com";
    const string PASSWORD1 = "123123123test";

    const string LOGIN2 = "taskqaautotest3@gmail.com";
    const string PASSWORD2 = "123123123test";

    const string TITLE = "New title(test)";

    static void Main(string[] args)
    {
        Credentials credentials1 = new Credentials(LOGIN1, PASSWORD1);
        Credentials credentials2 = new Credentials(LOGIN2, PASSWORD2);        
        
        string sentMessage = RandomValueGenerator.RandomString(10);
        string responseMessage = RandomValueGenerator.RandomString(10);
        LetterInfo sentLetterInfo = new LetterInfo(TITLE, sentMessage, credentials2.UserName);

        Gmail mail = new Gmail();
        mail.LogIn(credentials1);
        mail.SendLetter(sentLetterInfo);
        mail.LogOut();

        mail.LogIn(credentials2);
        mail.ReplyToLetter(sentMessage, responseMessage);
        mail.LogOut();

        mail.LogIn(credentials1);
        mail.NavigateToInboxLetter(responseMessage);

        mail.Close();            
    }
}