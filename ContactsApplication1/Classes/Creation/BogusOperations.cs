using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using ContactsApplication1.Models;
using Person = ContactsApplication1.Models.Person;
using Address = ContactsApplication1.Models.Address;

namespace ContactsApplication1.Classes.Creation;
internal class BogusOperations
{
    /// <summary>
    /// Generates a list of <see cref="Person"/> objects with randomized data using the Bogus library.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Person"/> objects, each populated with random values
    /// for properties such as name, gender, date of birth, and notes.
    /// </returns>
    /// <remarks>
    /// This method utilizes the Bogus library to create realistic fake data for testing or demonstration purposes.
    /// The generated data includes:
    /// <list type="bullet">
    /// <item><description>Randomly assigned gender and corresponding gender ID.</description></item>
    /// <item><description>First name and last name based on the assigned gender.</description></item>
    /// <item><description>A middle name, which is required and non-null.</description></item>
    /// <item><description>A random date of birth between 18 and 78 years ago.</description></item>
    /// <item><description>Notes generated as a random sentence.</description></item>
    /// <item><description>Updated timestamp based on the creation timestamp.</description></item>
    /// </list>
    /// </remarks>
    public static List<Person> GeneratePeople()
    {
        // Fake lookup values (match how your DB likely stores them)
        var male = new Gender { GenderId = 1, GenderName = "Male" };
        var female = new Gender { GenderId = 2, GenderName = "Female" };

        var faker = new Faker<Person>()
            .RuleFor(p => p.Gender, f => f.PickRandom(male, female))
            .RuleFor(p => p.GenderId, (f, p) => p.Gender.GenderId)

            .RuleFor(p => p.FirstName, (f, p) =>
                string.Equals(p.Gender.GenderName, "Male", StringComparison.OrdinalIgnoreCase)
                    ? f.Name.FirstName(Name.Gender.Male)
                    : f.Name.FirstName(Name.Gender.Female))

            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.MiddleName, f => f.Name.FirstName()) // required non-null in your model
            .RuleFor(p => p.Notes, f => f.Lorem.Sentence())

            .RuleFor(p => p.DateOfBirth, f =>
            {
                var dt = f.Date.Past(60, DateTime.Today.AddYears(-18)); // 18-78ish
                return DateOnly.FromDateTime(dt);
            })
            .RuleFor(p => p.UpdatedAt, (f, p) => f.Date.Between(p.CreatedAt, DateTime.Now));

