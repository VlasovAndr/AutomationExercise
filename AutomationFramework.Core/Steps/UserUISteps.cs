using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFramework.Core.Steps;

public class UserUISteps : IUserSteps
{
    public IServiceProvider container;
    private readonly SignupAndLoginPage signupAndLoginPage;
    private readonly SignupPage signupPage;

    public UserUISteps(IServiceProvider container)
    {
        this.container = container;
        signupAndLoginPage = container.GetRequiredService<SignupAndLoginPage>();
        signupPage = container.GetRequiredService<SignupPage>();
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
}
