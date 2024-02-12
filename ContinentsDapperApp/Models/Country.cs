#nullable disable
namespace ContinentsDapperApp.Models;

public partial class Country
{
    public int Id { get; set; }

    public int? ContinentId { get; set; }

    public string CountryName { get; set; }

    public string CapitalName { get; set; }

    public override string ToString() => CountryName;


}