using Microsoft.EntityFrameworkCore;
using SqlServerDateOnlyTimeOnlySampleApp.Models;

namespace SqlServerDateOnlyTimeOnlySampleApp.Data;

public class BritishSchoolsContextSqlite : BritishSchoolsContextBase
{
    public BritishSchoolsContextSqlite() : base(useSqlite: true) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<School>().OwnsMany(
            e => e.OpeningHours, b =>
            {
                b.Property<int>("Id");
                b.HasKey("Id");
            });
    }
}