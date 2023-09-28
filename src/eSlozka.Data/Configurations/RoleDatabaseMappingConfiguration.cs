using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Helpers;
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

        builder.Property(role => role.Permission)
            .HasConversion(
                v => PolicyNameHelpers.GetPolicyNameFor(v),
                v => PolicyNameHelpers.GetPermissionsFrom(v));

        builder.HasMany(role => role.Users)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new Role
        {
            RoleID = 1,
            Name = "Admin",
            Permission = Permission.All,
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            IsActive = true
        }, new Role
        {
            RoleID = 2,
            Name = "Manager",
            Permission = Permission.ViewFiles | Permission.EditFiles | Permission.ShareFiles | Permission.ViewUsers | Permission.EditUsers | Permission.ViewRoles | Permission.ViewCodeLists | Permission.EditCodeLists,
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            IsActive = true
        }, new Role
        {
            RoleID = 3,
            Name = "Default",
            Permission = Permission.ViewFiles | Permission.EditFiles | Permission.ShareFiles,
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            IsActive = true
        }, new Role
        {
            RoleID = 4,
            Name = "Visitor",
            Permission = Permission.None,
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            IsActive = true
        });
    }
}