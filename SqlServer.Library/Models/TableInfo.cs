﻿
#nullable disable
namespace SqlServer.Library.Models;
/// <summary>
/// Represents information about a SQL Server table, including its schema, name, and row count.
/// </summary>
public class TableInfo
{
    public string TableSchema { get; set; }
    public string TableName { get; set; }
    public int RowCount { get; set; }
}

