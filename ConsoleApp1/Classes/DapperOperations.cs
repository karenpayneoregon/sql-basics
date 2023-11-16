using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using ConsoleApp1.Handlers;
using ConsoleApp1.Models;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using System.Drawing.Printing;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable DAP018
#pragma warning disable DAP018
#pragma warning disable CS8603 // Possible null reference return.

[module: DapperAot]
//[module: DapperAot(false)]

namespace ConsoleApp1.Classes;

[SqlSyntax(SqlSyntax.SqlServer)]

internal class DapperOperations
{

    private readonly IDbConnection _cn;
    private readonly IDbConnection _cn1;
    private readonly IDbConnection _cn2;

    public DapperOperations()
    {
        _cn = new SqlConnection(ConnectionString());
        _cn1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=DataGridViewCodeSample;Integrated Security=True;Encrypt=False");
        _cn2 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=NorthWind2022;Integrated Security=True;Encrypt=False");
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    /// <summary>
    /// Get all records in the Person table synchronously
    /// </summary>
    public List<Person> GetAll()
        => _cn.Query<Person>(SqlStatements.ReadPeople).AsList();

    public (List<OrdersPagination> list, Exception exception) GetOrdersPagination()
    {
        const string statement =
            """
            SELECT o.OrderID,
                   o.CustomerIdentifier,
                   o.EmployeeID,
                   o.OrderDate,
                   o.ShippedDate,
                   o.DeliveredDate,
                   o.ShipVia,
                   o.Freight,
            	   s.CompanyName AS Shipper,
            	   e.FirstName + ' ' + e.LastName AS FullName
            FROM dbo.Orders AS o
            INNER JOIN dbo.Shippers AS s
                    ON o.ShipVia = s.ShipperID
                INNER JOIN dbo.Employees AS e
                    ON o.EmployeeID = e.EmployeeID
            ORDER BY o.OrderID
            OFFSET @OffSet ROWS FETCH NEXT @PageSize ROW ONLY;
            """;
        try
        {
            var parameters = new { OffSet = 827, PageSize = 5 };
            return (_cn2.Query<OrdersPagination>(statement, parameters).AsList(), null);
        }
        catch (Exception localException)
        {
            return (null, localException);
        }
    }

    ///// <summary>
    ///// To test Dapper AOT SQL syntax
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public Person GetPerson(int id) => _cn.QueryFirstOrDefault<Person>(
    //    "select * from dbo.Person where Id=@id",
    //    new { Id = id });


    /// <summary>
    /// DAP214: Variable @Id is not declared and no corresponding parameter exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Person GetPerson1(int id)
    {
        const string statement =
            """
                SELECT Id,
                       FirstName,
                       LastName,
                       BirthDate
                FROM dbo.Person
                WHERE Id = @Id
                """;
        return _cn.QueryFirstOrDefault<Person>(
            statement,
            new { Id = id });
    }

    /// <summary>
    /// error DAP206: Error 46010: Incorrect syntax near @ByteArray.
    /// warning DAP236: Parameter 'ByteArray' is not used, but will be included
    ///
    ///  Change VALUES to VALUE
    /// 
    /// </summary>
    /// <param name="imageBytes"></param>
    public static int InsertImage(byte[] imageBytes)
    {
        const string statement =
            """
            INSERT INTO dbo.Pictures (Photo) 
            OUTPUT Inserted.Id VALUES (@ByteArray)
            """;
        using var cn = new SqlConnection(ConnectionString());
        var parameters = new { ByteArray = imageBytes };
        return cn.ExecuteScalar<int>(statement, parameters);
    }

    public  (bool success, Exception exception) UpdateRow(ProductItem item)
    {
        const string statement =
            """
            UPDATE dbo.Product
            SET @Item = @Item, @ColorId = @ColorId
            WHERE id = @Id;
            """;

        var parameters = new { item.Item, item.ColorId, item.Id };
        try
        {
            var affected = _cn1.Execute(statement, parameters);
            return (affected == 1, null)!;

        }
        catch (Exception localException)
        {
            return (false, localException);
        }
    }

}