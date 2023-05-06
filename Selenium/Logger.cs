using System.Reflection;

namespace Selenium;

public abstract class Logger : ILogger
{

    public void WriteToLog(string message)
    {
        var messageWithMethodName = MessageWithMethodInfo();
        Log($"{messageWithMethodName} {message}");
    }

    public void WriteToLog()
    {
        Log(MessageWithMethodInfo());
    }

    public abstract void Log(string message);

    private string MessageWithMethodInfo()
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        var method = stackTrace.GetFrame(4)?.GetMethod();
        var parameters = method?.GetParameters();

         foreach (ParameterInfo param in parameters)
    {
        Console.WriteLine(param.Name + " = " + param.DefaultValue);
    }

        var methodName = method is null ? "" : method.Name;
        var stringParams = parameters is null ? "without parameters" : $"with {string.Join(", ", (object[]) parameters)}";
        return $"Method '{methodName}' {stringParams} is running";
    }
}
