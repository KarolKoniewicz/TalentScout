using System.Collections.ObjectModel;

namespace Byte.TalentScout.UI.Handlers;

public class HttpClientErrorWrapper
{
    private readonly HttpClient _httpClient;
    public ObservableCollection<Exception> Exceptions;

    public HttpClientErrorWrapper(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Exceptions = new ObservableCollection<Exception>();
    }

    public async Task<HttpResponseMessage> GetAsync(string? requestUri)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }
    public async Task<HttpResponseMessage> PutAsync(string? requestUri, HttpContent content)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.PutAsync(requestUri, content);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }
    public async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent content)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.PostAsync(requestUri, content);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }
    public async Task<HttpResponseMessage> DeleteAsync(string? requestUri)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }
    public async Task<TValue?> GetFromJsonAsync<TValue>(string? requestUri)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<TValue>(requestUri);

        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return default;
    }
    public async Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string? requestUri, TValue content)
    {
        var response = new HttpResponseMessage();
        try
        {
            response = await _httpClient.PutAsJsonAsync(requestUri, content);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }
    public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue content)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.PostAsJsonAsync(requestUri, content);
            if (!response.IsSuccessStatusCode)
                Exceptions.Add(new Exception(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception ex)
        {
            Exceptions.Add(ex);
        }

        return response;
    }

    public void RemoveException(Exception ex)
    {
        Exceptions.Remove(ex);
    }
}
