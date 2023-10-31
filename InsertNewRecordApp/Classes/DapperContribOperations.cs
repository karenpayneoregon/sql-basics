using Dapper;
using InsertNewRecordApp.Handlers;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.Classes;
/// <summary>
/// This class is for https://github.com/DapperLib/Dapper.Contrib
/// TODO Add CRUD
/// </summary>
public partial class DataOperations
{
    /// <summary>
    /// Get all <see cref="Customer"/> in database table
    /// </summary>
    /// <returns></returns>
    public static async Task<List<Customer>> GetAllContrib()
    {

        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        await using SqlConnection cn = new(ConnectionString());

        return (List<Customer>)await cn.GetAllAsync<Customer>();
    }
}