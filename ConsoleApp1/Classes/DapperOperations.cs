using Microsoft.Data.SqlClient;
using System.Data;
using ConsoleApp1.Handlers;
using ConsoleApp1.Models;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
#pragma warning disable CS8603 // Possible null reference return.

    //[module: DapperAot]
    [module: DapperAot(false)]

    namespace ConsoleApp1.Classes;

    [SqlSyntax(SqlSyntax.SqlServer)]
    internal class DapperOperations
    {
        private readonly IDbConnection _cn;

        public DapperOperations()
        {
            _cn = new SqlConnection(ConnectionString());
            SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        }

        /// <summary>
        /// Get all records in the Person table synchronously
        /// </summary>
        public List<Person> GetAll()
            => _cn.Query<Person>(SqlStatements.ReadPeople).AsList();

        /// <summary>
        /// To test Dapper AOT SQL syntax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Person GetPerson(int id) => _cn.QueryFirstOrDefault<Person>(
            "select * from dbo.Person where Id=@id", 
            new { Id = id });
    }
