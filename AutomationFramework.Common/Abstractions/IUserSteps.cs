using AutomationFramework.Common.Models;

namespace AutomationFramework.Common.Abstractions;

public interface IUserSteps
{
    Task RegisterUser(User user);
    Task DeleteUser(string email, string password);
}
