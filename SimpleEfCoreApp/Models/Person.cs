﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using SimpleEfCoreApp.Classes;

namespace SimpleEfCoreApp.Models;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    /// <summary>
    /// If set to DateOnly then the <see cref="CalendarColumn"/> would not handle DateOnly
    /// and nothing easy to fix it.
    /// </summary>
    public DateTime? BirthDate { get; set; }
}