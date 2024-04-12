using Microsoft.Extensions.Configuration;

namespace DapperGetDatabaseAndTableNamesApp.Classes;
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
        ConfigurationRoot().GetSection("ApplicationSettings")["Server"];
    public static string Catalog() =>
        ConfigurationRoot().GetSection("ApplicationSettings")["Catalog"];
}
