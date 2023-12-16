using System.Text.Json;
using TransferFromJsonToDatabaseToExcel.Models;

namespace TransferFromJsonToDatabaseToExcel.Classes;
internal class FileOperations
{
    public static string FileName => "MockedData.json";
    public static Container Receive()
    {
        var container = JsonSerializer.Deserialize<List<Container>>(File.ReadAllText(FileName));
        if (container.Count > 0)
        {
            Container item = container[0];
            Console.SetCursorPosition(0, 1);

            AnsiConsole.MarkupLine($"[yellow]Reading[/] [lightseagreen]" +
                                   $"{item.Id,-4}{item.InputDate,-12}" +
                                   $"{item.Category} " +
                                   $"{item.Value,-4}" +
                                   $"{item.Specification}[/]       ");

            container.RemoveAt(0);

            File.WriteAllText(FileName, JsonSerializer.Serialize(container, 
                new JsonSerializerOptions { WriteIndented = true }));

            return item;
        }

        return null;
    }
}
