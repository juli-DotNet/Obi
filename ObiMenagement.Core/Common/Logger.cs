namespace ObiMenagement.Core.Common;

public static class ErrorMessages
{
    public static string InvalidEntity(string name) => $"Entity with property '{name}' has invalid data.";
    public static string EntityExist(string name) => $"Entity with property '{name}' already exists.";
    public static string EntityDoesntExist(object id) => $"Entity with property id= '{id}' doesn't  exists.";
    public static string NotNull(string name) => $"Please provide a value for property '{name}'.";
}

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