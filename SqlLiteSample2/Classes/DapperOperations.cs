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


}
