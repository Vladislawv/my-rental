using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MyRental.Blazor.Authentication;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public MyAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
            var session = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

            if (session == null) return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, session.Id + ""),
                new(ClaimTypes.Name, session.UserName),
                new(ClaimTypes.Email, session.Email),
                new(ClaimTypes.MobilePhone, session.PhoneNumber),
                new(ClaimTypes.Role, session.Role)
            }, "CustomAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationStateAsync(UserSession session)
    {
        ClaimsPrincipal claims;

        if (session != null)
        {
            await _sessionStorage.SetAsync("UserSession", session);
            claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, session.Id + ""),
                new(ClaimTypes.Name, session.UserName),
                new(ClaimTypes.Email, session.Email),
                new(ClaimTypes.MobilePhone, session.PhoneNumber),
                new(ClaimTypes.Role, session.Role)
            }, "CustomAuth"));
        }
        else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            claims = _anonymous;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
    }
}