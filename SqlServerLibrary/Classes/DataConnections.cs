using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace SqlServerLibrary.Classes;
#nullable disable

public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;

    public string MainConnection { get; init; }

    private DataConnections()
    {
        MainConnection = Configurations.GetMainConnectionString();
    }
}

public class Configurations
{
    public static string GetMainConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return config.GetConnectionString("MainConnection");
    }

}


