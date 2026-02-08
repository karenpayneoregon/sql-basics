using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Data;
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
        
        var contact = context.Contacts
            .Include(c => c.ContactTypeIdentifierNavigation)
            .Include(c => c.ContactDevices)
            .FirstOrDefault(c => c.ContactId == order!.CustomerIdentifier);

        if (order != null)
        {
            Console.WriteLine($"    Order ID: {order.OrderID}");
            Console.WriteLine($"    Customer: {order.CustomerIdentifierNavigation?.CompanyName}");
            Console.WriteLine($"     Contact: {contact?.FullName ?? "N/A"}");
            Console.WriteLine($"       Title: {contact?.ContactTypeIdentifierNavigation?.ContactTitle ?? "N/A"}");
            Console.WriteLine($"       Phone: {contact?.ContactDevices.FirstOrDefault()?.PhoneNumber ?? "N/A"}");
            Console.WriteLine($"  Order Date: {order.OrderDate:MM/dd/yyyy}");
            Console.WriteLine($"Total Amount: {order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice)}");

            AnsiConsole.MarkupLine($"{new string('_', 45)}");


            foreach (var detail in order.OrderDetails)
            {
                AnsiConsole.MarkupLine($"   [CadetBlue_1]{detail.Product?.ProductName, -30}{detail.Quantity, -5}{detail.UnitPrice:C}[/]");
            }
        }
        else
        {
            ErrorPill(Justify.Left, $"{orderId} was not found.");
        }
    }
}
