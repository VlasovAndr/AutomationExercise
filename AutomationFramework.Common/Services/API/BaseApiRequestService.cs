using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services.API.Responses;
using System.Text.Json;

namespace AutomationFramework.Common.Services.API;

public class BaseApiRequestService
{
    private readonly APIClient apiClient;
    protected readonly ILogging log;

    public BaseApiRequestService(APIClient apiClient, ILogging log)
    {
        this.apiClient = apiClient;
        this.log = log;
    }

    public Task<HttpResponseMessage> PerformGet(string url, Dictionary<string, string> parameters)
    {
        var queryString = string.Join("?", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
        var urlWithParam = $"{url}?{queryString}";

        var response = apiClient.GetAsync(urlWithParam);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }

    public Task<HttpResponseMessage> PerformPostFormData(string url, FormUrlEncodedContent content)
    {
        var response = apiClient.PostAsync(url, content);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }

    public Task<HttpResponseMessage> PerformPutFormData(string url, FormUrlEncodedContent content)
    {
        var response = apiClient.PutAsync(url, content);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }

    public Task<HttpResponseMessage> PerformDeleteFormData(string url, FormUrlEncodedContent content)
    {
        var response = apiClient.SendDeleteRequest(url, content);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }

    public T HandleResponseMessage<T>(string response)
        where T : BaseAPIResponse
    {
        var a = new JsonSerializerOptions();
        a.IncludeFields = false;
        var result = JsonSerializer.Deserialize<T>(response, a);
        return result;
    }
}
