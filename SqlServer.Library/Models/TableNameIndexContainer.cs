namespace SqlServer.Library.Models;

public class TableNameIndexContainer(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;

    public override string ToString() => Name;
}