using AutomationFramework.Common.Models;

namespace AutomationFramework.Common.Abstractions;

public interface IUserSteps
{
    void RegisterUser(User user);
    void DeleteUser(string email, string password);
}
