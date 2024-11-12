using BirthdaysFromDatabase.Models.Configuration;
using Microsoft.Extensions.Options;

namespace BirthdaysFromDatabase.Classes.Configuration;
internal class SetupServices
{
    private readonly ConnectionStrings _options;

    public SetupServices(IOptions<ConnectionStrings> options)
    {
        _options = options.Value;

    }
    public void GetConnectionStrings()
    {
        DataConnections.Instance.MainConnection = _options.MainConnection;
    }
}
