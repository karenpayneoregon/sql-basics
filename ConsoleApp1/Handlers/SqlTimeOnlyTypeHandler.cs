using System.Data;
using Dapper;

namespace ConsoleApp1.Handlers;

// https://github.com/DapperLib/Dapper/issues/1715#issuecomment-1146141064
public class SqlTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly time)
    {
        parameter.Value = time.ToString();
    }

    public override TimeOnly Parse(object value) => TimeOnly.FromTimeSpan((TimeSpan)value);
}