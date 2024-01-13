using System.Text.Json.Serialization;

namespace AutomationFramework.Common.Models;

public class UserResponseData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("birth_day")]
    public string BirthDay { get; set; }

    [JsonPropertyName("birth_month")]
    public string BirthMonth { get; set; }

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; }
}
