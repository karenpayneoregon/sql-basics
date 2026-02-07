namespace NorthWindSqlLiteApp1.Models.Configuration;
public class ContextOptions
{
    public bool UseAuditInterceptor { get; set; }

    public CustomerOptions CustomersOptions { get; set; } = new();
}