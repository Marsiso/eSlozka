using eSlozka.Domain.Enums;

namespace eSlozka.Domain.Helpers;

public static class PolicyNameHelpers
{
    public const string Prefix = "Permissions";

    public static bool IsValidPolicyName(string? policyName)
    {
        return !string.IsNullOrWhiteSpace(policyName) && policyName.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase);
    }

    public static string GetPolicyNameFor(Permission permissions)
    {
        return $"{Prefix}{(int)permissions}";
    }

    public static Permission GetPermissionsFrom(string? policyName)
    {
        if (string.IsNullOrWhiteSpace(policyName)) return Permission.None;

        if (int.TryParse(policyName[Prefix.Length..], out var permissionsValue)) return (Permission)permissionsValue;

        return Permission.None;
    }
}
