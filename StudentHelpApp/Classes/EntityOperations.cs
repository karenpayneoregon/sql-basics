using Microsoft.EntityFrameworkCore;
using StudentHelpApp.Data;
using StudentHelpApp.Models;

namespace StudentHelpApp.Classes;
internal class EntityOperations
{
    public static List<Item> GetAll()
    {
        using var context = new Context();
        return context.Item.ToList();
    }
    public static async Task<bool> Add(Item item)
    {
        await using var context = new Context();
        context.Add(item);
        return await context.SaveChangesAsync() == 1;
    }
    public static Item Find(int id)
    {
        using var context = new Context();
        return context.Item.FirstOrDefault(x => x.Id == id);
    }
    /// <summary>
    /// Update an existing record.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static async Task<bool> Update(Item item)
    {
        await using var context = new Context();
        context.Attach(item).State = EntityState.Modified;
        return await context.SaveChangesAsync() == 1;
    }

    /// <summary>
    /// Simple example to show how to inspect in development mode original and changed property values
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static async Task LongView(Item item)
    {
        await using var context = new Context();
        var current = context.Item.FirstOrDefault(x => x.Id == item.Id);
        if (current is not null)
        {
            current.Name = "Well now";
            await File.WriteAllTextAsync("LongView.txt", context.ChangeTracker.DebugView.LongView);
        }
    }
    public static async Task<bool> RemoveAsync(Item item)
    {
        await using var context = new Context();
        context.Remove(item);
        return await context.SaveChangesAsync() == 1;
    }
    public static void Reset()
    {
        using var context = new Context();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    /// <summary>
    /// Check the SQL out, EF Core does a MERGE/INSERT in the log file
    /// </summary>
    public static void Populate()
    {
        using var context = new Context();
        context.AddRange(BogusOperations.Items());
        context.SaveChanges();
    }
}
