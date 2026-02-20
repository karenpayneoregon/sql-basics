using ContactsApplication1.Classes.Core;
using ContactsApplication1.Classes.Creation;
using ContactsApplication1.Classes.Extensions;
using ContactsApplication1.Data;
using ContactsApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console;

namespace ContactsApplication1;
internal partial class Program
{
    static void Main(string[] args)
    {
        //GenerateAndAddPeople();
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
        var people = BogusOperations.GeneratePeople();
        foreach (var person in people)
        {
            MockOperations.AddPerson(person);
        }

        using var context = new Context();
        Console.WriteLine(context.People.Count());
    }

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
        
        SpectreConsoleHelpers.WindowTitle(Justify.Center, "Contacts Application");

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
   


        Console.WriteLine(); // Add a blank line for spacing
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
    private static void GetFirstPerson(int id)
    {
        using var context = new Context();
        var firstPerson = context.People
            // no need to track entities for read-only operations, improves performance
            .AsNoTracking()
            .TagWithDebugInfo("GetFirstPerson")
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
    }
    
}
