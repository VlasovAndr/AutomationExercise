using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using AutomationFramework.Core.Pages.ContactUsPage;
using AutomationFramework.Core.Pages.HomePage;
using AutomationFramework.Core.Pages.SignupAndLoginPage;
using AutomationFramework.Core.Pages.SignupPage;

[assembly: LevelOfParallelism(3)]

namespace AutomationExerciseUI.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
[AllureFeature("UI Tests")]
public class AutomationExerciseUITests : TestBase
{
    private readonly HomePage homePage;
    private readonly SignupAndLoginPage signupAndLoginPage;
    private readonly SignupPage signupPage;
    private readonly ContactUsPage contactUsPage;
    private readonly DataGeneratorService generatorService;
    private readonly IUserSteps userSteps;

    public AutomationExerciseUITests()
    {
        homePage = container.GetRequiredService<HomePage>();
        signupAndLoginPage = container.GetRequiredService<SignupAndLoginPage>();
        signupPage = container.GetRequiredService<SignupPage>();
        userSteps = container.GetRequiredKeyedService<IUserSteps>("API");
        contactUsPage = container.GetRequiredService<ContactUsPage>();
        generatorService = container.GetRequiredService<DataGeneratorService>();
    }

    [Test, Property("TMSId", "Test Case 1"), Description("Register User")]
    [AllureName("Register User")]
    [Retry(3)]
    public void RegisterUser()
    {
        var user = generatorService.GenerateRandomUser(newsletterInput: true, specialOffersInput: true);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.SignupForm.GetTitle().Should().Be("New User Signup!");
        signupAndLoginPage.SignupForm.Fill(user.Account.Name, user.Account.Email);
        signupAndLoginPage.SignupForm.Submit();

        signupPage.GetSignupFormTitle().Should().Be("ENTER ACCOUNT INFORMATION");
        signupPage.FillAccountInfoForm(user.Account);
        signupPage.FillAddressInfoForm(user.Address);
        signupPage.SubmitSignupForm();
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
    [AllureName("Login User with correct email and password")]
    [Retry(2)]
    public void LoginUserCorrectEmailAndPassword()
    {
        var user = generatorService.GenerateRandomUser();
        userSteps.RegisterUser(user);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.LoginForm.GetTitle().Should().Be("Login to your account");
        signupAndLoginPage.LoginForm.Fill(user.Account.Email, user.Account.Password);
        signupAndLoginPage.LoginForm.Submit();

        homePage.Header.GetAllHeadersText()
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        homePage.Header.ClickOnDeleteAccountMenu();
        signupPage.GetAccountDeletedMessage().Should().Be("ACCOUNT DELETED!");
    }

    [Test, Property("TMSId", "Test Case 3"), Description("Login User with incorrect email and password")]
    [AllureName("Login User with incorrect email and password")]
    [Retry(2)]
    public void LoginUserIncorrectEmailAndPassword()
    {
        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.LoginForm.GetTitle().Should().Be("Login to your account");
        signupAndLoginPage.LoginForm.Fill("EmailNotExist@mail.com", "Password");
        signupAndLoginPage.LoginForm.Submit();

        signupAndLoginPage.LoginForm.GetErrorMessage()
            .Should().Be("Your email or password is incorrect!");
    }

    [Test, Property("TMSId", "Test Case 4"), Description("Logout User")]
    [AllureName("Logout User")]
    [Retry(2)]
    public void LogoutUser()
    {
        var user = generatorService.GenerateRandomUser();
        userSteps.RegisterUser(user);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.LoginForm.GetTitle().Should().Be("Login to your account");
        signupAndLoginPage.LoginForm.Fill(user.Account.Email, user.Account.Password);
        signupAndLoginPage.LoginForm.Submit();

        homePage.Header.GetAllHeadersText()
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        homePage.Header.ClickOnLogoutMenu();
        signupAndLoginPage.LoginForm.GetTitle().Should().Be("Login to your account");
    }

    [Test, Property("TMSId", "Test Case 5"), Description("Register User with existing email")]
    [AllureName("Register User with existing email")]
    [Retry(2)]
    public void RegisterUserWithExistingEmail()
    {
        var user = generatorService.GenerateRandomUser(newsletterInput: true, specialOffersInput: true);
        userSteps.RegisterUser(user);

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();
        homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.SignupForm.GetTitle().Should().Be("New User Signup!");
        signupAndLoginPage.SignupForm.Fill(user.Account.Name, user.Account.Email);
        signupAndLoginPage.SignupForm.Submit();
        signupAndLoginPage.SignupForm.GetErrorMessage().Should().Be("Email Address already exist!");
    }

    [Test, Property("TMSId", "Test Case 6"), Description("Contact Us Form")]
    [AllureName("Contact Us Form")]
    [Retry(3)]
    public void ContactUsForm()
    {
        var contactUsData = generatorService.GenerateContactUsInfo();

        homePage.Open();
        homePage.IsPageOpened().Should().BeTrue();

        homePage.Header.GoToContactUsMenu();
        contactUsPage.GetContactUsFormTitle().Should().Be("GET IN TOUCH");

        contactUsPage.FillContactUsForm(contactUsData);
        contactUsPage.UploadFile("ContactUsData.jpg");
        contactUsPage.Submit();
        contactUsPage.GetSuccessfulMessage()
            .Should().Be("Success! Your details have been submitted successfully.");

        contactUsPage.Header.GoToHomeMenu();
        homePage.IsPageOpened().Should().BeTrue();
    }
}