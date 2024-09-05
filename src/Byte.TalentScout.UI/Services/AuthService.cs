using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Byte.TalentScout.Domain.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace Byte.TalentScout.UI.Services;

public class AuthService : IAuthService
{
    private const string authBaseUri = "https://localhost:7199";
    private readonly IJSRuntime jsRuntime;
    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly ILocalStorageService localStorageService;
    private readonly NavigationManager navigationManager;

    public AuthService(
        IJSRuntime jsRuntime,
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        ILocalStorageService localStorageService, 
        NavigationManager navigationManager)
    {
        this.jsRuntime = jsRuntime;
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
        this.localStorageService = localStorageService;
        this.navigationManager = navigationManager;
    }
    public async Task<RegisterResult> Register(RegisterViewModel registerModel)
    {
        var response = await httpClient.PostAsJsonAsync("api/accounts", registerModel);
        return JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<LoginResult> Login(LoginViewModel loginModel)
    {
        try
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await httpClient.PostAsync($"{authBaseUri}/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
                return loginResult;

            await SetTokenInLocalStorageAndAuthorize(loginResult.AccessToken, loginModel.Email);

            return loginResult;
        }
        catch (Exception)
        {
            return new LoginResult();
        }
    }

    private async Task SetTokenInLocalStorageAndAuthorize(string token, string email)
    {
        if (jsRuntime is IJSInProcessRuntime)
        {
            await localStorageService.SetItemAsStringAsync("authToken", token);
            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(email);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            navigationManager.NavigateTo("/");
        }
    }

    public async Task Logout()
    {
        try
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                await localStorageService.RemoveItemAsync("authToken");
                httpClient.DefaultRequestHeaders.Authorization = null;
                ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
                navigationManager.NavigateTo("/login");
            }
        }
        catch (Exception ex)
        {
          
        }
    }
}
