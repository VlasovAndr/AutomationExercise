using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Text;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace AutomationExerciseUI.Tests;

public class TestBase : PageTest
{
    protected IServiceProvider container;
    private ILogging _log;
    private CleanupTestService _cleanupService;
    private CleanupPlaywrightTestService _cleanupPlaywrightService;
    private ITestReporter _reporter;

    [SetUp]
    public void SetUp()
    {
        container = new DIContainer()
            .RegisterPlaywrightService(Page)
            .RegisterPlaywrightAPIService(CreateAPIRequestContext())
            .Build();

        _log = container.GetRequiredService<ILogging>();
        _cleanupService = container.GetRequiredService<CleanupTestService>();
        _cleanupPlaywrightService = container.GetRequiredService<CleanupPlaywrightTestService>();
        _reporter = container.GetRequiredService<ITestReporter>();
    }

    private IAPIRequestContext CreateAPIRequestContext()
    {
        IAPIRequestContext request = Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "https://automationexercise.com/api/"
        }).Result;

        return request;
    }

    [TearDown]
    [AllureAfter("Teardown")]
    public async Task AfterEach()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var webDriver = container.GetRequiredService<IPage>();

        if (outcome == TestStatus.Passed)
        {
            _log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            _log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
            var screenshot = await webDriver.ScreenshotAsync();
            var pageSource = await webDriver.ContentAsync();
            _reporter.AddAttachment("errorScreenshot.png", "image/png", screenshot);
            _reporter.AddAttachment("pageSource.html", "text/html", Encoding.ASCII.GetBytes(pageSource));
        }
        else
        {
            _log.Warning("Outcome: " + outcome);
        }

        _log.Information("--------------------------------------");
        _log.Information("Starting teardown");
        _log.Information("--------------------------------------");
        _cleanupService.Cleanup();
        await _cleanupPlaywrightService.CleanupAsync();
    }
}
