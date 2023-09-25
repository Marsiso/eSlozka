using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class CodeListItemDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<CodeListItem>
{
    public override void Configure(EntityTypeBuilder<CodeListItem> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.CodeListItems);

        builder.HasKey(codeListItem => codeListItem.CodeListItemID);

        builder.Property(codeListItem => codeListItem.Value).HasMaxLength(256).IsUnicode();
    }
}