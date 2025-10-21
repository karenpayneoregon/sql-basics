using Dapper;
using kp.Dapper.Handlers;
using System.Data;
using System.Data.OleDb;

namespace AccessToExcelApp.Classes;
internal class DataOperations
{
    private readonly string _connectionString;
    public DataOperations(string connectionString)
    {
        _connectionString = connectionString;
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
    }

    /// <summary>
    /// Returns schema information for the given table.
    /// </summary>
    /// <remarks>
    /// Includes column name, ordinal position, data type, max length, and nullability.
    /// </remarks>
    public DataTable GetTableSchema(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("Table name is required.", nameof(tableName));

        using var conn = new OleDbConnection(_connectionString);
        conn.Open();

        // Use OleDb schema retrieval
        var restrictions = new string[4];
        restrictions[2] = tableName; // TABLE_NAME restriction

        var schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, restrictions);

        if (schema == null || schema.Rows.Count == 0)
            throw new InvalidOperationException($"No schema information found for table '{tableName}'.");

        // Optional: sort columns by ordinal position
        var view = new DataView(schema)
        {
            Sort = "ORDINAL_POSITION ASC"
        };

        return view.ToTable();
    }

    /// <summary>
    /// Loads all non-system tables into a dictionary of DataTables keyed by table name.
    /// </summary>
    /// <param name="includeLinkedTables">True to include linked tables; false to load only local tables.</param>
    /// <param name="includeSystemTables">True to include MSys* and hidden/system tables. Default false.</param>
    public IDictionary<string, DataTable> LoadAllTables(bool includeLinkedTables = true, bool includeSystemTables = false)
    {
        using var conn = new OleDbConnection(_connectionString);
        conn.Open();

        var tables = GetTableNames(conn, includeLinkedTables, includeSystemTables);

        var result = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);

        foreach (var tableName in tables)
        {
            var sql = $"SELECT * FROM [{tableName}]";

            using var reader = conn.ExecuteReader(sql);
            var dt = new DataTable(tableName);
            dt.Load(reader);
            result[tableName] = dt;
        }

        return result;
    }

    /// <summary>
    /// Loads a single table into a DataTable.
    /// </summary>
    public DataTable LoadTable(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("Table name is required.", nameof(tableName));

        using var conn = new OleDbConnection(_connectionString);
        conn.Open();

        var sql = $"SELECT * FROM [{tableName}]";
        using var reader = conn.ExecuteReader(sql);
        var dt = new DataTable(tableName);
        dt.Load(reader);
        return dt;
    }

    /// <summary>
    /// Returns user table names from the Access schema.
    /// </summary>
    private static IEnumerable<string> GetTableNames(OleDbConnection connection, bool includeLinkedTables, bool includeSystemTables)
    {
        // OleDb schema columns for "Tables" are:
        // TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE ("TABLE" | "VIEW" | "SYSTEM TABLE" | "LINK")
        using var schema = connection.GetSchema("Tables");
        var rows = schema.Rows.Cast<DataRow>();

        // Filter by type
        var validTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "TABLE" };
        if (includeLinkedTables)
            validTypes.Add("LINK"); // Access exposes linked tables as type LINK on ACE providers

        var filtered = rows.Where(r =>
        {
            var type = (r["TABLE_TYPE"] as string) ?? "";
            if (!validTypes.Contains(type)) return false;

            var name = (r["TABLE_NAME"] as string) ?? "";

            // Exclude Access system tables unless requested
            if (!includeSystemTables && name.StartsWith("MSys", StringComparison.OrdinalIgnoreCase))
                return false;

            // Exclude temp/hidden weirdness
            if (!includeSystemTables && (name.StartsWith("~", StringComparison.Ordinal) || name.StartsWith("USys", StringComparison.OrdinalIgnoreCase)))
                return false;

            // Some providers return hidden tables as type "TABLE"—additional guard:
            if (!includeSystemTables)
            {
                // Optionally skip replication/system artifacts by heuristic
                if (name.EndsWith("_Conflict", StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        });

        // Sorted for stability
        return filtered
            .Select(r => (r["TABLE_NAME"] as string)!)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(n => n, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}
