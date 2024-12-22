namespace InterfaceExtensionApp;

internal partial class Program
{
    private static void Main()
    {

        List<IHuman> humans = [new Person()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1980, 1, 1),
            Gender = Gender.Male
        }, new Customer()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1990, 12, 7),
            Gender = Gender.Female
        }];

        foreach (var human in humans)
        {
            human.Print();
            Console.WriteLine(); 
        }

        Console.ReadLine();
    }
}

public enum Gender
{
    Female,
    Male,
    Other
}

public interface IIdentity
{
    public int Id { get; set; }
}
public interface IHuman
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}

public class Person : IHuman, IIdentity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}

public class Customer : IHuman, IIdentity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}

public static class Extensions
{
    public static void Print(this IHuman human)
    {
        Console.WriteLine($"      Type: {human.GetType().Name}");

        if (human is IIdentity identity)
        {
            Console.WriteLine($"        Id: {identity.Id}");
        }

        Console.WriteLine($"First Name: {human.FirstName}");
        Console.WriteLine($" Last Name: {human.LastName}");
        Console.WriteLine($"    Gender: {human.Gender}");
        Console.WriteLine($"     Birth: {human.DateOfBirth}");
    }
}
