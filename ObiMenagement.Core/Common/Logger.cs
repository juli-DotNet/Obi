namespace ObiMenagement.Core.Common;

public class Logger
{
    public static Logger Instance = new Logger();

    public void LogInfo(string message)
    {
        System.Console.WriteLine(message);
    }

    public void LogError(string error)
    {
        System.Console.Error.WriteLine(error);
    }
    public void LogError(Exception error)
    {
        System.Console.Error.WriteLine(error.ToString());
    }
}