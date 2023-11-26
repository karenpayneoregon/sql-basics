using Bogus;
using StudentHelpApp.Models;

namespace StudentHelpApp.Classes;
internal class BogusOperations
{
    public static List<Item> Items(int count = 5)
    {
        var fakePerson = new Faker<Item>()
            .RuleFor(c => c.Name, f => f.Commerce.ProductName())
            .RuleFor(c => c.Description, f => f.Commerce.ProductDescription());

        return fakePerson.Generate(count);

    }
}
