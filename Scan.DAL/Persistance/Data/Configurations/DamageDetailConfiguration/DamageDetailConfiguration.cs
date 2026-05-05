using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DamageDetailConfiguration : IEntityTypeConfiguration<DamageDetail>
{
    public void Configure(EntityTypeBuilder<DamageDetail> builder)
    {
        builder.ToTable("DamageDetails");

        builder.HasKey(dd => dd.DamageDetailId);

        builder.Property(dd => dd.DamageType)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(dd => dd.SeverityLevel)
               .HasMaxLength(30);

        builder.Property(dd => dd.DamagedAreaLocation)
               .HasMaxLength(50);

        builder.Property(dd => dd.ConfidenceScore)
               .HasPrecision(5, 2);

        builder.Property(dd => dd.EstimatedCost)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(dd => dd.Detection)
               .WithMany(d => d.DamageDetails)
               .HasForeignKey(dd => dd.DetectionId);
    }
}
