using ConsoleApp1.Classes;
using ConsoleApp1.Models;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ConsoleApp1;

internal class Program
{
    static async Task Main(string[] args)
    {
        List<Customers> customers = EntityOperations.CustomersList();
        //var dump = ObjectDumper.Dump(customers.FirstOrDefault());
        //Debug.WriteLine(dump);
        //Console.ReadLine();
        Console.WriteLine("Results");
        DapperOperation1 operations = new ();
        var list1 = await operations.GetAllAsync();
        //var person = operations.GetPerson(2);
        //UpDateItem(operations);

        //var (list, exception) = operations.GetOrdersPagination();
        //if (exception is null)
        //{
        //    Console.WriteLine(list.Count == 3);
        //}

        //operations.OrdersPaginationStatementInResourcesNotBrokenStatement();
        Console.ReadLine();

    }

    private static void UpDateItem(DapperOperations operations)
    {
        ProductItem item = new()
        {
            Id = 1,
            ColorId = 7,
            Item = "iPhone 9"
        };

        var (success, exception) = operations.UpdateRow(item);
    }

    private static void PrimaryConstructorSimple()
    {
        var karen = new PersonPrimary("Karen", 66);
    }

    private static void FrozenCollections()
    {
        // https://learn.microsoft.com/en-us/dotnet/api/system.collections.frozen.frozendictionary.tofrozendictionary?view=net-8.0

        List<int> normalList = new() { 1, 2, 3 };
        ReadOnlyCollection<int> readonlyList = normalList.AsReadOnly();
        FrozenSet<int> frozenSet = normalList.ToFrozenSet();
        ImmutableList<int> immutableList = normalList.ToImmutableList();

        normalList.Add(4);

        Console.WriteLine($"List count: {normalList.Count}");
        Console.WriteLine($"ReadOnlyList count: {readonlyList.Count}");
        Console.WriteLine($"FrozenSet count: {frozenSet.Count}");
        Console.WriteLine($"ImmutableList count: {immutableList.Count}");
    }
}