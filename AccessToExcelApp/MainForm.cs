
using AccessToExcelApp.Classes;
using AccessToExcelApp.Classes.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace AccessToExcelApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        return;
        var fileName = "ExportedData.xlsx";
        ExcelOperations.CreateFile(fileName);

        var ops = new DataOperations(DataConnections.Instance.MainConnection);
        IDictionary<string, DataTable> dict = ops.LoadAllTables();

        ExcelOperations.ExportAllToOneWorkbook(dict, fileName, true);


        foreach (var (key, dataTable) in dict)
        {
            Debug.WriteLine($"Table: {key}");
            Debug.WriteLine($"Table: {key}, Rows: {dataTable.Rows.Count}");


            var schema = ops.GetTableSchema(key);

            foreach (DataRow row in schema.Rows)
            {
                var dataTypeValue = Convert.ToInt32(row["DATA_TYPE"]);
                string csType = OleDbTypeMapper.ToCSharpType(dataTypeValue);
                Debug.WriteLine($"{row["COLUMN_NAME"],-20} {row["DATA_TYPE"],-8} {csType}");
            }

            Debug.WriteLine("");

        }
    }

    private void FindDatabasesButton_Click(object sender, EventArgs e)
    {
        var (mdbFiles, accdbFiles) = AccessFileFinder.GetAccessFiles("C:\\OED\\PROD\\Back-end");

        foreach (var mdbFile in mdbFiles)
        {
            //Debug.WriteLine($"Found MDB file: {mdbFile}");
            TestDatabaseConnection(mdbFile);
        }

        Debug.WriteLine("");
        foreach (var accdbFile in accdbFiles)
        {
            //Debug.WriteLine($"Found ACCDB file: {accdbFile}");
            TestDatabaseConnection(accdbFile);
        }
    }

    private void LocalExportTestButton_Click(object sender, EventArgs e)
    {
        var fileName = "ExportedData.xlsx";
        ExcelOperations.CreateFile(fileName);

        var ops = new DataOperations(DataConnections.Instance.MainConnection);
        IDictionary<string, DataTable> dict = ops.LoadAllTables();

        ExcelOperations.ExportAllToOneWorkbook(dict, fileName, true);
    }

    /// <summary>
    /// Tests the connection to a Microsoft Access database using the provided file path.
    /// </summary>
    /// <param name="source">
    /// The file path of the Microsoft Access database to test. 
    /// This can be an .mdb or .accdb file.
    /// </param>
    /// <remarks>
    /// The method attempts to establish a connection to the database using the 
    /// Microsoft ACE OLEDB provider. If the connection is successful, a success message 
    /// is logged. If the connection fails, an error message is logged with details.
    /// </remarks>
    /// <exception cref="OleDbException">
    /// Thrown when there is an issue with the database connection.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when an unexpected error occurs during the connection attempt.
    /// </exception>
    private static void TestDatabaseConnection(string source)
    {
        var connectionString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={source};Persist Security Info=False;";

        try
        {
            using var connection = new OleDbConnection(connectionString);
            connection.Open();
            Debug.WriteLine($"Successfully opened: {source}");
        }
        catch (OleDbException ex)
        {
            Debug.WriteLine($" Failed to open: {source} - {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error opening {source}: {ex}");
        }
    }
}
