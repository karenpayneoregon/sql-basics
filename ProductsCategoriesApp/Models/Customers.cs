﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategoriesApp.Models;

[Index("City", Name = "City")]
[Index("CompanyName", Name = "CompanyName")]
[Index("ContactId", Name = "IX_Customers_ContactId")]
[Index("ContactTypeIdentifier", Name = "IX_Customers_ContactTypeIdentifier")]
[Index("CountryIdentifier", Name = "IX_Customers_CountryIdentifier")]
[Index("PostalCode", Name = "PostalCode")]
[Index("Region", Name = "Region")]
public partial class Customers
{
    [Key]
    public int CustomerIdentifier { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    [Required]
    [StringLength(40)]
    public string CompanyName { get; set; }

    public int? ContactId { get; set; }

    [StringLength(60)]
    public string Street { get; set; }

    [StringLength(15)]
    public string City { get; set; }

    [StringLength(15)]
    public string Region { get; set; }

    [StringLength(10)]
    public string PostalCode { get; set; }

    public int? CountryIdentifier { get; set; }

    [StringLength(24)]
    public string Phone { get; set; }

    [StringLength(24)]
    public string Fax { get; set; }

    public int? ContactTypeIdentifier { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("ContactId")]
    [InverseProperty("Customers")]
    public virtual Contacts Contact { get; set; }

    [ForeignKey("ContactTypeIdentifier")]
    [InverseProperty("Customers")]
    public virtual ContactType ContactTypeIdentifierNavigation { get; set; }

    [ForeignKey("CountryIdentifier")]
    [InverseProperty("Customers")]
    public virtual Countries CountryIdentifierNavigation { get; set; }

    [InverseProperty("CustomerIdentifierNavigation")]
    public virtual ICollection<Orders> Orders { get; } = new List<Orders>();
}