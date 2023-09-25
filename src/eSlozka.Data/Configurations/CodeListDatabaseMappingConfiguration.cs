using eSlozka.Data.Configurations.Common;
using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSlozka.Data.Configurations;

public class CodeListDatabaseMappingConfiguration : ChangeTrackingEntityDatabaseMappingConfiguration<CodeList>
{
    public override void Configure(EntityTypeBuilder<CodeList> builder)
    {
        base.Configure(builder);

        builder.ToTable(Tables.CodeLists);

        builder.HasKey(codeList => codeList.CodeListID);

        builder.Property(codeList => codeList.Name).HasMaxLength(256).IsUnicode();

        builder.HasMany(codeList => codeList.CodeListItems)
            .WithOne()
            .HasForeignKey(codeListItem => codeListItem.CodeListID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}