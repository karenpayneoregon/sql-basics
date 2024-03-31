using SqlServer.Library.Classes;
using SqlServer.Library.Models;
using static DapperGetDatabaseAndTableNamesApp.Classes.SpectreConsoleHelpers;

namespace DapperGetDatabaseAndTableNamesApp.Classes;
internal class Operations
{
    public static void GetForeignKeysForNorthWind2024()
    {
        var catalog = "NorthWind2024";
        if (DataOperations.CatalogExists(catalog))
        {
            var foreignKeyContainers = DataOperations.ReadForeignKeys(Utilities.ServerName(), catalog);
            foreach (var item in foreignKeyContainers)
            {
                Console.WriteLine($"{item.TableName}");
                Console.WriteLine($"   {item.ColumnName,-20}{item.ForeignTableName,-20}{item.UpdateRule,-10} {item.DeleteRule}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to find {Utilities.ServerName()}\\{catalog}");
        }

        Continue();

    }

    public static void IterateDatabases()
    {
        List<IGrouping<string, DataContainer>> grouped = DataOperations.ReadDataContainersGrouped();

        FileOperations.WriteToFile(grouped);

        foreach (var groupItem in grouped.Where(groupItem => !Exclude.DatabaseNameList.Contains(groupItem.Key)))
        {
            AnsiConsole.MarkupLine($"[hotpink]{groupItem.Key}[/]");
            foreach (var item in groupItem)
            {
                if (Exclude.TableNameList.Contains(item.TableName))
                {
                    continue;
                }
                AnsiConsole.MarkupLine($"     [springgreen1]{item.SchemaName}.{item.TableName}[/]");
                var columns = DataOperations.ReadColumnDetailsForTable(Utilities.ServerName(), groupItem.Key, item.TableName);
                foreach (var column in columns)
                {
                    Console.WriteLine($"          {column.Position} {column.ColumnName}");
                }
            }
        }

        Continue();

    }
}
