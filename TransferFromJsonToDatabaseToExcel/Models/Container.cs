namespace TransferFromJsonToDatabaseToExcel.Models;

public class Container
{
    public int Id { get; set; }
    public DateOnly InputDate { get; set; }
    public string Specification { get; set; }
    public string Category { get; set; }
    public int Value { get; set; }
}
