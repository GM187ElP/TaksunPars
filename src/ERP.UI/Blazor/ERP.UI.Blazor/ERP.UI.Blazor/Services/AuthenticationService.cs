using ERP.UI.Blazor.Models;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ERP.UI.Blazor.Services;

//public class AuthenticationService
//{
//    private readonly IHttpClientFactory _clientFactory;
//    private readonly ITokenStore _tokenStore;
//    private readonly IHttpContextAccessor _httpContextAccessor;

//    public AuthenticationService(
//        IHttpClientFactory clientFactory,
//        ITokenStore tokenStore,
//        IHttpContextAccessor httpContextAccessor)
//    {
//        _clientFactory = clientFactory;
//        _tokenStore = tokenStore;
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public async Task<bool> AuthenticateUserAsync(string username, string password)
//    {
//        var client = _clientFactory.CreateClient("ERP.Api");

//        var response = await client.PostAsJsonAsync("IAM/login", new { username, password });

//        if (!response.IsSuccessStatusCode)
//            return false;

//        var jwtResult = await response.Content.ReadFromJsonAsync<Result<TokenResponse>>();

//        if (jwtResult?.Payload?.Value == null || !jwtResult.Status.IsSuccess)
//            return false;

//        var accessToken = jwtResult.Payload.Value.AccessToken;
//        var refreshToken = jwtResult.Payload.Value.RefreshToken;

//        if (string.IsNullOrWhiteSpace(accessToken))
//            return false;

//        var handler = new JwtSecurityTokenHandler();
//        var jwt = handler.ReadJwtToken(accessToken);

//        var userId = jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
//        var usernameFromToken = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? username;

//        var httpContext = _httpContextAccessor.HttpContext
//                          ?? throw new InvalidOperationException("No HttpContext");

//        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
//        var userAgent = httpContext.Request.Headers["User-Agent"].ToString() ?? "Unknown";

//        var sessionId = Guid.NewGuid().ToString();

//        var saveDto = new SaveTokensDto
//        {
//            UserId = userId,
//            SessionId = sessionId,
//            AccessToken = accessToken,
//            RefreshToken = refreshToken,
//            IpAddress = ip,
//            UserAgent = userAgent,
//            Os = "Unknown OS",
//            DeviceInfo = "Unknown Device"
//        };

//        await _tokenStore.SaveTokens(saveDto);

//        var identity = new ClaimsIdentity(
//            new[] { new Claim("session_id", sessionId) },
//            "OpaqueCookie");

//        var principal = new ClaimsPrincipal(identity);

//        await httpContext.SignInAsync(
//            "OpaqueCookie",
//            principal,
//            new AuthenticationProperties
//            {
//                IsPersistent = true,
//                ExpiresUtc = DateTime.UtcNow.AddHours(12),
//                AllowRefresh = true
//            });

//        return true;
//    }
//}



public class AuthenticationService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ApiAuthStateProvider _authStateProvider;

    public AuthenticationService(
        IHttpClientFactory clientFactory,
        ApiAuthStateProvider authStateProvider)
    {
        _clientFactory = clientFactory;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> AuthenticateUserAsync(string username, string password)
    {
        var client = _clientFactory.CreateClient("ERP.API");

        var response = await client.PostAsJsonAsync("IAM/login", new { username, password });

        if (!response.IsSuccessStatusCode)
            return false;

        _authStateProvider.NotifyUserAuthentication();

        return true;
    }
}



public interface ITokenStore
{
    Task SaveTokens(SaveTokensDto tokens);

    Task<GetTokensDto?> GetTokensBySession(string sessionId);
    Task<IEnumerable<GetTokensDto>> GetTokensByUser(string userId);

    Task InvalidateTokensByUser(string userId);
    Task InvalidateSession(string sessionId);

    Task UpdateRefreshToken(string sessionId, string newRefreshToken);
}


public class SaveTokensDto
{
    public string UserId { get; set; } = default!;
    public string SessionId { get; set; } = Guid.NewGuid().ToString();

    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAtUtc { get; set; }

    public string IpAddress { get; set; } = "";
    public string UserAgent { get; set; } = "";
    public string Os { get; set; } = "";
    public string DeviceInfo { get; set; } = "";
}

public class GetTokensDto
{
    public string UserId { get; set; } = default!;
    public string SessionId { get; set; } = default!;

    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ExpiresAtUtc { get; set; }
    public DateTime? LastUsedAtUtc { get; set; }

    public bool IsRevoked { get; set; }
    public DateTime? RevokedAtUtc { get; set; }
    public string? ReplacedBySessionId { get; set; }

    public string IpAddress { get; set; } = "";
    public string UserAgent { get; set; } = "";
    public string Os { get; set; } = "";
    public string DeviceInfo { get; set; } = "";
}
