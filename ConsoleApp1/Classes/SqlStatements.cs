using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ConsoleApp1.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Get all records from Person table
    /// </summary>
    public static string ReadPeople =>
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person;
        """;
}
