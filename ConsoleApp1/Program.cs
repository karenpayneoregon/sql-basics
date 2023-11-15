
using ConsoleApp1;
using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;

//using Karen = (string test, int test1);


namespace ConsoleApp1;


internal class Program
{
    static void Main(string[] args)
    {


        //int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        //// Create a jagged 2D array from variables:
        //int[] row0 = [1, 2, 3];
        //int[] row1 = [4, 5, 6];
        //int[] row2 = [7, 8, 9];
        //int[][] twoDFromVariables = [row0, row1, row2];

        using var context = new Context();
        var customers = context.Customers
            .Include(x => x.Contact)
            .ThenInclude(x => x.ContactDevices)
            .Include(x => x.ContactTypeIdentifierNavigation)
            .Include(x => x.CountryIdentifierNavigation)
            .Where(x => x.Contact.ContactTypeIdentifierNavigation.ContactTitle.Contains("ne"))
            .ToList();


        Console.WriteLine("Hello, World!");
    }
}

