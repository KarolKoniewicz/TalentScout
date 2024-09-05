using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Byte.TalentScout.Domain.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace Byte.TalentScout.UI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly ILocalStorageService localStorageService;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorageService)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
        this.localStorageService = localStorageService;
    }

    public async Task<RegisterResult> Register(RegisterViewModel registerModel)
    {
        var response = await httpClient.PostAsJsonAsync("api/accounts", registerModel);
        return JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<LoginResult> Login(LoginViewModel loginModel)
    {
        var loginAsJson = JsonSerializer.Serialize(loginModel);
        var response = await httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
        var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await localStorageService.SetItemAsync("authToken", loginResult.Token);
        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

        return loginResult;
    }

    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
