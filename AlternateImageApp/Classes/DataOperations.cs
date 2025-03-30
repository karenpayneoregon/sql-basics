using System.Text.RegularExpressions;
using AlternateImageApp.Models;
using ConsoleConfigurationLibrary.Classes;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AlternateImageApp.Classes;

/// <summary>
/// Provides operations for managing and manipulating image alt text data within a database.
/// There is no error handling but feel free to add error handling in.
/// </summary>
/// <remarks>
/// This class includes methods for adding image alt text data to the database and truncating database tables.
/// It utilizes Dapper for database interactions and supports handling image files as byte arrays.
/// </remarks>
public partial class DataOperations
{
    /// <summary>
    /// Adds a range of image alt text data to the database by inserting each item into the specified table.
    /// </summary>
    /// <param name="list">The list of <see cref="ImageAltText"/> objects to be added to the database.</param>
    /// <param name="filePath">The file path where the image files are located.</param>
    /// <remarks>
    /// This method truncates the "Categories" table before inserting the new data. 
    /// Each image file is read as a byte array and stored in the database along with its associated name and alt text.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue executing the SQL commands.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when there is an error reading the image files from the specified <paramref name="filePath"/>.
    /// </exception>
    public static void AddRange(List<ImageAltText> list, string filePath)
    {
            
        TruncateTable("Categories");

        using var db = new SqlConnection(AppConnections.Instance.MainConnection);

        foreach (var item in list)
        {
            var bytes = File.ReadAllBytes(Path.Combine(filePath, item.Src));

            db.Execute(
                """
                INSERT INTO Categories (Name, Ext, AltText, Photo) 
                VALUES (@Name, @Ext, @AltText, @Photo)
                """, 
                new
                {
                    item.Name,
                    Ext = item.Ext,
                    AltText = item.Alt, 
                    Photo = bytes
                });
        }
    }
    /// <summary>
    /// Writes image data retrieved from the database to the file system.
    /// </summary>
    /// <remarks>
    /// This method queries the database for image data, including file names and photo content, 
    /// and writes the photo content as files to a directory named "Result". 
    /// If the directory does not exist, it is created.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue connecting to or querying the database.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when there is an issue creating the directory or writing files to the file system.
    /// </exception>
    public static void Write()
    {
        using var db = new SqlConnection(AppConnections.Instance.MainConnection);
        var results = db.Query<ImageAltText>(
            """
            SELECT Id, Name as Src, Ext, AltText as Alt, Photo 
            FROM dbo.Categories
            """).ToList();

        if (!Directory.Exists("Result"))
        {
            Directory.CreateDirectory("Result");
        }
            
        foreach (var item in results)
        {
            File.WriteAllBytes(Path.Combine("Result", item.FileName), item.Photo);
        }

    }

    /// <summary>
    /// Truncates the specified table in the database and resets its identity seed value to 1.
    /// </summary>
    /// <param name="tableName">The name of the table to truncate.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="tableName"/> is null, empty, consists only of white-space characters,
    /// or is not a valid SQL identifier.
    /// </exception>
    public static void TruncateTable(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName) || !IsValidSqlIdentifier(tableName))
            throw new ArgumentException("Invalid table name.", nameof(tableName));

        var sql = 
            $"""
             TRUNCATE TABLE dbo.{tableName};
             DBCC CHECKIDENT ('dbo.{tableName}', RESEED, 1);
             """;

        using var cn = new SqlConnection(AppConnections.Instance.MainConnection);
        cn.Execute(sql);
    }

    /// <summary>
    /// Determines whether the specified string is a valid SQL identifier.
    /// </summary>
    /// <param name="name">The string to validate as a SQL identifier.</param>
    /// <returns>
    /// <see langword="true"/> if the specified string is a valid SQL identifier; otherwise, <see langword="false"/>.
    /// </returns>
    private static bool IsValidSqlIdentifier(string name)
    {
        return SqlIdentifierRegex().IsMatch(name);
    }

    [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex SqlIdentifierRegex();
}