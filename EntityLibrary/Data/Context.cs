using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
#nullable disable

namespace EntityLibrary.Data;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    public DbSet<CurrentWeek> CurrentWeeks { get; set; }
    public virtual DbSet<Birthdays> Birthdays { get; set; }

    public virtual DbSet<Calendar> Calendar { get; set; }

    public virtual DbSet<DateFormats> DateFormats { get; set; }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<Movies> Movies { get; set; }

    public virtual DbSet<People> People { get; set; }

    public virtual DbSet<Person1> Person1 { get; set; }

    public virtual DbSet<Room> Room { get; set; }

    public virtual DbSet<Sales> Sales { get; set; }

    public virtual DbSet<TimeTable> TimeTable { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString()).EnableSensitiveDataLogging()
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.ApplyConfiguration(new Configurations.BirthdaysConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CalendarConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.DateFormatsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EventsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Person1Configuration());
            modelBuilder.ApplyConfiguration(new Configurations.RoomConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SalesConfiguration());

            modelBuilder.Entity<CurrentWeek>().HasNoKey();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
