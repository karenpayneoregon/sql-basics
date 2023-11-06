
namespace DapperWithExcel.Models
{
    public class Contacts
    {
        public int ContactId { get; set; }
        // computed column
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
    }
}