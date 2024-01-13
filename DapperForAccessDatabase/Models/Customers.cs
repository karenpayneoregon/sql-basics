
// ReSharper disable ConvertConstructorToMemberInitializers
#pragma warning disable CA1416

namespace DapperForAccessDatabase.Models;
public class Customers
{
    public int Identifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public override string ToString() => $"{Identifier} {FirstName} {LastName}";
}