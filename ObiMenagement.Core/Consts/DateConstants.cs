namespace ObiMenagement.Core.Models;

public  static class DateConstants
{
    public const string FormatUI = "dd/mm/yy";
    public const string Format = "dd/MM/yyyy";

    public static DateTime? TryConvertToDate(this string dateData)
    {
        if (string.IsNullOrEmpty(dateData))
        {
            return null;
        }

        return ConvertToDate(dateData);
    }
    public static DateTime ConvertToDate(this string dateData)
    {
        if (string.IsNullOrWhiteSpace(dateData))
        {
            return DateTime.MinValue;
        }
        return DateTime.ParseExact(dateData,Format,null).ToUniversalTime();
    }
    public static string CovertDateToString(this DateTime dateData)
    {
        if (dateData==DateTime.MinValue)
        {
            return "";
        }
        return dateData.ToLocalTime().ToString(Format);
    }
    public static string CovertDateToString(this DateTime? dateData)
    {
        if (dateData==null|| dateData==DateTime.MinValue)
        {
            return "";
        }

        return (dateData??DateTime.MinValue).ToString(Format);
    }
}