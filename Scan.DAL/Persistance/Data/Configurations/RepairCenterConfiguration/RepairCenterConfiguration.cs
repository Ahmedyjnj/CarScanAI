using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RepairCenterConfiguration : IEntityTypeConfiguration<RepairCenter>
{
    public void Configure(EntityTypeBuilder<RepairCenter> builder)
    {
        builder.ToTable("RepairCenters");

        builder.HasKey(r => r.CenterId);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(r => r.Phone)
               .HasMaxLength(20);

        builder.Property(r => r.Address)
               .HasMaxLength(255);

        builder.Property(r => r.Location)
               .HasMaxLength(100);

      

        builder.Property(r => r.IsActive)
               .HasDefaultValue(true);
    }
}
