using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Services.API.Responses;
using Microsoft.Playwright;

namespace AutomationFramework.Common.Services.API;

public class UserAPIPlaywrightService : BaseApiPlaywrightService
{
    private CleanupPlaywrightTestService _cleanup;

    public UserAPIPlaywrightService(ILogging log, CleanupPlaywrightTestService cleanup, IAPIRequestContext request) : base(log, request)
    {
        _cleanup = cleanup;
    }

    public async Task<APIResponseWithMessage> RegisterUserAccount(User user, bool isTeardownNeeded = true)
    {
        var formData = request.CreateFormData();
        formData.Append("name", $"{user.Account.Name}");
        formData.Append("email", $"{user.Account.Email}");
        formData.Append("password", $"{user.Account.Password}");
        formData.Append("title", $"{user.Account.Gender}");
        formData.Append("birth_date", $"{user.Account.DateOfBirth.Date}");
        formData.Append("birth_month", $"{user.Account.DateOfBirth.Month}");
        formData.Append("birth_year", $"{user.Account.DateOfBirth.Year}");
        formData.Append("firstname", $"{user.Address.FirstName}");
        formData.Append("lastname", $"{user.Address.LastName}");
        formData.Append("company", $"{user.Address.Country}");
        formData.Append("address1", $"{user.Address.Address}");
        formData.Append("address2", $"{user.Address.Address2}");
        formData.Append("country", $"{user.Address.Country}");
        formData.Append("zipcode", $"{user.Address.Zipcode}");
        formData.Append("state", $"{user.Address.State}");
        formData.Append("city", $"{user.Address.City}");
        formData.Append("mobile_number", $"{user.Address.MobileNumber}");
        formData.Append("birth_year", $"{user.Account.DateOfBirth.Year}");
        formData.Append("birth_year", $"{user.Account.DateOfBirth.Year}");
        formData.Append("birth_year", $"{user.Account.DateOfBirth.Year}");

        var response = await request.PostAsync("createAccount", new() { Form = formData, IgnoreHTTPSErrors = true });
        var resMessage = await response.TextAsync();
        log.Information($"User with email: {user.Account.Email} is registered");

        if (isTeardownNeeded)
        {
            _cleanup.AddCleanupAction(async () => await DeleteUserAccount(user.Account.Email, user.Account.Password));
        }

        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public async Task<APIResponseWithMessage> DeleteUserAccount(string email, string password)
    {
        var formData = request.CreateFormData();
        formData.Append("email", $"{email}");
        formData.Append("password", $"{password}");

        var response = await request.DeleteAsync("deleteAccount", new() { Form = formData, IgnoreHTTPSErrors = true });

        log.Information($"User with email: {email} is deleted");

        var resMessage = await response.TextAsync();
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public async Task<APIResponseWithMessage> UpdateUserAccount(User user, bool isTeardownNeeded = true)
    {
        var formData = request.CreateFormData();
        formData.Append("name", $"{user.Account.Name}");
        formData.Append("email", $"{user.Account.Email}");
        formData.Append("password", $"{user.Account.Password}");
        formData.Append("title", $"{user.Account.Gender}");
        formData.Append("birth_date", $"{user.Account.DateOfBirth.Date}");
        formData.Append("birth_month", $"{user.Account.DateOfBirth.Month}");
        formData.Append("birth_year", $"{user.Account.DateOfBirth.Year}");
        formData.Append("firstname", $"{user.Address.FirstName}");
        formData.Append("lastname", $"{user.Address.LastName}");
        formData.Append("company", $"{user.Address.Country}");
        formData.Append("address1", $"{user.Address.Address}");
        formData.Append("address2", $"{user.Address.Address2}");
        formData.Append("country", $"{user.Address.Country}");
        formData.Append("zipcode", $"{user.Address.Zipcode}");
        formData.Append("state", $"{user.Address.State}");
        formData.Append("city", $"{user.Address.City}");
        formData.Append("mobile_number", $"{user.Address.MobileNumber}");

        var response = await request.PutAsync("updateAccount", new() { Form = formData, IgnoreHTTPSErrors = true });
        var resMessage = await response.TextAsync();
        log.Information($"User with email: {user.Account.Email} is registered");

        if (isTeardownNeeded)
        {
            _cleanup.AddCleanupAction(async () => await DeleteUserAccount(user.Account.Email, user.Account.Password));
        }

        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public async Task<UserAPIResponse> GetUserAccountDetail(string email)
    {
        var parameters = new Dictionary<string, object>
        {
            { "email", $"{email}" }
        };

        var response = await request.GetAsync("getUserDetailByEmail", new APIRequestContextOptions() { Params = parameters });
        var resMessage = await response.TextAsync();
        log.Information($"Getting user with email: {email}");

        var result = HandleResponseMessage<UserAPIResponse>(resMessage);

        return result;
    }
}
