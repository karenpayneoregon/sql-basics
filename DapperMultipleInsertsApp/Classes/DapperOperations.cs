using Bogus;
using ConsoleConfigurationLibrary.Classes;
using Dapper;
using DapperMultipleInsertsApp.Models;
using Microsoft.Data.SqlClient;

namespace DapperMultipleInsertsApp.Classes;
internal class DapperOperations
{
    /// <summary>
    /// Inserts multiple category records into the database using Dapper.
    /// </summary>
    /// <returns>
    /// The number of rows affected by the insert operation.
    /// </returns>
    /// <remarks>
    /// This method uses a predefined set of mocked category data and executes an SQL 
    /// INSERT statement to add the data to the "Categories" table.
    /// </remarks>
    public static int InsertCategories()
    {
        using var db = new SqlConnection(AppConnections.Instance.MainConnection);
        return db.Execute(
            """
            INSERT INTO Categories(CategoryName, Description) 
            VALUES (@CategoryName, @description)
            """,
            MockedCategories()
        );
    }

    /// <summary>
    /// Retrieves all categories from the Categories table using Dapper.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Categories"/> objects representing all categories in the database.
    /// </returns>
    /// <remarks>
    /// Created by Copilot agent including docs other than remarks
    /// </remarks>
    public static List<Categories> ViewCategories()
    {
        using var db = new SqlConnection(AppConnections.Instance.MainConnection);
        const string sql = "SELECT Id, CategoryName, Description FROM Categories";
        return db.Query<Categories>(sql).AsList();
    }

    private static Categories[] MockedCategories(int count = 10, int? seed = null)
    {
        if (seed is not null)
            Randomizer.Seed = new Random(seed.Value); 



        var faker = new Faker<Categories>()
            .RuleFor(c => c.CategoryName,
                f => f.Commerce.Categories(1)[0])   
            .RuleFor(c => c.Description,
                f => $"{f.Commerce.ProductAdjective()} {f.Commerce.ProductMaterial()} {f.Commerce.Product()}");                      // “Savory steel chair”

        return faker.Generate(count).ToArray();
    }
}