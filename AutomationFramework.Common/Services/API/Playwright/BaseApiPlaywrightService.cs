using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services.API.Responses;
using Microsoft.Playwright;
using System.Text.Json;

namespace AutomationFramework.Common.Services.API;

public class BaseApiPlaywrightService
{
    protected readonly ILogging log;
    protected readonly IAPIRequestContext request;

    public BaseApiPlaywrightService(ILogging log, IAPIRequestContext request)
    {
        this.request = request;
        this.log = log;
    }

    public T HandleResponseMessage<T>(string response)
        where T : BaseAPIResponse
    {
        var result = JsonSerializer.Deserialize<T>(response);
        return result;
    }
}
