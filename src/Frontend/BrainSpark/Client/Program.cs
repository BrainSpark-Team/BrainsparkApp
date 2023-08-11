global using System.Net.Http.Json;
global using BrainSpark.Client.Services;

using BrainSpark.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped< IExamService, ExamService>();

builder.Services.AddScoped<ExamPageService>();

await builder.Build().RunAsync();
