using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerGetJsonRaw.Models;

public class Address(string street, string city, string addressType)
{
    [Column(Order = 1)]
    public string Street { get; } = street;
    [Column(Order = 3)]
    public string City { get; } = city;
    [Column(Order = 2)]
    public string AddressType { get; } = addressType;
    public override string ToString() => AddressType;

}