global using System.Net.Http.Json;
global using BrainSpark.Client.Services;

using BrainSpark.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<RemoteAuthenticationService<RemoteAuthenticationState, RemoteUserAccount, OidcProviderOptions>>();


builder.Services.AddMsalAuthentication(options =>
{
    // Bind the AzureAdB2C configuration section to the options
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);

    // Set up the default access token scopes
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/offline_access");

    // Additional configuration (if needed)
    options.ProviderOptions.LoginMode = "redirect"; // Use "redirect" or "popup" based on your preference
});



await builder.Build().RunAsync();
