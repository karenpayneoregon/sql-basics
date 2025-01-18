using System.Text.Json;
using StudentHelpApp.Models;

namespace StudentHelpApp.Classes;

/// <summary>
/// Works with data via a json file
/// </summary>
public class JsonOperations
{
    /// <summary>
    /// File to store data
    /// </summary>
    private static string _fileName = "Items.json";

    /// <summary>
    /// List of items from file
    /// </summary>
    public static List<Item> Items { get; set; }

    /// <summary>
    /// Create or recreate file with data
    /// </summary>
    /// <param name="createNew"></param>
    public static void CreateFile(bool createNew = true)
    {
        if (!createNew)
        {
            if (File.Exists(_fileName))
            {
                ReadAll();
            }
            return;
        }

        var serializerOptions = JsonSerializerOptions();
        //List<Item> list = new List<Item>
        //{
        //    new Item() { Id = 1, Name = "Item 1", Description = "Description 1" },
        //    new Item() { Id = 2, Name = "Item 2", Description = "Description 2" }
        //};
        var list = BogusOperations.Items();
        for (int index = 0; index < list.Count; index++)
        {
            list[index].Id = index + 1;
        }
        File.WriteAllText(_fileName, JsonSerializer.Serialize(list, serializerOptions));
        ReadAll();
    }

    /// <summary>
    /// Determine if the list has items
    /// </summary>
    /// <returns></returns>
    public static bool HasItems() => Items.Count > 0;

    private static JsonSerializerOptions JsonSerializerOptions()
    {
        var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        return serializerOptions;
    }

    /// <summary>
    /// Read all items from the json file
    /// </summary>
    public static void ReadAll()
    {
        if (!File.Exists(_fileName))
        {
            CreateFile(true);
        }
        var json = File.ReadAllText(_fileName);
        Items = JsonSerializer.Deserialize<List<Item>>(json);
    }

    /// <summary>
    /// Add a new <seealso cref="Item"/> to the list and file
    /// </summary>
    /// <param name="item"></param>
    public static void Add(Item item)
    {
        Items.Add(item);
        File.WriteAllText(_fileName, JsonSerializer.Serialize(Items, JsonSerializerOptions()));
    }

    /// <summary>
    /// Locate and return a <seealso cref="Item"/> by id
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Found item or null if not found</returns>
    public static Item Find(int id) => Items.Count > 0 ? Items.FirstOrDefault(x => x.Id == id) : null;

    /// <summary>
    /// Remove a <seealso cref="Item"/> from the json file and items list
    /// </summary>
    /// <param name="item">Valid item</param>
    public static void Remove(Item item)
    {
        if (Items.Count <= 0) return;
        Items.Remove(item);
        File.WriteAllText(_fileName, JsonSerializer.Serialize(Items, JsonSerializerOptions()));
        ReadAll();
    }
    /// <summary>
    /// Update an existing item, save to file
    /// </summary>
    public static void Update()
    {
        if (Items.Count > 0)
        {
            File.WriteAllText(_fileName, JsonSerializer.Serialize(Items, JsonSerializerOptions()));
            ReadAll();
        }
    }
}