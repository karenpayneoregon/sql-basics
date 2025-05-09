﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using CategoriesApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoriesApplication1.Data;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC076A2A72DC");

            entity.Property(e => e.AltText).IsRequired();
            entity.Property(e => e.Ext).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Photo).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}