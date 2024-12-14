namespace SqlServerGetJsonRaw.Models;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string Company { get; }

    public Address(string street, string city, string company)
    {
        Street = street;
        City = city;
        Company = company;
    }
}