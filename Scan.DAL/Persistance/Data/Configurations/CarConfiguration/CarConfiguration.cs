using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scan.DAL.Models.Car;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");

        builder.HasKey(c => c.CarId);

        builder.Property(c => c.PlateNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(c => c.PlateNumber)
               .IsUnique();

        builder.Property(c => c.Brand)
               .HasMaxLength(50);

        builder.Property(c => c.Model)
               .HasMaxLength(50);

        builder.Property(c => c.Color)
               .HasMaxLength(30);

        builder.Property(c => c.Year)
               .IsRequired();

        builder.HasOne(c => c.User)
               .WithMany(u => u.Cars)
               .HasForeignKey(c => c.UserId);
    }
}
