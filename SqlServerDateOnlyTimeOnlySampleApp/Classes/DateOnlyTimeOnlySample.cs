using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using SqlServerDateOnlyTimeOnlySampleApp.Data;
// ReSharper disable EntityFramework.UnsupportedServerSideFunctionCall

namespace SqlServerDateOnlyTimeOnlySampleApp.Classes;
public static class DateOnlyTimeOnlySample
{
    public static Task Can_use_DateOnly_TimeOnly_on_SQL_Server()
    {
        PrintSampleName();
        return DateOnlyTimeOnlyTest<BritishSchoolsContext>();
    }

    public static Task Can_use_DateOnly_TimeOnly_on_SQL_Server_with_JSON()
    {
        PrintSampleName();
        return DateOnlyTimeOnlyTest<BritishSchoolsContextJson>();
    }

    public static Task Can_use_DateOnly_TimeOnly_on_SQLite()
    {
        PrintSampleName();
        return DateOnlyTimeOnlyTest<BritishSchoolsContextSqlite>();
    }

    private static async Task DateOnlyTimeOnlyTest<TContext>() where TContext : BritishSchoolsContextBase, new()
    {
        await using var context = new TContext();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        await context.Seed();

        context.LoggingEnabled = true;
        context.ChangeTracker.Clear();

        Console.WriteLine();

        // Issue https://github.com/dotnet/efcore/issues/25103
        if (!context.UseSqlite)
        {
            #region FromDateTime
            var visitedTime = new TimeOnly(12, 0);
            var visited = await context.Schools
                .Where(p => TimeOnly.FromDateTime(p.LastVisited) >= visitedTime)
                .ToListAsync();
            #endregion
        }

        Console.WriteLine();

        // Issue https://github.com/dotnet/efcore/issues/25103
        if (!context.UseSqlite)
        {
            #region FromTimeSpan
            var visitedTime = new TimeOnly(12, 0);
            var visited = await context.Schools
                .Where(p => TimeOnly.FromTimeSpan(p.LegacyTime) >= visitedTime)
                .ToListAsync();
            #endregion
        }

        Console.WriteLine();

        // Issue https://github.com/dotnet/efcore/issues/25103
        if (!context.UseSqlite)
        {
            var visitedAt = DateTime.UtcNow;
            var visitedSchools = await context.Schools
                .AsNoTracking()
                .SelectMany(e => e.OpeningHours)
                .Where(e => e.OpensAt <= TimeOnly.FromDateTime(visitedAt) && e.OpensAt > TimeOnly.FromDateTime(visitedAt))
                .ToListAsync();
        }

        // Issue https://github.com/dotnet/efcore/issues/33937
        // // Issue https://github.com/dotnet/efcore/issues/30223
        // if (!context.UsesJson
        //     && !context.UseSqlite)
        // {
        //     await context.Schools
        //         .SelectMany(e => e.OpeningHours)
        //         .Where(e => e.DayOfWeek == DayOfWeek.Friday)
        //         .ExecuteUpdateAsync(s => s.SetProperty(t => t.OpensAt, t => t.OpensAt!.Value.AddHours(-1)));
        // }

        Console.WriteLine();
    }

    private static void PrintSampleName([CallerMemberName] string methodName = null)
    {
        SpectreConsoleHelpers.PrintCyan(methodName);
        Console.WriteLine();
    }
    
}
