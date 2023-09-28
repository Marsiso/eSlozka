using eSlozka.Domain.Enums;
using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace eSlozka.Application.Authorization;

public class PermissionAuthorizeView : AuthorizeView
{
    [Parameter]
    public Permission Permissions
    {
        get => string.IsNullOrWhiteSpace(Policy) ? Permission.None : PolicyNameHelpers.GetPermissionsFrom(Policy);
        set => Policy = PolicyNameHelpers.GetPolicyNameFor(value);
    }
}