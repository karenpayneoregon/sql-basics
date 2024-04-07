using System.Diagnostics;
using DbPeekQueryLibrary.LanguageExtensions;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using EnumWithSqlClient.Models;

namespace EnumWithSqlClient.Classes;
internal class WineOperations
{
    public static List<Wine> AllWines()
    {
        List<Wine> wines = new();
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            ORDER BY Name
            """;

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand(statement, cn);

        cn.Open();

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Wine wine = new()
            {
                WineId = reader.GetInt32(0),
                Name = reader.GetString(1),
                WineType = reader.ToWineType(2)
            };
            wines.Add(wine);
        }

        return wines;
    }

    /// <summary>
    /// Get all wines of a specific types
    /// </summary>
    /// <param name="wineTypes">one or more type types</param>
    /// <returns>List of wines</returns>
    public static List<Wine> WhereIn(WineType[] wineTypes)
    {
        List<Wine> wines = new();
        const string statement =
            """
            SELECT WineId,Name,WineType
            FROM dbo.Wine
            WHERE WineType IN ({0})
            ORDER BY Name
            """;

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand(statement, cn);

        var values = wineTypes.Select(x => x.IntValue()).ToArray();
        cmd.WhereInConfiguration(statement, "px", values);

        if (Debugger.IsAttached)
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine("[green]Command[/]");
            /*
             * This comes from the following NuGet package:
             * https://www.nuget.org/packages/DbPeekQueryLibrary/1.0.1?_src=template
             */
            Console.WriteLine(cmd.ActualCommandText());
            Console.WriteLine();
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine();
        }
        
        cn.Open();

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Wine wine = new()
            {
                WineId = reader.GetInt32(0),
                Name = reader.GetString(1),
                WineType = reader.ToWineType(2)
            };
            wines.Add(wine);
        }

        return wines;
    
    }

    /// <summary>
    /// Get all wines of a specific type
    /// </summary>
    /// <param name="wineType">Type of wine</param>
    /// <returns></returns>
    public static List<Wine> WinesByType(WineType wineType)
    {
        List<Wine> wines = new();
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            WHERE WineType = @WineType
            ORDER BY Name
            """;

        using var cn = new SqlConnection(ConnectionString());

        using var cmd = new SqlCommand(statement, cn);
        cmd.Parameters.AddWithValue("@WineType", wineType.IntValue());

        cn.Open();

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Wine wine = new()
            {
                WineId = reader.GetInt32(0),
                Name = reader.GetString(1),
                WineType = reader.ToWineType(2)
            };
            wines.Add(wine);
        }

        return wines;
    }
}
