using System.Security.Claims;
using eSlozka.Domain.Constants;
using eSlozka.Domain.DataTransferObjects.Users;
using eSlozka.Domain.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Logging;

namespace eSlozka.Application.Authentication;

public class RevalidatingAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "RevalidationAuthentication";

    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private readonly ILogger<RevalidatingAuthenticationStateProvider> _logger;
    private readonly ProtectedSessionStorage _sessionStorage;

    public RevalidatingAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, ILogger<RevalidatingAuthenticationStateProvider> logger)
    {
        _sessionStorage = sessionStorage;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var sessionStorageResult = await _sessionStorage.GetAsync<SessionProperties>(nameof(SessionProperties));

            if (sessionStorageResult is not { Success: true, Value: not null }) return await Task.FromResult(new AuthenticationState(_anonymous));

            var sessionProperties = sessionStorageResult.Value;

            var claims = new List<Claim>
            {
                new(UserClaimTypes.ProtectedIdentifier, sessionProperties.ProtectedUserID),
                new(UserClaimTypes.GivenName, sessionProperties.GivenName),
                new(UserClaimTypes.FamilyName, sessionProperties.FamilyName),
                new(UserClaimTypes.Email, sessionProperties.Email),
                new(UserClaimTypes.EmailConfirmed, sessionProperties.EmailConfirmed.ToString()),
                new(UserClaimTypes.DarkThemeEnabled, sessionProperties.DarkThemeEnabled.ToString()),
                new(UserClaimTypes.Permissions, PolicyNameHelpers.GetPolicyNameFor(sessionProperties.Permissions))
            };

            if (!string.IsNullOrWhiteSpace(sessionProperties.ProfilePhoto)) claims.Add(new Claim(UserClaimTypes.ProfilePhoto, sessionProperties.ProfilePhoto));

            var claimsIdentity = new ClaimsIdentity(claims, AuthenticationType);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);

            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(SessionProperties? sessionProperties)
    {
        AuthenticationState authenticationState;
        if (sessionProperties is not null)
        {
            await _sessionStorage.SetAsync(nameof(SessionProperties), sessionProperties);

            var claims = new List<Claim>
            {
                new(UserClaimTypes.ProtectedIdentifier, sessionProperties.ProtectedUserID),
                new(UserClaimTypes.GivenName, sessionProperties.GivenName),
                new(UserClaimTypes.FamilyName, sessionProperties.FamilyName),
                new(UserClaimTypes.Email, sessionProperties.Email),
                new(UserClaimTypes.EmailConfirmed, sessionProperties.EmailConfirmed.ToString()),
                new(UserClaimTypes.DarkThemeEnabled, sessionProperties.DarkThemeEnabled.ToString()),
                new(UserClaimTypes.Permissions, PolicyNameHelpers.GetPolicyNameFor(sessionProperties.Permissions))
            };

            if (!string.IsNullOrWhiteSpace(sessionProperties.ProfilePhoto)) claims.Add(new Claim(UserClaimTypes.ProfilePhoto, sessionProperties.ProfilePhoto));

            var claimsIdentity = new ClaimsIdentity(claims, AuthenticationType);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            authenticationState = new AuthenticationState(claimsPrincipal);
        }
        else
        {
            await _sessionStorage.DeleteAsync(nameof(SessionProperties));

            authenticationState = new AuthenticationState(_anonymous);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }
}
