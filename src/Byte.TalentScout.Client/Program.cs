using Byte.TalentScout.Client;
using Byte.TalentScout.Client.Services;
using Byte.TalentScout.Client.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddHttpClient<HttpClientErrorWrapper>((sp, httpClient)
    => httpClient.BaseAddress = new Uri("https://localhost:7199/"));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IStorageService, StorageService>();

await builder.Build().RunAsync();
