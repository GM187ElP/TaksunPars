using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class SignInModel : PageModel
{
    private readonly ILogger<SignInModel> _logger;

    public SignInModel(ILogger<SignInModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string userName, string roles)
    {
        // Log in attempt (you can remove this if logging is not required)
        _logger.LogInformation("Signing in user: {UserName}", userName);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, userName)
    };

        // Add roles if provided
        if (!string.IsNullOrWhiteSpace(roles))
        {
            foreach (var role in roles.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        var principal = new ClaimsPrincipal(identity);

        // Ensure SignInAsync is called correctly
        await HttpContext.SignInAsync("CustomAuthScheme", principal);

        // Redirect to home page or protected area after successful sign-in
        return Redirect("/");
    }

}
