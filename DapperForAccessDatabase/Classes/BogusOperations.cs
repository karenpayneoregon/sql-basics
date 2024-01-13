using Bogus;
using DapperForAccessDatabase.Models;

namespace DapperForAccessDatabase.Classes;
internal class BogusOperations
{
    public static List<Customers> CustomersListHasNoIdentifiers(int count = 100)
    {
        var faker = new Faker<Customers>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(e => e.UserName, f => f.Person.UserName);
        
        return faker.Generate(count);

    }
}
