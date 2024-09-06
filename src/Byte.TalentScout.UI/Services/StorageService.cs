using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Byte.TalentScout.UI.Services
{
    public class StorageService : IStorageService
    {
        private readonly IJSRuntime jsRuntime;

        public StorageService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetLocalStorageAsync(string key, string value)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
            }
        }

        public async Task<string> GetLocalStorageAsync(string key)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                return await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            }
            return string.Empty;
        }

        public async Task SetSessionStorageAsync(string key, string value)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, value);
            }
        }

        public async Task<string> GetSessionStorageAsync(string key)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                return await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
            }
            return string.Empty;
        }

        public async Task RemoveSessionStorageAsync(string key)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
            }
        }

        public async Task RemoveLocalStorageAsync(string key)
        {
            if (jsRuntime is IJSInProcessRuntime)
            {
                await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
        }
    }
}
