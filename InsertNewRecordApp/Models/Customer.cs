using Dapper.Contrib.Extensions;

namespace InsertNewRecordApp.Models;

/// <summary>
/// Dapper.Contrib expects a pluralized name so we need to
/// tell Dapper.Contrib the table name.
/// </summary>
[Table("Customer")]
public class Customer
{
    /// <summary>
    /// If the primary key is not Id, read the docs for Dapper.Contrib
    /// </summary>
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
    public override string ToString() => $"{Id} {FirstName} {LastName}";
}