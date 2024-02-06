using DapperForAccessDatabase.Models;

namespace DapperForAccessDatabase.Classes;
public class MockedData
{
    public static List<Person> People()
    {
        return
        [
            new Person() { FirstName = "Kim", LastName = "Adams", BirthDate = new DateOnly(1978, 11, 1), Active = true },
            new Person() { FirstName = "Mary", LastName = "Peters", BirthDate = new DateOnly(2000, 5, 8), Active = true },
            new Person() { FirstName = "John", LastName = "Smith", BirthDate = new DateOnly(1945, 9, 12), Active = false }
        ];
    }
}
