using System.Data;
using Dapper;
using DbPeekQueryLibrary.LanguageExtensions;
using GlobalConnection.Classes;
using GlobalConnection.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace GlobalConnection;

internal class Program
{
    static void Main(string[] args)
    {
        //Playground1("Roel's");
        //Playground1DataTable(20);
        Playground1Dapper(20);
        //Playground2(8,7);
        //Playground3(8,7);
        //Playground4(8,7);
        Console.ReadLine();
    }

    static void UsingGlobalConnection()
    {
        var cn = SqlServerConnections.Instance.Connection();
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = "SELECT CategoryID, CategoryName FROM dbo.Categories;"
        };

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0),-5}{reader.GetString(1)}");
        }
    }

    static void Recommended()
    {
        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = "SELECT CategoryID, CategoryName FROM dbo.Categories;"
        };

        cn.Open();

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0),-5}{reader.GetString(1)}");
        }
    }

    static void Playground1(string lastName)
    {

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = "SELECT ContactId FROM  dbo.Contacts WHERE  (LastName = @LastName)"
        };

        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
        Console.WriteLine(cmd.ActualCommandText());
        cn.Open();

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Console.WriteLine(reader.GetInt32(0));
        }
        else
        {
            Console.WriteLine("No matches");
        }
    }

    static DataTable Playground1DataTable(int top)
    {

        DataTable dt = new DataTable();

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = SqlStatements.ContactDemo()
        };

        cmd.Parameters.Add("@Top", SqlDbType.NVarChar).Value = top;

        cn.Open();

        dt.Load(cmd.ExecuteReader());
        return dt;
    }

    static void Playground2(int countryIdentifier, int contactTypeIdentifier)
    {

        string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=NorthWind2022;Integrated Security=True";

        using var cn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = """
                SELECT C.CustomerIdentifier,
                       C.CompanyName,
                       C.ContactId,
                       C.Street,
                       C.City,
                       C.PostalCode,
                       C.CountryIdentifier,
                       C.Phone,
                       C.ContactTypeIdentifier,
                       CT.ContactTitle,
                       Cont.FirstName,
                       Cont.LastName
                FROM dbo.Customers AS C
                    INNER JOIN dbo.Countries AS A
                        ON C.CountryIdentifier = A.CountryIdentifier
                    INNER JOIN dbo.ContactType AS CT
                        ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                    INNER JOIN dbo.Contacts AS Cont
                        ON C.ContactId = Cont.ContactId
                           AND CT.ContactTypeIdentifier = Cont.ContactTypeIdentifier
                WHERE (C.CountryIdentifier = @CountryIdentifier)
                      AND (C.ContactTypeIdentifier = @ContactTypeIdentifier);
                """
        };

        cmd.Parameters.Add("@ContactTypeIdentifier", SqlDbType.NVarChar).Value = contactTypeIdentifier;
        cmd.Parameters.Add("@CountryIdentifier", SqlDbType.NVarChar).Value = countryIdentifier;
        Console.WriteLine(cmd.ActualCommandText());
        cn.Open();

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader.GetInt32(0),-4} Company: {reader.GetString(1)}");
            }
            
        }
        else
        {
            Console.WriteLine("No matches");
        }
    }

    static void Playground3(int countryIdentifier, int contactTypeIdentifier)
    {


        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand
        {
            Connection = cn,
            CommandText = SqlStatements.CustomersByContactTypeAndCountry()

        };

        cmd.Parameters.Add("@ContactTypeIdentifier", 
            SqlDbType.NVarChar).Value = contactTypeIdentifier;
        cmd.Parameters.Add("@CountryIdentifier", 
            SqlDbType.NVarChar).Value = countryIdentifier;

        cn.Open();

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader.GetInt32(0),-4} Company: {reader.GetString(1)}");
            }

        }
        else
        {
            Console.WriteLine("No matches");
        }
    }
    static void Playground4(int countryIdentifier, int contactTypeIdentifier)
    {
        using var cn = new SqlConnection(ConnectionString());
  
        var parameters = new
        {
            @CountryIdentifier = countryIdentifier,
            @ContactTypeIdentifier = contactTypeIdentifier
        };

        List<Customer> list = cn.Query<Customer>(
            SqlStatements.CustomersByContactTypeAndCountry(),
            parameters).ToList();

    }

    static List<Contact> Playground1Dapper(int top)
    {

        using SqlConnection cn = new (ConnectionString());
        // specify our parameter for the WHERE condition
        var parameters = new { @Top = top };

        List<Contact> list = cn.Query<Contact>(
            SqlStatements.ContactDemo(), 
            parameters).ToList();


        return list;
    }

}
