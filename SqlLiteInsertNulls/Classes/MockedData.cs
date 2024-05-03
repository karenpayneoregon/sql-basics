namespace SqlLiteInsertNulls.Classes;

public class MockedData
{
    public static List<InsertExample> GetMockedData() =>
    [
        new() { Column1 = null, Column2 = 2, Column3 = 3, Column4 = "A", Column5 = null },
        new() { Column1 = 4, Column2 = null, Column3 = 6, Column4 = null, Column5 = "D" },
        new() { Column1 = 7, Column2 = 8, Column3 = 9, Column4 = "E", Column5 = "F" },
        new() { Column1 = 28, Column2 = 29, Column3 = 30, Column4 = "S", Column5 = "T" }
    ];
}