using System.Data;
using System.Data.OleDb;
using Dapper;
using DapperForAccessDatabase.Models;
using kp.Dapper.Handlers;

#pragma warning disable CA1416

namespace DapperForAccessDatabase.Repositories;
public class PersonRepository
{
    private IDbConnection _cn;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRepository"/> class.
    /// - Setup for DateOnly handler and for creating a connection
    /// - Connection string is stored in appsettings.json amd uses ConfigurationLibrary NuGet package
    /// </summary>
    public PersonRepository()
    {
        _cn = new OleDbConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    /// <summary>
    /// Retrieves all the people from the database.
    /// </summary>
    /// <returns>A list of all the people.</returns>
    public List<Person> GetAll()
        => _cn.Query<Person>("SELECT * FROM Person").ToList();

    /// <summary>
    /// Adds a person to the database.
    /// </summary>
    /// <param name="person">The person to add.</param>
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

    /// <summary>
    /// Adds a range of people to the database.
    /// </summary>
    /// <param name="people">The list of people to add.</param>
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
    /// <summary>
    /// Updates the birthdate of a person in the database.
    /// </summary>
    /// <param name="id">The ID of the person.</param>
    /// <param name="birthDate">The new birthdate.</param>
    /// <returns>True if the birthdate was successfully updated, otherwise false.</returns>
    public bool UpdateBirthDate(int id, DateOnly birthDate)
    {
        const string statement =
            """
                    UPDATE Person
                    SET BirthDate = @BirthDate 
                    WHERE Id = @Id
                    """;

        return _cn.Execute(statement, new { BirthDate = birthDate, Id = id }) == 1;
    }

    /// <summary>
    /// Retrieves a person from the database based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The person with the specified ID.</returns>
    public Person Get(int id) =>
        _cn.QueryFirst<Person>(
            """
             SELECT Id, FirstName, LastName, BirthDate, Active 
             FROM Person 
             WHERE Id = @Id
             """,
            new { Id = id });

    /// <summary>
    /// Removes a person from the database based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the person to remove.</param>
    /// <returns>True if the person was successfully removed, otherwise false.</returns>
    public bool Remove(int id)
    {
        const string statement =
            """
            DELETE FROM Person
            WHERE Id = @Id
            """;

        return _cn.Execute(statement, new { Id = id }) == 1;
    }
}
