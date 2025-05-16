namespace NorthCustomersToolGenerated.Classes;

/// <summary>
/// Some developers may consider using stored procedures or views to encapsulate SQL queries.
/// https://dev.to/karenpayneoregon/dapper-stored-procedure-tip-13j4
/// </summary>
internal class SqlStatements
{
    /// <summary>
    /// Represents a SQL query that retrieves customer details along with their associated contacts, 
    /// countries, and contact types from the database.
    /// </summary>
    /// <remarks>
    /// The query joins the <c>Customers</c>, <c>Contacts</c>, <c>Countries</c>, and <c>ContactType</c> tables 
    /// to provide comprehensive customer information, including identifiers, names, addresses, 
    /// contact details, and related metadata.
    /// </remarks>
    public static string CustomerWithContacts =>
        """
            SELECT CU.CustomerIdentifier,
                   CU.CompanyName,
                   CU.ContactId,
                   CU.Street,
                   CU.City,
                   CU.PostalCode,
                   CU.CountryIdentifier,
                   CU.Phone,
                   CU.Fax,
                   CU.Region,
                   CU.ModifiedDate,
                   CU.ContactTypeIdentifier,
                   C.ContactId,
                   C.FirstName,
                   C.LastName,
                   C.ContactTypeIdentifier,
                   CO.CountryIdentifier,
                   CO.Name,
                   CT.ContactTypeIdentifier,
                   CT.ContactTitle
            FROM dbo.Customers AS CU
                INNER JOIN dbo.Contacts AS C
                    ON CU.ContactId = C.ContactId
                INNER JOIN dbo.Countries AS CO
                    ON CU.CountryIdentifier = CO.CountryIdentifier
                INNER JOIN dbo.ContactType AS CT
                    ON CU.ContactTypeIdentifier = CT.ContactTypeIdentifier
                       AND C.ContactTypeIdentifier = CT.ContactTypeIdentifier;
        """;

    /// <summary>
    /// Gets the SQL query string for retrieving a specific customer and their associated details,
    /// including contact information, country, and contact type, based on the provided customer identifier.
    /// </summary>
    /// <remarks>
    /// This query joins the <c>dbo.Customers</c>, <c>dbo.Contacts</c>, <c>dbo.Countries</c>, and <c>dbo.ContactType</c> tables
    /// to fetch comprehensive customer information. It requires the <c>@CustomerIdentifier</c> parameter to filter the results.
    /// </remarks>
    public static string GetCustomer =>
        """
        SELECT      CU.CustomerIdentifier,
                    CU.CompanyName,
                    CU.ContactId,
                    CU.Street,
                    CU.City,
                    CU.PostalCode,
                    CU.CountryIdentifier,
                    CU.Phone,
                    CU.Fax,
                    CU.Region,
                    CU.ModifiedDate,
                    CU.ContactTypeIdentifier,
                    C.ContactId,
                    C.FirstName,
                    C.LastName,
                    C.ContactTypeIdentifier,
                    CO.CountryIdentifier,
                    CO.Name,
                    CT.ContactTypeIdentifier,
                    CT.ContactTitle
        FROM        dbo.Customers AS CU
        INNER JOIN  dbo.Contacts AS C
            ON CU.ContactId = C.ContactId
        INNER JOIN  dbo.Countries AS CO
            ON CU.CountryIdentifier = CO.CountryIdentifier
        INNER JOIN  dbo.ContactType AS CT
            ON CU.ContactTypeIdentifier = CT.ContactTypeIdentifier
               AND C.ContactTypeIdentifier = CT.ContactTypeIdentifier
        WHERE       CU.CustomerIdentifier = @CustomerIdentifier;
        """;

    /// <summary>
    /// Gets the SQL query string to retrieve detailed information about a specific contact.
    /// </summary>
    /// <remarks>
    /// The query joins the <c>Contacts</c>, <c>ContactType</c>, and <c>ContactDevices</c> tables
    /// to fetch details such as contact's first name, last name, contact type, and associated devices.
    /// </remarks>
    /// <returns>
    /// A SQL query string that includes placeholders for parameters, such as <c>@ContactId</c>.
    /// </returns>
    public static string GetContact => 
        """
        SELECT      C.ContactId,
                    C.FirstName,
                    C.LastName,
                    C.ContactTypeIdentifier,
                    CT.ContactTypeIdentifier AS CTIdentifier,
                    CT.ContactTitle,
                    CD.DeviceId,
                    CD.ContactId AS CDContactId,
                    CD.PhoneTypeIdentifier,
                    CD.PhoneNumber
        FROM        dbo.Contacts AS C
        INNER JOIN  dbo.ContactType AS CT
            ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
        LEFT JOIN   dbo.ContactDevices AS CD
            ON C.ContactId = CD.ContactId
        WHERE       C.ContactId = @ContactId
        """;

    public static string GetContactsByType => 
        """
        SELECT      C.ContactId,
                    C.FirstName,
                    C.LastName,
                    C.ContactTypeIdentifier,
                    CT.ContactTypeIdentifier AS CTIdentifier,
                    CT.ContactTitle,
                    CD.DeviceId,
                    CD.ContactId AS CDContactId,
                    CD.PhoneTypeIdentifier,
                    CD.PhoneNumber
        FROM        dbo.Contacts AS C
        INNER JOIN  dbo.ContactType AS CT
            ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
        LEFT JOIN   dbo.ContactDevices AS CD
            ON C.ContactId = CD.ContactId
        WHERE       C.ContactTypeIdentifier = @ContactTypeIdentifier
        """;
}

