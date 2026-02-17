using ConsoleConfigurationLibrary.Classes;
using Dapper;
using Microsoft.Data.SqlClient;
using NorthWindSqlLiteApp1.Models;
using Serilog;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace NorthWindSqlLiteApp1.Classes.MemberAccess;
/// <summary>
/// Provides sample methods to demonstrate safe member access techniques in C#.
/// </summary>
/// <remarks>
/// This class focuses on showcasing <b>best practices</b> for accessing object members
/// while handling potential null values. It includes examples of using null-conditional
/// and null-coalescing operators to ensure safe and efficient code execution.
/// </remarks>
internal class MemberAccessSamples
{
    /// <summary>
    /// Demonstrates the usage of <b>null-conditional</b> and <b>null-coalescing</b> operators
    /// to safely access object members and handle potential null values.
    /// </summary>
    /// <remarks>
    /// This method showcases the following:
    /// <list type="bullet">
    /// <item>The behavior of accessing a null object's member, which results <br/>in a <see cref="NullReferenceException"/>.</item>
    /// <item>The use of the null-conditional operator (<c>?.</c>) to safely access<br/> members of potentially null objects.</item>
    /// <item>The use of the null-coalescing operator (<c>??</c>) to provide a default<br/> value when a null value is encountered.</item>
    /// </list>
    /// <br/><br/>
    /// <para>
    ///     <b>References</b>:
    /// </para>
    /// 
    /// <para>
    ///      <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-">Member access operators</see>
    /// </para>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator">null-coalescing operators </see>
    /// </remarks>
    public static void NullCondition()
    {
        Customers customer1 = new Customers { CompanyName = "Customer 1" };

        try
        {
            // since CountryIdentifierNavigation is null, this will throw a NullReferenceException
            var country0 = customer1.CountryIdentifierNavigation.Name;
        }
        catch (Exception exception)
        {
            Debugger.Break();
            // TODO
        }


        if (customer1.CountryIdentifierNavigation is null)
        {
            Console.WriteLine("Country is unknown");
        }
        else
        {
            Console.WriteLine($"Country is {customer1.CountryIdentifierNavigation.Name}");
        }

        // The null conditional operator (?.) allows you to safely access members.
        var country1 = customer1.CountryIdentifierNavigation?.Name;

        /*
         * The null-coalescing operator ?? returns the value of its left-hand operand if it's not null.
         * Otherwise, it evaluates the right-hand operand and returns its result.
         */
        var country2 = customer1.CountryIdentifierNavigation?.Name ?? "Unknown";

        // Here we know that CountryIdentifierNavigation is not null,
        // so we can safely access its Name property.
        customer1.CountryIdentifier = 20;
        customer1.CountryIdentifierNavigation = new Countries { Name = "USA" };
        var country3 = customer1.CountryIdentifierNavigation.Name;

        Debugger.Break();
    }

    public static async Task<int> GetCustomerCountUsingDapper()
    {
        try
        {
            const string SqlServerQuery =
                """
                SELECT Max(Len(CompanyName))
                FROM Customers
                """;

            const string SqliteQuery =
                """
                SELECT MAX(LENGTH(CompanyName))
                FROM Customers;
                """;


            await using SqliteConnection cn = new(AppConnections.Instance.MainConnection);

            var result = await cn.ExecuteScalarAsync<int?>(SqliteQuery);

            return result ?? -1;
        }
        catch (Exception ex)
        {
            // uses SeriLog
            Log.Error(ex, "Error occurred while getting the count.");
            return -1;
        }
    }
}
