namespace WebMVC.Client.Services;

public interface IApiService
{
    Task<T> GetAsync<T>(string url, Dictionary<string, string> queryParams = null);
    Task<T> PostAsync<T>(string url, object data);
    Task<T> PutAsync<T>(string url, object data);
    Task<T> DeleteAsync<T>(string url);
    void AddAuthorizationHeader(string token);
}
