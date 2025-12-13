using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;

namespace Maui.Web.Pages;

public class SignInModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SignInModel> _logger;

    [BindProperty]
    public LoginInputModel Input { get; set; } = new();

    [TempData]
    public string? ErrorMessage { get; set; }

    [TempData]
    public string? SuccessMessage { get; set; }

    public SignInModel(IHttpClientFactory httpClientFactory, ILogger<SignInModel> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public void OnGet(string? returnUrl = null)
    {
        // Check if user is already authenticated
        if (User.Identity?.IsAuthenticated == true)
        {
            Response.Redirect(returnUrl ?? "/");
            return;
        }

        // Check for logout message
        if (Request.Query.ContainsKey("logout"))
        {
            SuccessMessage = "You have been successfully logged out.";
        }
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= "/";

        // ✅ Debug ModelState
        if (!ModelState.IsValid)
        {
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if (state.Errors.Count > 0)
                {
                    _logger.LogWarning("ModelState Error for {Key}: {Errors}",
                        key, string.Join(", ", state.Errors.Select(e => e.ErrorMessage)));
                }
            }

            ErrorMessage = "Please fill in all required fields.";
            return Page();
        }

        try
        {
            var client = _httpClientFactory.CreateClient("Api");

            // Call your API
            var response = await client.PostAsJsonAsync("iam/login", new
            {
                UserName = Input.UserName,
                Password = Input.Password
            });

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                _logger.LogWarning("Login failed for user {UserName}. Status: {StatusCode}, Error: {Error}",
                    Input.UserName, response.StatusCode, errorContent);

                ErrorMessage = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => "Invalid username or password.",
                    System.Net.HttpStatusCode.Forbidden => "Your account has been locked.  Please contact support.",
                    System.Net.HttpStatusCode.TooManyRequests => "Too many login attempts. Please try again later.",
                    _ => "Login failed. Please try again."
                };

                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null)
            {
                ErrorMessage = "Invalid response from server.";
                return Page();
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result. UserId),
                new Claim(ClaimTypes.Name, result.UserName),
                new Claim("FullName", result.FullName ??  result.UserName)
            };

            // Add roles if present
            if (result.Roles != null && result.Roles.Any())
            {
                foreach (var role in result.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // Add email if present
            if (!string.IsNullOrEmpty(result.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, result.Email));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = Input.RememberMe,
                    ExpiresUtc = Input.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(30)
                        : DateTimeOffset.UtcNow.AddHours(8)
                });

            _logger.LogInformation("User {UserName} (ID: {UserId}) signed in successfully",
                Input.UserName, result.UserId);

            return LocalRedirect(returnUrl);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during login for user {UserName}", Input.UserName);
            ErrorMessage = "Unable to connect to the server. Please check your connection. ";
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for user {UserName}", Input.UserName);
            ErrorMessage = "An unexpected error occurred. Please try again.";
            return Page();
        }
    }
}

// ✅ Separate input model class
public class LoginInputModel
{
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string UserName { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; } = false;
}

// Response DTO
public class LoginResponse
{
    public string UserId { get; set; } = "";
    public string UserName { get; set; } = "";
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
}