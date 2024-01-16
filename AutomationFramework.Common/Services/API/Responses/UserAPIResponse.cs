using AutomationFramework.Common.Models;
using System.Text.Json.Serialization;

namespace AutomationFramework.Common.Services.API.Responses;

public class UserAPIResponse : BaseAPIResponse
{
    [JsonPropertyName("user")]
    public UserResponseData User { get; set; }
}
