using AutomationFramework.Core.Pages;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: LevelOfParallelism(3)]

namespace AutomationExerciseUI.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
[AllureFeature("UI Tests")]
public class AutomationExerciseUITests : TestBase
{
    private HomePage homePage => container.GetRequiredService<HomePage>();
    private SignupAndLoginPage signupAndLoginPage => container.GetRequiredService<SignupAndLoginPage>();
    private SignupPage signupPage => container.GetRequiredService<SignupPage>();
    private ContactUsPage contactUsPage => container.GetRequiredService<ContactUsPage>();
    private DataGeneratorService generatorService => container.GetRequiredService<DataGeneratorService>();
    private IUserSteps userSteps => container.GetRequiredKeyedService<IUserSteps>("API");

    [Test, Property("TMSId", "Test Case 1"), Description("Register User")]
    [AllureName("Register User")]
    public async Task RegisterUser()
    {
        var user = generatorService.GenerateRandomUser(newsletterInput: true, specialOffersInput: true);

        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();
        await homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetSignupFormTitle().Result.Should().Be("New User Signup!");
        await signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
        await signupAndLoginPage.SubmitSignupForm();

        signupPage.GetSignupFormTitle().Result.Should().Be("Enter Account Information");
        await signupPage.FillAccountInfoForm(user.Account);
        await signupPage.FillAddressInfoForm(user.Address);
        await signupPage.SubmitSignupForm();
        signupPage.GetAccountCreatedMessage().Result.Should().Be("Account Created!");
        await signupPage.ClickOnContinueBtn();

        homePage.Header.GetAllHeadersText().Result
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        await homePage.Header.ClickOnDeleteAccountMenu();
        signupPage.GetAccountDeletedMessage().Result.Should().Be("Account Deleted!");
        await signupPage.ClickOnContinueBtn();
    }

    [Test, Property("TMSId", "Test Case 2"), Description("Login User with correct email and password")]
    [AllureName("Login User with correct email and password")]
    public async Task LoginUserCorrectEmailAndPassword()
    {
        var user = generatorService.GenerateRandomUser();
        await userSteps.RegisterUser(user);

        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();
        await homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetLoginFormTitle().Result.Should().Be("Login to your account");
        await signupAndLoginPage.FillLoginForm(user.Account.Email, user.Account.Password);
        await signupAndLoginPage.SubmitLoginForm();

        homePage.Header.GetAllHeadersText().Result
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        await homePage.Header.ClickOnDeleteAccountMenu();
        signupPage.GetAccountDeletedMessage().Result.Should().Be("Account Deleted!");
    }

    [Test, Property("TMSId", "Test Case 3"), Description("Login User with incorrect email and password")]
    [AllureName("Login User with incorrect email and password")]
    [Retry(2)]
    public async Task LoginUserIncorrectEmailAndPassword()
    {
        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();
        await homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetLoginFormTitle().Result.Should().Be("Login to your account");
        await signupAndLoginPage.FillLoginForm("EmailNotExist@mail.com", "Password");
        await signupAndLoginPage.SubmitLoginForm();

        signupAndLoginPage.GetLoginFormErrorMessage().Result
            .Should().Be("Your email or password is incorrect!");
    }

    [Test, Property("TMSId", "Test Case 4"), Description("Logout User")]
    [AllureName("Logout User")]
    public async Task LogoutUser()
    {
        var user = generatorService.GenerateRandomUser();
        await userSteps.RegisterUser(user);

        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();
        await homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetLoginFormTitle().Result.Should().Be("Login to your account");
        await signupAndLoginPage.FillLoginForm(user.Account.Email, user.Account.Password);
        await signupAndLoginPage.SubmitLoginForm();

        homePage.Header.GetAllHeadersText().Result
            .Any(x => x.Contains($"Logged in as {user.Account.Name}"))
            .Should().BeTrue();

        await homePage.Header.ClickOnLogoutMenu();
        signupAndLoginPage.GetLoginFormTitle().Result.Should().Be("Login to your account");
    }

    [Test, Property("TMSId", "Test Case 5"), Description("Register User with existing email")]
    [AllureName("Register User with existing email")]
    public async Task RegisterUserWithExistingEmail()
    {
        var user = generatorService.GenerateRandomUser(newsletterInput: true, specialOffersInput: true);
        await userSteps.RegisterUser(user);

        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();
        await homePage.Header.GoToSignupLoginMenu();

        signupAndLoginPage.GetSignupFormTitle().Result.Should().Contain("New User Signup!");
        await signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
        await signupAndLoginPage.SubmitSignupForm();
        signupAndLoginPage.GetSignUpErrorMessage().Result.Should().Be("Email Address already exist!");
    }

    [Test, Property("TMSId", "Test Case 6"), Description("Contact Us Form")]
    [AllureName("Contact Us Form")]
    public async Task ContactUsForm()
    {
        var contactUsData = generatorService.GenerateContactUsInfo();

        await homePage.Open();
        homePage.IsPageOpened().Result.Should().BeTrue();

        await homePage.Header.GoToContactUsMenu();
        contactUsPage.GetContactUsFormTitle().Result.Should().Be("Get In Touch");

        await contactUsPage.FillContactUsForm(contactUsData);
        await contactUsPage.UploadFile("ContactUsData.jpg");
        await contactUsPage.Submit();
        contactUsPage.GetSuccessfulMessage().Result
            .Should().Be("Success! Your details have been submitted successfully.");

        await contactUsPage.Header.GoToHomeMenu();
        homePage.IsPageOpened().Result.Should().BeTrue();
    }
}