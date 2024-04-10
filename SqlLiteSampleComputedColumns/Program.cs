using SqlLiteSampleComputedColumns.Classes;

namespace SqlLiteSampleComputedColumns;

internal partial class Program
{
    static void Main(string[] args)
    {
        var people = DapperOperations.GetPeople();
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Id,-5}{person.FullName}");
        }
        Console.ReadLine();
    }
}