using System.ComponentModel.DataAnnotations;

namespace GlobalConnection.Models;

public class Category : IEquatable<Category>
{
    [Key]
    public int CategoryID { get; set; }

    public string CategoryName { get; set; }
    public bool Equals(Category other) => CategoryID == other?.CategoryID;
    public override string ToString() => CategoryName;

}

