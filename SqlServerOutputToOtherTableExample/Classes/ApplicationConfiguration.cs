using Microsoft.Extensions.DependencyInjection;
using SqlServerOutputToOtherTableExample.Models;
using ConsoleConfigurationLibrary.Classes;
namespace SqlServerOutputToOtherTableExample.Classes;

internal class ApplicationConfiguration
{
    public static ServiceCollection ConfigureServices()
    {
        static void ConfigureService(IServiceCollection services)
        {

            services.Configure<ConnectionStrings>(Configuration.JsonRoot()
                .GetSection(nameof(ConnectionStrings)));

            services.AddTransient<SetupServices>();
        }

        var services = new ServiceCollection();
        ConfigureService(services);

        return services;

    }
}


