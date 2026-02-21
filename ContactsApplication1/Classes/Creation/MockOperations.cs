using Bogus;
using ContactsApplication1.Classes.Core;
using ContactsApplication1.Data;
using ContactsApplication1.Models;
using Serilog;
using Spectre.Console;

using Person = ContactsApplication1.Models.Person;

namespace ContactsApplication1.Classes.Creation;
/// <summary>
/// Provides mock operations for managing and manipulating <see cref="Person"/> entities 
/// and their related data, such as addresses and devices, within the application.
/// </summary>
/// <remarks>
/// This class is designed to facilitate the creation and addition of <see cref="Person"/> entities 
/// to the database, along with their associated addresses and devices. It utilizes a combination 
/// of random data generation and database operations to populate the application's data context.
/// </remarks>
internal class MockOperations
{
    public static void AddPerson(Person p)
    {
        using var context = new Context();

        Person person = new()
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            MiddleName = p.MiddleName,
            DateOfBirth = p.DateOfBirth,
            GenderId = p.GenderId,
            Notes = p.Notes,
        };

        context.People.Add(person);

        // save to get the generated PersonId for the new person
        var count1 = context.SaveChanges();

        if (count1 == 1)
        {
            Address address1 = BogusOperations.GenerateRandomAddress();

            context.Add(address1);

            // save to get the generated AddressId for the new address
            var count2 = context.SaveChanges();

            if (count2 == 1)
            {
                PersonAddress personAddress1 = new()
                {
                    PersonId = person.PersonId,
                    IsPrimary = true,
                    Address = address1,
                    AddressId = address1.AddressId,
                    AddressTypeId = 1,
                    StartDate = DateOnly.FromDateTime(DateTime.Now)
                };

                context.Add(personAddress1);
                var count3 = context.SaveChanges();

                Device device1 = new()
                {
                    DeviceTypeId = 1,
                    DeviceValue = new Faker().Phone.PhoneNumber(),
                    IsActive = true,
                };

                context.Add(device1);

                // save to get the generated DeviceId for the new device
                var count4 = context.SaveChanges();

                PersonDevice personDevice1 = new()
                {
                    PersonId = person.PersonId,
                    DeviceId = 1,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    IsPrimary = true,
                    Device = device1
                };

                context.Add(personDevice1);

                var count5 = context.SaveChanges();
                EditPerson_AddAddress(person);
            }

        }
        else
        {
            Log.Information("Failed to add person to the database.");
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Failed to add person to the database.");
        }

        SpectreConsoleHelpers.SuccessPill(Justify.Left, $"Person '{person.FirstName} {person.LastName}' added successfully.");
    }
    /// <summary>
    /// Adds a new address to the specified <see cref="Person"/> and associates it with the person.
    /// </summary>
    /// <param name="person">
    /// The <see cref="Person"/> object to which the new address will be added. 
    /// This must include a valid <c>PersonId</c> that exists in the database.
    /// </param>
    /// <remarks>
    /// This method generates a new address using <see cref="BogusOperations.GenerateAddress"/> and saves it to the database.
    /// It then creates a new <see cref="PersonAddress"/> entry to associate the generated address with the specified person.
    /// If the operation is successful, a success message is displayed using <see cref="SpectreConsoleHelpers.SuccessPill"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the specified <see cref="Person"/> does not exist in the database.
    /// </exception>
    private static void EditPerson_AddAddress(Person person)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == person.PersonId);

        Address address = BogusOperations.GenerateAddress();

        context.Add(address);
        var count1 = context.SaveChanges();

        PersonAddress personAddress1 = new()
        {
            PersonId = contact.PersonId,
            IsPrimary = true,
            Address = address,
            AddressId = address.AddressId,
            AddressTypeId = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now)
        };

        context.Add(personAddress1);
        var count2 = context.SaveChanges();

        SpectreConsoleHelpers.SuccessPill(Justify.Left, "Added address");
        EditPerson_AddDevice(contact);

    }

    /// <summary>
    /// Associates a new device with the specified person in the database.
    /// </summary>
    /// <param name="person">
    /// The <see cref="Person"/> object representing the individual to whom the device will be added.
    /// </param>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Retrieves the person record from the database using the provided <paramref name="person"/>.
    /// 2. Creates a new <see cref="Device"/> instance and populates its properties.
    /// 3. Saves the new device to the database to generate its unique identifier.
    /// 4. Creates a <see cref="PersonDevice"/> instance to associate the device with the person.
    /// 5. Saves the association to the database.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the specified person does not exist in the database.
    /// </exception>
    private static void EditPerson_AddDevice(Person person)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == person.PersonId);

        Device device1 = new()
        {
            DeviceTypeId = 3,
            DeviceValue = $"{person.FirstName}{person.LastName}@comcast.net",
            IsActive = true,
        };

        context.Add(device1);

        // save to get the generated DeviceId for the new device
        var count1 = context.SaveChanges();

        PersonDevice personDevice1 = new()
        {
            PersonId = contact.PersonId,
            DeviceId = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            IsPrimary = true,
            Device = device1
        };

        context.Add(personDevice1);

        var count2 = context.SaveChanges();
    }
}
