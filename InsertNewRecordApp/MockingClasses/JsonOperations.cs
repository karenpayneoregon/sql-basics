using System.Text.Json;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.MockingClasses;

/*
 * Data.json has fixed data for Person model
 */

internal class JsonOperations
{
    /// <summary>
    /// Read people from a static json file
    /// </summary>
    public static List<Person> GetAll() 
        => JsonSerializer.Deserialize<List<Person>>(
            File.ReadAllText("Data.json"));
}
