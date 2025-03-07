﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlLiteSample2.Data;
using SqlLiteSample2.Models;
using System;
using System.Collections.Generic;

namespace SqlLiteSample2.Data.Configurations
{
    public partial class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> entity)
        {
            entity.HasKey(e => e.CategoryID);

            entity.Property(e => e.CategoryName)
            .IsRequired()
            .UseCollation("NOCASE")
            .HasColumnType("nvarchar(15)");
            entity.Property(e => e.Description)
            .UseCollation("NOCASE")
            .HasColumnType("text(1073741823)");
            entity.Property(e => e.Picture).HasColumnType("blob(2147483647)");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Categories> entity);
    }
}
