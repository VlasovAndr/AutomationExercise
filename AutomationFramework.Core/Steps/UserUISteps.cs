using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages;

namespace AutomationFramework.Core.Steps;

public class UserUISteps : IUserSteps
{
    private readonly SignupAndLoginPage _signupAndLoginPage;
    private readonly SignupPage _signupPage;
    private HomePage _homePage;

    public UserUISteps(SignupAndLoginPage signupAndLoginPage, SignupPage signupPage, HomePage homePage)
    {
        _signupAndLoginPage = signupAndLoginPage;
        _signupPage = signupPage;
        _homePage = homePage;
    }

    public async Task RegisterUser(User user)
    {
        await _signupAndLoginPage.Open();
        await _signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
        await _signupAndLoginPage.SubmitSignupForm();

        await _signupPage.FillAccountInfoForm(user.Account);
        await _signupPage.FillAddressInfoForm(user.Address);
        await _signupPage.SubmitSignupForm();
        await _signupPage.ClickOnContinueBtn();
        await _signupPage.ClearPage();
    }

    public async Task DeleteUser(string email, string password)
    {
        await _signupAndLoginPage.Open();
        await _signupAndLoginPage.FillLoginForm(email, password);
        await _signupAndLoginPage.SubmitLoginForm();
        await _homePage.Header.ClickOnDeleteAccountMenu();
    }
}
