using Serilog;
using ValueConversionsEncryptProperty.Classes;
using ValueConversionsEncryptProperty.Data;
using ValueConversionsEncryptProperty.Models;

namespace ValueConversionsEncryptProperty;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        await using (var context = new Context())
        {
            AnsiConsole.MarkupLine("[cyan]Creating database[/]");
            SetupDatabase.CleanDatabase(context);
            AnsiConsole.MarkupLine("[cyan]Database created...[/]");

            AnsiConsole.MarkupLine("[cyan]Save a new entity...[/]");

            context.Add(new User { Name = "Karen", Password = "password" });
            await context.SaveChangesAsync();
        }

        await using (Context context = new())
        {
            AnsiConsole.MarkupLine("[cyan]Read the entity back[/]");

            var user = context.Set<User>().Single();

            AnsiConsole.MarkupLine($"User [cyan]{user.Name}[/] has password [cyan]'{user.Password}'[/]");
            
            var verified = BC.Verify("password", user.Password);
            AnsiConsole.MarkupLine(verified ? 
                "[green]Password is correct[/]" : 
                "[red]Password is incorrect[/]");

            verified = BC.Verify("password1", user.Password);
            Log.Information("User {@user} password from database {@password}", user.Name, user.Password);
            AnsiConsole.MarkupLine(verified ? "[green]Password is correct[/]" : "[red]Password is incorrect[/]");

        }

        Console.ReadLine();
    }
}