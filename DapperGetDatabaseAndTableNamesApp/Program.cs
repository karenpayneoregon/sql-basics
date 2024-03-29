using System.Text;
using DapperGetDatabaseAndTableNamesApp.Classes;

namespace DapperGetDatabaseAndTableNamesApp;

internal partial class Program
{
    static void Main()
    {
        var list = DataOperations.ReadDataContainers();

        List<IGrouping<string, DataContainer>> grouped = 
            list.GroupBy(x => x.DatabaseName).ToList();

        List<string> avoidList = new() { "master", "msdb" };
        StringBuilder builder = new();

        foreach (var groupItem in grouped.Where(groupItem => !avoidList.Contains(groupItem.Key)))
        {
            builder.AppendLine(groupItem.Key);
            foreach (var item in groupItem)
            {
                builder.AppendLine($"     {item.SchemaName}.{item.TableName}");
            }
        }
        
        File.WriteAllText("DatabaseAndTableNames.txt", builder.ToString());

    }
}