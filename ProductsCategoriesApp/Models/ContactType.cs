﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategoriesApp.Models;

public partial class ContactType
{
    [Key]
    public int ContactTypeIdentifier { get; set; }

    public string ContactTitle { get; set; }

    [InverseProperty("ContactTypeIdentifierNavigation")]
    [JsonIgnore]
    public virtual ICollection<Contacts> Contacts { get; } = new List<Contacts>();

    [InverseProperty("ContactTypeIdentifierNavigation")]
    [JsonIgnore]
    public virtual ICollection<Customers> Customers { get; } = new List<Customers>();

    [InverseProperty("ContactTypeIdentifierNavigation")]
    [JsonIgnore]
    public virtual ICollection<Employees> Employees { get; } = new List<Employees>();
}