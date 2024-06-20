using System.Runtime.CompilerServices;
using Serilog;

namespace DapperPersonRepository.Classes;

#pragma warning disable CS8602
public class SetupLogging
{
    [ModuleInitializer]
    public static void Init()
    {
        Initialize();
    }
    public static void Initialize()
    {

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}