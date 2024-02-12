using GitHubSamples.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

#pragma warning disable CS8619

namespace GitHubSamples.Classes;

public class DataOperations
{

    /// <summary>
    /// Read Categories, ContactType and Countries tables
    /// </summary>
    /// <param name="referenceTables">An instance of <see cref="ReferenceTables"/></param>
    /// <returns>Success of the operation and if an exception, the exception is returned</returns>
    public static async Task<(bool success, Exception exception)> GetReferenceTables(ReferenceTables referenceTables)
    {
        

        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new()
        {
            Connection = cn, CommandText = SqlStatements.ReferenceTableStatements
        };


        try
        {
            await cn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                referenceTables.CategoriesList.Add(new Categories()
                {
                    CategoryId = reader.GetInt32(0), 
                    CategoryName = reader.GetString(1)
                });
            }

            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                referenceTables.ContactTypesList.Add(new ContactType()
                {
                    ContactTypeIdentifier = reader.GetInt32(0), 
                    ContactTitle = reader.GetString(1)
                });
            }
            
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                referenceTables.CountriesList.Add(new Countries()
                {
                    CountryIdentifier = reader.GetInt32(0), 
                    Name = reader.GetString(1)
                });
            }

            return (true, null);
        }
        catch (Exception localException)
        {
            return (false, localException);
        }
    }

    public static async Task GetReferenceTablesDapper(ReferenceTables referenceTables)
    {
        await using SqlConnection cn = new(ConnectionString());

        const string statement = 
            """
            SELECT CategoryID,CategoryName FROM dbo.Categories;
            SELECT ContactTypeIdentifier,ContactTitle FROM dbo.ContactType;
            SELECT CountryIdentifier,[Name] FROM dbo.Countries;
            """;

        SqlMapper.GridReader results = await cn.QueryMultipleAsync(statement);

        referenceTables.CategoriesList = results.Read<Categories>().ToList();
        referenceTables.ContactTypesList = results.Read<ContactType>().ToList();
        referenceTables.CountriesList = results.Read<Countries>().ToList();

    }

    public static async Task GetReferenceTablesDapperStoredProcedure(ReferenceTables referenceTables)
    {
        await using SqlConnection cn = new(ConnectionString());

        SqlMapper.GridReader results = await cn.QueryMultipleAsync("usp_SelectCatCountryContactType", 
            commandType:CommandType.StoredProcedure);

        referenceTables.CategoriesList = results.Read<Categories>().ToList();
        referenceTables.ContactTypesList = results.Read<ContactType>().ToList();
        referenceTables.CountriesList = results.Read<Countries>().ToList();

    }

    /// <summary>
    /// not part of any articles -  left it from a forum question that might be helpful to those learning Dapper
    /// </summary>
    public static async Task<IEnumerable<Categories>> GetCategories()
    {
        await using SqlConnection cn = new(ConnectionString());
        return await cn.QueryAsync<Categories>(SqlStatements.GetCategories);
    }

    public static async Task<(bool success, Exception exception, DataSet dataSet)> GetReferenceTablesDataSet()
    {
        DataSet ds = new();

        try
        {
            SqlDataAdapter adapter = new();
            await using SqlConnection cn = new(ConnectionString());
            SqlCommand command = new(SqlStatements.ReferenceTableStatements, cn);
            adapter.SelectCommand = command;

            adapter.Fill(ds);

            ds.Tables[0].TableName = "Categories";
            ds.Tables[1].TableName = "ContactType";
            ds.Tables[2].TableName = "Countries";

            return (true, null, ds);
        }
        catch (Exception localException)
        {
            return (false, localException, null);
        }
    }

    /// <summary>
    /// Alternate to above method as a developer in a forum post could not get
    /// the above method to compile as most likely they are using a lesser edition of VS2022 
    /// </summary>
    public static (bool success, Exception exception, DataSet dataSet) GetReferenceTablesDataSet1()
    {

        DataSet ds = new();

        try
        {
            SqlDataAdapter adapter = new();

            using var cn = new SqlConnection(ConnectionString());
            SqlCommand command = new(SqlStatements.ReferenceTableStatements1, cn);
            adapter.SelectCommand = command;

            adapter.Fill(ds);

            ds.Tables[0].TableName = "Categories";
            ds.Tables[1].TableName = "ContactType";
            ds.Tables[2].TableName = "Countries";

            return ((true, null, ds));
        }
        catch (Exception localException)
        {
            return ((false, localException, null));
        }
    }
}