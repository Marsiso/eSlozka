using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class Permission : ChangeTrackingEntity
{
    public int PermissionID { get; set; }
    public int RoleID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}