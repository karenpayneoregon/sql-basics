namespace ConsoleApp1.Models;

public class PersonPrimary(string name, int age)
{
    public string Name { get; set; } = name;

    public int Age { get; set; } = age;

    // Just like records we have to call this(...)
    public PersonPrimary() : this("Karen", 66)
    {
    }
}