using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Text;

namespace AutomationExerciseUI.Tests;

public class TestBase
{
    public readonly IServiceProvider container;
    public readonly ILogging log;
    private readonly CleanupTestService cleanupService;
    private readonly ITestReporter reporter;

    public TestBase()
    {
        container = DIContainer.ConfigureServices();
        log = container.GetRequiredService<ILogging>();
        cleanupService = container.GetRequiredService<CleanupTestService>();
        reporter = container.GetRequiredService<ITestReporter>();
    }

    [TearDown]
    [AllureAfter("Teardown")]
    public void AfterEach()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var webDriver = container.GetRequiredService<IWebDriverWrapper>();

        if (outcome == TestStatus.Passed)
        {
            log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
            var screenshot = webDriver.GetScreenshot();
            var browserLogs = webDriver.GetBrowserLogs();
            var pageSource = webDriver.WebDriver.PageSource;
            reporter.AddAttachment("errorScreenshot.png", "image/png", screenshot);
            reporter.AddAttachment("browserLogs.txt", "text/plain", Encoding.ASCII.GetBytes(browserLogs));
            reporter.AddAttachment("pageSource.html", "text/html", Encoding.ASCII.GetBytes(pageSource));
        }
        else
        {
            log.Warning("Outcome: " + outcome);
        }

        log.Information("--------------------------------------");
        log.Information("Starting teardown");
        log.Information("--------------------------------------");

        try
        {
            if (webDriver.IsWebDriverCreated)
                webDriver.CloseDriver();
        }
        catch (Exception ex)
        {
            log.Error($"Failed on closing web driver on after test run event. {ex.Message}");
        }

        cleanupService.Cleanup();
    }
}
