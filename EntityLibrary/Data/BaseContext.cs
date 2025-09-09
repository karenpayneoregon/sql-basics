using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace EntityLibrary.Data;
public abstract class BaseContext : DbContext
{
    protected BaseContext() : base(BuildOptions()) { }

    private static DbContextOptions BuildOptions()
    {
        var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString());

        var env = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT") ?? "Production";

        if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            builder.EnableSensitiveDataLogging().LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        return builder.Options;
    }
}




