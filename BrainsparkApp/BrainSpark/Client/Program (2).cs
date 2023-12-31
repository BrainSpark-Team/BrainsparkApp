global using System.Net.Http.Json;
global using BrainSpark.Client.Services;
using Blzr.BootstrapSelect;
using BrainSpark.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBootstrapSelect();
builder.Services.AddBlazorBootstrap();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IExamService, ExamService>();

builder.Services.AddScoped< IExamService, ExamService>();

builder.Services.AddScoped<ExamPageService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<RemoteAuthenticationService<RemoteAuthenticationState, RemoteUserAccount, OidcProviderOptions>>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/offline_access");
    options.ProviderOptions.LoginMode = "redirect";
});

await builder.Build().RunAsync();
