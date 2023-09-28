using eSlozka.Domain.Constants;
using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eSlozka.Application.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        var permissionClaim = context.User.FindFirst(UserClaimTypes.Permissions);

        if (permissionClaim is null) return Task.CompletedTask;

        var permissions = PolicyNameHelpers.GetPermissionsFrom(permissionClaim.Value);

        if ((permissions & requirement.Permission) != 0) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}