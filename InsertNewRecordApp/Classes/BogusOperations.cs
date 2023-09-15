using Bogus;

namespace InsertNewRecordApp.Classes;
internal class BogusOperations
{
    public static List<Models.Person> People(int count = 20) =>
        new Faker<Models.Person>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.BirthDate, f => 
                f.Date.BetweenDateOnly(new DateOnly(1999,1,1),
                    new DateOnly(2010, 1, 1))).Generate(count);
}
