# About

In this article learn how to create Excel spreadsheet documents from reading data from two SQL-Server table from a modified version of Microsoft NorthWind database using NuGet package [Dapper](https://www.nuget.org/packages/Dapper) and [SpreadSheetLight](https://www.nuget.org/packages/SpreadsheetLight.Cross.Platform/3.5.1?_src=template) to create and populate the spreadsheet files.

New to Dapper, check out [Dapper Tutorial](https://dev.to/karenpayneoregon/using-dapper-c-part-1-a-eec)

Typically, developers will attempt to create and populate the spreadsheet using Office automation although using Office automation tends to be problematic on several levels and generally does not work well on web servers.

Do a search on the web for C# list to Excel and at the top of the list will be Office automation, avoid these solutions.

There are many Excel libraries for C# while some are free while others are paid for libraries when on a budget a decent library is SpreadSheetLight which does not offer an easy way to import data in a single method other than by passing a DataTable to a specific method so some developers may discount using SpreadSheetLight.

To get around this there is a NuGet package [FastMember](https://www.nuget.org/packages/FastMember/1.5.0?_src=template) which allows a strong type list to create a DataTable which can be passed to SpreadSheetLight.

## SQL

We want to get office phone numbers for contacts in the NorthWind database, the following SQL will return the data we need by contact type.

First write the SELECT statement in SSMS (SQL-Server Management Studio).

```sql
DECLARE @PhoneTypeIdentifier INT =3
DECLARE @ContactTypeIdentifier INT = 1

SELECT C.ContactId,
       C.FullName,
       CD.PhoneNumber
FROM dbo.Contacts AS C
    INNER JOIN dbo.ContactDevices AS CD
        ON C.ContactId = CD.ContactId
WHERE (CD.PhoneTypeIdentifier = @PhoneTypeIdentifier)
      AND (C.ContactTypeIdentifier = @ContactTypeIdentifier)
ORDER BY C.LastName;
```

Once the statement works, place the statement into a read-only  property.

```csharp
internal class SqlStatements
{
    public static string GetContactOwners => 
        """
        SELECT C.ContactId,
               C.FullName,
               CD.PhoneNumber
        FROM dbo.Contacts AS C
            INNER JOIN dbo.ContactDevices AS CD
                ON C.ContactId = CD.ContactId
        WHERE (CD.PhoneTypeIdentifier = @PhoneTypeIdentifier)
              AND (C.ContactTypeIdentifier = @ContactTypeIdentifier)
        ORDER BY C.LastName;
        """;
}
```

## Create a model class to hold the data.

```csharp
public class Contacts
{
    public int ContactId { get; set; }
    // computed column
    public string Fullname { get; set; }
    public string PhoneNumber { get; set; }
}
```

Create a method for Dapper to read the data.

```csharp
internal class DataOperation
{
    /// <summary>
    /// Get contact office phone number by contact type
    /// </summary>
    /// <param name="contactType"><see cref="ContactType"/></param>
    public static async Task<List<Contacts>> GetContactOfficePhoneNumbers(ContactType contactType)
    {
        await using SqlConnection cn = new(ConnectionString());
        return (List<Contacts>)await cn.QueryAsync<Contacts>(SqlStatements.GetContactOwners,
            new
            {
                PhoneTypeIdentifier = 3,
                ContactTypeIdentifier = contactType
            });
    }
}
```

## Code to work with Excel

The following code will create a spreadsheet file and populate the data from the list.

 1. Using DataOperation.GetContactOfficePhoneNumbers to contact data for a specific contact type.
 1. Using FastMember to create a DataTable from the list of contacts.
 1. Change the column names to something more readable.
 1. Create an instance of SpreadSheetLight document
 1. Create the style for the first row in the sheet
 1. Import the DataTable into the spreadsheet starting at the first row and including column names.
 1. Auto fit the columns to fit data
 1. Set the style for the first row
 1. Make the first cell active in the first row of data, A2.
 1. Change the default worksheet name from Sheet1 to the contact type name.
 1. Save the file.
    

```csharp
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

        table.Columns["ContactId"]!.ColumnName = "Id";
        table.Columns["FullName"]!.ColumnName = "Full name";
        table.Columns["PhoneNumber"]!.ColumnName = "Phone";

        using SLDocument document = new();
        SLStyle headerStyle = HeaderRowStyle(document);
        
        document.ImportDataTable(1, SLConvert.ToColumnIndex("A"), table, true);

        for (int columnIndex = 1; columnIndex < table.Columns.Count; columnIndex++)
        {
            document.AutoFitColumn(columnIndex);
        }

        document.SetCellStyle(1, 1, 1, 4, headerStyle);
        document.SetActiveCell("A2");
        document.RenameWorksheet(SLDocument.DefaultFirstSheetName, 
            sheetName.SplitCamelCase());
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
```

## Code to call the method above

```csharp
internal partial class Program
{
    static async Task Main(string[] args)
    {

        AnsiConsole.MarkupLine("[cyan]Creating[/]");

        foreach (var enumValue in Enum.GetValues(typeof(ContactType)))
        {
            ContactType current = ConvertFromObject<ContactType>((ContactType)enumValue);
            Console.WriteLine($"   {current}.xlsx");
            await SpreadSheetLightOperations.Write($"{current}.xlsx", $"{current}", current);
        }

        ExitPrompt();
    }
}
```

 1. Iterate through the enum values for ContactType.
 2. Create the spreadsheet file for each contact type.

 ## Summary

 In this article techniques were shown to read data from a database into Excel spreadsheets easily.

 ## Where to go from here

 Take time to explore both Dapper and [SpreadSheetLight](https://spreadsheetlight.com/).

 On SpreadSheetLight home page, download the help file and take time to explore the many features of this library.
