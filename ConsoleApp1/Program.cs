using ConsoleApp1.Classes;
using ConsoleApp1.Models;

//using Karen = (string test, int test1);

namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {

        DapperOperations operations = new ();
        //var list = operations.GetAll();
        //var person = operations.GetPerson(2);

        ProductItem item = new()
        {
            Id = 1,
            ColorId = 7,
            Item = "iPhone 9"
        };

        var (success, exception) = operations.UpdateRow(item);
    }
}

