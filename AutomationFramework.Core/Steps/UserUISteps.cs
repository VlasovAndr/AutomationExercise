using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFramework.Core.Steps;

public class UserUISteps : IUserSteps
{
    private IServiceProvider container;
    private readonly SignupAndLoginPage signupAndLoginPage;
    private readonly SignupPage signupPage;

    public UserUISteps(IServiceProvider container)
    {
        this.container = container;
        signupAndLoginPage = this.container.GetRequiredService<SignupAndLoginPage>();
        signupPage = this.container.GetRequiredService<SignupPage>();
    }

    public void RegisterUser(User user)
    {
        signupAndLoginPage.Open();
        signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
        signupAndLoginPage.ClickOnSignUpBtn();

        signupPage.FillAccountInfoForm(user.Account);
        signupPage.FillAddressInfoForm(user.Address);
        signupPage.ClickOnCreateAccountBtn();
        signupPage.ClickOnContinueBtn();
        signupPage.Close();
    }

    public void DeleteUser(string email, string password)
    {
        signupAndLoginPage.Open();
        signupAndLoginPage.FillLoginForm(email, password);
        signupAndLoginPage.ClickOnLoginBtn();
        signupAndLoginPage.Close();
    }
}
