using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ProductsCategoriesApp.Classes;

public static class DapperOperations
{
    public static IEnumerable<Contact> GetContacts()
    {
        using SqlConnection cn = new(ConnectionString());
        string query = """
                       SELECT c.ContactId, c.FirstName, c.LastName, ct.ContactTypeIdentifier, cd.PhoneNumber, pt.[PhoneTypeDescription] 
                       FROM Contacts c 
                       JOIN ContactType ct ON c.ContactTypeIdentifier = ct.ContactTypeIdentifier 
                       JOIN ContactDevices cd ON c.ContactId = cd.ContactId 
                       JOIN PhoneType pt ON cd.PhoneTypeIdentifier = pt.PhoneTypeIdenitfier 
                       """ ;

        return cn.Query<Contact>(query);
    }


    public static async Task<List<ContactDetailDto>> GetContactDetailsAsync()
    {
        await using SqlConnection cn = new(ConnectionString());
        const string sql =
            """
            SELECT     c.ContactId,
                       c.FirstName,
                       c.LastName,
                       ct.ContactTypeIdentifier,
                       cd.PhoneNumber,
                       pt.PhoneTypeDescription,
                       ct.ContactTitle
             FROM      dbo.Contacts AS c
            INNER JOIN dbo.ContactType AS ct
               ON c.ContactTypeIdentifier = ct.ContactTypeIdentifier
            INNER JOIN dbo.ContactDevices AS cd
               ON c.ContactId             = cd.ContactId
            INNER JOIN dbo.PhoneType AS pt
               ON cd.PhoneTypeIdentifier  = pt.PhoneTypeIdenitfier;
            """; 

        return (await cn.QueryAsync<ContactDetailDto>(sql)).AsList();

    }

    public static async Task<List<ContactTypeGroupDto>> GetContactsGroupedByContactTypeAsync()
    {
        await using SqlConnection cn = new(ConnectionString());

        const string sql =
            """
            SELECT 
                c.ContactId, 
                c.FirstName, 
                c.LastName, 
                ct.ContactTypeIdentifier, 
                cd.PhoneNumber, 
                pt.PhoneTypeDescription, 
                ct.ContactTitle
            FROM Contacts c
            JOIN ContactType ct ON c.ContactTypeIdentifier = ct.ContactTypeIdentifier
            JOIN ContactDevices cd ON c.ContactId = cd.ContactId
            JOIN PhoneType pt ON cd.PhoneTypeIdentifier = pt.PhoneTypeIdenitfier
            ORDER BY ct.ContactTypeIdentifier
            """;

        var rawContacts = await cn.QueryAsync<ContactDetailDto>(sql);

        var grouped = rawContacts
            .GroupBy(c => c.ContactTypeIdentifier)
            .Select(g => new ContactTypeGroupDto
            {
                ContactTypeIdentifier = g.Key,
                Contacts = g.ToList()
            })
            .ToList();

        return grouped;
    }




}

public class Contact
{
    public int ContactId { get; set; }
    public string ContactName { get; set; }
    public string ContactType { get; set; }
    public string ContactDevices { get; set; }
    public string PhoneType { get; set; }
}

public class ContactDetailDto
{
    public int ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ContactTypeIdentifier { get; set; }
    public string ContactTitle { get; set; }
    public string PhoneNumber { get; set; }
    public string PhoneTypeDescription { get; set; }
}


public class ContactTypeGroupDto
{
    public int ContactTypeIdentifier { get; set; }
    public List<ContactDetailDto> Contacts { get; set; } = [];
}

