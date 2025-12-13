using ERP.UI.Blazor.Components;
using Maui.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;
using PersonellInfo.Blazor;
using PersonellInfo.Blazor.Components.Services;
using PersonellInfo.Blazor.Components.Services.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// ✅ Add CookieForwardingHandler
builder.Services.AddTransient<CookieForwardingHandler>();

// ✅ Add HttpClient for API calls
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://taksunparsapi.runasp.net");
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddHttpMessageHandler<CookieForwardingHandler>();

// ✅ Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_session";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.LoginPath = "/signin";
        options.LogoutPath = "/signout";
    });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();

// ✅ Add Razor Pages for SignIn/SignOut
builder.Services.AddRazorPages();


// ------------------from another project--------------------------
//builder.Services.AddScoped<EmployeeState>();
builder.Services.AddScoped<EnumServices>();
builder.Services.AddSingleton<AppLanguageService>();
builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ApiConfiguration"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiConfiguration:Url"]) });
builder.Services.AddSingleton<IDisplayName, DisplayName>();
builder.Services.AddMudServices();
// ------------------from another project--------------------------



// Add device-specific services used by the Maui.Shared project

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// ✅ Add Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ✅ Map Razor Pages
app.MapRazorPages();

app.Run();










//using Maui.Shared.Services;
//using Maui.Web.Components;
//using Maui.Web.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//// Add device-specific services used by the Maui.Shared project
//builder.Services.AddSingleton<IFormFactor, FormFactor>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
//app.UseHttpsRedirection();

//app.UseAntiforgery();

//app.MapStaticAssets();

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode()
//    .AddAdditionalAssemblies(
//        typeof(Maui.Shared._Imports).Assembly);

//app.Run();
