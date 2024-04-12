using SqlServer.Library.Classes;
using SqlServer.Library.Models;
using static DapperGetDatabaseAndTableNamesApp.Classes.SpectreConsoleHelpers;

namespace DapperGetDatabaseAndTableNamesApp.Classes;
internal class Operations
{
    public static void GetForeignKeysForNorthWind2024()
    {
        var catalog = Utilities.Catalog();
        if (DataOperations.CatalogExists(catalog))
        {
            var foreignKeyContainers = 
                DataOperations.ReadForeignKeys(Utilities.ServerName(), catalog);
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
        List<IGrouping<string, DataContainer>> grouped = [];
        AnsiConsole.Progress()
            
            .Start(ctx =>
            {
                var task = ctx.AddTask("[yellow]Reading data[/]");
                
                while (!ctx.IsFinished)
                {

                    grouped = DataOperations.ReadDataContainersGrouped();
                    task.Increment(45);

                }

            });

        //FileOperations.WriteToFile(grouped);
        var root = new Tree("[darkseagreen2_1]Databases[/]");

        foreach (var groupItem in grouped.Where(groupItem => !Exclude.DatabaseNameList.Contains(groupItem.Key)))
        {

            var mainNode = root.AddNode($"[yellow]{groupItem.Key}[/]");
            foreach (var item in groupItem)
            {
                if (Exclude.TableNameList.Contains(item.TableName))
                {
                    continue;
                }

                var childAddNode = mainNode.AddNode($"[dodgerblue1]{item.TableName}[/]");
                
                var columns = DataOperations.ReadColumnDetailsForTable(Utilities.ServerName(), groupItem.Key, item.TableName);
                foreach (var column in columns)
                {
                    childAddNode.AddNode($"{column.ColumnName}");
                    
                }
            }
        }
        Console.Clear();
        AnsiConsole.Write(root);
        Continue();

    }
}
