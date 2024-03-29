namespace DapperGetDatabaseAndTableNamesApp.Classes;

internal class ViewContainer
{
    public string Schema { get; set; }
    public string Name { get; set; }
    public override string ToString() => Schema;
}