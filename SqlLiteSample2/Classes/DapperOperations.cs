using System.Data.SQLite;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

using Dapper;
using SqlLiteSample2.Models;
using static SqlLiteSample2.Classes.SpectreConsoleHelpers;

namespace SqlLiteSample2.Classes;
internal class DapperOperations
{
    public static async Task<List<string>> TableNames()
    {
        PrintMethod();

        var cn = new SQLiteConnection(ConnectionString());
        const string sql = 
            """
            SELECT name 
            FROM sqlite_master 
            WHERE type = 'table' AND name <> 'sqlite_sequence'
            """;
        return (await cn.QueryAsync<string>(sql)).AsList();
    }

    public static async Task<List<ContactType>> ContactTypesAsync()
    {
        var cn = new SQLiteConnection(ConnectionString());
        const string sql =
            """
            SELECT ContactTypeIdentifier
                ,ContactTitle
            FROM ContactType
            """;
        return (await cn.QueryAsync<ContactType>(sql)).AsList();
    }

    public static List<ContactType> ContactTypes()
    {
        var cn = new SQLiteConnection(ConnectionString());
        const string sql =
            """
            SELECT ContactTypeIdentifier
                ,ContactTitle
            FROM ContactType
            """;
        return ( cn.Query<ContactType>(sql)).AsList();
    }

    public static List<Countries> Countries()
    {
        var cn = new SQLiteConnection(ConnectionString());
        const string sql =
            """
            SELECT [CountryIdentifier]
                ,[Name]
            FROM Countries
            """;
        return (cn.Query<Countries>(sql)).AsList();
    }

    public static List<Contacts> Contacts()
    {
        var cn = new SQLiteConnection(ConnectionString());
        const string sql =
            """
            SELECT [ContactId]
                ,[FirstName]
                ,[LastName]
                ,[ContactTypeIdentifier]
            FROM Contacts
            """;
        return (cn.Query<Contacts>(sql)).AsList();
    }

    public static List<Customers> CustomersJoinedSample()
    {
        var cn = new SQLiteConnection(ConnectionString());

        var list = cn.Query<Customers, Contacts, Countries, Customers>(
            SqlStatements.CustomerJoined(), (customers, contacts, country) =>
            {
                customers.Contact = contacts;
                customers.ContactId = contacts.ContactId;
                customers.CountryIdentifier = country.CountryIdentifier;
                customers.CountryIdentifierNavigation = country;
                return customers;

            }, splitOn: "ContactId,CountryIdentifier");

        return list.ToList();
    }

    public static List<Customers> CustomersJoinedSample1()
    {
        var cn = new SQLiteConnection(ConnectionString());

        var list = cn.Query<Customers, Contacts, ContactType, Countries, Customers>(
            SqlStatements.CustomerJoined(), (customers, contacts, contactTypes, country) =>
            {
                customers.Contact = contacts;
                customers.ContactId = contacts.ContactId;
                customers.ContactTypeIdentifierNavigation = contactTypes;
                customers.CountryIdentifier = country.CountryIdentifier;
                customers.CountryIdentifierNavigation = country;
                return customers;

            }, splitOn: "ContactId,ContactTypeIdentifier,CountryIdentifier");

        return list.ToList();
    }
}
