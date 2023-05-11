using Selenium;
using System.Text;

public class TxtLogger : Logger, IDisposable
{
    private readonly StreamWriter _streamWriter;
    private const string PATH = "d:\\Stas\\обучение\\log.txt";

    public TxtLogger()
    {
        _streamWriter = new StreamWriter(PATH, true, Encoding.UTF8);
    }


    public void Dispose()
    {
        _streamWriter.Dispose();
    }

    public override void Log(string message)
    {
        _streamWriter.WriteLine($"[{DateTime.Now}] {message}");
    }
  
}

