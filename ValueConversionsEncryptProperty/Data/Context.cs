using EntityCoreFileLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ValueConversionsEncryptProperty.Models;

using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ValueConversionsEncryptProperty.Data;

public class Context : DbContext
{

    /// <summary>
    /// Configures the model for the database context by defining entity mappings and property conversions.
    /// </summary>
    /// <param name="modelBuilder">
    /// The <see cref="ModelBuilder"/> used to configure the model for the context.
    /// </param>
    /// <remarks>
    /// This method sets up a value conversion for the <see cref="User.Password"/> property, 
    /// ensuring that passwords are hashed when stored in the database.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
            v => BC.HashPassword(v),
            v => v);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(new DbContextToFileLogger().Log,
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .UseSqlServer(ConnectionString())
            .EnableSensitiveDataLogging();
}