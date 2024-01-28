# About

Upgrading from Framework 4.8, Dapper to EF Core NET 8.

This project came from the .NET Framework 4.8 project DapperSimple which uses Dapper while this project uses EF Core 8.

The model Person BirthDate property should be DateOnly but that would mean losing the custom DataGridViewColumn for the calendar functionality. None of the code is affected going to and from the database as SQL Server understands how to handle the DateTime to Date.

