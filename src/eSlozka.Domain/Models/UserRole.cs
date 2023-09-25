using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class UserRole : ChangeTrackingEntity
{
    public int UserRoleID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }

    public User? User { get; set; }
    public Role? Role { get; set; }
}