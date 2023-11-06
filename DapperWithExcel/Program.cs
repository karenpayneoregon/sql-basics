using DapperWithExcel.Classes;
using DapperWithExcel.Models;
using static DapperWithExcel.Classes.SpectreConsoleHelpers;
using static DapperWithExcel.Classes.Utilities;

namespace DapperWithExcel;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        AnsiConsole.MarkupLine("[cyan]Creating[/]");

        foreach (var enumValue in Enum.GetValues(typeof(ContactType)))
        {
            ContactType current = ConvertFromObject<ContactType>((ContactType)enumValue);
            Console.WriteLine($"   {current}.xlsx");
            await SpreadSheetLightOperations.Write($"{current}.xlsx", $"{current}", current);
        }

        ExitPrompt();
    }
}