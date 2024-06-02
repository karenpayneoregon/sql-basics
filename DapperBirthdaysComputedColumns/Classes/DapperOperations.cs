using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        _cn = new SqlConnection(DataConnections.Instance.MainConnection);
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
    }

    public async Task<List<BirthDays>> GetBirthdaysAsync()
    {
        return (await _cn.QueryAsync<BirthDays>(SqlStatements.GetBirthdays)).AsList();
    }

}
