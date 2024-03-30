using System.Text;
using DapperGetDatabaseAndTableNamesApp.Classes;
using SqlServer.Library.Classes;
using SqlServer.Library.Models;

namespace DapperGetDatabaseAndTableNamesApp;

internal partial class Program
{
    private static void Main()
    {
       
        List<IGrouping<string, DataContainer>> grouped = DataOperations
            .ReadDataContainers()
            .GroupBy(x => x.DatabaseName)
            .ToList();

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

        Console.ReadLine();
    }
}