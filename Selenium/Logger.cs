using OpenQA.Selenium.DevTools;
using System.Reflection;
using System.Text;

public class Logger : IDisposable
{
    private readonly StreamWriter _streamWriter;
    private const string PATH = "d:\\Stas\\обучение\\log.txt";

    public Logger()
    {
        _streamWriter = new StreamWriter(PATH, true, Encoding.UTF8);
    }

    public void WriteToLog(string message)
    {
        var messageWithMethodName = MessageWithMethodName();
        Log($"{messageWithMethodName} {message}");
    }

    public void WriteToLog()
    {
        Log(MessageWithMethodName());
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
    }

    private void Log(string message)
    {
        _streamWriter.WriteLine($"[{DateTime.Now}] {message}");
    }

    private string MessageWithMethodName()
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        var method = stackTrace.GetFrame(3)?.GetMethod();
        var methodName = method is null ? "" : method.Name;
        return $"Method '{methodName}' is running";
    }
}

