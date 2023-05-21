using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityLibrary.Data.Configurations;

public partial class BirthdaysConfiguration : IEntityTypeConfiguration<Birthdays>
{
    public void Configure(EntityTypeBuilder<Birthdays> entity)
    {
        entity.Property(e => e.BirthDate).HasColumnType("date");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Birthdays> entity);
}