using eSlozka.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations.Common;

public class ChangeTrackingEntityDatabaseMappingConfiguration<TChangeTrackingEntity> : EntityBaseDatabaseMappingConfiguration<TChangeTrackingEntity> where TChangeTrackingEntity : ChangeTrackingEntity
{
    public override void Configure(EntityTypeBuilder<TChangeTrackingEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(entity => entity.UserCreatedBy)
            .WithMany()
            .HasForeignKey(entity => entity.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(entity => entity.UserUpdatedBy)
            .WithMany()
            .HasForeignKey(entity => entity.UpdatedBy)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
