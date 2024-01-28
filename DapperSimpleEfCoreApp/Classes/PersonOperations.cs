using DapperSimpleEfCoreApp.Data;
using DapperSimpleEfCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperSimpleEfCoreApp.Classes;
internal class PersonOperations
{
    public static List<Person> GetAll()
    {
        using var context = new Context();
        return context.Person.ToList();
    }

    public static Person? Get(int id)
    {
        using var context = new Context();
        return context.Person.FirstOrDefault(x => x.Id == id);
    }
    public static bool Update(Person person)
    {
        using var context = new Context();
        context.Update(person).State = EntityState.Modified;
        return context.SaveChanges() == 1;
    }

    public static bool Add(Person person)
    {
        using var context = new Context();
        context.Add(person).State = EntityState.Added;
        return context.SaveChanges() == 1;
    }
    public static bool Delete(int id)
    {
        using var context = new Context();
        var person = context.Person.FirstOrDefault(x => x.Id == id);
        context.Person.Remove(person);
        return context.SaveChanges() == 1;
    }
    public static void ResetData()
    {
        using var context = new Context();
        context.Database.ExecuteSql($"DELETE FROM dbo.Person");
        context.Database.ExecuteSql($"DBCC CHECKIDENT ({nameof(Person)}, RESEED, 0)");
        context.Database.ExecuteSql($"INSERT INTO dbo.Person ([FirstName], [LastName], [BirthDate]) VALUES ( N'Benny', N'Anderson', N'2005-05-27' ), ( N'Teri', N'Schaefer', N'2002-12-19' ), ( N'Clint', N'Mante', N'2005-09-15' ), ( N'Drew', N'Green', N'2002-01-08' ), ( N'Denise', N'Schaden', N'2001-01-08' )");
    }
}
