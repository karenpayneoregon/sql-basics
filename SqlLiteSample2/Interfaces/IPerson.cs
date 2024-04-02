namespace SqlLiteSample2.Interfaces;

/// <summary>
/// For validation rules for first and last name
/// </summary>
public interface IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
