using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperStoredProcedures1.Models;
public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Gender { get; set; }
    public int GenderId { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";

}
