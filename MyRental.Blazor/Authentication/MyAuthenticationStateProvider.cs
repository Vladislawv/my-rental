using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyRental.Blazor.Authentication;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private ClaimsIdentity _identity = new();
    private readonly NavigationManager _navManager;

    public MyAuthenticationStateProvider(ILocalStorageService localStorageService, NavigationManager navManager)
    {
        _localStorageService = localStorageService;
        _navManager = navManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetItemAsync<string>("Jwt");
        
        if (!string.IsNullOrEmpty(token))
        {
            _identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        }

        var user = new ClaimsPrincipal(_identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));
        
        return state;
    }

    public async Task LogoutAsync()
    {
        await _localStorageService.SetItemAsync<string>("Jwt", "");
        await GetAuthenticationStateAsync();
        _navManager.NavigateTo("/", true);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
    
    public UserSession GetUserSession()
    {
        var claims = _identity.Claims;
        
        return new UserSession
        {
            Id = int.Parse(claims.ElementAt(0).Value),
            UserName = claims.ElementAt(1).Value,
            Email = claims.ElementAt(2).Value,
            PhoneNumber = claims.ElementAt(3).Value,
            Role = claims.ElementAt(4).Value
        };
    }
}