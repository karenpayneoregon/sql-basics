﻿using System.Data;
using System.Data.OleDb;
using Dapper;
using DapperForAccessDatabase.Models;
using kp.Dapper.Handlers;

#pragma warning disable CA1416

namespace DapperForAccessDatabase.Repositories;
public class PersonRepository
{
    private IDbConnection _cn;

    public PersonRepository()
    {
        _cn = new OleDbConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }
    public List<Person> GetAll()
        => _cn.Query<Person>("SELECT * FROM Person").ToList();

    public void Add(Person person)
    {
        const string statement =
            """
            INSERT INTO Person
            (
                FirstName,LastName,BirthDate,Active
            )
            VALUES
            (
                @FirstName,@LastName,@BirthDate,@Active
            )
            """;
        _cn.Execute(statement, new { person.FirstName, person.LastName, person.BirthDate, person.Active });
    }

    public void Insert(Person person)
    {
        const string statement =
            """
            INSERT INTO Person
            (
                FirstName,LastName,BirthDate,Active
            )
            VALUES
            (
                @FirstName,@LastName,@BirthDate,@Active
            )
            """;

        _cn.Open();
        _cn.Execute(statement, new { person.FirstName, person.LastName, person.BirthDate, person.Active });
        person.Id = _cn.QueryFirst<int>("SELECT @@IDENTITY");
        _cn.Close();
    }

    public void AddRange(List<Person> people)
    {

        foreach (var person in people)
        {
            Add(person);
        }
    }

    /*
     * Basic/one property update. Add another method for say only updating First/Last name
     * The idea is usually all properties need not be updated.
     */
    public bool UpdateBirthDate(int id, DateOnly birthDate)
    {
        const string statement =
            """
            UPDATE Person
            SET BirthDate = @BirthDate 
            WHERE Id = @Id
            """;

        return _cn.Execute(statement, new {BirthDate = birthDate, Id = id}) == 1;
    }

    public Person Get(int id) =>
        _cn.QueryFirst<Person>(
            """
            SELECT Id, FirstName, LastName, BirthDate, Active 
            FROM Person 
            WHERE Id = @Id
            """, 
            new {Id = id});

    public void Remove(int id)
    {
        // TODO
    }
}
