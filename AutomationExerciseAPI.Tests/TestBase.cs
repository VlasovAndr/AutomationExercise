using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutomationExerciseAPI.Tests;

public class TestBase : PlaywrightTest
{
    protected IServiceProvider container;
    private ILogging _log;
    private CleanupPlaywrightTestService _cleanupService;

    [SetUp]
    public void SetUp()
    {
        container = new DIContainer()
            .RegisterPlaywrightAPIService(CreateAPIRequestContext())
            .Build();

        _log = container.GetRequiredService<ILogging>();
        _cleanupService = container.GetRequiredService<CleanupPlaywrightTestService>();
    }

    private IAPIRequestContext CreateAPIRequestContext()
    {
        IAPIRequestContext request = Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "https://automationexercise.com/api/",
        }).Result;

        return request;
    }

    [TearDown]
    public void AfterEach()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == TestStatus.Passed)
        {
            _log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            _log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
        }
        else
        {
            _log.Warning("Outcome: " + outcome);
        }

        _log.Information("--------------------------------------");
        _log.Information("Starting tearsown");
        _log.Information("--------------------------------------");

        _cleanupService.CleanupAsync().Wait();
    }
}
