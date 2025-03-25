using System.Net.Http.Json;
using MaskingSocialApp.Classes;


namespace MaskingSocialApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[cyan]SSN            Phone[/]");
        var maskedList = await DapperOperations.GetTaxpayersAsNonPrivilegedUserAsync();

        for (var index = 0; index < 5; index++)
        {
            Console.WriteLine($"{maskedList[index].SocialSecurityNumber, -15}{maskedList[index].PhoneNumber}");
        }

        Console.WriteLine();

        var unmaskedList = await DapperOperations.GetTaxpayersAsync();

        for (var index = 0; index < 5; index++)
        {
            Console.WriteLine($"{unmaskedList[index].SocialSecurityNumber,-15}{unmaskedList[index].PhoneNumber}");
        }

        Console.WriteLine();

        Console.WriteLine("Done");
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