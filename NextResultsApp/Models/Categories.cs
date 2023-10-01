namespace GitHubSamples.Models;

public class Categories
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public override string ToString() => CategoryName;
}