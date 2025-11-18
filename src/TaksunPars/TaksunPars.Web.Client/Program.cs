using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaksunPars.Shared.Services;
using TaksunPars.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the TaksunPars.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
