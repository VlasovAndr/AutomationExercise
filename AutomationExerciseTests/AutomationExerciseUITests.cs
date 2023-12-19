using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using AutomationFrameworkCommon.Services;

[assembly: LevelOfParallelism(3)]

namespace AutomationExercise.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

public class ElementsMenuTests : TestBase
{
    private readonly HomePage homePage;
    private readonly SignupAndLoginPage signupAndLoginPage;
    private readonly SignupPage signupPage;

    public ElementsMenuTests()
    {
        homePage = container.GetRequiredService<HomePage>();
        signupAndLoginPage = container.GetRequiredService<SignupAndLoginPage>();
        signupPage = container.GetRequiredService<SignupPage>();
    }

    [Test, Property("TMSId", "Test Case 1")]
    [Category("Register User")]
    public void RegisterUser()
    {
        var user = new DataGeneratorService().GenerateRandomUser(newsletterInput: true, specialOffersInput: true);

        homePage.Open();
        homePage.IsPageOpen().Should().BeTrue();
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
}