using Dapper;
using Microsoft.Data.SqlClient;
using StudentHelpApp.Models;
using System.Data;

namespace StudentHelpApp.Classes;
internal class DapperOperations
{
    private IDbConnection _cn;

    /// <summary>
    /// Setup connection for all operations
    /// </summary>
    public DapperOperations()
    {
        _cn = new SqlConnection(ConnectionString());
    }

    /// <summary>
    /// Get all records in the <seealso cref="Item"/> table synchronously
    /// </summary>
    public List<Item> GetAll()
        => _cn.Query<Item>(
            """
            SELECT [Id],[Name],[Description] 
            FROM [dbo].[Item]
            """).ToList();

    /// <summary>
    /// Populate <see cref="Item"/>  table with mocked data
    /// </summary>
    public void Populate()
    {
        Reset();
        var statement = 
            """
            INSERT INTO [dbo].[Item] ([Name],[Description]) 
            VALUES (@Name,@Description)
            """;
        _cn.Execute(statement, BogusOperations.Items());

    }
    /// <summary>
    /// Add a single <seealso cref="Item"/>
    /// </summary>
    /// <param name="item">Valid item without id set</param>
    public async Task Add(Item item)
    {
        var identifier = await _cn.QueryFirstAsync<int>(
            """
                INSERT INTO [dbo].[Item] ([Name],[Description]) 
                VALUES (@Name,@Description);
                SELECT CAST(scope_identity() AS int);
                """, item);
        item.Id = identifier;
    }
    /// <summary>
    /// Get a single <seealso cref="Item"/> by id
    /// </summary>
    /// <param name="id">Existing primary key</param>
    /// <returns>A single person or null if not located</returns>
    public async Task<Item> Find(int id)
        => (await _cn.QueryFirstOrDefaultAsync<Item>(
            """
                SELECT Id, Name,Description FROM dbo.Item WHERE Id = @Id;
                """, new { Id = id }))!;

    /// <summary>
    /// Update an existing <see cref="Item"/>
    /// </summary>
    /// <param name="item"></param>
    /// <returns>success and on failure an exception</returns>
    public async Task<(bool, Exception ex)> Update(Item item)
    {
        try
        {
            var affected = await _cn.ExecuteAsync(
                """
                    UPDATE [dbo].[Item]
                      SET [Name] = @Name,
                         [Description] = @Description
                    WHERE Id = @Id
                    """, new { item.Name, item.Description, item.Id });

            return (affected == 1, null);

        }
        catch (Exception localException)
        {
            return (false, localException);
        }
    }
    /// <summary>
    /// Remove a single <seealso cref="Item"/> record
    /// </summary>
    /// <param name="item">Existing person</param>
    /// <returns>success of operation</returns>
    public async Task<bool> Remove(Item item)
    {
        var affected = await _cn.ExecuteAsync(
            """
                DELETE FROM [dbo].[Item] 
                WHERE Id = @Id
                """, new { item.Id });
        return affected == 1;
    }

    /// <summary>
    /// Truncate the <seealso cref="Item"/> table and reset identity
    /// </summary>
    public void Reset()
    {
         _cn.Execute($"DELETE FROM dbo.{nameof(Item)}");
         _cn.Execute($"DBCC CHECKIDENT ({nameof(Item)}, RESEED, 0)");
    }
}
