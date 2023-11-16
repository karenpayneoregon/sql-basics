using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ConsoleApp1.Models;
internal class OrdersPagination
{
    public int OrderID { get; set; }

    public int? CustomerIdentifier { get; set; }

    public int? EmployeeID { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DeliveredDate { get; set; }

    public int? ShipVia { get; set; }

    public decimal? Freight { get; set; }

    public string ShipAddress { get; set; }

    public string ShipCity { get; set; }

    public string ShipRegion { get; set; }

    public string ShipPostalCode { get; set; }

    public string ShipCountry { get; set; }
    public string Shipper { get; set; }
    public string FullName { get; set; }
}
