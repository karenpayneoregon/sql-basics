using ContactsApplication1.Classes.Configuration;
using ContactsApplication1.Classes.Core;
using ContactsApplication1.Classes.Creation;
using ContactsApplication1.Classes.Extensions;
using ContactsApplication1.Data;
using ContactsApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console;
using System.Diagnostics;

namespace ContactsApplication1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        if (AppConfiguration.Instance.Use)
        {
            await Warmup();
        }

        GenerateAndAddPeople();
        DisplayPeople();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Generates a collection of people with randomized data and adds them to the mock operations.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// <list type="number">
    /// <item><description>Generates a list of <see cref="Person"/> objects using <see cref="BogusOperations.GeneratePeople"/>.</description></item>
    /// <item><description>Iterates through the generated list and adds each <see cref="Person"/> to the mock operations using <see cref="MockOperations.AddPerson"/>.</description></item>
    /// <item><description>Logs the total count of people in the database context to the console.</description></item>
    /// </list>
    /// </remarks>
    private static void GenerateAndAddPeople()
    {
        using var context = new Context();
        if (context.People.Any())
        {
            AnsiConsole.MarkupLine("[yellow]People already exist in the database. Skipping generation.[/]");
            return;
        }
        
        var people = BogusOperations.GeneratePeople();
        foreach (var person in people)
        {
            MockOperations.AddPerson(person);
        }

    }

    /// <summary>
    /// Displays a list of people and their associated details in the console.
    /// </summary>
    /// <remarks>
    /// This method retrieves people data from the database, including related entities such as addresses,
    /// address types, genders, and devices. It uses Spectre.Console for enhanced console output.
    /// </remarks>
    /// <example>
    /// The output includes:
    /// - Person details such as ID, name, date of birth, and gender.
    /// - Address details including address line, city, and state.
    /// - Device details including device type, value, start date, and whether it is primary or secondary.
    /// </example>
    private static void DisplayPeople()
    {
        using var context = new Context();
        
        var people = context.People
            .AsNoTracking()
            .Include(p => p.PersonAddresses)
            .ThenInclude(pa => pa.Address)
            .ThenInclude(a => a.StateProvince)
            .Include(p => p.PersonAddresses)
            .ThenInclude(pa => pa.AddressType)
            .Include(p => p.Gender)
            .Include(p => p.PersonDevices)
            .ThenInclude(pd => pd.Device)
            .ThenInclude(d => d.DeviceType)
            .ToList();
        

        SpectreConsoleHelpers.InfoPill(Justify.Left, "Generated People Data");


        foreach (var p in people)
        {
            AnsiConsole.MarkupLine($"[cyan]{p.PersonId,-4}{p.FirstName,-12}{p.LastName,-12}{p.DateOfBirth, -12:MM/dd/yyyy}{p.Gender?.GenderName}[/]");

            foreach (var pPersonAddress in p.PersonAddresses)
            {
                Console.WriteLine($"  {pPersonAddress.Address.AddressLine1, -25}{pPersonAddress.Address?.City,-18}{pPersonAddress.Address?.StateProvince.StateName}");
            }
            
            foreach (var d in p.PersonDevices)
            {
                AnsiConsole.MarkupLine($"  {d.Device.DeviceType.DeviceTypeName, -25}{d.Device.DeviceValue, -30}{d.StartDate, -12:MM/dd/yyyy}{(d.IsPrimary ? "[green]Primary[/]" : "[red]Secondary[/]")}");
            }

            Console.WriteLine();
        }
   


        Console.WriteLine();
    }

    /// <summary>
    /// Retrieves the first <see cref="Person"/> from the database, including related entities such as
    /// addresses, address types, state/province, gender, and devices.
    /// </summary>
    /// <remarks>
    /// This method uses Entity Framework Core to query the database and includes multiple related
    /// entities to ensure all necessary data is loaded. Debug information is tagged to the query
    /// for easier debugging in development mode.
    /// </remarks>
    private static void GetPerson(int id)
    {
        using var context = new Context();
        var person = context.People
            // no need to track entities for read-only operations, improves performance
            .AsNoTracking()
            .TagWithDebugInfo("GetPerson")
            .Include(p => p.PersonAddresses)
            .ThenInclude(pa => pa.Address)
            .ThenInclude(a => a.StateProvince)
            .Include(p => p.PersonAddresses)
            .ThenInclude(pa => pa.AddressType)
            .Include(p => p.Gender)
            .Include(p => p.PersonDevices)
            .ThenInclude(pd => pd.Device)
            .ThenInclude(d => d.DeviceType)
            .FirstOrDefault(x => x.PersonId == id);
        
        Debugger.Break();
        
    }
    
}
