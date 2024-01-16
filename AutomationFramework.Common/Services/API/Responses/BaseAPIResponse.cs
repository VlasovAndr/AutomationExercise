using System.Text.Json.Serialization;

namespace AutomationFramework.Common.Services.API.Responses;

public class BaseAPIResponse
{
    [JsonPropertyName("responseCode")]
    public int StatusCode { get; set; }
}
