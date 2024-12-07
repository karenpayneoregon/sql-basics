# About

This is a sample application that works wit DateOnly and TimeOnly using EF Core 9.

Original code from [here](https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Miscellaneous/NewInEFCore9/DateOnlyTimeOnlySample.cs) which has been modified to use SQL-Server, removed code for SqlLite.

Data generated (ignore table name)

```sql
CREATE TABLE #temptable (Id INT,
                         Name NVARCHAR(MAX),
                         Founded DATE,
                         LastVisited DATETIME2(7),
                         LegacyTime TIME(7),
                         OpeningHours NVARCHAR(MAX));

INSERT INTO #temptable (Id,
                        Name,
                        Founded,
                        LastVisited,
                        LegacyTime,
                        OpeningHours)
VALUES (1, N'Stowe School', N'1923-05-11', N'0001-01-01T00:00:00', N'00:00:00',
        N'[{"ClosesAt":null,"DayOfWeek":0,"OpensAt":null},{"ClosesAt":"18:00:00.0000000","DayOfWeek":1,"OpensAt":"08:00:00.0000000"},{"ClosesAt":"18:00:00.0000000","DayOfWeek":2,"OpensAt":"08:00:00.0000000"},{"ClosesAt":"18:00:00.0000000","DayOfWeek":3,"OpensAt":"08:00:00.0000000"},{"ClosesAt":"18:00:00.0000000","DayOfWeek":4,"OpensAt":"08:00:00.0000000"},{"ClosesAt":"18:00:00.0000000","DayOfWeek":5,"OpensAt":"08:00:00.0000000"},{"ClosesAt":"17:00:00.0000000","DayOfWeek":6,"OpensAt":"08:00:00.0000000"}]'),
       (2, N'Farr High School', N'1964-05-01', N'0001-01-01T00:00:00', N'00:00:00',
        N'[{"ClosesAt":null,"DayOfWeek":0,"OpensAt":null},{"ClosesAt":"15:35:00.0000000","DayOfWeek":1,"OpensAt":"08:45:00.0000000"},{"ClosesAt":"15:35:00.0000000","DayOfWeek":2,"OpensAt":"08:45:00.0000000"},{"ClosesAt":"15:35:00.0000000","DayOfWeek":3,"OpensAt":"08:45:00.0000000"},{"ClosesAt":"15:35:00.0000000","DayOfWeek":4,"OpensAt":"08:45:00.0000000"},{"ClosesAt":"12:50:00.0000000","DayOfWeek":5,"OpensAt":"08:45:00.0000000"},{"ClosesAt":null,"DayOfWeek":6,"OpensAt":null}]');


```