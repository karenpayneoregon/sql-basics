using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Data;


namespace AccessToExcelApp.Classes;
internal class ExcelOperations
{
    public static void ExportToExcel(DataTable table, string fileName, bool includeHeader, string sheetName)
    {
        using var document = new SLDocument(fileName);

        // import to first row, first column
        document.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, includeHeader);

        // give sheet a useful name
        document.RenameWorksheet(SLDocument.DefaultFirstSheetName, sheetName);

        document.Save();
    }

    public static void CreateFile(string fileName)
    {
        using var document = new SLDocument();
        document.SaveAs(fileName);

    }

    public static void ExportAllToOneWorkbook(IDictionary<string, DataTable> tablesByName, string fileName, bool includeHeader)
    {
        using SLDocument doc = new(); // starts with "Sheet1"
        
        SLStyle dateStyle = doc.CreateStyle();
        dateStyle.FormatCode = "mm-dd-yyyy";

        bool first = true;
        foreach (var (sheetName, table) in tablesByName)
        {
            if (first)
            {

                doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, sheetName);
                doc.SelectWorksheet(sheetName);
                first = false;
            }
            else
            {
                doc.AddWorksheet(sheetName);
                doc.SelectWorksheet(sheetName);
            }
            
            doc.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, includeHeader);

            doc.SetCellStyle(1, 1, 1, table.Columns.Count, HeaderStyle(doc));
            doc.SetActiveCell("A2");

            // Identify DateTime columns by ordinal
            for (int index = 0; index < table.Columns.Count; index++)
            {
                if (table.Columns[index].DataType == typeof(DateTime))
                {
                    doc.SetColumnStyle(index +1, dateStyle);
                    doc.AutoFitColumn(index +1);
                }
            }
        }

        doc.SaveAs(fileName);
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
            SLThemeColorIndexValues.Dark2Color);

        return headerStyle;
    }
}
