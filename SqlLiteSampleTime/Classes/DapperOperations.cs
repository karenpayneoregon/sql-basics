

using System.Data.SQLite;
using Dapper;
using SqlLiteSampleTime.Models;

namespace SqlLiteSampleTime.Classes;
internal class DapperOperations
{
    private static string ConnectionString()
        => "Data Source=ForumExamplesSmall.db";

    public static void Read()
    {
        var cn = new SQLiteConnection(ConnectionString());

        List<TimeTable> data = cn.Query<TimeTable>(
                """
                SELECT id,
                     FirstName,
                     LastName,
                     StartTime
                FROM TimeTable;
                """)
            .AsList();


        var result = ObjectDumper.Dump(data).Replace("{TimeOnly}", "{[hotpink]TimeOnly[/]}");

        AnsiConsole.MarkupLine(result);

    }
}
