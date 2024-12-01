using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services.API.Responses;
using Microsoft.Playwright;

namespace AutomationFramework.Common.Services.API;

public class LoginAPIPlaywrightService : BaseApiPlaywrightService
{
    public LoginAPIPlaywrightService(ILogging log, IAPIRequestContext request) : base(log, request)
    {
    }

    public async Task<APIResponseWithMessage> VerifyLoginForUser(string email, string password)
    {
        var formData = request.CreateFormData();

        if (!string.IsNullOrEmpty(email))
        {
            formData.Append("email", $"{email}");
        }

        if (!string.IsNullOrEmpty(password))
        {
            formData.Append("password", $"{password}");
        }

        var newIssue = await request.PostAsync("verifyLogin", new() { Form = formData, IgnoreHTTPSErrors = true });

        log.Information($"Verifying user with an email: {email}");


        var resMessage = await newIssue.TextAsync();

        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public async Task<APIResponseWithMessage> VerifyLoginWithIncorrectMethod()
    {
        var response = await request.DeleteAsync("verifyLogin", new() { Form = null, IgnoreHTTPSErrors = true });
        var resMessage = await response.TextAsync();
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);
        return result;
    }
}
