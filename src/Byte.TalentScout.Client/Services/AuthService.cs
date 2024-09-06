using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Byte.TalentScout.Domain.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Byte.TalentScout.Client.Services
{
    public class AuthService : IAuthService
    {
        private const string authBaseUri = "https://localhost:7199";
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager navigationManager;

        public AuthService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
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

                // Set token and update authentication state
                await SetTokenAndAuthorize(loginResult.AccessToken, loginModel.Email);

                return loginResult;
            }
            catch (Exception)
            {
                return new LoginResult();
            }
        }

        private async Task SetTokenAndAuthorize(string token, string email)
        {
            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(email);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public async Task Logout()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
