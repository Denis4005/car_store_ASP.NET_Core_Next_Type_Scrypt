using Backend.Core.Models;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Dataaccess.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Brand).HasMaxLength(Car.MAX_TITLE_LENGTH).IsRequired();
        builder.Property(b => b.Model).HasMaxLength(Car.MAX_TITLE_LENGTH).IsRequired();
        builder.Property(b => b.HorsePower).IsRequired();
        builder.Property(b => b.Color).HasMaxLength(Car.MAX_TITLE_LENGTH).IsRequired();
        builder.Property(b => b.Price).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.ToTable("Car", t =>
        {
            t.HasCheckConstraint("CK_Car_HorsePower", "\"HorsePower\" > 0");
            t.HasCheckConstraint("CK_Car_Price", "\"Price\" >= 0");
        });

        builder.HasOne(c => c.User).WithMany(u => u.Car).HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.UserId);

    }
}

