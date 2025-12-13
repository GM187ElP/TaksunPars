using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

public class SignInModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string UserId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Username { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {   

        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Login");
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, UserId),
        new Claim(ClaimTypes.Name, Username)
    };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return Redirect("/");   // ← this now works
    }


}
