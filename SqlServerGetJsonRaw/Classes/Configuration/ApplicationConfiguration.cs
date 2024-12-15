using Microsoft.Extensions.DependencyInjection;
using SqlServerGetJsonRaw.Models.Configuration;


namespace SqlServerGetJsonRaw.Classes.Configuration;
internal class ApplicationConfiguration
{
    /// <summary>
    /// Configures services for managing connection strings
    /// and determines whether to use mock data.
    /// </summary>
    /// <returns>ServiceCollection</returns>
    public static ServiceCollection ConfigureServices()
    {
        static void ConfigureService(IServiceCollection services)
        {

            services.Configure<ConnectionStrings>(Config.Configuration.JsonRoot()
                .GetSection(nameof(ConnectionStrings)));

            services.Configure<EntityConfiguration>(Config.Configuration.JsonRoot()
                .GetSection(nameof(EntityConfiguration)));

            services.AddTransient<SetupServices>();
        }

        var services = new ServiceCollection();
        ConfigureService(services);

        return services;

    }
}


