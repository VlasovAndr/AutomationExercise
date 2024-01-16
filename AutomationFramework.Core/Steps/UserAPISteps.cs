using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Services.API;

namespace AutomationFramework.Core.Steps;

public class UserAPISteps : IUserSteps
{
    private readonly UserAPIService userAPIService;
    private readonly ILogging log;

    public UserAPISteps(UserAPIService userAPIService, ILogging log)
    {
        this.userAPIService = userAPIService;
        this.log = log;
    }

    public void RegisterUser(User user)
    {
        userAPIService.RegisterUserAccount(user);
    }

    public void DeleteUser(string email, string password)
    {
        userAPIService.DeleteUserAccount(email, password);
    }
}
