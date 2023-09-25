using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class PermissionDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.Permissions);

        builder.HasKey(permission => permission.PermissionID);

        builder.HasIndex(permission => permission.RoleID);

        builder.Property(permission => permission.Type).HasMaxLength(256);
        builder.Property(permission => permission.Value).HasMaxLength(256);

        builder.HasData(new Permission[]
        {
            new()
            {
                PermissionID = 1,
                RoleID = 1,
                Type = "Data",
                Value = "Read",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                PermissionID = 2,
                RoleID = 1,
                Type = "Data",
                Value = "Write",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                PermissionID = 3,
                RoleID = 1,
                Type = "Data",
                Value = "Delete",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                PermissionID = 4,
                RoleID = 2,
                Type = "Data",
                Value = "Read",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                PermissionID = 5,
                RoleID = 2,
                Type = "Data",
                Value = "Write",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            },
            new()
            {
                PermissionID = 6,
                RoleID = 3,
                Type = "Data",
                Value = "Read",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsActive = true
            }
        });
    }
}
