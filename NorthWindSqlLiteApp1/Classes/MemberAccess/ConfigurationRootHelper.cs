using ConsoleConfigurationLibrary.Models;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace NorthWindSqlLiteApp1.Classes.MemberAccess;
internal static class ConfigurationRootHelper
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

    public static string DataConnection() =>
        ConfigurationRoot().GetSection("ConnectionStrings")["MainConnection"] ?? "Unknown";

    public static string DataConnection1() =>
        ConfigurationRoot().GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>()
            ?.MainConnection;
}
