namespace DapperNorthWind2024.Models;

public class ProcedureProperty
{
    public string Schema { get; set; }
    public string Name { get; set; }
    public string Information { get; set; }
    public string PropertyValue { get; set; }
    public override string ToString() => $"{Information} = {PropertyValue}";
}
