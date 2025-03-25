using MaskingSocialApp.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using Dapper;

namespace MaskingSocialApp.Classes;

public class DapperOperations
{
    /// <summary>
    /// Retrieves a list of taxpayers from the database while executing as a non-privileged user.
    /// </summary>
    /// <remarks>
    /// This method executes a SQL query using the 'NonPrivilegedUser' context to fetch taxpayer details.
    /// The query retrieves the taxpayer's ID, first name, last name, social security number, and phone number.
    /// </remarks>
    /// <returns>
    /// A list of <see cref="Taxpayer"/> objects.
    /// </returns>
    public static async Task<List<Taxpayer>> GetTaxpayersAsNonPrivilegedUserAsync()
    {
        const string sql = """
                            EXECUTE AS USER = 'NonPrivilegedUser';
                           
                            SELECT t.Id,
                                   t.FirstName,
                                   t.LastName,
                                   t.SocialSecurityNumber,
                                   t.PhoneNumber
                              FROM dbo.Taxpayer AS t;
                           
                            REVERT;
                           """;

        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<Taxpayer>(sql)).ToList();

    }

    public static async Task<List<Taxpayer>> GetTaxpayersAsync()
    {
        const string sql = """
                            SELECT t.Id,
                                   t.FirstName,
                                   t.LastName,
                                   t.SocialSecurityNumber,
                                   t.PhoneNumber
                              FROM dbo.Taxpayer AS t;
                           """;

        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<Taxpayer>(sql)).ToList();

    }

}