using Microsoft.EntityFrameworkCore.Query.Internal;

public static class EnumExtensions
{
    public static bool Contains<T>(this T enumValue, string chars)
    {
        var enumToString = enumValue.ToString();
        if (enumToString.Contains(chars))
        {
            return true;
        }
        return false;
    }
}