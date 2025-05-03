using System.Data;
using ConsoleConfigurationLibrary.Classes;
using Dapper;
using DapperBirthdaysComputedColumns.Models;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;

namespace DapperBirthdaysComputedColumns.Classes;
internal class DapperOperations
{
    private IDbConnection _cn;

    public DapperOperations()
    {
        _cn = new SqlConnection(AppConnections.Instance.MainConnection);
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    public async Task<List<BirthDays>> GetBirthdaysAsync()
    {
        return (await _cn.QueryAsync<BirthDays>(SqlStatements.GetBirthdays)).AsList();
    }

}
