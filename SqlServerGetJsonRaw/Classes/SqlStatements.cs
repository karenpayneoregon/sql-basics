namespace SqlServerGetJsonRaw.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Gets the SQL query string that retrieves person addresses from the database.
    /// 
    /// ===> This is a bad design. <===
    /// 
    /// </summary>
    /// <remarks>   
    /// The query iterates through a JSON array of address  es for each person, extracting
    /// specific fields such as Street, City, and Company. It uses the <c>JSON_VALUE</c>
    /// function to parse JSON data and assumes the presence of a variable <c>@index</c>
    /// for iteration. The query filters results where the last name is 'Payne'.
    ///
    /// This is not recommended as it is hard coded for two addresses. If business requirements
    /// were to require two addresses it would be fine like address for an account and one for shipping
    /// address, otherwise if there were not two addresses an arithmetic overflow error converting
    /// expression to data type nvarchar would occur.
    /// </remarks>
    /// <returns>
    /// A SQL query string that retrieves person addresses.
    /// </returns>
    public static string GetPersonAddressesDapper =>
        """
        DECLARE @index INT = 0;
        WHILE @index < 2
        BEGIN
            SELECT Id,
                   FirstName,
                   LastName,
                   DateOfBirth,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].Street') AS Street,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].City')   AS City,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].AddressType') AS AddressType
            FROM dbo.Person WHERE LastName = @LastName;
            SET @index = @index + 1;
        END;
        """;

    /// <summary>
    /// Gets the SQL query string used to retrieve person addresses from the database using SQL Client.
    /// </summary>
    /// <remarks>
    /// This query utilizes a Common Table Expression (CTE) to parse JSON data stored in the "Addresses" column
    /// of the "Person" table. It extracts individual address components such as Street, City, and Company
    /// for each person, and assigns a row number to each address for further processing.
    /// </remarks>
    /// <value>
    /// A SQL query string that retrieves person details and their associated addresses.
    /// </value>
    public static string GetPersonAddressesDapperOrSqlClient =>
        """
        WITH PersonAddresses AS (
          SELECT
            p.Id,
            p.FirstName,
            p.LastName,
            p.DateOfBirth,
            a.Street,
            a.City,
            a.AddressType,
            ROW_NUMBER() OVER (PARTITION BY p.Id ORDER BY a.Street) AS AddressIndex
          FROM
            dbo.Person p
          CROSS APPLY
            OPENJSON(p.Addresses)
            WITH (
              Street NVARCHAR(MAX),
              City NVARCHAR(MAX),
              AddressType NVARCHAR(MAX)
            ) a
          WHERE
            p.LastName = @LastName
        )
        SELECT
          pa.Id,
          pa.FirstName,
          pa.LastName,
          pa.DateOfBirth,
          pa.Street,
          pa.City,
          pa.AddressType
        FROM
          PersonAddresses pa;
        """;

    /// <summary>
    /// Represents a SQL statement for inserting a new record into the <c>dbo.Person</c> table 
    /// and retrieving the generated primary key.
    /// </summary>
    /// <remarks>
    /// This SQL statement is used to insert a person's details, including their first name, 
    /// last name, date of birth, and addresses (in JSON format), into the database. 
    /// After the insertion, it retrieves the newly generated primary key using <c>scope_identity()</c>.
    /// </remarks>
    public static string DapperInsert =>
        """
        INSERT INTO dbo.Person (FirstName, LastName, DateOfBirth, Addresses)
        VALUES (@FirstName, @LastName, @DateOfBirth, @Addresses);
        SELECT CAST(scope_identity() AS int);
        """;
}
