using Dapper1App.Classes;
using Dapper1App.Models;
using System.Runtime.InteropServices;

namespace Dapper1App;

    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            DataOperations operations = new();
            operations.CarbunqlBasic();
            
            List<Contact> list = await operations.AllContacts();

            AnsiConsole.Record();

            AnsiConsole.MarkupLine("[u]Contact list[/]");
            foreach (var contact in list)
            {
                AnsiConsole.MarkupLine($"[indianred_1]{contact.Title,-20}{contact.FirstName,-15}{contact.LastName}[/]");

                foreach (var address in contact.Addresses)
                {
                    AnsiConsole.MarkupLine(address.AddressType == "Home"
                        ? $"  [mediumpurple4]{address.AddressType}[/]"
                        : $"  [green][b]{address.AddressType}[/][/]");
                    AnsiConsole.MarkupLine($"  [teal]{address.StreetAddress,-20}{address.State.StateName,-20}{address.PostalCode}[/]");
                }

                AnsiConsole.WriteLine("");
            }

            await File.WriteAllTextAsync("Demo.html", AnsiConsole.ExportHtml());

            Console.ReadLine();
        }
    }

