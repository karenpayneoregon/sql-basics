using DapperLibrary1.Models;
using DapperPersonRepository.MockingClasses;
using DapperLibrary1.Repositories;
using static DapperPersonRepository.Classes.SpectreConsoleHelpers;

namespace DapperPersonRepository;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        /*
         * Create an new instance of PersonRepository which
         *  - Sets up the connection
         *  - Sets up a handler for DateOnly work
         */
        PersonRepository repo = new();
        var nullPerson = repo.BuilderWithNull();

        /*
         * Get current count of records if any
         */
        Console.WriteLine($"Record count: {await repo.Count()}");

        /*
         * Remove all existing records
         */
        await repo.Reset();
        Console.WriteLine($"Record count: {await repo.Count()}");


        /*
         * Populate the table with five records created by Bogus package
         */
        await repo.AddRange(BogusOperations.People(5));
        Console.WriteLine($"Record count: {await repo.Count()}");

        /*
         * List all records to the console.
         */
        var table = PersonTable();
        var people = await repo.GetAllAsync();
        foreach (var peep in people)
        {
            table.AddRow(peep.Id.ToString(), peep.FirstName, peep.LastName, peep.BirthDate.ToString());
        }

        AnsiConsole.MarkupLine($"[LightGreen]All people[/]");
        AnsiConsole.Write(table);


        //https://github.com/thomasgalliker/ObjectDumper?tab=readme-ov-file
        //Console.WriteLine(ObjectDumper.Dump(people, DumpStyle.CSharp));

        DumpOptions options = new()
        {
            DumpStyle = DumpStyle.CSharp,
            MaxLevel = 3
        };
        Console.WriteLine(ObjectDumper.Dump(people, options));

        /*
         * Get the second record, change first name followed
         * by saving to the table
         */
        Person person = await repo.Get(2);
        Console.WriteLine($"First name is {person.FirstName}");
        person.FirstName += " Changed";

        // return values not used here but shows how to deconstruct 
        var (success, exception) = await repo.Update(person);

        /*
         * Get the first record and delete from the table
         */
        person = await repo.Get(1);
        Console.WriteLine($"First name is now {person.FirstName}");

        await repo.Remove(person);
        person = await repo.Get(1);
        Console.WriteLine(person is null ? "Person remove" : "Failed to remove person");

        /*
         * Show all records, first record has been removed so there are now
         * only four records.
         */
        table = PersonTable();
        people = await repo.GetAllAsync();
        foreach (var peep in people)
        {
            table.AddRow(peep.Id.ToString(), peep.FirstName, peep.LastName, peep.BirthDate.ToString());
        }

        AnsiConsole.MarkupLine($"[LightGreen]All people after delete[/]");
        AnsiConsole.Write(table);

        int[] identifiers = { 2, 4 };
        var whereIn = await repo.WhereIn(identifiers);
        table = PersonTable();

        foreach (var peep in whereIn)
        {
            table.AddRow(peep.Id.ToString(), peep.FirstName, peep.LastName, peep.BirthDate.ToString());
        }

        AnsiConsole.MarkupLine($"[LightGreen]Where in results[/]");
        AnsiConsole.Write(table);

        var fromJson = await repo.GetAllJson();
        ExitPrompt();
    }
}