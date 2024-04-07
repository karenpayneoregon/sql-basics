using Dapper;
using System.Data;
#nullable disable

namespace ContinentsDapperApp.Classes;
// Dapper.SqlMapper.AddTypeHandler(new TrimStringHandler());
public class TrimStringHandler : SqlMapper.TypeHandler<string>
{
    public override string Parse(object value)
    {
        return ((string)value)?.Trim();
    }

    public override void SetValue(IDbDataParameter parameter, string value)
    {
        parameter.Value = value;
    }
}
