#nullable disable


namespace ContinentsDapperApp.Models;

public partial class CountryInformation
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string Fact { get; set; }
}