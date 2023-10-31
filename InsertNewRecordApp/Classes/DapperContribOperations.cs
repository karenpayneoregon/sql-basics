using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.Classes;
/// <summary>
/// This class is for https://github.com/DapperLib/Dapper.Contrib
/// TODO Add CRUD
///
/// With Dapper.Contrib library, no need for SQL although we will go over this
/// in a upcoming article.
/// </summary>
public partial class DataOperations
{
    /// <summary>
    /// Get all <see cref="Customer"/> in database table
    /// </summary>
    public static async Task<List<Customer>> GetAllContrib()
    {
        await using SqlConnection cn = new(ConnectionString());
        return (List<Customer>)await cn.GetAllAsync<Customer>();
    }
    /// <summary>
    /// Get customer by primary key
    /// </summary>
    /// <param name="id">Existing key</param>
    public static async Task<Customer> GetContrib(int id)
    {
        await using SqlConnection cn = new(ConnectionString());
        return await cn.GetAsync<Customer>(id);
    }
}