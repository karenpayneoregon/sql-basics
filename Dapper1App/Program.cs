using Dapper1App.Classes;
using Dapper1App.Models;
using System.Runtime.InteropServices;

namespace Dapper1App;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        DataOperations operations = new();
        
        List<Contact> list = await operations.AllContacts();

        Console.WriteLine(ObjectDumper.Dump(list));

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.SetWindowPosition(0, 0);
        }
        
        Console.ReadLine();
    }
}