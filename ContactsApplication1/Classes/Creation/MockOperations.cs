using Bogus;
using ContactsApplication1.Classes.Core;
using ContactsApplication1.Data;
using ContactsApplication1.Models;
using Serilog;
using Spectre.Console;

using Person = ContactsApplication1.Models.Person;

namespace ContactsApplication1.Classes.Creation;
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
    private static void EditPerson_AddAddress(Person person)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == person.PersonId);

        Address address = BogusOperations.GenerateAddress();

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
        EditPerson_AddDevice(contact);

    }

    private static void EditPerson_AddDevice(Person person)
    {
        using var context = new Context();

        var contact = context.People.FirstOrDefault(p => p.PersonId == person.PersonId);

        Device device1 = new Device
        {
            DeviceTypeId = 3,
            DeviceValue = $"{person.FirstName}{person.LastName}@comcast.net",
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
