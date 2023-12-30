using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services.API;
using AutomationFrameworkCommon.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Net;

namespace AutomationExerciseAPI.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class AutomationExerciseAPITests : TestBase
{
    private readonly UserAPIService userAPIService;

    public AutomationExerciseAPITests()
    {
        userAPIService = container.GetRequiredService<UserAPIService>();
    }

    [Test, Property("TMSId", "API 11"), Description("POST To Create/Register User Account")]
    public void CreateRegisterUserAccount()
    {
        var user = new DataGeneratorService().GenerateRandomUser();

        var response = userAPIService.RegisterUserAccount(user);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = response.Content.ReadAsStringAsync().Result;
        result.Should().Contain("User created!");
    }

}