using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Services.API.Responses;

namespace AutomationFramework.Common.Services.API;

public class UserAPIService : BaseApiRequestService
{
    private CleanupTestService cleanup;

    public UserAPIService(APIClient apiClient, ILogging log, CleanupTestService cleanup) : base(apiClient, log)
    {
        this.cleanup = cleanup;
    }

    public APIResponseWithMessage RegisterUserAccount(User user, bool isTeardownNeeded = true)
    {
        var url = $"{BASEURL}/createAccount";
        var parameters = new Dictionary<string, string>
        {
            { "name", $"{user.Account.Name}" },
            { "email", $"{user.Account.Email}" },
            { "password", $"{user.Account.Password}" },
            { "title", $"{user.Account.Gender}" },
            { "birth_date", $"{user.Account.DateOfBirth.Date}" },
            { "birth_month", $"{user.Account.DateOfBirth.Month}" },
            { "birth_year", $"{user.Account.DateOfBirth.Year}" },
            { "firstname", $"{user.Address.FirstName}" },
            { "lastname", $"{user.Address.LastName}" },
            { "company", $"{user.Address.Country}" },
            { "address1", $"{user.Address.Address}" },
            { "address2", $"{user.Address.Address2}" },
            { "country", $"{user.Address.Country}" },
            { "zipcode", $"{user.Address.Zipcode}" },
            { "state", $"{user.Address.State}" },
            { "city", $"{user.Address.City}" },
            { "mobile_number", $"{user.Address.MobileNumber}" }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = PerformPostFormData(url, content);

        log.Information($"User with email: {user.Account.Email} is registered");

        if (isTeardownNeeded)
        {
            cleanup.AddCleanupAction(() => DeleteUserAccount(user.Account.Email, user.Account.Password));
        }

        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public APIResponseWithMessage DeleteUserAccount(string email, string password)
    {
        var url = $"{BASEURL}/deleteAccount";
        var parameters = new Dictionary<string, string>
        {
            { "email", $"{email}" },
            { "password", $"{password}" }
        };

        var formData = new FormUrlEncodedContent(parameters);
        var response = PerformDeleteFormData(url, formData);

        log.Information($"User with email: {email} is deleted");

        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public APIResponseWithMessage UpdateUserAccount(User user, bool isTeardownNeeded = true)
    {
        var url = $"{BASEURL}/updateAccount";
        var parameters = new Dictionary<string, string>
        {
            { "name", $"{user.Account.Name}" },
            { "email", $"{user.Account.Email}" },
            { "password", $"{user.Account.Password}" },
            { "title", $"{user.Account.Gender}" },
            { "birth_date", $"{user.Account.DateOfBirth.Date}" },
            { "birth_month", $"{user.Account.DateOfBirth.Month}" },
            { "birth_year", $"{user.Account.DateOfBirth.Year}" },
            { "firstname", $"{user.Address.FirstName}" },
            { "lastname", $"{user.Address.LastName}" },
            { "company", $"{user.Address.Country}" },
            { "address1", $"{user.Address.Address}" },
            { "address2", $"{user.Address.Address2}" },
            { "country", $"{user.Address.Country}" },
            { "zipcode", $"{user.Address.Zipcode}" },
            { "state", $"{user.Address.State}" },
            { "city", $"{user.Address.City}" },
            { "mobile_number", $"{user.Address.MobileNumber}" }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = PerformPutFormData(url, content);

        cleanup.AddCleanupAction(() => DeleteUserAccount(user.Account.Email, user.Account.Password));
        log.Information($"User with email: {user.Account.Email} is registered");

        if (isTeardownNeeded)
        {
            cleanup.AddCleanupAction(() => DeleteUserAccount(user.Account.Email, user.Account.Password));
        }

        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<APIResponseWithMessage>(resMessage);

        return result;
    }

    public UserAPIResponse GetUserAccountDetail(string email)
    {
        var url = $"{BASEURL}/getUserDetailByEmail";
        var parameters = new Dictionary<string, string>
        {
            { "email", $"{email}" }
        };

        var response = PerformGet(url, parameters);
        var resMessage = response.Result.Content.ReadAsStringAsync().Result;
        var result = HandleResponseMessage<UserAPIResponse>(resMessage);
        log.Information($"Getting user with email: {email}");

        return result;
    }
}
