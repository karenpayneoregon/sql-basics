using System.Diagnostics;
using EnumHasConversion.Classes;
using EnumHasConversion.Models;
using Microsoft.EntityFrameworkCore;
using PayneServiceLibrary.Classes.Configuration;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

#pragma warning disable CS8618

namespace EnumHasConversion.Data;

public class WineContext : DbContext
{
    public DbSet<Wine> Wines { get; set; }
    //public DbSet<WineTypes> WineTypes { get; set; }

    /// <summary>
    /// Configures the database context options such as the database provider and logging behavior.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An instance of <see cref="DbContextOptionsBuilder"/> used to configure the database context.
    /// </param>
    /// <remarks>
    /// This method sets up the SQL Server provider using a connection string and enables logging of database operations
    /// with a log level of <see cref="Microsoft.Extensions.Logging.LogLevel.Information"/>.
    /// </remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString())
            .LogTo(message => 
                Debug.WriteLine(message), 
                LogLevel.Information);


    /// <summary>
    /// Configures the model for the database context by defining entity mappings and data conversions.
    /// </summary>
    /// <param name="modelBuilder">
    /// An instance of <see cref="ModelBuilder"/> used to configure the model's schema, relationships, and data.
    /// </param>
    /// <remarks>
    /// This method sets up a conversion for the <see cref="Wine.WineType"/> property to store it as an integer in the database.
    /// Additionally, it seeds initial data for the <see cref="WineTypes"/> and <see cref="Wine"/> entities
    /// if the <see cref="EntitySettings.CreateNew"/> property is set to <c>true</c>.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Setup conversion to and from int/enum
        modelBuilder
            .Entity<Wine>()
            .Property(e => e.WineType)
            .HasConversion<int>();

        if (!EntitySettings.Instance.CreateNew) return;
        modelBuilder.Entity<WineTypes>().HasData(MockedData.WineTypes());
        modelBuilder.Entity<Wine>().HasData(MockedData.Wines());

    }
}