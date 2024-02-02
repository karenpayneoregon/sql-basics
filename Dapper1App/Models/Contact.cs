using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable ConvertConstructorToMemberInitializers

namespace Dapper1App.Models;
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string StateName { get; set; }
    public bool IsNew => this.Id == default(int);
    public List<Address> Addresses { get; } = new List<Address>();
}
public class States
{
    public int Id { get; set; }
    public string StateName { get; set; }
}