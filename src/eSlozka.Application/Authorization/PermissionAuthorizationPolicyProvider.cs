using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace eSlozka.Application.Authorization;

public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;

    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
        _options = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (string.IsNullOrWhiteSpace(policyName) || !PolicyNameHelpers.IsValidPolicyName(policyName)) return policy;

        var permissions = PolicyNameHelpers.GetPermissionsFrom(policyName);

        policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionAuthorizationRequirement(permissions))
            .Build();

        _options.AddPolicy(policyName, policy);

        return policy;
    }
}