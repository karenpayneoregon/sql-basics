using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using ContactsApplication1.Models;
using Person = ContactsApplication1.Models.Person;

namespace ContactsApplication1.Classes.Creation;
internal class BogusOperations
{
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
}
