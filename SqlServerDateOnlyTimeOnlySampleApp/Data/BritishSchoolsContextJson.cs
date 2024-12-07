using Microsoft.EntityFrameworkCore;
using SqlServerDateOnlyTimeOnlySampleApp.Models;

namespace SqlServerDateOnlyTimeOnlySampleApp.Data;

public class BritishSchoolsContextJson : BritishSchoolsContextBase
{

    public override bool UsesJson => true;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<School>().OwnsMany(e => e.OpeningHours).ToJson();
    }
}