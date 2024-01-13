namespace AutomationFramework.Common.Services.API;

public class APIClient : IDisposable
{
    private readonly Lazy<HttpClient> httpClientService;
    private HttpClient HttpClient => httpClientService.Value;

    public APIClient()
    {
        httpClientService = new Lazy<HttpClient>(CreateHttpClient, true);
    }

    private HttpClient CreateHttpClient()
    {
        var client = new HttpClient();
        return client;
    }

    public Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return HttpClient.GetAsync(requestUri);
    }

    public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent)
    {
        return HttpClient.PostAsync(requestUri, httpContent);
    }

    public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent httpContent)
    {
        return HttpClient.PutAsync(requestUri, httpContent);
    }

    public Task<HttpResponseMessage> SendDeleteRequest(string requestUri, HttpContent httpContent)
    {
        var request = new HttpRequestMessage
        {
            Content = httpContent,
            Method = HttpMethod.Delete,
            RequestUri = new Uri(requestUri)
        };

        return HttpClient.SendAsync(request);
    }

    public void Dispose()
    {
        if (httpClientService.IsValueCreated)
        {
            HttpClient.Dispose();
        }
    }
}
