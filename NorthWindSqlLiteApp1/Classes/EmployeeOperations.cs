using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using Spectre.Console;

using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes;


/// <summary>
/// Provides operations related to managing and displaying employee data.
/// </summary>
/// <remarks>
/// This class includes methods for retrieving detailed information about individual employees,
/// displaying hierarchical relationships between employees and their managers, and other
/// employee-related functionalities. It interacts with the database to fetch and process
/// employee data, leveraging navigation properties and grouping techniques for efficient
/// data organization and presentation.
/// </remarks>
internal class EmployeeOperations
{

    /// <summary>
    /// Retrieves and displays detailed information about a single employee based on the specified employee ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the employee to retrieve. Defaults to <c>1</c> if no value is provided.
    /// </param>
    /// <remarks>
    /// This method queries the database for an employee with the specified ID, including related navigation properties
    /// such as contact type, country, and manager information. If the employee is found, their details are displayed
    /// in a formatted table using NuGet package <a href="https://www.nuget.org/packages/Spectre.Console/0.54.0?_src=template#readme-body-tab">Spectre.Console</a>. If no employee is found, a message is displayed indicating this.
    /// </remarks>
    public static void GetSingleEmployee(int id = 5)
    {
        PrintPink();

        using var context = new Context();

        var employee = context.Employees.Include(employees => employees.ContactTypeIdentifierNavigation)
            .Include(employees => employees.CountryIdentifierNavigation)
            .Include(employees => employees.ReportsToNavigationEmployee)
            .FirstOrDefault(e => e.EmployeeID == id);

        if (employee != null)
        {
            var table = new Table().Border(TableBorder.Rounded)
                .BorderColor(Color.Teal)
                .Title($"[white]{employee.FullName}[/]")
                .AddColumn(new TableColumn("[u]Field[/]"))
                .AddColumn(new TableColumn("[u]Value[/]"));

            table.AddRow("Employee ID", employee.EmployeeID.ToString());
            table.AddRow("Title", employee.ContactTypeIdentifierNavigation?.ContactTitle ?? "N/A");
            table.AddRow("Birth Date", employee.BirthDate?.ToString("yyyy-MM-dd") ?? "N/A");
            table.AddRow("Hire Date", employee.HireDate?.ToString("yyyy-MM-dd") ?? "N/A");
            table.AddRow("Address", employee.Address ?? "N/A");
            table.AddRow("City", employee.City ?? "N/A");
            table.AddRow("Region", employee.Region ?? "N/A");
            table.AddRow("Postal Code", employee.PostalCode ?? "N/A");
            table.AddRow("Country", employee.CountryIdentifierNavigation?.Name ?? "N/A");
            table.AddRow("Home Phone", employee.HomePhone ?? "N/A");

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Employee not found.[/]");
        }
    }
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
                /*
                 * Here a worker can also be a manager, so we check if the worker is also listed as a manager
                 * in our managers list. If they are, we format their name differently to indicate that
                 * they are both a worker and a manager.
                 */
                if (managers.Any(m => m.Employee.EmployeeID == worker.EmployeeID))
                {
                    currentNode.AddNode($"{worker.FullName} [Gray37](Manager)[/]");
                }
                else
                {
                    currentNode.AddNode(worker.FullName);
                }
                
            }
        }

        AnsiConsole.Write(root);

        Console.WriteLine();

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

