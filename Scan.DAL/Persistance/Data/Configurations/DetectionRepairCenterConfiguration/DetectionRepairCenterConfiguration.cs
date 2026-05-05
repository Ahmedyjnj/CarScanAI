using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DetectionRepairCenterConfiguration
    : IEntityTypeConfiguration<DetectionRepairCenter>
{
    public void Configure(EntityTypeBuilder<DetectionRepairCenter> builder)
    {
        builder.ToTable("DetectionRepairCenters");

        builder.HasKey(x => new { x.DetectionId, x.CenterId });

        builder.Property(x => x.IsContacted)
               .HasDefaultValue(false);

        builder.HasOne(x => x.Detection)
               .WithMany(d => d.DetectionRepairCenters)
               .HasForeignKey(x => x.DetectionId);

        builder.HasOne(x => x.RepairCenter)
               .WithMany(r => r.DetectionRepairCenters)
               .HasForeignKey(x => x.CenterId);
    }
}
