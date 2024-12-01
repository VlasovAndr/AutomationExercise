using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services.API.Responses;

namespace AutomationFramework.Common.Services.API;

public class LoginAPIService : BaseApiRequestService
{
    public LoginAPIService(APIClient apiClient, ILogging log) : base(apiClient, log)
    {
    }

    public APIResponseWithMessage VerifyLoginForUser(string email, string password)
    {
        var url = $"{BASEURL}/verifyLogin";
        var parameters = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(email))
        {
            parameters["email"] = email;
        }

        if (!string.IsNullOrEmpty(password))
        {
            parameters["password"] = password;
        }

        var content = new FormUrlEncodedContent(parameters);
        var response = PerformPostFormData(url, content);

        log.Information($"Verifying user with an email: {email}");

        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public APIResponseWithMessage VerifyLoginWithIncorrectMethod()
    {
        var url = $"{BASEURL}/verifyLogin";
        var response = PerformDeleteFormData(url, null);
        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }
}
