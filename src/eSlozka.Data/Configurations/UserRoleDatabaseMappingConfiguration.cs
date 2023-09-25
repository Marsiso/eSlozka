using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class UserRoleDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.UserRoles);

        builder.HasKey(role => role.UserRoleID);

        builder.HasIndex(userRole => userRole.UserID);
        builder.HasIndex(userRole => userRole.RoleID);
    }
}
