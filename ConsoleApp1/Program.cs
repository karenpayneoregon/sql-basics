using ConsoleApp1.Classes;
using ConsoleApp1.Models;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Numerics;
// ReSharper disable LocalizableElement

namespace ConsoleApp1;

internal class Program
{
    static async Task Main(string[] args)
    {
        //List<Customers> customers = EntityOperations.CustomersList();
        
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

        
        const int intValue = 123__456___789;
        const decimal decimalValue = 123_456_789.43_534m;
        const double doubleValue = 123_456_789.43534;

        
        
        
        int places = 3;

        Console.WriteLine($"{decimalValue.Truncate(places).ToCurrency(places)}");
        Console.WriteLine($"{doubleValue.Truncate(places).ToCurrency(places)}");
        Console.WriteLine(doubleValue.GetFractionalPart(places));
        Console.WriteLine(doubleValue.GetFractionalPartInt(places));
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

        List<int> normalList = [1, 2, 3];
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

public static class NumericExtensions
{
    public static double Truncate(this double value, int places = 2)
    {
        var factor = Math.Pow(10, places);
        return Math.Truncate(value * factor) / factor;
    }
    public static decimal Truncate(this decimal value, int decimals = 2)
    {
        var factor = (decimal)Math.Pow(10, decimals);
        decimal result = Math.Truncate(factor * value) / factor;
        return result;
    }
    // Get fractional part of a number as a decimal
    public static decimal GetFractionalPart<T>(this T sender, int places) where T : INumber<T>
        => Math.Round(decimal.CreateChecked(sender) - Math.Truncate(decimal.CreateChecked(sender)), places);

    public static int GetFractionalPartInt<T>(this T sender, int places = 2) where T : INumber<T>
    {
        var value = Convert.ToInt32((decimal.CreateChecked(sender) % 1).ToString(CultureInfo.InvariantCulture).Replace("0.", ""));
        var value1 = value.ToString();
        if (places < value1.Length)
        {
            value = Convert.ToInt32(value1.Substring(0, places));
        }
        return int.IsNegative(value) ? value.Invert() : value;
    }

    public static T Invert<T>(this T source) where T : INumber<T>
        => -source;
    public static string ToCurrency<T>(this T sender, int places = 2) where T : INumber<T> => sender switch
        {
            decimal => decimal.CreateChecked(sender).ToString($"C{places}"),
            double => double.CreateChecked(sender).ToString($"C{places}"),
            _ => throw new ArgumentException("Invalid type")
        };
}