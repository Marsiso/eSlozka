using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class RoleDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.Roles);

        builder.HasKey(role => role.RoleID);

        builder.Property(role => role.Name).HasMaxLength(256).IsUnicode();

        builder.HasMany(role => role.Users)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(role => role.Permissions)
            .WithOne()
            .HasForeignKey(permission => permission.RoleID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new Role[]
        {
            new()
            {
                RoleID = 1,
                Name = "Admin",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                RoleID = 2,
                Name = "Manager",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                RoleID = 3,
                Name = "Default",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
        });
    }
}
