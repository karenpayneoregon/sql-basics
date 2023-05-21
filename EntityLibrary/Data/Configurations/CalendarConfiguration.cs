using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityLibrary.Data.Configurations;

public partial class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> entity)
    {
        entity.HasKey(e => e.CalendarDate);

        entity.Property(e => e.CalendarDate).HasColumnType("date");
        entity.Property(e => e.Description)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.DayOfWeekName)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.FirstDateOfMonth).HasColumnType("date");
        entity.Property(e => e.FirstDateOfQuarter).HasColumnType("date");
        entity.Property(e => e.FirstDateOfWeek).HasColumnType("date");
        entity.Property(e => e.FirstDateOfYear).HasColumnType("date");
        entity.Property(e => e.LastDateOfMonth).HasColumnType("date");
        entity.Property(e => e.LastDateOfQuarter).HasColumnType("date");
        entity.Property(e => e.LastDateOfWeek).HasColumnType("date");
        entity.Property(e => e.LastDateOfYear).HasColumnType("date");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Calendar> entity);
}