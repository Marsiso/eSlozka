using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class UserDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.Users);

        builder.HasKey(user => user.UserID);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Email).HasMaxLength(256);
        builder.Property(user => user.GivenName).HasMaxLength(256).IsUnicode();
        builder.Property(user => user.FamilyName).HasMaxLength(256).IsUnicode();
        builder.Property(user => user.Password).HasMaxLength(512);
        builder.Property(user => user.PasswordSalt).HasMaxLength(512);
        builder.Property(user => user.Locale).HasMaxLength(64);
        builder.Property(user => user.ProfilePhoto).HasMaxLength(4096);

        builder.HasMany(user => user.Roles)
            .WithOne(userRoles => userRoles.User)
            .HasForeignKey(userRole => userRole.UserID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(user => user.Folders)
            .WithOne(folder => folder.User)
            .HasForeignKey(folder => folder.UserID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}