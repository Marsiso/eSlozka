using eSlozka.Domain.Enums;
using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class Role : ChangeTrackingEntity
{
    public int RoleID { get; set; }
    public string Name { get; set; } = string.Empty;
    public Permission Permission { get; set; }

    public ICollection<UserRole>? Users { get; set; }
}