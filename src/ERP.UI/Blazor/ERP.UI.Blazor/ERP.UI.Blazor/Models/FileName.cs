namespace ERP.UI.Blazor.Models;

public class UserIdentityDto
{
    public string UserId { get; set; } = "";
    public string Username { get; set; } = "";
    public List<string> Roles { get; set; } = new();
}
