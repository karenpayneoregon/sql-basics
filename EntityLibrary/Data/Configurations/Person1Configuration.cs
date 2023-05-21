using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityLibrary.Data.Configurations;

public partial class Person1Configuration : IEntityTypeConfiguration<Person1>
{
    public void Configure(EntityTypeBuilder<Person1> entity)
    {
        entity.Property(e => e.BirthDate).HasColumnType("date");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Person1> entity);
}