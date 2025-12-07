using ERP.UI.Blazor.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("Api", option =>
{
    option.BaseAddress = new Uri("https://localhost:7144/");
});

builder.Services.AddAuthentication("CustomAuthScheme")
    .AddCookie("CustomAuthScheme", options =>
    {
        options.Cookie.Name = "auth_session";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });


builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorPages();
app.MapStaticAssets();

// These lines render Blazor, but do NOT include automatic fallback
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// You must add this for redirecting to "/" to work


app.Run();
