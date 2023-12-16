using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using SpreadsheetLight;
using TransferFromJsonToDatabaseToExcel.Classes.Handlers;
using TransferFromJsonToDatabaseToExcel.Models;

namespace TransferFromJsonToDatabaseToExcel.Classes;

public class DataOperations
{
    public DataOperations()
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        Reset();
    }

    public void Reset()
    {
        using SqlConnection cn = new(ConnectionString());
        cn.Execute("DELETE FROM dbo.Data");
        cn.Execute("DBCC CHECKIDENT (Data, RESEED, 0)");
    }
    public bool AddRecords(List<Container> containers)
    {
        const string statement = 
            """
            INSERT INTO [dbo].[Data]
                  ([InputDate]
                  ,[Specification]
                  ,[Category]
                  ,[Value])
            VALUES
                  (@InputDate,
                  @Specification,
                  @Category,
                  @Value)
            """;
        using SqlConnection cn = new(ConnectionString());
        cn.Execute(statement, containers);

        return true;
    }

    public void ExportFromDatabaseToExcel()
    {
        const string statement =
            """
            SELECT Id,
                   InputDate,
                   Specification,
                   Category,
                   [Value]
            FROM dbo.Data;
            """;
        using SqlConnection cn = new(ConnectionString());
        List<Container> containers = cn.Query<Container>(statement).AsList();

        var table = containers.ToDataTable();
        table.Columns["Id"]!.SetOrdinal(0);
        table.Columns["InputDate"]!.SetOrdinal(1);
        table.Columns["Specification"]!.SetOrdinal(2);
        table.Columns["Category"]!.SetOrdinal(3);
        table.Columns["Value"]!.SetOrdinal(4);


        // ordinal index to the Modified column/property in the model
        int dateColumnIndex = 2;

        // Create an instance of SpreadSheetLight document
        using var document = new SLDocument();

        // Setup first row style for worksheet
        var headerStyle = HeaderStyle(document);

        // Create a format/style for Modified data column
        SLStyle dateStyle = document.CreateStyle();
        dateStyle.FormatCode = "mm-dd-yyyy";

        /*
         * Import DataTable to first row, first column in Sheet1 and include column names
         */

        document.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, true);

        document.SetColumnStyle(dateColumnIndex, dateStyle);

        for (int columnIndex = 1; columnIndex < table.Columns.Count; columnIndex++)
        {
            document.AutoFitColumn(columnIndex);
        }

        document.AutoFitColumn(dateColumnIndex + 1);

        document.SetColumnStyle(dateColumnIndex, dateStyle);

        document.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Imports");

        /*
         * Format the first row with column names
         */
        document.SetCellStyle(1, 1, 1, 6, headerStyle);

        // one row below header
        document.SetActiveCell("A2");

        var fileName = "Imports.xlsx";
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        document.SaveAs("Imports.xlsx");

    }

    /// <summary>
    /// Create the first row format/style
    /// </summary>
    /// <param name="document">Instance of a <see cref="SLDocument"/></param>
    /// <returns>A <see cref="SLStyle"/></returns>
    public static SLStyle HeaderStyle(SLDocument document)
    {

        SLStyle headerStyle = document.CreateStyle();

        headerStyle.Font.Bold = true;
        headerStyle.Font.FontColor = System.Drawing.Color.White;
        headerStyle.Fill.SetPattern(
            PatternValues.LightGray,
            SLThemeColorIndexValues.Accent1Color,
            SLThemeColorIndexValues.Accent5Color);

        return headerStyle;
    }
}
