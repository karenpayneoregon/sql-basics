using Microsoft.Extensions.Configuration;

namespace DapperGetDatabaseAndTableNamesApp1.Classes;
internal class Utilities
{
    /// <summary>
    /// Read sections from appsettings.json
    /// </summary>
    public static IConfigurationRoot ConfigurationRoot() =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

    public static string ServerName() =>
        ConfigurationRoot().GetSection("ApplicationSettings")["Server"]!;
}
