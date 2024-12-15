using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerGetJsonRaw.Models;

public class Address(string street, string city, string company)
{
    [Column(Order = 1)]
    public string Street { get; } = street;
    [Column(Order = 3)]
    public string City { get; } = city;
    [Column(Order = 2)]
    public string Company { get; } = company;
    public override string ToString() => Company;

}