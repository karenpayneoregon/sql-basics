namespace DeleteDuplicateRowsSqlServerTable.Models;

public class ItemContainer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public override string ToString() => Name;

}