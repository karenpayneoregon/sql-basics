using System.Net.Http.Json;
using MaskingSocialApp.Data;

namespace MaskingSocialApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        using (var context = new Context())
        {
            var list = context.Taxpayer.ToList();
            AnsiConsole.MarkupLine("[yellow]Hello[/]");
        }

        Console.ReadLine();
    }
}

public static class HttpHelper
{
    private static readonly HttpClient httpClient = new();

    public static async Task<string> Read(string uri)
        => await httpClient.GetStringAsync(uri);
    public static async Task<Person> Read1(string uri)
        => await httpClient.GetFromJsonAsync<Person>(uri);

    public static async Task<T> ReadJson<T>(string uri)
        => await httpClient.GetFromJsonAsync<T>(uri);
}

















public class Person
{

}