using Microsoft.Data.SqlClient;
using Dapper;
using DapperWithExcel.Models;

namespace DapperWithExcel.Classes;
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
