namespace ObiMenagement.Core.Common;

public static class ErrorMessages
{
    public static string InvalidEntity(string name) => $"Entity with property '{name}' has invalid data.";
    public static string EntityExist(string name) => $"Entity with property '{name}' already exists.";
    public static string EntityDoesntExist(object id) => $"Entity with property id= '{id}' doesn't  exists.";
    public static string NotNull(string name) => $"Please provide a value for property '{name}'.";
    public static string BiggernThan(string name,int number=0) => $"Please provide a value bigger than '{number}' for property '{name}'.";
    public static string InvalidRequest(string name) => $"Invalid reques.Query param '{name}' is required.";
}