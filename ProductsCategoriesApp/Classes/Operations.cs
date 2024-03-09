using Dapper;
using Microsoft.Data.SqlClient;
using ProductsCategoriesApp.Models;
using ProductsCategoriesApp.Models.Projections;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ProductsCategoriesApp.Classes;
public class Operations
{

    public static async Task<List<Customers>> CustomersWithContacts1()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Customers, Contacts,  Countries, Customers>(
            SQL.CustomerWithContacts1(), (customers,contacts,  country) =>
        {
            customers.Contact = contacts;
            customers.ContactId = contacts.ContactId;
            customers.CountryIdentifier = country.CountryIdentifier;
            customers.CountryIdentifierNavigation = country;
            return customers;

        }, splitOn: "ContactId,CountryIdentifier");

        return list.ToList();
    }
    public static async Task<List<Customers>> CustomersWithContacts2()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Customers, Contacts, ContactType, Countries, Customers>(
            SQL.CustomerWithContacts2(), (customers, contacts, contactType, country) =>
            {
                customers.Contact = contacts;
                customers.Contact.ContactTypeIdentifierNavigation = contactType;
                customers.ContactId = contacts.ContactId;
                customers.CountryIdentifier = country.CountryIdentifier;
                customers.CountryIdentifierNavigation = country;
                return customers;

            }, splitOn: "ContactId,CountryIdentifier");

        return list.ToList();
    }

    public static async Task<List<Contacts>> GetContacts()
    {

        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Contacts, ContactType, Contacts>(
            SQL.Contacts(), (contact, contactType) =>
        {
            contact.ContactTypeIdentifierNavigation = contactType;
            contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;
            return contact;
        }, splitOn: "ContactTypeIdentifier");


        return list.ToList();
    }
    public static async Task<List<Contacts>> GetContactsWithDevices()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Contacts, ContactType, ContactDevices, Contacts>(
            SQL.ContactsWithDevices(), (contact, contactType, contactDevices) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;

                contact.ContactDevices.Add(contactDevices); 
                return contact;
            }, splitOn: "ContactTypeIdentifier,ContactId");


        return list.ToList();
    }

    // Gets back DeviceId
    public static async Task<List<Contacts>> GetContactsAndDevices()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Contacts, ContactDevices, Contacts>(
            SQL.ContactsWithDevices(), (contact,  contactDevices) =>
            {

                contact.ContactDevices.Add(contactDevices);
                return contact;
            }, splitOn: "ContactId,DeviceId");


        return list.ToList();
    }
    public static async Task<Contacts> GetContactsAndDevicesSingle()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Contacts, ContactDevices, Contacts>(
            SQL.ContactsWithDevices(), (contact, contactDevices) =>
            {

                contact.ContactDevices.Add(contactDevices);
                return contact;
            }, splitOn: "ContactId,DeviceId").FirstOrDefault();


        return list;
    }
    /// <summary>
    /// Get contacts with an office phone
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// The primary key for <see cref="PhoneType"/> has a typo so with that the following was done
    /// - In <see cref="PhoneType"/> we alias PhoneTypeIdenitfier to PhoneTypeIdentifier
    /// - In SqlStatements.ContactsWithDevicesAndPhoneType we also alias PhoneTypeIdenitfier to PhoneTypeIdentifier
    /// </remarks>
    public static async Task<List<Contacts>> GetContactsWithOfficePhone(int phoneIdentifier)
    {

        await using SqlConnection cn = new(ConnectionString());

        // 3 is for office phone
        var parameters = new { @PhoneTypeIdenitfier = phoneIdentifier };

        var list = await cn.QueryAsync<Contacts, ContactType, ContactDevices, PhoneType, Contacts>(
            SQL.ContactsWithDevicesAndPhoneType(),  
            (contact, contactType, contactDevices, phoneType) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;
                contact.ContactDevices.Add(new ContactDevices());

                ContactDevices device = contact.ContactDevices.FirstOrDefault();
                
                device!.PhoneTypeIdentifierNavigation = phoneType;
                device.PhoneNumber = contactDevices.PhoneNumber;
                device.PhoneTypeIdentifier = phoneType.PhoneTypeIdentifier;
                device.ContactId = contact.ContactId;
                device.PhoneTypeIdentifierNavigation = phoneType;

                device.Contact = contact;
                contact.ContactDevices.Add(contactDevices);
                
                return contact;

            },
            parameters, 
            splitOn: 
            """
                ContactTypeIdentifier,
                ContactId,
                PhoneTypeIdentifier
            """);


        return list.ToList();
    }
    public static async Task<List<ContactOffice>> GetContactsForOffice()
    {
        await using SqlConnection cn = new(ConnectionString());

        var parameters = new { @PhoneTypeIdenitfier = (int)DeviceType.Office };
        var list = await cn.QueryAsync<
            ContactOffice, 
            ContactType, ContactDevices, PhoneType, 
            ContactOffice>(
            SQL.ContactsWithDevicesAndPhoneType(),
            (contact, contactType, contactDevices, phoneType) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;
                ContactDevices device = contact.OfficeDevice;
                
                device.PhoneTypeIdentifierNavigation = phoneType;
                device.PhoneNumber = contactDevices.PhoneNumber;
                device.PhoneTypeIdentifier = phoneType.PhoneTypeIdentifier;
                device.ContactId = contact.ContactId;
                device.DeviceId = contact.OfficeDevice.DeviceId;
                device.PhoneTypeIdentifierNavigation = phoneType;

                return contact;
            },
            parameters,
            splitOn:
            """
                DeviceId,
                ContactTypeIdentifier,
                ContactId,
                PhoneTypeIdentifier
            """);
        
        return list.ToList();
    }
}
