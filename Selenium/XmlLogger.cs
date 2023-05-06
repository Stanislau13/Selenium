using Selenium;
using System.Xml.Linq;

public class XmlLogger : Logger
{
    private const string PATH = "d:\\Stas\\обучение\\log.xml";
    private const string LOGS = "Logs";
    private const string LOG = "Log";
    private const string TIME = "Time";
    private const string MESSAGE = "Message";


    public override void Log(string message) 
    {
        XDocument doc = File.Exists(PATH) ? XDocument.Load(PATH) : new XDocument();
        XElement? logs = doc.Element(LOGS);

        if (logs is null)
        {
            logs = new XElement(LOGS);
            doc.Add(logs);
        }
        logs.Add(new XElement(LOG,
                              new XAttribute(TIME, DateTime.Now.ToString()),
                              new XAttribute(MESSAGE, message)));
        doc.Save(PATH);
    }
}
