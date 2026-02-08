using NorthWindSqlLiteApp1.Classes.Extensions;
using NorthWindSqlLiteApp1.Data;

namespace NorthWindSqlLiteApp1.Classes.Core;

using static Core.SpectreConsoleHelpers;

internal class UtilityCode
{
    /// <summary>
    /// Retrieves and displays the names of all model types defined in the database context.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="Context"/> class to access the database context
    /// and the <see cref="EntityExtensions.GetModelTypes"/> extension method
    /// to retrieve the model types. The names of the models are printed to the console.
    /// </remarks>
    public static void GetModelNames()
    {
        PrintPink();

        using (var context = new Context())
        {
            var modelTypes = context.GetModelTypes();

            foreach (var modelName in modelTypes)
            {
                Console.WriteLine(modelName.Name);
            }
        }

        Console.WriteLine();
    }


}
