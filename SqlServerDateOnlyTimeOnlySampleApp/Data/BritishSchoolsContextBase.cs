using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqlServerDateOnlyTimeOnlySampleApp.Classes;
using SqlServerDateOnlyTimeOnlySampleApp.Models;
using static SqlServerDateOnlyTimeOnlySampleApp.Classes.DateOnlyTimeOnlySample;

namespace SqlServerDateOnlyTimeOnlySampleApp.Data;
public abstract class BritishSchoolsContextBase : DbContext
{
    protected BritishSchoolsContextBase(bool useSqlite = false)
    {
        UseSqlite = useSqlite;
    }

    public bool UseSqlite { get; }
    public virtual bool UsesJson => false;
    public bool LoggingEnabled { get; set; }

    public DbSet<School> Schools => Set<School>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => (optionsBuilder.UseSqlServer(
                    @$"Server=(localdb)\mssqllocaldb;Database={GetType().Name};ConnectRetryCount=0",
                    sqlServerOptionsBuilder => sqlServerOptionsBuilder.UseNetTopologySuite()))
            .EnableSensitiveDataLogging()
            .LogTo(
                s =>
                {
                    if (LoggingEnabled)
                    {
                        Print(s);
                    }
                }, LogLevel.Information);

    /// <summary>
    /// Outputs the specified string to the console with formatting applied from LogTo above.
    /// </summary>
    /// <param name="x">The string to be printed, which will be formatted using Spectre.Console markup.</param>
    public void Print(string x)
    {
        AnsiConsole.MarkupLine(x.ConsoleEscape().HighlightKeyWords());

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
    }

    public async Task Seed()
    {
        AddRange(MockedData.Schools());

        await SaveChangesAsync();
    }
}