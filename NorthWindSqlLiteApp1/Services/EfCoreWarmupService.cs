using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NorthWindSqlLiteApp1.Data;

namespace NorthWindSqlLiteApp1.Services;

/// <summary>
/// Represents a hosted service designed to warm up Entity Framework Core by initializing the model
/// and executing a lightweight query. This ensures that query compilation and model initialization
/// are performed during application startup, improving runtime performance.
/// </summary>
/// <remarks>
/// This actually doesn't help much in this specific application, but in larger applications with complex models and queries,
/// it can improve startup performance by pre-compiling queries and initializing the model early.
/// </remarks>
public class EfCoreWarmupService(IServiceProvider serviceProvider) : IHostedService
{
    /// <summary>
    /// Starts the hosted service to warm up Entity Framework Core by initializing the model
    /// and executing a lightweight query. This ensures that query compilation and model initialization
    /// are performed during application startup, improving runtime performance.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the warm-up process.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// </returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            _ = context.Model;

            // Execute a simple, lightweight query to warm up query compilation
            await context.Categories.Take(1).AnyAsync(cancellationToken); 
        }
        
        await Task.CompletedTask;
        
    }

    /// <summary>
    /// Stops the hosted service gracefully.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to signal the stop operation should be aborted.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of stopping the service.
    /// </returns>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}