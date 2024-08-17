using Microsoft.EntityFrameworkCore;
using SimpleEfCoreApp.Data;
using SimpleEfCoreApp.Models;
using System.Runtime.CompilerServices;
#pragma warning disable CS8604 // Possible null reference argument.

namespace SimpleEfCoreApp.Classes;
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

        context.Database.ExecuteSql(FormattableStringFactory.Create(InsertPeople));
        //context.Database.ExecuteSql(InsertPeopleFormattableString);
        //context.Database.ExecuteSql(InsertPeopleFormattableString);

    }

    private static string InsertPeople => 
        """
        INSERT INTO dbo.Person ([FirstName], [LastName], [BirthDate]) 
        VALUES 
        ( N'Benny', N'Anderson', N'2005-05-27' ), 
        ( N'Teri', N'Schaefer', N'2002-12-19' ), 
        ( N'Clint', N'Mante', N'2005-09-15' ), 
        ( N'Drew', N'Green', N'2002-01-08' ), 
        ( N'Denise', N'Schaden', N'2001-01-08' )
        """;
    private static FormattableString InsertPeopleFormattableString =>
        $"""
         INSERT INTO dbo.Person ([FirstName], [LastName], [BirthDate]) 
         VALUES 
         ( N'Benny', N'Anderson', N'2005-05-27' ), 
         ( N'Teri', N'Schaefer', N'2002-12-19' ), 
         ( N'Clint', N'Mante', N'2005-09-15' ), 
         ( N'Drew', N'Green', N'2002-01-08' ), 
         ( N'Denise', N'Schaden', N'2001-01-08' )
         """;
}