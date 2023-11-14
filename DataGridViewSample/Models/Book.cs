namespace DataGridViewSample.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Categories Category { get; set; }
    public override string ToString() => Title;
}