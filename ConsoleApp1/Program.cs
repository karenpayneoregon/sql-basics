using ConsoleApp1.Classes;

//using Karen = (string test, int test1);

namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {

        DapperOperations operations = new ();
        var list = operations.GetAll();
        //var person = operations.GetPerson(2);
    }
}

