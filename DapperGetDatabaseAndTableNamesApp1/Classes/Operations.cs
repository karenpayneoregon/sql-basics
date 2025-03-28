using System.Text;
using SqlServer.Library.Classes;
using SqlServer.Library.Models;

namespace DapperGetDatabaseAndTableNamesApp1.Classes;
internal class Operations
{
    public static string GetForeignKeys(string catalog)
    {
        StringBuilder builder = new();

        if (DataOperations.CatalogExists(catalog))
        {
            var foreignKeyContainers = DataOperations.ReadForeignKeys(Utilities.ServerName(), catalog);
            foreach (var item in foreignKeyContainers)
            {
                builder.AppendLine($"{item.TableName}");
                builder.AppendLine($"   {item.ColumnName,-20}{item.ForeignTableName,-20}{item.UpdateRule,-10} {item.DeleteRule}");
            }
        }
        else
        {
            builder.AppendLine($"Failed to find {Utilities.ServerName()}\\{catalog}");
        }

        return builder.ToString();

    }

    /// <summary>
    /// Asynchronously iterates through databases and their tables, generating a formatted string representation.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a formatted string that lists
    /// database names, their tables, and the columns of each table, excluding specified databases and tables.
    /// </returns>
    /// <remarks>
    /// This method retrieves grouped database and table information using <see cref="DataOperations.ReadDataContainersGroupedAsync"/>.
    /// It writes the grouped data to a file using <see cref="FileOperations.WriteToFileAsync"/> and excludes databases
    /// and tables specified in <see cref="Exclude.DatabaseNameList"/> and <see cref="Exclude.TableNameList"/>.
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if an error occurs during the asynchronous operations or data processing.
    /// </exception>
    public static async Task<string> IterateDatabases()
    {
        StringBuilder builder = new();
        List<IGrouping<string, DataContainer>> grouped = await DataOperations.ReadDataContainersGroupedAsync();

        await FileOperations.WriteToFileAsync(grouped);

        var serverName = Utilities.ServerName();
        var excludeDatabaseNames = Exclude.DatabaseNameList;
        var excludeTableNames = Exclude.TableNameList;

        foreach (var groupItem in grouped)
        {
            if (excludeDatabaseNames.Contains(groupItem.Key))
            {
                continue;
            }

            builder.AppendLine($"{groupItem.Key}");
            foreach (var item in groupItem)
            {
                if (excludeTableNames.Contains(item.TableName))
                {
                    continue;
                }

                builder.AppendLine($"   {item.SchemaName}.{item.TableName}");
                var columns = await DataOperations.ReadColumnDetailsForTableAsync(serverName, groupItem.Key, item.TableName);
                foreach (var column in columns)
                {
                    builder.AppendLine($"     {column.IsPrimaryKey.Primary(),-5}{column.Position,-5}{column.ColumnName,-30}{column.DataTypeFull,-15}{column.IsComputed.ToYesNo()}");
                }
            }
        }

        return builder.ToString();
    }
}
