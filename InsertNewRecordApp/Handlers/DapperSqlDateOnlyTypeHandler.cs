using Dapper;
using System.Data;

namespace InsertNewRecordApp.Handlers;
// https://github.com/DapperLib/Dapper/issues/1715#issuecomment-1149665776
public class DapperSqlDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
        => parameter.Value = date.ToDateTime(new TimeOnly(0, 0));

    public override DateOnly Parse(object value)
        => DateOnly.FromDateTime((DateTime)value);
}