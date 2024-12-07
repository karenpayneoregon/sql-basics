using Microsoft.Extensions.Options;
using SqlServerDateOnlyTimeOnlySampleApp.Models.Configuration;

namespace SqlServerDateOnlyTimeOnlySampleApp.Classes.Configuration;
internal class SetupServices
{
    private readonly ConnectionStrings _options;

    public SetupServices(IOptions<ConnectionStrings> options)
    {
        _options = options.Value;
    }
    /// <summary>
    /// Read connection strings from appsettings
    /// </summary>
    public void GetConnectionStrings()
    {
        DataConnections.Instance.MainConnection = _options.MainConnection;
        DataConnections.Instance.SecondaryConnection = _options.SecondaryConnection;
    }
}
