using ContactsApplication1.Classes.Core;
using ContactsApplication1.Classes.Extensions;
using ContactsApplication1.Data;
using ContactsApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace ContactsApplication1;
internal partial class Program
{
    static void Main(string[] args)
    {
        //AddPerson();
        GetFirstPerson();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
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
    private static void GetFirstPerson()
    {
        using var context = new Context();
        var firstPerson = context.People
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
            .FirstOrDefault();
    }
    /// <summary>
    /// Adds a new person to the database, along with their associated address, device, 
    /// and relationships between these entities.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// 1. Creates a new <see cref="Person"/> entity and saves it to the database.
    /// 2. Creates a new <see cref="Address"/> entity and associates it with the person.
    /// 3. Establishes a relationship between the person and the address using <see cref="PersonAddress"/>.
    /// 4. Creates a new <see cref="Device"/> entity and associates it with the person.
    /// 5. Establishes a relationship between the person and the device using <see cref="PersonDevice"/>.
    /// </remarks>
    /// <exception cref="DbUpdateException">
    /// Thrown if there is an error while saving changes to the database.
    /// </exception>
    private static void AddPerson()
    {
        using var context = new Context();

        var person = new Person
        {
            FirstName = "Jane",
            LastName = "Miller",
            MiddleName = "A",
            GenderId = 2,
            DateOfBirth = new DateOnly(1966, 7, 6),
            Notes = "Test person",
        };

        context.People.Add(person);

        // save to get the generated PersonId for the new person
        var count1 = context.SaveChanges();
        
        if (count1 == 1)
        {
            Address address = new Address
            {
                AddressLine1 = "123 Main St",
                City = "Salem",
                StateProvinceId = 37,
                PostalCode = "12345",
            };

            context.Add(address);

            // save to get the generated AddressId for the new address
            var count2 = context.SaveChanges();
            
            if (count2 == 1)
            {
                PersonAddress personAddress = new PersonAddress
                {
                    PersonId = person.PersonId,
                    IsPrimary = true,
                    Address = address,
                    AddressId = address.AddressId,
                    AddressTypeId = 1, 
                    StartDate = DateOnly.FromDateTime(DateTime.Now)
                };

                context.Add(personAddress);
                var count3 = context.SaveChanges();

                Device device = new Device
                {
                    DeviceTypeId = 1, 
                    DeviceValue = "5039991345", 
                    IsActive = true, 
                };

                context.Add(device);
                
                // save to get the generated DeviceId for the new device
                var count4 = context.SaveChanges();
                
                PersonDevice personDevice = new PersonDevice
                {
                    PersonId = person.PersonId,
                    DeviceId = 1,
                    
                    IsPrimary = true, 
                    Device = device
                };

                context.Add(personDevice);

                var count5 = context.SaveChanges();
            }
            
        }
        
        SpectreConsoleHelpers.SuccessPill(Justify.Left,
            $"Person '{person.FirstName} {person.LastName}' added successfully.");
    }
}