        return faker.Generate(20);
    }
    
    /// <summary>
    /// Generates a list of <see cref="Address"/> objects with randomized data using the Bogus library.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Address"/> objects, each populated with random values
    /// for properties such as address lines, city, state/province ID, and postal code.
    /// </returns>
    /// <remarks>
    /// This method utilizes the Bogus library to create realistic fake data for testing or demonstration purposes.
    /// The generated data includes:
    /// <list type="bullet">
    /// <item><description>Random street address for <see cref="Address.AddressLine1"/>.</description></item>
    /// <item><description>Optional secondary address for <see cref="Address.AddressLine2"/>.</description></item>
    /// <item><description>Random city name for <see cref="Address.City"/>.</description></item>
    /// <item><description>Random state or province ID for <see cref="Address.StateProvinceId"/>, assuming a range of 1 to 50.</description></item>
    /// <item><description>Random postal code for <see cref="Address.PostalCode"/>.</description></item>
    /// </list>
    /// </remarks>
    public static List<Address> GenerateAddresses()
    {
        var faker = new Faker<Address>()
            .RuleFor(a => a.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(a => a.AddressLine2, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvinceId, f => f.Random.Short(1, 50)) // Assuming you have 50 states/provinces
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());
        return faker.Generate(50);
    }

    /// <summary>
    /// Selects and returns a single random <see cref="Address"/> object from a list of generated addresses.
    /// </summary>
    /// <returns>
    /// A randomly selected <see cref="Address"/> object containing realistic fake data for properties such as
    /// address lines, city, state/province ID, and postal code.
    /// </returns>
    /// <remarks>
    /// This method internally calls <see cref="GenerateAddresses"/> to create a list of fake addresses using the Bogus library.
    /// It then selects one address at random from the generated list.
    /// </remarks>
    public static Address GenerateAddress()
    {
        var addresses = GenerateAddresses();

        var random = new Random();
        int index = random.Next(addresses.Count);

        return addresses[index];
    }

    /// <summary>
    /// Generates a random <see cref="Address"/> object with realistic fake data using the Bogus library.
    /// </summary>
    /// <returns>
    /// An <see cref="Address"/> object populated with random values for properties such as
    /// address lines, city, state/province ID, and postal code.
    /// </returns>
    /// <remarks>
    /// This method utilizes the Bogus library to create realistic fake address data for testing or demonstration purposes.
    /// The generated data includes:
    /// <list type="bullet">
    /// <item><description>A primary address line (e.g., street address).</description></item>
    /// <item><description>An optional secondary address line (e.g., apartment or suite number).</description></item>
    /// <item><description>A randomly generated city name.</description></item>
    /// <item><description>A state/province ID randomly selected from a range of 1 to 50.</description></item>
    /// <item><description>A postal code generated in a realistic format.</description></item>
    /// </list>
    /// </remarks>
    public static Address GenerateRandomAddress()
    {
        var faker = new Faker<Address>()
            .RuleFor(a => a.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(a => a.AddressLine2, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvinceId, f => f.Random.Short(1, 50))
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

        return faker.Generate();
    }

    /// <summary>
    /// Generates a home phone <see cref="Device"/> with predefined properties.
    /// </summary>
    /// <returns>
    /// A <see cref="Device"/> object representing a home phone, populated with:
    /// <list type="bullet">
    /// <item><description>A <see cref="Device.DeviceTypeId"/> set to 1, indicating a home phone device type.</description></item>
    /// <item><description>A randomly generated phone number for <see cref="Device.DeviceValue"/>.</description></item>
    /// <item><description>An <see cref="Device.IsActive"/> flag set to <c>true</c>.</description></item>
    /// <item><description>A <see cref="Device.CreatedAt"/> timestamp set to the current date and time.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method uses the Bogus library to generate a realistic phone number for the device.
    /// </remarks>
    public static Device GenerateHomePhoneDevice()
    {
        Device device = new()
        {
            DeviceTypeId = 1, 
            DeviceValue = new Faker().Phone.PhoneNumber(),
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        return device;
    }

    /// <summary>
    /// Generates a work phone <see cref="Device"/> object with predefined properties.
    /// </summary>
    /// <returns>
    /// A <see cref="Device"/> object representing a work phone, with the following properties:
    /// <list type="bullet">
    /// <item><description><see cref="Device.DeviceTypeId"/> set to 2, indicating a work phone.</description></item>
    /// <item><description><see cref="Device.DeviceValue"/> populated with a random phone number using the Bogus library.</description></item>
    /// <item><description><see cref="Device.IsActive"/> set to <c>true</c>.</description></item>
    /// <item><description><see cref="Device.CreatedAt"/> set to the current date and time.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method is useful for generating realistic work phone device data for testing or demonstration purposes.
    /// </remarks>
    public static Device GenerateWorkPhoneDevice()
    {
        Device device = new()
        {
            DeviceTypeId = 2,
            DeviceValue = new Faker().Phone.PhoneNumber(),
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        return device;
    }

    /// <summary>
    /// Generates a <see cref="Device"/> object representing a home email device with randomized data.
    /// </summary>
    /// <returns>
    /// A <see cref="Device"/> object with the following properties populated:
    /// <list type="bullet">
    /// <item><description><see cref="Device.DeviceTypeId"/> set to 3, representing a home email device type.</description></item>
    /// <item><description><see cref="Device.DeviceValue"/> set to a randomly generated email address.</description></item>
    /// <item><description><see cref="Device.IsActive"/> set to <c>true</c>.</description></item>
    /// <item><description><see cref="Device.CreatedAt"/> set to the current date and time.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method utilizes the Bogus library to generate a realistic email address for testing or demonstration purposes.
    /// </remarks>
    public static Device GenerateHomeEmailDevice()
    {
        Device device = new()
        {
            DeviceTypeId = 3,
            DeviceValue = new Faker().Internet.Email(),
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        return device;
    }

    /// <summary>
    /// Generates a <see cref="Device"/> object representing a work email device with randomized data.
    /// </summary>
    /// <returns>
    /// A <see cref="Device"/> object with the following properties populated:
    /// <list type="bullet">
    /// <item><description><see cref="Device.DeviceTypeId"/> set to 4, representing a work email device.</description></item>
    /// <item><description><see cref="Device.DeviceValue"/> set to a randomly generated email address.</description></item>
    /// <item><description><see cref="Device.IsActive"/> set to <c>true</c>.</description></item>
    /// <item><description><see cref="Device.CreatedAt"/> set to the current date and time.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method uses the Bogus library to generate a realistic random email address for the device.
    /// </remarks>
    public static Device GenerateWorkEmailDevice()
    {
        Device device = new()
        {
            DeviceTypeId = 4,
            DeviceValue = new Faker().Internet.Email(),
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        return device;
    }

}
