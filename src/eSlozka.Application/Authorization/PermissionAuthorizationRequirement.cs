using eSlozka.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace eSlozka.Application.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Permission permission)
    {
        Permission = permission;
    }

    public Permission Permission { get; }
}