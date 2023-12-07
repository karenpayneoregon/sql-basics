using Dapper;
using ProductCatalogApp.Models;
using System.Data.SqlClient;
using System.Text.Json;
using static ProductCatalogApp.Classes.SpectreConsoleHelpers;

namespace ProductCatalogApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        SqlMapper.AddTypeHandler(typeof(string[]), new StringArrayJsonMapper());
        SqlMapper.AddTypeHandler(typeof(object), new ObjectJsonMapper());

        using SqlConnection cn = new(ConnectionString());
        var products = cn.Query<Product>(@"select * from Product");

        AnsiConsole.MarkupLine("[blue]Id  Name                          Color     Cost    Country[/]  Sale");

        foreach (var product in products)
        {
            var onSale = "No";
            if (product.Tags is not null)
            {
                onSale = product.Tags.Any(tag => tag == "promo") ? "Yes" : "No";
            }

            if (product.Data.Length > 2)
            {
                Manufacturing manufacturing = JsonSerializer.Deserialize<Manufacturing>(product.Data);
                AnsiConsole.MarkupLine($"{product.ProductID,-4}" +
                                       $"{product.Name,-30}" +
                                       $"{product.Color ?? "(none)",-10}[cyan]" +
                                       $"{manufacturing.ManufacturingCost,-8:C}" +
                                       $"{manufacturing.MadeIn ?? "(?)",-10}[/] " +
                                       $"{onSale}");
            }
            else
            {
                Console.WriteLine($"{product.ProductID,-4}" +
                                  $"{product.Name,-30}" +
                                  $"{product.Color ?? "(Unknown)"} " +
                                  $"{onSale}");
            }
        }

        ExitPrompt();
    }
}