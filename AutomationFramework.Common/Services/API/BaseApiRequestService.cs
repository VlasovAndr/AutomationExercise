using AutomationFramework.Common.Abstractions;

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

    public Task<HttpResponseMessage> PerformPostFormData(string url, FormUrlEncodedContent content)
    {
        var response = apiClient.PostAsync(url, content);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }

    public Task<HttpResponseMessage> PerformDeleteFormData(string url, FormUrlEncodedContent content)
    {
        var response = apiClient.SendDeleteRequest(url, content);
        response.Result.EnsureSuccessStatusCode();

        return response;
    }
}
