namespace Byte.TalentScout.UI.Services
{
    public interface IStorageService
    {
        Task SetLocalStorageAsync(string key, string value);
        Task<string> GetLocalStorageAsync(string key);
        Task SetSessionStorageAsync(string key, string value);
        Task<string> GetSessionStorageAsync(string key);
        Task RemoveSessionStorageAsync(string key);
        Task RemoveLocalStorageAsync(string key);
    }
}
