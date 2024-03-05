using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using WebMVC.Client.HttpClientHelpers.Exceptions;
using WebMVC.Client.HttpClientHelpers.Responses;

namespace WebMVC.Client.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("BaseAPIUrl"));
    }

    public void AddAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<T> GetAsync<T>(string url, Dictionary<string, string> queryParams = null)
    {
        var response = await SendRequestAsync<T>(HttpMethod.Get, url, queryParams);
        return response;
    }

    public async Task<T> PostAsync<T>(string url, object data)
    {
        var response = await SendRequestAsync<T>(HttpMethod.Post, url, null, data);
        return response;
    }

    public async Task<T> PutAsync<T>(string url, object data)
    {
        var response = await SendRequestAsync<T>(HttpMethod.Put, url, null, data);
        return response;
    }

    public async Task<T> DeleteAsync<T>(string url)
    {
        var response = await SendRequestAsync<T>(HttpMethod.Delete, url);
        return response;
    }

    private async Task<T> SendRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> queryParams = null, object data = null)
    {
        HttpRequestMessage request = new HttpRequestMessage(method, BuildUrl(url, queryParams));

        if (data != null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var apiError = JsonConvert.DeserializeObject<ApiErrorResponse>(errorContent);
            throw new BaseApiException(apiError);
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    private string BuildUrl(string url, Dictionary<string, string> queryParams)
    {
        if (queryParams != null && queryParams.Any())
        {
            var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            return $"{url}?{queryString}";
        }

        return url;
    }

}
