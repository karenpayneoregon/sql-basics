#nullable disable

namespace ContinentsDapperApp.Models;

public partial class Continent
{
    public int Id { get; set; }

    public string ContinentName { get; set; }
    public override string ToString() => ContinentName;

}