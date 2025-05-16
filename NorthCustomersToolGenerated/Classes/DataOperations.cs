using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using NorthCustomersToolGenerated.Models;
using static NorthCustomersToolGenerated.Classes.Configuration.DataConnections;

namespace NorthCustomersToolGenerated.Classes;

/// <summary>
/// Provides data access operations for retrieving and managing customer and contact information.
/// </summary>
/// <remarks>
/// This class contains methods that utilize Dapper to execute SQL queries for fetching data
/// from the database. It includes functionality for retrieving customer details, contact information,
/// and their associated relationships, such as contact types and devices.
///
/// NOTES:
/// * There is no exception handling to keep focus on Dapper operations.
/// * Recommend logging e.g. SeriLog to a file.
/// </remarks>

internal class DataOperations
{
    /// <summary>
    /// Retrieves a collection of customer details, including associated contact, country, and contact type information.
    /// </summary>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that joins multiple tables to fetch customer details
    /// along with their related contact, country, and contact type data. The results are mapped into a dictionary
    /// to ensure unique customer entries.
    /// </remarks>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
    /// of <see cref="Customer"/> objects with populated related data.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue executing the SQL query.
    /// </exception>
    /// <example>
    /// <code>
    /// var customers = (await DataOperations.GetCustomerDetails()).ToList();
    /// foreach (var customer in customers)
    /// {
    ///     Console.WriteLine(customer.CompanyName);
    /// }
    /// </code>
    /// </example>
    public static async Task<IEnumerable<Customer>> GetCustomerDetails()
    {

        using IDbConnection connection = new SqlConnection(Instance.MainConnection);
        var customerDictionary = new Dictionary<int, Customer>();

        var customers = await connection.QueryAsync<Customer, Contact, Country, ContactType, Customer>(
            SqlStatements.CustomerWithContacts,
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
            splitOn: $"{nameof(Customer.ContactId)}," +
                     $"{nameof(Customer.CountryIdentifier)}," +
                     $"{nameof(ContactType.ContactTypeIdentifier)}");

        return customers;
    }

    /// <summary>
    /// Retrieves a customer by their unique identifier, including related details such as contact, country, and contact type.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to retrieve.</param>
    /// <returns>
    /// A <see cref="Customer"/> object containing the customer's details, or <c>null</c> if no customer is found.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves customer information along with related entities.
    /// The query results are mapped to a <see cref="Customer"/> object and its associated properties.
    /// </remarks>
    public static async Task<Customer> GetCustomer(int id)
    {
        using IDbConnection connection = new SqlConnection(Instance.MainConnection);
        var customerDictionary = new Dictionary<int, Customer>();

        var customers = await connection.QueryAsync<Customer, Contact, Country, ContactType, Customer>(
            SqlStatements.GetCustomer,
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
            new { CustomerIdentifier = id }, // Pass the id parameter here
            splitOn: "ContactId,CountryIdentifier,ContactTypeIdentifier");


        return customerDictionary.Values.FirstOrDefault();
    }


    /// <summary>
    /// Retrieves a contact by its unique identifier asynchronously.
    /// </summary>
    /// <param name="contactId">
    /// The unique identifier of the contact to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the 
    /// <see cref="Contact"/> object if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves the contact details, 
    /// including its associated contact type and devices.
    /// </remarks>
    public static async Task<Contact> GetContactByIdAsync(int contactId)
    {

        using IDbConnection connection = new SqlConnection(Instance.MainConnection);
        var contactDictionary = new Dictionary<int, Contact>();

        var result = await connection.QueryAsync<Contact, ContactType, ContactDevices, Contact>(
            SqlStatements.GetContact,
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

    /// <summary>
    /// Retrieves a list of contacts filtered by the specified contact type identifier.
    /// </summary>
    /// <param name="contactTypeIdentifier">
    /// The identifier of the contact type used to filter the contacts.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of 
    /// <see cref="Models.Contact"/> objects associated with the specified contact type.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves contacts and their associated contact type.
    /// The query results are mapped to <see cref="Models.Contact"/> and 
    /// <see cref="Models.ContactType"/> objects.
    /// </remarks>
    public static async Task<List<Contact>> ContactsByType(int contactTypeIdentifier)
    {

        using IDbConnection connection = new SqlConnection(Instance.MainConnection);
        var contactList = new List<Contact>();

        var contacts = await connection.QueryAsync<Contact, ContactType, Contact>(
            SqlStatements.GetContactsByType,
            (contact, contactType) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contactList.Add(contact);
                return contact;
            },
            param: new { ContactTypeIdentifier = contactTypeIdentifier },
            splitOn: "CTIdentifier");

        return contactList;
    }
}