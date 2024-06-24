using System.Reflection;

public static class UtilsMethods
{
    public static bool MatchProperties<T,Q>(this T objAim, Q query)     
    {        
        var queryProperties = typeof(Q).GetProperties();
        if (queryProperties == null )
        {
            return false;
        }
        bool matches = true;
        foreach (PropertyInfo prop in queryProperties)
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(query, null);
            if (propValue == null) 
            {
                continue;
            }
            var propValueToCompare = typeof(T).GetProperty(propName).GetValue(objAim, null);
            if (!propValueToCompare.Equals(propValue))
            {
                matches = false;
                break;
            }
        }
        return matches;
    }

    public static object? GetTheProperty<T>(T obj, string propertyName) where T : new()
    {        
        var propertyValue = obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        Console.WriteLine(propertyValue.ToString());
        return propertyValue;
    }

    public static void SetNewPropertyValues<B, A>(this B CurrentObject, A newPropertyValues)
    {
        var properties = newPropertyValues.GetType().GetProperties();
        foreach (var property in properties)
        {
            var name = property.Name;
            var value = property.GetValue(newPropertyValues);
            if (value == null)
            {
                continue;
            }
            CurrentObject.GetType().GetProperty(name).SetValue(CurrentObject, value);
        }
    }
}