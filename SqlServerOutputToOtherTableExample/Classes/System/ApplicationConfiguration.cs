using Microsoft.Extensions.DependencyInjection;
using SqlServerOutputToOtherTableExample.Models;
using ConsoleConfigurationLibrary.Classes;
namespace SqlServerOutputToOtherTableExample.Classes.System;

/// <summary>
/// Provides configuration for the application, including setting up services and dependencies.
/// </summary>
/// <remarks>
/// This class is responsible for configuring the application's dependency injection container
/// and registering required services. It ensures that the necessary configurations, such as
/// connection strings, are properly set up for the application to function correctly.
/// </remarks>
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


