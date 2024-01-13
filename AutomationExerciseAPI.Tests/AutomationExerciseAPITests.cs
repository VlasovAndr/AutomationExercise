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
public class AutomationExerciseAPITests : TestBase
{
    private readonly UserAPIService userAPIService;
    private readonly DataGeneratorService generatorService;

    public AutomationExerciseAPITests()
    {
        userAPIService = container.GetRequiredService<UserAPIService>();
        generatorService = container.GetRequiredService<DataGeneratorService>();
    }

    [Test, Property("TMSId", "API 11"), Description("POST To Create/Register User Account")]
    public void CreateRegisterUserAccount()
    {
        var user = new DataGeneratorService().GenerateRandomUser();

        var response = userAPIService.RegisterUserAccount(user);

        response.StatusCode.Should().Be(201);
        response.Message.Should().Be("User created!");
    }

    [Test, Property("TMSId", "API 12"), Description("DELETE METHOD To Delete User Account")]
    public void DeleteUserAccount()
    {
        var user = generatorService.GenerateRandomUser();
        userAPIService.RegisterUserAccount(user);

        var response = userAPIService.DeleteUserAccount(user.Account.Email, user.Account.Password);

        response.StatusCode.Should().Be(200);
        response.Message.Should().Be("Account deleted!");
    }

    [Test, Property("TMSId", "API 13"), Description("PUT METHOD To Update User Account")]
    public void UpdateUserAccount()
    {
        var user = generatorService.GenerateRandomUser();
        userAPIService.RegisterUserAccount(user, false);
        user.Address.Address = "New Address";
        user.Account.IsNewsletter = true;
        user.Account.IsSpecialOffers = true;

        var response = userAPIService.UpdateUserAccount(user);

        response.StatusCode.Should().Be(200);
        response.Message.Should().Be("User updated!");
    }

    [Test, Property("TMSId", "API 14"), Description("GET user account detail by email")]
    public void GetUserAccountDetailByEmail()
    {
        var user = generatorService.GenerateRandomUser();
        userAPIService.RegisterUserAccount(user);

        var response = userAPIService.GetUserAccountDetail(user.Account.Email);

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