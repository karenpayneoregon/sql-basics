namespace GitHubSamples.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Statements to read reference tables for Categories, ContactType and Countries tables.
    /// </summary>
    public static string ReferenceTableStatements =>
        """
        SELECT CategoryID,CategoryName FROM dbo.Categories;
        SELECT ContactTypeIdentifier,ContactTitle FROM dbo.ContactType;
        SELECT CountryIdentifier,[Name] FROM dbo.Countries;
        """;

    /// <summary>
    /// Read each category primary key and category name
    /// </summary>
    public static string GetCategories =>
        """
        SELECT CategoryID,CategoryName FROM dbo.Categories;
        """;
}