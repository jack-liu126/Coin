using System.Reflection;
using System.Text.RegularExpressions;

namespace Coin.Server.Appcode;

public static class SanitizeFunc
{
    public static T SanitizeStringProperties<T>(this T obj) where T : class
    {
        if (obj == null)
        {
            return null;
        }

        Type type = obj.GetType();

        foreach (PropertyInfo property in type.GetProperties())
        {
            if (property.PropertyType == typeof(string))
            {
                string value = (string)property.GetValue(obj);
                string sanitizedValue = Regex.Replace(value, "[<>&'\"]", "").Trim();
                property.SetValue(obj, sanitizedValue);
            }
        }

        return obj;
    }
}
