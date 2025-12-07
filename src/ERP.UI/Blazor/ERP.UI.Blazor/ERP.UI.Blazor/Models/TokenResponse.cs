namespace ERP.UI.Blazor.Models;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? SessionId { get; set; } 
}
