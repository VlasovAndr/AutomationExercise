using AutomationFramework.Common.Services;
using AutomationFramework.Common.Services.API;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomationExerciseAPI.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class AutomationExerciseAPIPlaywrightTests : TestBase
{
    private DataGeneratorService generatorService => container.GetRequiredService<DataGeneratorService>();
    private UserAPIPlaywrightService userAPIPlaywrightService => container.GetRequiredService<UserAPIPlaywrightService>();
    private LoginAPIPlaywrightService loginAPIPlaywrightService => container.GetRequiredService<LoginAPIPlaywrightService>();

    [Test, Property("TMSId", "API 7"), Description("POST To Verify Login with valid details")]
    public async Task VerifyLoginWithValidDetails()
    {
        var user = new DataGeneratorService().GenerateRandomUser();
        await userAPIPlaywrightService.RegisterUserAccount(user);

        var response = await loginAPIPlaywrightService.VerifyLoginForUser(user.Account.Email, user.Account.Password);

        response.StatusCode.Should().Be(200);
        response.Message.Should().Be("User exists!");
    }

    [Test, Property("TMSId", "API 8"), Description("POST To Verify Login without email parameter")]
    public async Task VerifyLoginWithoutEmailParameter()
    {
        var user = new DataGeneratorService().GenerateRandomUser();
        await userAPIPlaywrightService.RegisterUserAccount(user);

        var response = await loginAPIPlaywrightService.VerifyLoginForUser(null, user.Account.Password);

        response.StatusCode.Should().Be(400);
        response.Message.Should().Be("Bad request, email or password parameter is missing in POST request.");
    }

    [Test, Property("TMSId", "API 9"), Description("DELETE To Verify Login")]
    public async Task VerifyLoginWithInvalidMethod()
    {
        var response = await loginAPIPlaywrightService.VerifyLoginWithIncorrectMethod();

        response.StatusCode.Should().Be(405);
        response.Message.Should().Be("This request method is not supported.");
    }

    [Test, Property("TMSId", "API 10"), Description("POST To Verify Login with invalid details")]
    public async Task VerifyLoginWithInvalidDetails()
    {
        string email = "notExistEmai@mail.com";
        string password = "notExistPassword";

        var response = await loginAPIPlaywrightService.VerifyLoginForUser(email, password);

        response.StatusCode.Should().Be(404);
        response.Message.Should().Be("User not found!");
    }

    [Test, Property("TMSId", "API 11"), Description("POST To Create/Register User Account")]
    public async Task CreateRegisterUserAccount()
    {
        var user = new DataGeneratorService().GenerateRandomUser();

        var response = await userAPIPlaywrightService.RegisterUserAccount(user);

        response.StatusCode.Should().Be(201);
        response.Message.Should().Be("User created!");
    }

    [Test, Property("TMSId", "API 12"), Description("DELETE METHOD To Delete User Account")]
    public async Task DeleteUserAccount()
    {
        var user = generatorService.GenerateRandomUser();
        await userAPIPlaywrightService.RegisterUserAccount(user);

        var response = await userAPIPlaywrightService.DeleteUserAccount(user.Account.Email, user.Account.Password);

        response.StatusCode.Should().Be(200);
        response.Message.Should().Be("Account deleted!");
    }

    [Test, Property("TMSId", "API 13"), Description("PUT METHOD To Update User Account")]
    public async Task UpdateUserAccount()
    {
        var user = generatorService.GenerateRandomUser();
        await userAPIPlaywrightService.RegisterUserAccount(user, false);
        user.Address.Address = "New Address";
        user.Account.IsNewsletter = true;
        user.Account.IsSpecialOffers = true;

        var response = await userAPIPlaywrightService.UpdateUserAccount(user);

        response.StatusCode.Should().Be(200);
        response.Message.Should().Be("User updated!");
    }

    [Test, Property("TMSId", "API 14"), Description("GET user account detail by email")]
    public async Task GetUserAccountDetailByEmail()
    {
        var user = generatorService.GenerateRandomUser();
        await userAPIPlaywrightService.RegisterUserAccount(user);

        var response = await userAPIPlaywrightService.GetUserAccountDetail(user.Account.Email);

        response.StatusCode.Should().Be(200);
        response.User.FirstName.Should().Be(user.Address.FirstName);
        response.User.LastName.Should().Be(user.Address.LastName);
        response.User.Address1.Should().Be(user.Address.Address);
        response.User.Address2.Should().Be(user.Address.Address2);
        response.User.Country.Should().Be(user.Address.Country);
        response.User.City.Should().Be(user.Address.City);
        response.User.State.Should().Be(user.Address.State);
        response.User.Zipcode.Should().Be(user.Address.Zipcode);
    }
}