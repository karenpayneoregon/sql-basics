namespace NorthWindSqlLiteApp1.Models.Configuration;
public class ContextOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the audit interceptor should be used.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true"/>, the audit interceptor is enabled, allowing
    /// for auditing of database operations. This can be useful for tracking changes
    /// or monitoring database activity. When set to <see langword="false"/>, the
    /// audit interceptor is disabled.
    /// </remarks>
    public bool UseAuditInterceptor { get; set; }

    /// <summary>
    /// Gets or sets the configuration options for customers model.
    /// </summary>
    /// <remarks>
    /// This property provides access to the <see cref="CustomerOptions"/> class, 
    /// which contains specific settings related to customer operations, such as enabling 
    /// query filters or other customer-specific configurations.
    /// </remarks>
    public CustomerOptions CustomersOptions { get; set; } = new();
}