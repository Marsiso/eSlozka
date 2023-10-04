﻿using eSlozka.Domain.Enums;
using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eSlozka.Application.Authorization;

public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    public PermissionAuthorizeAttribute()
    {
    }

    public PermissionAuthorizeAttribute(string policy) : base(policy)
    {
    }

    public PermissionAuthorizeAttribute(Permission permissions)
    {
        Permissions = permissions;
    }

    public Permission Permissions
    {
        get => !string.IsNullOrWhiteSpace(Policy) ? PolicyNameHelpers.GetPermissionsFrom(Policy) : Permission.None;
        set => Policy = value != Permission.None ? PolicyNameHelpers.GetPolicyNameFor(value) : string.Empty;
    }
}
