using eSlozka.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = eSlozka.Domain.Models.File;

namespace eSlozka.Data.Configurations;

public class FileDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<File>
{
    public override void Configure(EntityTypeBuilder<File> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.Files);

        builder.HasKey(file => file.FileID);

        builder.Property(file => file.Location).HasMaxLength(256);
        builder.Property(file => file.SafeName).HasMaxLength(256);
        builder.Property(file => file.UnsafeName).HasMaxLength(256).IsUnicode();

        builder.HasOne(file => file.Extension)
            .WithMany()
            .HasForeignKey(file => file.ExtensionID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(file => file.MimeType)
            .WithMany()
            .HasForeignKey(file => file.MimeTypeID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}