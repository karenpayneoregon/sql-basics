using SqlServer.Library.Models;
using System.Text;

namespace SqlServer.Library.Classes;
public class FileOperations
{
    /// <summary>
    /// Write table names to a file for an instance of SQL-Server
    /// </summary>
    /// <param name="grouped"></param>
    /// <param name="fileName"></param>
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
    /// Write table names to a file for an instance of SQL-Server
    /// </summary>
    /// <param name="grouped"></param>
    /// <param name="fileName"></param>
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
