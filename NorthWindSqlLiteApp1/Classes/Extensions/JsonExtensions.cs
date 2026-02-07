using Newtonsoft.Json;

namespace NorthWindSqlLiteApp1.Classes.Extensions;
public static class JsonExtensions
{
    public static string ToJson<T>(this List<T> source) 
        => JsonConvert.SerializeObject(source, Formatting.Indented);
}