
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProductsCategoriesApp.Classes;
using ProductsCategoriesApp.Data;
using ProductsCategoriesApp.Models.Projections;

namespace ProductsCategoriesApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        //await EntityCode();


        var ddddd = await DapperOperations.GetContactDetailsAsync();

        var qqq = await DapperOperations.GetContactsGroupedByContactTypeAsync();
        foreach (var dto in qqq)
        {
            Console.WriteLine($"{dto.ContactTypeIdentifier,-4}{dto.Contacts.FirstOrDefault().ContactTitle}");
            foreach (var contact in dto.Contacts)
            {
                Console.WriteLine($"    {contact.ContactId,-4}{contact.FirstName,-20}{contact.LastName,-18}{contact.PhoneNumber,-18}{contact.PhoneTypeDescription}");
            }
        }

        //List<ContactOffice> officeContacts = await Operations.GetContactsForOffice();
        //var test = await Operations.GetContactsAndDevicesSingle(71);
        //var test1 = await Operations.GetContactsWithOfficePhone(3);

        //var json = JsonSerializer.Serialize(officeContacts, new JsonSerializerOptions()
        //{
        //    MaxDepth = 257,
        //    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles, 
        //    WriteIndented = true
        //}); 

        //string fileName = "results.json";
        //if (File.Exists(fileName))
        //{
        //    File.Delete(fileName);
        //}
        //await File.WriteAllTextAsync(fileName, json);

        Console.ReadLine();
    }

    private static async Task EntityCode()
    {
        await using var context = new Context();
        var customers = context.Customers
            .Include(c => c.CountryIdentifierNavigation)
            .Include(c => c.Contact)
            .ThenInclude(c => c.ContactDevices)
            //.ThenInclude(c => c.ContactTypeIdentifierNavigation)
            .ToList();
    }
}

public static class SequenceExtensions
{
    /// <summary>
    /// Find missing elements in a list of int
    /// </summary>
    /// <param name="sequence">int array which may have gaps</param>
    /// <returns>gap array or an empty array</returns>
    public static int[] Missing(this List<int> sequence)
    {
        sequence.Sort();

        return Enumerable
            .Range(1, sequence[^1])
            .Except(sequence)
            .ToArray();
    }
    public static int[] Missing(this int[] sequence)
    {
        //Array.Sort(sequence.ToArray());
        return Enumerable
            .Range(1, sequence[^1])
            .Except(sequence)
            .ToArray();
    }

}