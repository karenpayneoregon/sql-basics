using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using Spectre.Console;

using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes;
internal class EmployeeOperations
{
    /// <summary>
    /// Displays a hierarchical view of employees and their respective managers
    /// in a tree structure using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method retrieves employee data from the database, groups employees
    /// by their managers, and organizes the data into a tree structure. The tree
    /// is then displayed in the console with managers as parent nodes and their
    /// respective employees as child nodes.
    /// </remarks>
    public static void ReportsToManager()
    {

        PrintPink();

        using var context = new Context();

        var table = CreateViewTable();
        
        List<Employees> employees = [.. context.Employees];

        List<IGrouping<int?, Employees>> groupedData = employees
            .Where(employee => employee.ReportsTo.HasValue)
            .OrderBy(employee => employee.LastName)
            .GroupBy(employee => employee.ReportsTo)
            .ToList();

        List<Manager> managers = [];

        foreach (var group in groupedData)
        {

            Manager manager = new()
            {
                Employee = employees.Find(employee => employee.EmployeeID == group.Key.Value)
            };

            foreach (Employees groupedItem in group)
            {
                manager.Workers.Add(groupedItem);
            }

            managers.Add(manager);

        }

        managers = managers
            .OrderBy(employee => employee.Employee.LastName)
            .ToList();

        var root = new Tree("[white][B]Employees[/][/]");

        foreach (Manager manager in managers)
        {
            var currentNode = root.AddNode($"[Chartreuse1]{manager.Employee.FullName}[/] [Gray37](Manager)[/]");

            foreach (var worker in manager.Workers)
            {
                currentNode.AddNode(worker.FullName);
            }
        }

        AnsiConsole.Write(root);

    }

    private static Table CreateViewTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Center)
            .Title("~[white on blue][B]Employees[/][/]~")
            .AddColumn(new TableColumn("[u]Manager[/]"))
            .AddColumn(new TableColumn("[u]Workers[/]"));
}

