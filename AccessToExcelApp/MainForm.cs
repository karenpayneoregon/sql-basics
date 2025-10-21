
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

        var fileName = "ExportedData.xlsx";
        ExcelOperations.CreateFile(fileName);
        
        var ops = new DataOperations(DataConnections.Instance.MainConnection);
        IDictionary<string, DataTable> dict = ops.LoadAllTables();

        ExcelOperations.ExportAllToOneWorkbook(dict, fileName, true);
        
        return;
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

}
