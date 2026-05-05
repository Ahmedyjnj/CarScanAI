using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

       

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
               .IsUnique();

       

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.ProfileImage)
               .HasMaxLength(255);

        builder.Property(u => u.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(u => u.Status)
               .HasMaxLength(20);

        // Relationships
        builder.HasMany(u => u.Cars)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Detections)
               .WithOne(d => d.User)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
