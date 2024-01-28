using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DapperSimpleApp.Models;

namespace DapperSimpleApp.Classes
{
    internal class OldSchool
    {
        private static string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InsertExamples;Integrated Security=True;Encrypt=False";


        public static DataTable ConventionalDataTable()
        {
            var statement = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person;";
            using (var cn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(statement, cn))
                {
                    DataTable table = new DataTable();
                    cn.Open();
                    table.Load(cmd.ExecuteReader());
                    return table;
                }
            }
        }

        public static List<Person> ConventionalPeopleList()
        {
            List<Person> list = new List<Person>();
            var statement = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person;";
            using (var cn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(statement,cn))
                {
                    cn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Person()
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3)
                        });
                    }

                }
            }

            return list;
        }

        public static List<Person> DapperPeopleList()
        {
            var statement = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person;";
            using (var cn = new SqlConnection(connectionString))
            {
                return cn.Query<Person>(statement).AsList();
            }
        }

        public static Person ConventionalSinglePerson(int id)
        {
            var statement = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person WHERE Id = @Id";
            using (var cn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(statement, cn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cn.Open();
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    Person person = new Person()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        BirthDate = reader.GetDateTime(3)
                    };

                    return person;
                }
            }
        }

        public static Person DapperSingPerson(int id)
        {
            var statement = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person WHERE Id = @Id";
            using (var cn = new SqlConnection(connectionString))
            {
                return cn.QueryFirst<Person>(statement, new { Id = id });
            }
        }
    }
}
