using System.Data;
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
        var statement = 
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

        var contactDictionary = new Dictionary<int, Contact>();

        IEnumerable<Contact> contacts = await db.QueryAsync<Contact, Address, Contact>(statement, (contact, address) =>
        {
            if (!contactDictionary.TryGetValue(contact.Id, out var currentContact))
            {
                currentContact = contact;
                contactDictionary.Add(currentContact.Id, currentContact);
            }

            currentContact.Addresses.Add(address);
            return currentContact;
        });

        return contacts.Distinct().ToList();
    }
}