using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class CodeList : ChangeTrackingEntity
{
    public int CodeListID { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<CodeListItem>? CodeListItems { get; set; }
}
