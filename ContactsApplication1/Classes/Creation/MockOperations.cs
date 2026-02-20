using Serilog;
using Spectre.Console;
using ContactsApplication1.Classes.Core;
using ContactsApplication1.Models;
using ContactsApplication1.Data;

namespace ContactsApplication1.Classes.Creation;
internal class MockOperations
{
    public static void AddPerson()
    {
        using var context = new Context();

        Person person = new()
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
            Address address1 = new()
            {
                AddressLine1 = "123 Main St",
                City = "Salem",
                StateProvinceId = 37,
                PostalCode = "12345",
            };

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
                    DeviceValue = "5039991345",
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
                EditPerson_AddAddress(person.PersonId);
            }

        }
        else
        {
            Log.Information("Failed to add person to the database.");
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Failed to add person to the database.");
        }

        SpectreConsoleHelpers.SuccessPill(Justify.Left, $"Person '{person.FirstName} {person.LastName}' added successfully.");
    }
    private static void EditPerson_AddAddress(int personId)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == personId);

        Address address = new Address
        {
            AddressLine1 = "34 Cherry Ave",
            City = "Portland",
            StateProvinceId = 37,
            PostalCode = "12345",
        };

        context.Add(address);
        var count1 = context.SaveChanges();

        PersonAddress personAddress1 = new PersonAddress
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
        EditPerson_AddDevice(personId);

    }

    private static void EditPerson_AddDevice(int personId)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == personId);

        Device device1 = new Device
        {
            DeviceTypeId = 3,
            DeviceValue = "janeMiller@comcast.net",
            IsActive = true,
        };

        context.Add(device1);

        // save to get the generated DeviceId for the new device
        var count1 = context.SaveChanges();

        PersonDevice personDevice1 = new PersonDevice
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
