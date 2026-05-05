using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DetectionConfiguration : IEntityTypeConfiguration<Detection>
{
    public void Configure(EntityTypeBuilder<Detection> builder)
    {
        builder.ToTable("Detections");

        builder.HasKey(d => d.DetectionId);

        builder.Property(d => d.ImagePath)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(d => d.DetectionDate)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(d => d.OverallSeverity)
               .HasMaxLength(30);

        builder.Property(d => d.TotalCost)
               .HasColumnType("decimal(18,2)");

        builder.Property(d => d.AiModel)
               .HasMaxLength(100);

        builder.HasOne(d => d.User)
               .WithMany(u => u.Detections)
               .HasForeignKey(d => d.UserId);

        builder.HasOne(d => d.Car)
               .WithMany(c => c.Detections)
               .HasForeignKey(d => d.CarId);

        builder.HasMany(d => d.DamageDetails)
               .WithOne(dd => dd.Detection)
               .HasForeignKey(dd => dd.DetectionId);
    }
}
