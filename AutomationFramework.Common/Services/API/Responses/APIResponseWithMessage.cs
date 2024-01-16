using System.Text.Json.Serialization;

namespace AutomationFramework.Common.Services.API.Responses;

public class APIResponseWithMessage : BaseAPIResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
