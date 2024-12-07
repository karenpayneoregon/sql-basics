
using SqlServerDateOnlyTimeOnlySampleApp.Classes;

namespace SqlServerDateOnlyTimeOnlySampleApp;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();
        await DateOnlyTimeOnlySample.Can_use_DateOnly_TimeOnly_on_SQL_Server_with_JSON();
        ExitPrompt();
    }
}