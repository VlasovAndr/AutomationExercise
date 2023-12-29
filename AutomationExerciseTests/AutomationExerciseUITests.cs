using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using AutomationFrameworkCommon.Services;
using AutomationFramework.Common.Abstractions;

[assembly: LevelOfParallelism(3)]

namespace AutomationExerciseUI.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class AutomationExerciseUITests : TestBase
{
    private readonly HomePage homePage;
    private readonly SignupAndLoginPage signupAndLoginPage;
    private readonly SignupPage signupPage;
    private readonly IUserSteps userSteps;

    public AutomationExerciseUITests()
    {
        homePage = container.GetRequiredService<HomePage>();
        signupAndLoginPage = container.GetRequiredService<SignupAndLoginPage>();
        signupPage = container.GetRequiredService<SignupPage>();
        userSteps = container.GetRequiredKeyedService<IUserSteps>("API");
    }

    [Test, Property("TMSId", "Test Case 1"), Description("Register User")]
    public void RegisterUser()
    {
        var user = new DataGeneratorService().GenerateRandomUser(newsletterInput: true, specialOffersInput: true);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetSignupFormTitle().Should().Be("New User Signup!");
        signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
        signupAndLoginPage.ClickOnSignUpBtn();

        signupPage.GetSignupFormTitle().Should().Be("ENTER ACCOUNT INFORMATION");
        signupPage.FillAccountInfoForm(user.Account);
        signupPage.FillAddressInfoForm(user.Address);
        signupPage.ClickOnCreateAccountBtn();
        signupPage.GetAccountCreatedMessage().Should().Be("ACCOUNT CREATED!");
        signupPage.ClickOnContinueBtn();

        homePage.Header.GetAllHeadersText()
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        homePage.Header.ClickOnDeleteAccountMenu();
        signupPage.GetAccountDeletedMessage().Should().Be("ACCOUNT DELETED!");
        signupPage.ClickOnContinueBtn();
    }

    [Test, Property("TMSId", "Test Case 2"), Description("Login User with correct email and password")]
    public void LoginUserCorrectEmailAndPassword()
    {
        var user = new DataGeneratorService().GenerateRandomUser();
        userSteps.RegisterUser(user);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetLoginFormTitle().Should().Be("Login to your account");
        signupAndLoginPage.FillLoginForm(user.Account.Email, user.Account.Password);
        signupAndLoginPage.ClickOnLoginBtn();

        homePage.Header.GetAllHeadersText()
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        homePage.Header.ClickOnDeleteAccountMenu();
        signupPage.GetAccountDeletedMessage().Should().Be("ACCOUNT DELETED!");
    }

    [Test, Property("TMSId", "Test Case 3"), Description("Login User with incorrect email and password")]
    public void LoginUserIncorrectEmailAndPassword()
    {
        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetLoginFormTitle().Should().Be("Login to your account");
        signupAndLoginPage.FillLoginForm("EmailNotExist@mail.com", "Password");
        signupAndLoginPage.ClickOnLoginBtn();

        signupAndLoginPage.GetLoginFormErrorMessage()
            .Should().Be("Your email or password is incorrect!");
    }
}