using System.Security.Claims;
using eSlozka.Domain.DataTransferObjects.Sessions;
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
            var sessionStorageResult = await _sessionStorage.GetAsync<UserSession>(nameof(UserSession));

            if (sessionStorageResult is not { Success: true, Value: not null }) return await Task.FromResult(new AuthenticationState(_anonymous));

            var userSession = sessionStorageResult.Value;

            var claims = new List<Claim>
            {
                new(nameof(userSession.ProtectedUserID), userSession.ProtectedUserID),
                new(nameof(userSession.GivenName), userSession.GivenName),
                new(nameof(userSession.FamilyName), userSession.FamilyName),
                new(nameof(userSession.Email), userSession.Email),
                new(nameof(userSession.EmailConfirmed), userSession.EmailConfirmed.ToString()),
                new(nameof(userSession.DarkThemeEnabled), userSession.DarkThemeEnabled.ToString())
            };

            if (!string.IsNullOrWhiteSpace(userSession.ProfilePhoto)) claims.Add(new Claim(nameof(userSession.ProfilePhoto), userSession.ProfilePhoto));

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

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        AuthenticationState authenticationState;
        if (userSession is not null)
        {
            await _sessionStorage.SetAsync(nameof(UserSession), userSession);

            var claims = new List<Claim>
            {
                new(nameof(userSession.ProtectedUserID), userSession.ProtectedUserID),
                new(nameof(userSession.GivenName), userSession.GivenName),
                new(nameof(userSession.FamilyName), userSession.FamilyName),
                new(nameof(userSession.Email), userSession.Email),
                new(nameof(userSession.EmailConfirmed), userSession.EmailConfirmed.ToString()),
                new(nameof(userSession.DarkThemeEnabled), userSession.DarkThemeEnabled.ToString())
            };

            if (!string.IsNullOrWhiteSpace(userSession.ProfilePhoto)) claims.Add(new Claim(nameof(userSession.ProfilePhoto), userSession.ProfilePhoto));

            var claimsIdentity = new ClaimsIdentity(claims, AuthenticationType);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            authenticationState = new AuthenticationState(claimsPrincipal);
        }
        else
        {
            authenticationState = new AuthenticationState(_anonymous);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }
}
