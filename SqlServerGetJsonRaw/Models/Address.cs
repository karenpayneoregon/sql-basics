namespace SqlServerGetJsonRaw.Models;

public class Address(string street, string city, string company)
{
    public string Street { get; } = street;
    public string City { get; } = city;
    public string Company { get; } = company;
}