using ERP.UI.Blazor.Components;
using ERP.UI.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Services
// ---------------------------------------------------------------

// Razor / Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(o => o.DetailedErrors = true);

// HttpClient to call API
builder.Services.AddHttpClient("ERP.API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7144/");
});

// Auth services
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ApiAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();

// Our UI authentication service
builder.Services.AddScoped<AuthenticationService>();

// HttpContext only for initial connection
builder.Services.AddHttpContextAccessor();

// Antiforgery
builder.Services.AddAntiforgery();

// ---------------------------------------------------------------
// App
// ---------------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------------
// Middleware
// ---------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
