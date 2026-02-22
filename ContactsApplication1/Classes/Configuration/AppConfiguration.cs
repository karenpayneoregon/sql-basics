

using ContactsApplication1.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace ContactsApplication1.Classes.Configuration;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance = new(() => new AppConfiguration());

    public static AppConfiguration Instance => _instance.Value;

    /// <summary>
    /// Indicating whether the application should perform a warmup operation for <see cref="Data.Context"/>.
    /// </summary>
    public bool Use { get; }

    /// <summary>
    /// Gets the timeout duration, in milliseconds, used for cancellation token operations for <see cref="EntityCoreWarmupService"/>..
    /// </summary>
    /// <value>
    /// An integer representing the timeout duration in milliseconds.
    /// </value>
    /// <remarks>
    /// This property retrieves its value from the <c>Timeout</c> field in the 
    /// <c>UseCancellationTokenTimed</c> section of the application configuration.
    /// </remarks>
    public int Timeout { get; }

    /// <summary>
    /// Gets or sets a value indicating whether a timeout should be applied when using a cancellation token.
    /// </summary>
    /// <remarks>
    /// This property is configured based on the <c>UseTimeout</c> setting in the <c>WarmupContext</c> section
    /// of the <c>appsettings.json</c> file. When set to <c>true</c>, the application will use a timeout
    /// for operations that support cancellation tokens.
    /// </remarks>
    public bool UseTimeout { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It reads configuration settings
    /// from the <c>appsettings.json</c> file and initializes the <see cref="Use"/>, <see cref="Timeout"/>, 
    /// and <see cref="UseTimeout"/> properties based on the <see cref="WarmupContext"/> section.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the configuration section <see cref="WarmupContext.SectionName"/> is missing or invalid.
    /// </exception>
    private AppConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = configuration
                           .GetSection(WarmupContext.SectionName)
                           .Get<WarmupContext>();

        Use = settings!.Use;
        Timeout = settings.Timeout;
        UseTimeout = settings.UseTimeout;
    }
}

