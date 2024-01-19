using System.Data;
using DapperWithExcel.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using FastMember;
using SpreadsheetLight;
using Color = System.Drawing.Color;


namespace DapperWithExcel.Classes;
internal class SpreadSheetLightOperations
{

    /// <summary>
    /// Create a new Excel file for a specific contact type
    /// </summary>
    /// <param name="fileName">Name of file</param>
    /// <param name="sheetName">Rename Sheet1 to</param>
    /// <param name="contactType">Type of contact</param>
    /// <remarks>
    /// The key here is using FastMember to create a DataTable from the list of contacts
    /// as SpreadSheetLight expects a DataTable for ImportDataTable method
    /// </remarks>
    public static async Task Write(string fileName, string sheetName, ContactType contactType)
    {
        List<Contacts> list = await DataOperation.GetContactOfficePhoneNumbers(contactType);

        await using ObjectReader reader = ObjectReader.Create(list);

        DataTable table = new();
        table.Load(reader);

        /*
         * ReSharper reports possible NullReferenceException on the next three lines.
         * This is not possible to reach these next lines in that case, we would have
         * issues with table.Load instead. So I elect to not be concerned with this Exception.
         */
        table.Columns[nameof(Contacts.ContactId)].ColumnName = "Id";
        table.Columns[nameof(Contacts.Fullname)].ColumnName = "Full name";
        table.Columns[nameof(Contacts.PhoneNumber)].ColumnName = "Phone";

        using SLDocument document = new();
        SLStyle headerStyle = HeaderRowStyle(document);
        
        document.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, true);

        for (int columnIndex = 1; columnIndex < table.Columns.Count; columnIndex++)
        {
            document.AutoFitColumn(columnIndex);
        }

        document.SetCellStyle(1, 1, 1, 4, headerStyle);
        document.SetActiveCell("A2");
        document.RenameWorksheet(SLDocument.DefaultFirstSheetName, sheetName.SplitCamelCase());
        document.SaveAs(fileName);
    }

    /// <summary>
    /// Create the first row format/style
    /// </summary>
    /// <param name="document">Instance of a <see cref="SLDocument"/></param>
    /// <returns>A <see cref="SLStyle"/></returns>
    public static SLStyle HeaderRowStyle(SLDocument document)
    {

        SLStyle headerStyle = document.CreateStyle();

        headerStyle.Font.Bold = true;
        headerStyle.Font.FontColor = Color.White;
        headerStyle.Fill.SetPattern(
            PatternValues.LightGray,
            SLThemeColorIndexValues.Accent1Color,
            SLThemeColorIndexValues.Accent5Color);

        return headerStyle;
    }
}
