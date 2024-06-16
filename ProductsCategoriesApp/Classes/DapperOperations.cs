using Dapper;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ProductsCategoriesApp.Classes
{
    public static class DapperOperations
    {
        public static IEnumerable<Contact> GetContacts()
        {
            using SqlConnection cn = new(ConnectionString());
            string query = """
                           SELECT c.ContactId, c.FirstName, c.LastName, ct.ContactTypeIdentifier, cd.ContactDevices, pt.PhoneType 
                           FROM Contacts c 
                           JOIN ContactType ct ON c.ContactTypeIdentifier = ct.ContactTypeIdentifier 
                           JOIN ContactDevices cd ON c.ContactId = cd.ContactId 
                           JOIN PhoneType pt ON cd.PhoneTypeIdentifier = pt.PhoneTypeIdentifier
                           """ ;

            return cn.Query<Contact>(query);
        }


    }

    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string ContactDevices { get; set; }
        public string PhoneType { get; set; }
    }
}
