using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAddColumnWithValuesApp.Classes;
/// <summary>
/// SQL Statements
/// </summary>
/// <remarks>
/// Sure these can be stored procedures
/// </remarks>
internal class SqlStatements
{
    public static string SelectStatement => 
        """
        SELECT Id,
               FirstName,
               LastName,
               EmailAddress,
               Pin,
               ActiveMember,
               JoinDate,
               PhoneNumber
        FROM dbo.UserDetail;
        """;

    public static string DropPhoneNumberColumnStatement => 
        """
        IF EXISTS
        (
            SELECT 1
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = 'UserDetail'
                  AND COLUMN_NAME = 'PhoneNumber'
                  AND TABLE_SCHEMA = 'DBO'
        )
        BEGIN
            ALTER TABLE dbo.UserDetail DROP CONSTRAINT U_UserDetail_PhoneNumber;
            ALTER TABLE dbo.UserDetail DROP COLUMN PhoneNumber;
        END;
        """;

    public static string AddPhoneNumberColumnStatement => 
        """
        ALTER TABLE dbo.UserDetail
        ADD PhoneNumber NVARCHAR(MAX) NULL CONSTRAINT U_UserDetail_PhoneNumber
                                           DEFAULT ('(none)') WITH VALUES;
        """;

    public static string ColumnsForTable => 
        """
        SELECT COLUMN_NAME,
               ORDINAL_POSITION,
               COLUMN_DEFAULT,
               IS_NULLABLE,
               DATA_TYPE
        FROM MockupApplication1.INFORMATION_SCHEMA.COLUMNS
        ORDER BY ORDINAL_POSITION;
        """;

    public static string ColumnNames =>
        """
        SELECT
            CASE WHEN COL_LENGTH('UserDetail','PhoneNumber') IS NOT NULL
            THEN 'TRUE'
            ELSE 'FALSE'
        END
        """;

}
