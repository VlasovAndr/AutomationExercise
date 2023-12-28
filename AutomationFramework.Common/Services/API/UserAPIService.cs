using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;

namespace AutomationFramework.Common.Services.API;

public class UserAPIService : BaseApiRequestService
{
    private CleanupTestService cleanup;
    private const string BASEURL = "https://automationexercise.com/api";

    public UserAPIService(APIClient apiClient, ILogging log, CleanupTestService cleanup) : base(apiClient, log)
    {
        this.cleanup = cleanup;
    }

    public HttpResponseMessage RegisterUserAccount(User user)
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

        cleanup.AddCleanupAction(() => DeleteUserAccount(user.Account.Email, user.Account.Password));
        log.Information($"User with email: {user.Account.Email} is registered");

        return response.Result;
    }

    public HttpResponseMessage DeleteUserAccount(string email, string password)
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

        return response.Result;
    }
}
