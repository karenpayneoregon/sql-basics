using System.Data;
using Carbunql;
using Carbunql.Analysis;
using Carbunql.Analysis.Parser;
using Carbunql.Building;
using Dapper;
using Dapper1App.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;



namespace Dapper1App.Classes;

public class DataOperations
{
    private IDbConnection db = new SqlConnection(ConnectionString());


    public async Task<List<Contact>> AllContacts()
    {
        const string statesStatement = 
            """
            SELECT Id,StateName FROM dbo.States;
            """;
        const string contactStatement =
            """
            SELECT C.Id,
                   C.FirstName,
                   C.LastName,
                   C.Email,
                   C.Company,
                   C.Title,
                   A.Id,
                   A.ContactId,
                   A.AddressType,
                   A.StreetAddress,
                   A.City,
                   A.StateId,
                   A.PostalCode
            FROM dbo.Contacts AS C
                INNER JOIN dbo.Addresses AS A
                    ON A.ContactId = C.Id;
            """;

        var states = await db.QueryAsync <States>(statesStatement);

        var contactDictionary = new Dictionary<int, Contact>();

        IEnumerable<Contact> contacts = await db.QueryAsync<Contact, Address, Contact>(contactStatement, (contact, address) =>
        {
            if (!contactDictionary.TryGetValue(contact.Id, out var currentContact))
            {
                currentContact = contact;
                contactDictionary.Add(currentContact.Id, currentContact);
            }

            currentContact.Addresses.Add(address);
            foreach (var current in currentContact.Addresses)
            {
                current.State = states.FirstOrDefault(x => x.Id == current.StateId);
            }
            return currentContact;
        });

        return contacts.Distinct().ToList();
    }

    // experiments
    public void CarbunqlBasic()
    {
        var text = """
                   SELECT C.ContactId,
                          C.FullName,
                          CD.PhoneNumber
                   FROM dbo.Contacts AS C
                       INNER JOIN dbo.ContactDevices AS CD
                           ON C.ContactId = CD.ContactId
                   WHERE (CD.PhoneTypeIdentifier = 3)
                         AND (C.ContactTypeIdentifier = 7)
                   ORDER BY C.LastName;
                   """;

        var item = QueryParser.Parse(text) as SelectQuery;
        var tablenames = item.GetPhysicalTables().ToList();
    }


}
