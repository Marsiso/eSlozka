using System.Security.Claims;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eSlozka.Application.Authorization;

public static class AuthorizationServiceExtensions
{
    public static Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Permission permission)
    {
        return service.AuthorizeAsync(user, user, PolicyNameHelpers.GetPolicyNameFor(permission));
    }
}