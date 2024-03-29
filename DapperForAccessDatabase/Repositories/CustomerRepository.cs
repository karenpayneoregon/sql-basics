﻿using System.Data;
using System.Data.OleDb;
using Dapper;
using DapperForAccessDatabase.Interfaces;
using DapperForAccessDatabase.Models;
using kp.Dapper.Handlers;
#pragma warning disable CA1416

namespace DapperForAccessDatabase.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private IDbConnection _cn;

    public CustomerRepository()
    {
        _cn = new OleDbConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    private string _tableName = "";
    public CustomerRepository(string tableName)
    {
        _cn = new OleDbConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        _tableName = tableName;
    }
    public List<Customers> GetAll()
        => _cn.Query<Customers>($"SELECT Identifier, FirstName, LastName, UserName FROM {_tableName}").ToList();

    public void Add(List<Customers> customer)
    {

        var statement =
            $"""
            INSERT INTO {_tableName}
            (
                FirstName,LastName,UserName
            )
            VALUES
            (
                @FirstName,@LastName,@UserName
            )
            """;
        _cn.Execute(statement, customer);
    }
}