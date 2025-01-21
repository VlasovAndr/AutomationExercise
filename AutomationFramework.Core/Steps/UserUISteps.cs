using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages.SignupAndLoginPage;
using AutomationFramework.Core.Pages.SignupPage;
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
        signupAndLoginPage.SignupForm.Fill(user.Account.Name, user.Account.Email);
        signupAndLoginPage.SignupForm.Submit();

        signupPage.FillAccountInfoForm(user.Account);
        signupPage.FillAddressInfoForm(user.Address);
        signupPage.SubmitSignupForm();
        signupPage.ClickOnContinueBtn();
        signupPage.Close();
    }

    public void DeleteUser(string email, string password)
    {
        signupAndLoginPage.Open();
        signupAndLoginPage.LoginForm.Fill(email, password);
        signupAndLoginPage.LoginForm.Submit();
        signupAndLoginPage.Close();
    }
}
