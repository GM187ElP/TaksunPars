var builder = WebApplication.CreateBuilder(args);

// Optional: static file hosting for WASM updates or CDN-like behavior
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Do NOT use HTTPS redirect
// Do NOT use HSTS
// Do NOT use Antiforgery
// Do NOT map Razor components
// Do NOT run WASM debugging

// Static files (WASM bundle, css, js)
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});

app.UseDirectoryBrowser();

// You can put version.json here for Telegram-style updates
// app.MapGet("/version", () => "1.0.0");

app.Run();
