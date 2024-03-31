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

    public static async Task<string> IterateDatabases()
    {
        StringBuilder builder = new();
        List<IGrouping<string, DataContainer>> grouped = await DataOperations.ReadDataContainersGroupedAsync();

        await FileOperations.WriteToFileAsync(grouped);

        foreach (var groupItem in grouped.Where(groupItem => !Exclude.DatabaseNameList.Contains(groupItem.Key)))
        {
            builder.AppendLine($"{groupItem.Key}");
            foreach (var item in groupItem)
            {
                if (Exclude.TableNameList.Contains(item.TableName))
                {
                    continue;
                }
                builder.AppendLine($"     {item.SchemaName}.{item.TableName}");
                var columns = await DataOperations.ReadColumnDetailsForTableAsync(Utilities.ServerName(), groupItem.Key, item.TableName);
                foreach (var column in columns)
                {
                    builder.AppendLine($"          {column.Position} {column.ColumnName}");
                }
            }
        }

        return builder.ToString();

    }
}
