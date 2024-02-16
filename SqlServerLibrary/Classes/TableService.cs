using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SqlServerLibrary.Models;

namespace SqlServerLibrary.Classes;


    public class TableService
    {
        private readonly IDbConnection _cn = new SqlConnection(ConnectionString());
        public List<string> TableNames()
        {
            SqlConnectionStringBuilder builder = new(ConnectionString());
            
            return _cn.Query<string>(SqlStatements.TableNamesForDatabase(builder.InitialCatalog)).AsList();
        }
        public List<TableConstraints> GetAllTableConstraints()
        {
            return _cn.Query<TableConstraints>(SqlStatements.TableConstraintsForDatabase).AsList();
        }
    }

