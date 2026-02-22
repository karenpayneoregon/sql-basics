
using ContactsApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ContactsApplication1.Classes.Configuration;

/// <summary>
/// A hosted service designed to warm up Entity Framework Core (EF Core) by preloading the model and executing a lightweight query.
/// </summary>
/// <remarks>
/// <para>
/// This service ensures that EF Core's query compilation and model initialization are performed during application startup,
/// reducing latency for the first database operation during runtime.
/// </para>
/// <para>
/// On failure exception is written to a log file using Serilog, allowing for troubleshooting without crashing the application.
/// </para>
/// <br/>
/// <b>Potential Issues</b>
/// <list type="bullet">
/// <item>
/// <description><b>Delayed Application Startup:</b> Forcing the model to build and queries to compile during initialization increases the time before the application can accept requests.</description>
/// </item>
/// <item>
/// <description><b>Database Connection Failures:</b> If the database is unreachable at <br/> startup (e.g., during a simultaneous container restart),<br/> the warm-up query will fail and could crash the entire application process.</description>
/// </item>
/// </list>
/// </remarks>
public class EntityCoreWarmupService(IServiceProvider serviceProvider) : IHostedService
{
    /// <summary>
    /// Starts the EF Core warmup process by preloading the model and executing a lightweight query.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <remarks>
    /// This method creates a scoped service provider to access the EF Core <see cref="Context"/>.
    /// It preloads the EF Core model and executes a simple query to ensure query compilation and model initialization
    /// are completed during application startup, improving runtime performance.
    /// </remarks>
    public async Task StartAsync(CancellationToken cancellationToken)
    {

        if (AppConfiguration.Instance.UseTimeout)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(AppConfiguration.Instance.Timeout));
            cancellationToken = cts.Token;
        }
        
        try
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                _ = context.Model;

                // Execute a simple, lightweight query to warm up query compilation
                await context.Countries.Take(1).AnyAsync(cancellationToken);
            }

            await Task.CompletedTask;
        }
        catch (Exception exception)
        {
            Log.Error(exception, "EF Core warmup failed");
        }

    }

    /// <summary>
    /// Stops the EF Core warmup service.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation of stopping the service.
    /// </returns>
    /// <remarks>
    /// This method is invoked during the shutdown of the application to perform any necessary cleanup.
    /// Since the warmup service does not maintain any ongoing operations, this method completes immediately.
    /// </remarks>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}