# About

A very simple example to experiment with Dapper and MS-Access for bulk insert and read back.

- Connection string is read from appsettings.json using NuGet package [ConfigurationLibrary](https://www.nuget.org/packages/ConfigurationLibrary/1.0.6?_src=template).
- [kp.Dapper.Handlers](https://www.nuget.org/packages/kp.Dapper.Handlers/1.0.0?_src=template) NuGet package is used here to handle dates of type [DateOnly](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly?view=net-8.0), if used [DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-8.0) than this package is not required.


