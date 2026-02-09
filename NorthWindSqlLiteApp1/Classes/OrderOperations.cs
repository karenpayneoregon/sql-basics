using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using Spectre.Console;
using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes;
internal class OrderOperations
{
    /// <summary>
    /// Retrieves and displays details of a single order based on the specified order identifier.
    /// </summary>
    /// <param name="orderId">
    /// The identifier of the order to retrieve. Defaults to <c>10314</c> if not specified.
    /// </param>
    /// <remarks>
    /// This method fetches the order along with its associated details, including products, categories,
    /// and customer information. If the order is found, it displays the order details and a breakdown
    /// of the order items. If the order is not found, no output is displayed.
    ///
    /// In some cases CustomerIdentifierNavigation may be null even when there is a
    /// valid CustomerIdentifier in the Orders table.
    /// 
    /// </remarks>
    public static void GetSingleOrderByIdentifier(int orderId = 10314)
    {
        PrintPink();

        using var context = new Context();
        var order = context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .Include(o => o.CustomerIdentifierNavigation)
            .FirstOrDefault(o => o.OrderID == orderId);

        if (order != null)
        {
            
            var contact = context.Contacts
                .Include(c => c.ContactTypeIdentifierNavigation)
                .Include(c => c.ContactDevices)
                .FirstOrDefault(c => c.ContactId == order.CustomerIdentifier);

            Console.WriteLine($"    Order ID: {order.OrderID}");
            Console.WriteLine($"    Customer: {order.CustomerIdentifierNavigation?.CompanyName}");
            Console.WriteLine($"     Contact: {contact?.FullName ?? "N/A"}");
            Console.WriteLine($"       Title: {contact?.ContactTypeIdentifierNavigation?.ContactTitle ?? "N/A"}");
            Console.WriteLine($"       Phone: {contact?.ContactDevices.FirstOrDefault()?.PhoneNumber ?? "N/A"}");
            Console.WriteLine($"  Order Date: {order.OrderDate:MM/dd/yyyy}");

            // Compute total amount including discounts per line
            var totalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - od.Discount));
            Console.WriteLine($"Total Amount: {totalAmount:C}");

            AnsiConsole.MarkupLine($"{new string('_', 80)}");

            AnsiConsole.MarkupLine($"   [CadetBlue_1]{"Product",-30}{"Qty",-5}{"Unit Price",12}{"Discount",10}{"Line Total",14}[/]");

            foreach (var detail in order.OrderDetails)
            {
                var lineTotal = detail.Quantity * detail.UnitPrice * (1 - detail.Discount);
                AnsiConsole.MarkupLine($"   [CadetBlue_1]{detail.Product?.ProductName,-30}{detail.Quantity,-5}{detail.UnitPrice,12:C}{detail.Discount,10:P0}{lineTotal,14:C}[/]");
            }
        }
        else
        {
            ErrorPill(Justify.Left, $"{orderId} was not found.");
        }
    }

    /// <summary>
    /// Modifies the property values of a specific order in the database w/o saving changes.
    /// </summary>
    /// <param name="orderId">
    /// The identifier of the order to be modified. Defaults to <c>10311</c> if not specified.
    /// </param>
    /// <remarks>
    /// This method retrieves the original and modified versions of the specified order,
    /// including associated employee details, and applies changes to the order's properties.
    ///
    /// - Anonymous types are used but needs to be strongly typed to use as a return type.
    /// - ShipAddress could be in a getter of the property
    /// 
    /// </remarks>
    public static void AlterPropertyValue(int orderId = 10311)
    {
        PrintPink();

        using var context = new Context();

        var original = context.Orders
            .Include(x => x.Employee)
            .Select(o => new
            {
                o.OrderID,
                o.OrderDate,
                o.RequiredDate,
                o.ShippedDate,
                o.ShipAddress,
                o.ShipCity,
                o.ShipPostalCode,
                o.ShipCountry,
                o.Employee.FullName
            })
            .FirstOrDefault(o => o.OrderID == orderId);

        var changed = context.Orders
            .Include(x => x.Employee)
            .Select(o => new
            {
                o.OrderID,
                o.OrderDate,
                o.RequiredDate,
                o.ShippedDate,
                ShipAddress = o.ShipAddress.Replace(",", " "),
                o.ShipCity,
                o.ShipPostalCode,
                o.ShipCountry,
                o.Employee.FullName
            })
            .FirstOrDefault(o => o.OrderID == orderId);

        Console.WriteLine(original!.ShipAddress);
        Console.WriteLine(changed!.ShipAddress);
    }

    /// <summary>
    /// Creates a <b>new order</b> in the database with <b>predefined</b> customer, employee, and shipper details.<br/><br/>
    /// See <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags">DbContext.SaveChanges</see> for more on saving data with EF Core.<br/>
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// <list type="number">
    /// <item>Retrieves a customer, employee, and shipper from the database.</item>
    /// <item>Validates that the required entities are found.</item>
    /// <item>Creates a new order with the retrieved entities and predefined values.</item>
    /// <item>Saves the new order to the database to get new primary key.</item>
    /// <item>Adds order details for the new order using a subset of products.</item>
    /// <item>Saves the order details to the database.</item>
    /// </list>
    /// </remarks>
    public static void CreateNewOrder()
    {
        PrintPink();

        using var context = new Context();

        var customer = context.Customers
            .IgnoreQueryFilters()
            .Include(customers => customers.CountryIdentifierNavigation)
            .FirstOrDefault(x => x.CustomerIdentifier == 2);
        
        var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == 1);
        var shipper = context.Shippers.FirstOrDefault(x => x.ShipperID == 1);

        if (customer == null || employee == null || shipper == null)
        {
            ErrorPill(Justify.Left, "Could not find required entities to create a new order.");
            return;
        }

        var newOrder = new Orders
        {
            OrderDate = DateTime.UtcNow,
            RequiredDate = DateTime.UtcNow.AddDays(7),
            ShippedDate = DateTime.UtcNow.AddDays(3), // Simulate shipping
            ShipVia = shipper.ShipperID,
            Freight = 15.50,
            ShipAddress = customer.Street,
            ShipCity = customer.City,
            ShipPostalCode = customer.PostalCode,
            ShipCountry = customer.CountryIdentifierNavigation.Name,
            CustomerIdentifier = customer.CustomerIdentifier,
            EmployeeID = employee.EmployeeID
        };

        context.Orders.Add(newOrder);

        // Save changes to generate OrderID for the new order
        context.SaveChanges();

        // Add some order details
        var products = context.Products.Take(2).ToList();

        foreach (var product in products)
        {
            context.OrderDetails.Add(new OrderDetails
            {
                OrderID = newOrder.OrderID,
                ProductID = product.ProductID,
                UnitPrice = product.UnitPrice ?? 0,
                Quantity = 1,
                Discount = 0
            });
        }

        var count = context.SaveChanges();
        SuccessPill(Justify.Left, $"New order created with ID: {newOrder.OrderID} with {count} products");
    }
}
