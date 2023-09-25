using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class FolderDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<Folder>
{
    public override void Configure(EntityTypeBuilder<Folder> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.Folders);

        builder.HasKey(folder => folder.FolderID);

        builder.HasIndex(folder => folder.UserID);
        builder.HasIndex(folder => folder.ParentID);
        builder.HasIndex(folder => folder.CategoryID);

        builder.Property(folder => folder.Name).HasMaxLength(256).IsUnicode();

        builder.HasOne(folder => folder.Category)
            .WithMany()
            .HasForeignKey(folder => folder.CategoryID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(folder => folder.Children)
            .WithOne(folder => folder.Parent)
            .HasForeignKey(folder => folder.ParentID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(folder => folder.Files)
            .WithOne(file => file.Folder)
            .HasForeignKey(file => file.FolderID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}