using SqlServer.Library.Models;
using System.Text;

namespace SqlServer.Library.Classes;

/// <summary>
/// Provides functionality for file operations related to database and table information.
/// </summary>
/// <remarks>
/// This class includes methods to write database and table names to files, either synchronously or asynchronously.
/// It excludes databases and tables specified in <see cref="Exclude.DatabaseNameList"/> and <see cref="Exclude.TableNameList"/>.
/// </remarks>
public class FileOperations
{
    /// <summary>
    /// Writes the names of tables grouped by database to a specified file.
    /// </summary>
    /// <param name="grouped">
    /// A list of groupings where each group represents a database and contains its associated tables.
    /// </param>
    /// <param name="fileName">
    /// The name of the file to which the table names will be written. Defaults to "DatabaseAndTableNames.txt".
    /// </param>
    /// <remarks>
    /// Excludes databases and tables specified in <see cref="Exclude.DatabaseNameList"/> and <see cref="Exclude.TableNameList"/>.
    /// </remarks>
    public static void WriteToFile(List<IGrouping<string, DataContainer>> grouped, string fileName = "DatabaseAndTableNames.txt")
    {

        StringBuilder builder = new();

        foreach (var groupItem in grouped.Where(groupItem => !Exclude.DatabaseNameList.Contains(groupItem.Key)))
        {
            builder.AppendLine(groupItem.Key);
            foreach (var item in groupItem)
            {
                if (Exclude.TableNameList.Contains(item.TableName)) continue;
                builder.AppendLine($"     {item.SchemaName}.{item.TableName}");
            }
        }

        File.WriteAllText(fileName, builder.ToString());

    }

    /// <summary>
    /// Asynchronously writes the names of tables grouped by database to a specified file.
    /// </summary>
    /// <param name="grouped">
    /// A list of groupings where each group represents a database and contains its associated tables.
    /// </param>
    /// <param name="fileName">
    /// The name of the file to which the table names will be written. Defaults to "DatabaseAndTableNames.txt".
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous write operation.
    /// </returns>
    /// <remarks>
    /// Excludes databases and tables specified in <see cref="Exclude.DatabaseNameList"/> and <see cref="Exclude.TableNameList"/>.
    /// </remarks>
    public static async Task WriteToFileAsync(List<IGrouping<string, DataContainer>> grouped, string fileName = "DatabaseAndTableNames.txt")
    {

        StringBuilder builder = new();

        foreach (var groupItem in grouped.Where(groupItem => !Exclude.DatabaseNameList.Contains(groupItem.Key)))
        {
            builder.AppendLine(groupItem.Key);
            foreach (var item in groupItem)
            {
                if (Exclude.TableNameList.Contains(item.TableName))
                {
                    continue;
                }
                builder.AppendLine($"     {item.SchemaName}.{item.TableName}");
            }
        }


        await File.WriteAllTextAsync(fileName, builder.ToString());
    }
}
