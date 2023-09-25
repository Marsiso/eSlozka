using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class AuditDatabaseMappingConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable(Tables.Audits);

        builder.HasKey(audit => audit.AuditID);

        builder.HasIndex(audit => audit.UserID);

        builder.Property(audit => audit.OldValue).HasMaxLength(8192).IsUnicode();
        builder.Property(audit => audit.NewValue).HasMaxLength(8192).IsUnicode();
        builder.Property(audit => audit.AffectedColumns).HasMaxLength(1024);
        builder.Property(audit => audit.Type).HasMaxLength(256);
        builder.Property(audit => audit.EntityType).HasMaxLength(256);

        builder.HasOne(audit => audit.User)
            .WithMany()
            .HasForeignKey(audit => audit.UserID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}