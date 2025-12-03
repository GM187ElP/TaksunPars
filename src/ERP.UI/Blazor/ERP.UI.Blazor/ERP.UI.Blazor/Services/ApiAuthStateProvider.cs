using ERP.UI.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class ApiAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiAuthStateProvider(
        IHttpClientFactory clientFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _clientFactory = clientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context == null)
            return Anonymous();

        var cookie = context.Request.Cookies["auth_session"];
        if (string.IsNullOrEmpty(cookie))
            return Anonymous();

        var client = _clientFactory.CreateClient("ERP.API");

        var response = await client.GetAsync("IAM/me");
        if (!response.IsSuccessStatusCode)
            return Anonymous();

        var identityDto = await response.Content.ReadFromJsonAsync<UserIdentityDto>();
        if (identityDto == null)
            return Anonymous();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, identityDto.UserId),
            new Claim(ClaimTypes.Name, identityDto.Username)
        };

        foreach (var r in identityDto.Roles)
            claims.Add(new Claim(ClaimTypes.Role, r));

        var identity = new ClaimsIdentity(claims, "OpaqueCookie");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private AuthenticationState Anonymous()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyUserAuthentication()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
