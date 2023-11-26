using StudentHelpApp.Classes;
using StudentHelpApp.Data;
using StudentHelpApp.Models;
using static StudentHelpApp.Classes.SpectreConsoleHelpers;

namespace StudentHelpApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await EntityFrameworkCoreExample();
        await DapperExample();
        Console.WriteLine();
        JsonExample();
        ExitPrompt();
    }

    private static async Task EntityFrameworkCoreExample()
    {
        EntityOperations.Reset();
        EntityOperations.Populate();

        var newItem = BogusOperations.Items(1).First();
        await EntityOperations.Add(newItem);
        var foundItem = EntityOperations.Find(2);
        foundItem.Description = "Updated";
        await EntityOperations.Update(foundItem);

        var removeThisItem = EntityOperations.Find(3);
        await EntityOperations.RemoveAsync(removeThisItem);
        var list = EntityOperations.GetAll();

        var table = CreateViewTable("EF Core");
        foreach (var item in list)
        {
            table.AddRow(item.Id.ToString(), item.Name, item.Description);
        }

        AnsiConsole.Write(table);

    }

    /// <summary>
    /// Where <seealso cref="JsonExample()"/> uses json to act as a database, this
    /// example uses a SQL-Server local database.
    /// </summary>
    private static async Task DapperExample()
    {
        DapperOperations operations = new DapperOperations();
        operations.Populate();

        var newItem = BogusOperations.Items(1).First();
        await operations.Add(newItem);
        var foundItem = await operations.Find(2);
        foundItem.Description = "Updated";
        await operations.Update(foundItem);

        var removeThisItem = await operations.Find(3);
        await operations.Remove(removeThisItem);

        var list = operations.GetAll();

        var table = CreateViewTable("Dapper");
        foreach (var item in list)
        {
            table.AddRow(item.Id.ToString(), item.Name, item.Description);
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Examples using a json file to act as a database
    /// </summary>
    private static void JsonExample()
    {
        JsonOperations.CreateFile(false);

        JsonOperations.ReadAll();
        if (JsonOperations.HasItems() == false)
        {
            JsonOperations.CreateFile();
        }

        var identifier = JsonOperations.Items.Max(x => x.Id) + 1;

        Item item = new Item()
        {
            Id = identifier,
            Name = $"Item {DateTime.Now:ssmm}",
            Description = "Just added description"
        };

        JsonOperations.Add(item);


        var foundItem = JsonOperations.Find(1);
        JsonOperations.Remove(foundItem);


        JsonOperations.Items.FirstOrDefault()!.Description = "Just updated";
        JsonOperations.Update();

        var table = CreateViewTable("Json");
        foreach (var current in JsonOperations.Items)
        {
            table.AddRow(current.Id.ToString(), current.Name, current.Description);
        }

        AnsiConsole.Write(table);

    }
}