using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class CodeListItem : ChangeTrackingEntity
{
    public int CodeListItemID { get; set; }
    public int CodeListID { get; set; }
    public string Value { get; set; } = string.Empty;
}