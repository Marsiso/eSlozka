using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class Role : ChangeTrackingEntity
{
    public int RoleID { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Permission>? Permissions { get; set; }
}
