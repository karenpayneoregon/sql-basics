using ConsoleConfigurationLibrary.Classes;
using NorthWindSqlLiteApp1.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NorthWindSqlLiteApp1.Classes.Configuration;
public sealed class ContextSettings
{
    private static readonly Lazy<ContextSettings> Lazy = new Lazy<ContextSettings>(() => new ContextSettings());

    public static ContextSettings Instance => ContextSettings.Lazy.Value;

    /// <summary>
    /// Gets or sets a value indicating whether the audit interceptor should be enabled.
    /// </summary>
    /// <remarks>
    /// The audit interceptor is used to track and log changes made to the database context.
    /// This property is configured based on the <c>UseAuditInterceptor</c> setting in the application's configuration file.
    /// </remarks>
    public bool UseAuditInterceptor { get; set; }

    /// <summary>
    /// Gets or sets the configuration options related to customers.
    /// </summary>
    /// <remarks>
    /// This property is initialized from the <c>Customers</c> section of the application configuration file.
    /// It provides settings such as whether to use query filters for customer-related operations.
    /// </remarks>
    public CustomerOptions CustomerOptions { get; set; } 

    private ContextSettings()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Config is set up in the project file to bind the configuration
        // to the ContextOptions class, so we can directly retrieve it here.
        ContextOptions options = Config.Configuration.JsonRoot()
            .GetSection(nameof(ContextOptions))
            .Get<ContextOptions>()!;
        
        UseAuditInterceptor = options.UseAuditInterceptor;
        CustomerOptions = options.CustomersOptions;
    }
}
