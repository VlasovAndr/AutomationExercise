using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Services;
using AutomationFramework.Common.Services.API;

namespace AutomationFramework.Core.Steps;

public class UserAPISteps : IUserSteps
{
    private readonly UserAPIPlaywrightService _userAPIService;
    private readonly ILogging _log;
    private readonly CleanupPlaywrightTestService _cleanup;

    public UserAPISteps(UserAPIPlaywrightService userAPIService, ILogging log, CleanupPlaywrightTestService cleanup)
    {
        _userAPIService = userAPIService;
        _log = log;
        _cleanup = cleanup;
    }

    public async Task RegisterUser(User user)
    {
        await _userAPIService.RegisterUserAccount(user);
    }

    public async Task DeleteUser(string email, string password)
    {
        await _userAPIService.DeleteUserAccount(email, password);
    }
}
