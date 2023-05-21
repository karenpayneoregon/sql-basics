namespace GlobalConnection.Models;

public class CategoryGroup
{
    public int Id { get; }
    public List<Product> List { get; }

    public CategoryGroup(int id, List<Product> list)
    {
        Id = id;
        List = list;
    }
}