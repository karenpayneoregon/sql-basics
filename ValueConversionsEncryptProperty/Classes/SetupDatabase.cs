using Microsoft.EntityFrameworkCore;

namespace ValueConversionsEncryptProperty.Classes;

internal class SetupDatabase
{

    /// <summary>
    /// Deletes the existing database and recreates it to ensure a clean state.
    /// </summary>
    /// <param name="context">
    /// The <see cref="DbContext"/> instance representing the database to be cleaned.
    /// </param>
    /// <remarks>
    /// This method ensures that the database is deleted and then recreated, which is useful for scenarios 
    /// where a fresh database state is required, such as during testing or initial setup.
    /// </remarks>
    public static void CleanDatabase(DbContext context)
    {

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

    }

}