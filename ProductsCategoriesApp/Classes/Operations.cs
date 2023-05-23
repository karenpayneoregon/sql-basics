using System.Diagnostics;
using Dapper;
using Dommel;
using Microsoft.Data.SqlClient;
using ProductsCategoriesApp.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;


namespace ProductsCategoriesApp.Classes;
public class Operations
{
    public static async Task ProductsWithCategories()
    {
        await using SqlConnection cn = new(ConnectionString());

        //var product = await product.GetAsync<Products, Categories, Products>(1, (order, line) =>
        //{
        //    return order;
        //});
        //var products = await cn.GetAllAsync<Products>();

        //var products = cn.GetAll<Products>();
        //var product = cn.Get<Products, Categories, Products>(products.First().ProductID);
        var product = await cn.GetAllAsync<Products, Categories, Products>();
    }

    public static async Task<List<Products>> ProductsWithCategoriesAndSuppliers()
    {
        await using SqlConnection cn = new(ConnectionString());
        var product = await cn.GetAllAsync<Products, Categories, Suppliers, Products>();
        return product.ToList();
    }

    // works
    public static async Task<List<Customers>> CustomersWithContacts()
    {
        await using SqlConnection cn = new(ConnectionString());
        
        var customers = await cn.GetAllAsync<Customers, Contacts, Countries, Customers>();
        return customers.ToList();

    }

    // works
    public static async Task<List<Customers>> CustomersWithContacts1()
    {
        await using SqlConnection cn = new(ConnectionString());

        var list = cn.Query<Customers, Contacts,  Countries, Customers>(
            SqlStatements.CustomerWithContacts1(), (customers,contacts,  country) =>
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
            SqlStatements.CustomerWithContacts2(), (customers, contacts, contactType, country) =>
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
            SqlStatements.Contacts(), (contact, contactType) =>
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
            SqlStatements.ContactsWithDevices(), (contact, contactType, contactDevices) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;

                contact.ContactDevices.Add(contactDevices); 
                return contact;
            }, splitOn: "ContactTypeIdentifier,ContactId");


        return list.ToList();
    }

    public static async Task<List<Contacts>> GetContactsWithDevicesPhoneTypeIdentifierNavigation()
    {

        await using SqlConnection cn = new(ConnectionString());

        var parameters = new { @PhoneTypeIdenitfier = 3 };
        var list = cn.Query<Contacts, ContactType, ContactDevices, PhoneType, Contacts>(
            SqlStatements.ContactsWithDevicesAndPhoneType(),  (contact, contactType, contactDevices, phoneType) =>
            {
                contact.ContactTypeIdentifierNavigation = contactType;
                contact.ContactTypeIdentifier = contactType.ContactTypeIdentifier;


                if (phoneType is not null)
                {
                    contact.ContactDevices.Add(new ContactDevices());
                    var device = contact.ContactDevices.FirstOrDefault();
                    device!.PhoneTypeIdentifierNavigation = phoneType;
                    device.PhoneNumber = contactDevices.PhoneNumber;
                    device.PhoneTypeIdentifier = phoneType.PhoneTypeIdenitfier;
                    device.ContactId = contact.ContactId;
                    device.Contact = contact;
                }


                contact.ContactDevices.Add(contactDevices);
                return contact;
            },parameters, splitOn: "ContactTypeIdentifier,ContactId,PhoneTypeIdenitfier");


        return list.ToList();
    }
}
