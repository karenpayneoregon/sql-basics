using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using ProductsCategoriesApp1.Classes.Configuration;
using ProductsCategoriesApp1.Models;

namespace ProductsCategoriesApp1.Classes;

internal class DataOperations
{
    public static IEnumerable<Customer> GetCustomerDetails()
    {

        using IDbConnection connection = new SqlConnection(DataConnections.Instance.MainConnection);
        var customerDictionary = new Dictionary<int, Customer>();

        var customers = connection.Query<Customer, Contact, Country, ContactType, Customer>(
            SqlStatements.CustomerWithContacts(),
            (customer, contact, country, contactType) =>
            {
                if (!customerDictionary.TryGetValue(customer.CustomerIdentifier, out var existing))
                {
                    existing = customer;
                    customerDictionary[customer.CustomerIdentifier] = existing;
                }

                existing.Contact = contact;
                existing.Country = country;
                existing.ContactType = contactType;

                return existing;
            },
            splitOn: "ContactId,CountryIdentifier,ContactTypeIdentifier");

        return customers;
    }

    public static async Task<Contact> GetContactByIdAsync(int contactId)
    {
        const string query = @"
            SELECT      C.ContactId,
                        C.FirstName,
                        C.LastName,
                        C.ContactTypeIdentifier,
                        CT.ContactTypeIdentifier AS CTIdentifier,
                        CT.ContactTitle,
                        CD.DeviceId,
                        CD.ContactId AS CDContactId,
                        CD.PhoneTypeIdentifier,
                        CD.PhoneNumber
            FROM        dbo.Contacts AS C
            INNER JOIN  dbo.ContactType AS CT
                ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
            LEFT JOIN   dbo.ContactDevices AS CD
                ON C.ContactId = CD.ContactId
            WHERE       C.ContactId = @ContactId";

        using IDbConnection connection = new SqlConnection(DataConnections.Instance.MainConnection);
        var contactDictionary = new Dictionary<int, Contact>();

        var result = await connection.QueryAsync<Contact, ContactType, ContactDevices, Contact>(
            query,
            (contact, contactType, contactDevice) =>
            {
                if (!contactDictionary.TryGetValue(contact.ContactId, out var contactEntry))
                {
                    contactEntry = contact;
                    contactEntry.ContactTypeIdentifierNavigation = contactType;
                    contactDictionary.Add(contact.ContactId, contactEntry);
                }

                if (contactDevice != null)
                {
                    contactEntry.ContactDevices.Add(contactDevice);
                }

                return contactEntry;
            },
            param: new { ContactId = contactId },
            splitOn: "CTIdentifier,DeviceId");

        return contactDictionary.Values.FirstOrDefault();
    }
}