namespace Selenium;

public class Credentials
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public Credentials(string username, string password) 
    {
        UserName = username;
        Password = password;
    }
}
