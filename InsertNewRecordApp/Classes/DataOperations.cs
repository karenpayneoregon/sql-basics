using System.Data;
using InsertNewRecordApp.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
namespace InsertNewRecordApp.Classes;

/// <summary>
/// All data operations without any exception handling.
/// </summary>
internal class DataOperations
{
    /// <summary>
    /// Add new records and display only the newly added records
    /// </summary>
    /// <remarks>
    /// Many novice developers get this method wrong by
    ///  - Creating the command object in the for statement
    ///  - Creating parameters in the for statement
    ///  - Use AddRange rather than Add for Parameters
    ///
    /// Note:
    /// For BirthDate, we are using <see cref="DateOnly"/> and a extension
    /// method <see cref="SqlClientExtensions.GetDateOnly"/> to get <see cref="Person.BirthDate"/>
    /// </remarks>
    public static List<Person> AddRange()
    {

        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.InsertPeople };

        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
        cmd.Parameters.Add("@BirthDate", SqlDbType.Date);

        var bogusPeople = BogusOperations.People();

        cn.Open();

        for (int index = 0; index < bogusPeople.Count; index++)
        {
            cmd.Parameters["@FirstName"].Value = bogusPeople[index].FirstName;
            cmd.Parameters["@LastName"].Value = bogusPeople[index].LastName;
            cmd.Parameters["@BirthDate"].Value = bogusPeople[index].BirthDate;
            bogusPeople[index].Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        return bogusPeople;
    }
    /// <summary>
    /// Get all records for Person table
    /// </summary>
    public static List<Person> GetAll()
    {
        List<Person> list = new();
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.ReadPeople };

        cn.Open();

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Person person = new()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                BirthDate = reader.GetDateOnly(3)
            };

            list.Add(person);
        }

        return list;
    }

    /// <summary>
    /// Get count for Person table
    /// </summary>
    /// <returns>Count of records</returns>
    public static int PeopleCount()
    {
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.CountOfPeople };

        cn.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}