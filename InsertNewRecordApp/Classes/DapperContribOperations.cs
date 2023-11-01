using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.Classes;
/// <summary>
/// This class is for https://github.com/DapperLib/Dapper.Contrib
/// 
/// With Dapper.Contrib library, no need for SQL although we will go over this
/// in a upcoming article.
///
/// Before diving in, make sure to read the docs
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
    /// <summary>
    /// Add new customer
    /// </summary>
    /// <param name="customer">Customer to add</param>
    public static async Task AddContrib(Customer customer)
    {
        await using SqlConnection cn = new(ConnectionString());
        customer.Id = await cn.InsertAsync(customer);
    }
    /// <summary>
    /// Update an existing customer
    /// </summary>
    /// <param name="customer">Customer to update</param>
    /// <returns>Success/failure</returns>
    public static async Task<bool> UpdateContrib(Customer customer)
    {
        await using SqlConnection cn = new(ConnectionString());
        return await cn.UpdateAsync(customer);
    }
    /// <summary>
    /// Remove an existing customer
    /// </summary>
    /// <param name="customer">Customer to remove</param>
    /// <returns>Success/failure</returns>
    public static async Task<bool> RemoveContrib(Customer customer)
    {
        await using SqlConnection cn = new(ConnectionString());
        return await cn.DeleteAsync(customer);
    }
}