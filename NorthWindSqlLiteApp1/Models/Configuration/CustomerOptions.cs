namespace NorthWindSqlLiteApp1.Models.Configuration;

public class CustomerOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to apply a query filter for customers.
    /// </summary>
    /// <value>
    /// <c>true</c> if the query filter should be applied; otherwise, <c>false</c>.
    /// </value>
    public bool UseQueryFilter { get; set; }

    public int CountryCode { get; set; }
}